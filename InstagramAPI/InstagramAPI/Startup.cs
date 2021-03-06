﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramAPI.Interfaces.Services;
using InstagramAPI.Models;
using InstagramAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InstagramAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "Instagram.com",
                            ValidAudience = "Instagram.com",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[Constants.JWTSecurityKey]))
                        };
                    });

            services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc; // Adding this because in 
                //Created = DateTime.UtcNow it's missing 'z' in string getting back from asp.net so to avoid that this has to be added.
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            services.Configure<FormOptions>(x => { x.ValueLengthLimit = int.MaxValue; x.MultipartBodyLengthLimit = int.MaxValue; x.MemoryBufferThreshold = int.MaxValue; });
            // var connection = @"Server=(localdb)\mssqllocaldb;Database=InstagramDB;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddScoped<IEntityService<User>, UserService>();
            services.AddScoped<IEntityService<UserFriend>, UserFriendService>();
            services.AddScoped<IEntityService<Post>, PostService>();
            services.AddScoped<IEntityService<Comment>, CommentService>();
            services.AddScoped<IEntityService<CommentLike>, CommentLikeService>();
            services.AddScoped<IEntityService<PostLike>, PostLikeService>();


            services.AddDbContext<InstagramContext>
                (options => options.UseSqlServer(Configuration[Constants.DbConnection]));
            services.AddCors(o => o.AddPolicy(Constants.InstagramServerCorsPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication(); //it should be before UseMvc
            app.UseCors(Constants.InstagramServerCorsPolicy);

            app.UseMvc();
            app.UseStaticFiles();
        }
    }
}
