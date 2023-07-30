/*using BusinessObjects.Model;
using DataAccess.Dtos.Users;
using DataAccess.GenericRepositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UserRepositories
{
    public interface IAuthManager 
    {
        Task<BaseResponse<AuthResponseDto>> RegisterUser(ApiUserDto apiUser);        
        Task<BaseResponse<AuthResponseDto>> Login(LoginDto loginDto);
        Task<BaseResponse<UserWithToken>> RefreshToken(RefreshRequest refreshRequest);
        Task<BaseResponse<User>> GetUserByAccessToken(string accessToken);

        Task<BaseResponse<string>> DeleteTokenUser(Guid userId);
        string GenerateAccessTokenGoogle(string userId);
    }
}
*/