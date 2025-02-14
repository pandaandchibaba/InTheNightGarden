﻿using IOT.Core.IRepository.Activity;
using IOT.Core.IRepository.Bargain;
using IOT.Core.IRepository.Commodity;
using IOT.Core.IRepository.GroupBooking;
using IOT.Core.IRepository.Live;
using IOT.Core.IRepository.SeckillCom;
using IOT.Core.IRepository.OrderInfo;
using IOT.Core.IRepository.Colonel;
using IOT.Core.Repository.Activity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT.Core.IRepository.Warehouse;
using IOT.Core.Repository.Warehouse;
using IOT.Core.IRepository.Delivery;
using IOT.Core.Repository.Delivery;
using IOT.Core.IRepository.PutLibrary;
using IOT.Core.Repository.PutLibrary;
using IOT.Core.Repository.OrderInfo;
using IOT.Core.Repository.Commodity;
using IOT.Core.IRepository;
using IOT.Core.IRepository.ColonelManagement;
using IOT.Core.Repository.ColonelManagement;
using IOT.Core.IRepository.ColonelGrade;
using IOT.Core.Repository.ColonelGrade;
using IOT.Core.IRepository.GroupPurchase;
using IOT.Core.Repository.GroupPurchase;
using IOT.Core.IRepository.Path;
using IOT.Core.IRepository.Brokerage;
using IOT.Core.Repository.Brokerage;
using IOT.Core.Repository.Colonel;
using IOT.Core.IRepository.OrderComment;
using IOT.Core.Repository.MiniProgram;
using IOT.Core.IRepository.Users;
using IOT.Core.IRepository.OutLibrary;
using IOT.Core.Repository.OutLibrary;
using IOT.Core.Repository.SeckillCom;
using IOT.Core.Repository.Live;
using IOT.Core.Repository.Bargain;
using IOT.Core.Repository.GroupBooking;
using IOT.Core.IRepository.CheckRep;
using IOT.Core.Repository.CheckRep;
using IOT.Core.IRepository.NowRep;
using IOT.Core.Repository.NowRep;
using IOT.Core.IRepository.Group_Comm;
using IOT.Core.Repository.Group_Comm;
using IOT.Core.IRepository.CommType;
using IOT.Core.Repository.CommType;
using IOT.Core.IRepository.Specification;
using IOT.Core.Repository.Specification;
using IOT.Core.IRepository.Com_Comment;
using IOT.Core.Repository.Com_Comment;
using IOT.Core.Repository.Path;
using IOT.Core.IRepository.Roles;
using IOT.Core.Repository.Roles;
using IOT.Core.IRepository.Store_Configuration;
using IOT.Core.IRepository.Withdrawal;
using IOT.Core.Repository.Store_Configuration;
using IOT.Core.Repository.Withdrawal;
using IOT.Core.Repository.Users;
using IOT.Core.Repository.OrderComment;
using IOT.Core.IRepository.PayStore;
using IOT.Core.Repository.PayStore;
using IOT.Core.IRepository.Store;
using IOT.Core.Repository.Store;
using IOT.Core.IRepository.SVIP;
using IOT.Core.Repository.SVIP;
using IOT.Core.IRepository.Agent;
using IOT.Core.Repository.Agent;
using IOT.Core.IRepository.CommissionRecord;
using IOT.Core.Repository.CommissionRecord;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IOT.Core.IRepository.WorkBench;
using IOT.Core.Repository.WorkBench;
using IOT.Core.IRepository.Menu;
using IOT.Core.Repository.Menu;
using IOT.Core.IRepository.StaffAuthority;
using IOT.Core.Repository.StaffAuthority;

namespace IOT.Core.Api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[] { } }
                });
            });
            #region  注入
            services.AddSingleton<IActivityRepository, ActivityRepository>();
            services.AddSingleton<IColonelRepository, ColonelRepositoty>();
            services.AddSingleton<ICommodityRepository, CommodityRepository>();
            services.AddSingleton<IDeliveryRepository, DeliveryRepository>();
            services.AddSingleton<IPutLibraryRepository, PutLibraryRepository>();
            services.AddSingleton<IOrderInfoRepository, OrderInfoRepository>();
            services.AddSingleton<IWarehouseRepository, WarehouseRepository>();
            services.AddSingleton<IBargainRepository, BargainRepository>();
            services.AddSingleton<IBrokerageRepository, BrokerageRepository>();
            services.AddSingleton<ICheckRepRepository, CheckRepRepository>();
            services.AddSingleton<ICom_CommentRepository, Com_CommentRepository>();
            services.AddSingleton<IGroup_CommRepository, Group_CommRepository>();
            services.AddSingleton<IGroupBookingRepository, GroupBookingRepository>();
            services.AddSingleton<ILiveRepository, LiveRepository>();
            services.AddSingleton<IMiniProgramRepository, MiniProgramRepository>();
            services.AddSingleton<INowRepRepository, NowRepRepository>();
            services.AddSingleton<IOrderCommentRepository, OrderCommentRepository>();
            services.AddSingleton<ILiveRepository, LiveRepository>();
            services.AddSingleton<IOrderInfoRepository, OrderInfoRepository>();
            services.AddSingleton<IOutLibraryRepository, OutLibraryRepository>();
            services.AddSingleton<IPutLibraryRepository, PutLibraryRepository>();
            services.AddSingleton<ISeckillComRepository, SeckillComRepository>();
            services.AddSingleton<ISpecificationRepository, SpecificationRepository>();
            services.AddSingleton<IUsersRepository, UsersRepository>();
            services.AddSingleton<IWarehouseRepository, WarehouseRepository>();
            services.AddSingleton<IColonelGradeRepository, ColonelGradeRepository>();
            services.AddSingleton<IColonelManagementRepository, ColonelManagementRepository>();
            services.AddSingleton<ICommTypeRepository, CommTypeRepository>();
            services.AddSingleton<IGroupPurchaseRepository, GroupPurchaseRepository>();
            services.AddSingleton<IPathRepository, PathRepository>();
            services.AddSingleton<IRolesRepository, RolesRepository>();
            services.AddSingleton<IStore_ConfigurationRepository, Store_ConfigurationRepository>();
            services.AddSingleton<IWithdrawalRepository, WithdrawalRepository>();
            services.AddSingleton<IStoreRepository, StoreRepository>();
            services.AddSingleton<IPayStoreRepository, PayStoreRepository>();
            services.AddSingleton<ICom_CommentRepository, Com_CommentRepository>();
            services.AddSingleton<ISVIPRepository, SVIPRepository>();
            services.AddSingleton<IAgentRepository, AgentRepository>();
            services.AddSingleton<IMenuRepository, MenuRepository>();
            services.AddSingleton<ICommissionRecordRepository, CommissionRecordRepository>();
            #endregion
            //IMiniProgramRepository

            #region

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "Security:Tokens:Issuer",
                        ValidateAudience = true,
                        ValidAudience = "Security:Tokens:Audience",
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Security:Tokens:Key"))
                    };
                });
            #endregion


            services.AddCors(options => 
            options.AddPolicy("cors",
            p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
          
            }
            app.UseCors("cors");
            app.UseRouting();
            app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
           

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
