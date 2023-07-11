using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UserRepositories
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private User _user;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private const string _loginProvider = "FPTHCMAdventuresApi";
        private const string _refreshToken = "RefreshToken";
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly JWTSettings _jwtsettings;

        public AuthManager(FPTHCMAdventuresDBContext dbContext, IOptions<JWTSettings> jwtsettings, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this._mapper = mapper;
            this._configuration = configuration;
            this._httpContextAccessor = httpContextAccessor;
            this._dbContext = dbContext;
            this._jwtsettings = jwtsettings.Value;

        }
        public async Task<BaseResponse<UserWithToken>> Login(LoginDto loginDto)
        {
            var user = await _dbContext.Users.Include(u => u.Role)
                                        .Where(u => u.Email == loginDto.Email
                                                && u.Password == loginDto.Password).FirstOrDefaultAsync();

            UserWithToken userWithToken = null;

            if(user != null)
            {
                RefreshToken refreshToken = GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);
                await _dbContext.SaveChangesAsync();

                userWithToken = new UserWithToken(user);
                userWithToken.RefreshToken = refreshToken.Token;
            }
            
            if(userWithToken == null)
            {
                return new BaseResponse<UserWithToken>
                {
                    Data = null,
                    Message = "Faile data is null ",
                    Success = false
                };
            }
            userWithToken.AccessToken = GenerateAccessToken(user.Id);
            return new BaseResponse<UserWithToken>
            {
                Data = userWithToken,
                Message = "Success",
                Success = true
            };
        }


        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddMonths(6);
            return refreshToken;
        }

        private bool ValidateRefreshToken(User user, string refreshToken)
        {

            RefreshToken refreshTokenUser = _dbContext.RefreshTokens.Where(rt => rt.Token == refreshToken)
                                                .OrderByDescending(rt => rt.ExpiryDate)
                                                .FirstOrDefault();

            if (refreshTokenUser != null && refreshTokenUser.UserId == user.Id
                && refreshTokenUser.ExpiryDate > DateTime.UtcNow)
            {
                return true;
            }

            return false;
        }

        private async Task<User> GetUserFromAccessToken(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var userId = principle.FindFirst(ClaimTypes.Name)?.Value;

                    Guid userIdGuid;

                    if (Guid.TryParse(userId, out userIdGuid))
                    {
                        return await _dbContext.Users.Include(u => u.Role)
                                                    .FirstOrDefaultAsync(u => u.Id == userIdGuid);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return new User();
            }

            return new User();
        }
        public string GenerateAccessTokenGoogle(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId)
                }),
                Expires = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string GenerateAccessToken(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId))
                }),
                Expires = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public  string PasswordHash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Chuyển đổi mật khẩu sang mảng byte
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Tính toán mã hash của mật khẩu
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Chuyển đổi mã hash thành dạng chuỗi hex
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        public async Task<BaseResponse<UserWithToken>> RegisterUser(ApiUserDto apiUser)
        {
            var user = _mapper.Map<User>(apiUser);
            user.Id = Guid.NewGuid();
            user.RoleId = Guid.Parse("13c1b3fe-a3ac-44df-a4a0-22f0594834c0");
            string hashedPassword = PasswordHash(apiUser.Password);
            user.Password = hashedPassword;
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            //load role for registered user
            user = await _dbContext.Users.Include(u => u.Role)
                                        .Where(u => u.Id == user.Id).FirstOrDefaultAsync();

            UserWithToken userWithToken = null;

            if (user != null)
            {
                RefreshToken refreshToken = GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);
                await _dbContext.SaveChangesAsync();

                userWithToken = new UserWithToken(user);
                userWithToken.RefreshToken = refreshToken.Token;
            }

            if (userWithToken == null)
            {
                return new BaseResponse<UserWithToken>
                {
                    Data = null,
                    Success = false,
                    Message = "Error because userwithtoken is null",
                };
            }

            //sign your token here here..
            userWithToken.AccessToken = GenerateAccessToken(user.Id);
            return new BaseResponse<UserWithToken>
            {
                Data = userWithToken,
                Success = true,
                Message = "Success",
            };
        }

        public async Task<BaseResponse<UserWithToken>> RefreshToken(RefreshRequest refreshRequest)
        {
            User user = await GetUserFromAccessToken(refreshRequest.AccessToken);

            if (user != null && ValidateRefreshToken(user, refreshRequest.RefreshToken))
            {
                UserWithToken userWithToken = new UserWithToken(user);
                userWithToken.AccessToken = GenerateAccessToken(user.Id);

                return new BaseResponse<UserWithToken> 
                { 
                    Data= userWithToken,
                    Message = "Success",
                    Success = true,
                };
            }

            return new BaseResponse<UserWithToken>
            {
                Data = null,
                Message = "Failed",
                Success = false,
            }; 
        }

        public async Task<BaseResponse<User>> GetUserByAccessToken(string accessToken)
        {
            User user = await GetUserFromAccessToken(accessToken);

            if (user != null)
            {
                return new BaseResponse<User> { Data = user,Message = "Success", Success = true};
            }

            return null;
        }
    }

}

