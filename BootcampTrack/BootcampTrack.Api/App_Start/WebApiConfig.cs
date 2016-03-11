using AutoMapper;
using BootcampTrack.Core.Domain;
using BootcampTrack.Core.Models;
using System.Linq;
using System.Web.Http;

namespace BootcampTrack.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            CreateMaps();
        }

        public static void CreateMaps()
        {
            Mapper.CreateMap<Course, CourseModel>();
            Mapper.CreateMap<CourseTopic, CourseTopicModel>();
            Mapper.CreateMap<Enrollment, EnrollmentModel>();
            Mapper.CreateMap<Project, ProjectModel>();
            Mapper.CreateMap<Submission, SubmissionModel>();
            Mapper.CreateMap<Topic, TopicModel>();
            Mapper.CreateMap<User, UserModel>();
            Mapper.CreateMap<Topic, TopicModel>();
            Mapper.CreateMap<School, SchoolModel>();
            Mapper.CreateMap<SchoolBranch, SchoolBranchModel>();
            //TODO: Profile Stuff
            //Mapper.CreateMap<User, UserModel.Profile>();
        }
    }
}
