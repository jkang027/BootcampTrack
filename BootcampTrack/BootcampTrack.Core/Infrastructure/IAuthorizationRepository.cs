using BootcampTrack.Core.Domain;
using BootcampTrack.Core.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Infrastructure
{
    public interface IAuthorizationRepository : IDisposable
    {
        Task<User> FindUser(string username, string password);
        Task<IdentityResult> RegisterUser(RegistrationModel.SchoolAdministrator registration);
        Task<IdentityResult> RegisterStudent(RegistrationModel.Student registration);
        Task<IdentityResult> RegisterInstructor(RegistrationModel.Instructor registration);
    }
}