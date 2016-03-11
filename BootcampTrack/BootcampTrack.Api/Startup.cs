using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BootcampTrack;
using BootcampTrack.Api.Infrastructure;
using BootcampTrack.Core.Domain;
using BootcampTrack.Core.Infrastructure;
using BootcampTrack.Core.Repository;
using BootcampTrack.Data.Infrastructure;
using BootcampTrack.Data.Repository;

[assembly: OwinStartup(typeof(BootcampTrack.Api.Startup))]
namespace BootcampTrack.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = ConfigureSimpleInjector(app);
            
            HttpConfiguration config = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
            };
            WebApiConfig.Register(config);

            ConfigureOAuth(app, container);

            app.UseCors(CorsOptions.AllowAll);           
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app, Container container)
        {
            Func<IAuthorizationRepository> authRepositoryFactory = container.GetInstance<IAuthorizationRepository>;

            var authorizationOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new BootcampTrackAuthorizationServerProvider(authRepositoryFactory)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(authorizationOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        public Container ConfigureSimpleInjector(IAppBuilder app)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
            
            container.Register<IDatabaseFactory, DatabaseFactory>(Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>();
            
            container.Register<IUserRepository, UserRepository>();
            container.Register<IUserRoleRepository, UserRoleRepository>();
            container.Register<IRoleRepository, RoleRepository>();
            container.Register<ISchoolRepository, SchoolRepository>();
            container.Register<ICourseRepository, CourseRepository>();
            container.Register<ICourseTopicRepository, CourseTopicRepository>();
            container.Register<ITopicRepository, TopicRepository>();
            container.Register<IEnrollmentRepository, EnrollmentRepository>();
            container.Register<IProjectRepository, ProjectRepository>();
            container.Register<ISubmissionRepository, SubmissionRepository>();
            container.Register<IStudentInviteRepository, StudentInviteRepository>();
            container.Register<IInstructorInviteRepository, InstructorInviteRepository>();

            container.Register<IUserStore<User, string>, UserStore>(Lifestyle.Scoped);
            container.Register<IAuthorizationRepository, AuthorizationRepository>(Lifestyle.Scoped);
            //container.Register<IKeyPaymentService, StripeKeyPaymentService>();

            // more code to facilitate a scoped lifestyle
            app.Use(async (context, next) =>
            {
                using (container.BeginExecutionContextScope())
                {
                    await next();
                }
            });

            container.Verify();

            return container;
        }
    }
}