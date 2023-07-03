using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.GenericRepositories;
using DataAccess.Repositories.EventRepositories;
using DataAccess.Repositories.EventTaskRepositories;
using DataAccess.Repositories.MajorRepositories;
using DataAccess.Repositories.QuestionRepositories;
using DataAccess.Repositories.TaskRepositories;
using DataAccess.Repositories.UserRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Service.Services.EventService;
using Service.Services.EventTaskService;
using Service.Services.MajorService;
using Service.Services.QuestionService;
using Service.Services.TaskService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace XavalorAdventuresAPI
{
    public class Startup
    {
        public string ConectionString { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConectionString = Configuration.GetConnectionString("CapstonProjectDbConnectionString");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

          
          

            services.AddDbContext<FPTHCMAdventuresDBContext>(options => options.UseSqlServer(ConectionString));
            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddAutoMapper(typeof(MapperConfig));
            services.AddIdentityCore<User>()
                     .AddRoles<IdentityRole>()
                     .AddTokenProvider<DataProtectorTokenProvider<User>>("FPTHCMAdventureAPI")
                     .AddEntityFrameworkStores<FPTHCMAdventuresDBContext>()
                     .AddDefaultTokenProviders();
            services.AddCors(options => {
                options.AddPolicy("AllowAll",
                    b => b.AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowAnyMethod());
            });
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IEventRepositories, EventRepositories>();
            services.AddScoped<ITaskRepositories, TaskRepositories>();
            services.AddScoped<IEventTaskRepository, EventTaskRepository>();
            services.AddScoped<IMajorRepository, MajorRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddHttpContextAccessor();



            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "FPTHCM Adventure API", Version = "v1" });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                            Type = ReferenceType.SecurityScheme,
                                            Id = JwtBearerDefaults.AuthenticationScheme
                                        },
                            Scheme = "0auth2",
                            Name = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
            services.AddAuthentication().AddJwtBearer();

            //For DI Service
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IEventTaskService, EventTaskService>();
            services.AddScoped<IMajorService, MajorService>();
            services.AddScoped<IQuestionService, QuestionService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "XavalorAdventuresAPI v1"));
            }
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
