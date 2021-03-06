﻿using BootcampTrack.Core.Constants;
using BootcampTrack.Core.Domain;
using BootcampTrack.Core.Infrastructure;
using BootcampTrack.Core.Models;
using BootcampTrack.Core.Repository;
using BootcampTrack.Core.Utility;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Data.Infrastructure
{
    public class AuthorizationRepository : IAuthorizationRepository, IDisposable
    {
        private readonly IUserStore<User, string> _userStore;
        private readonly IDatabaseFactory _databaseFactory;
        private readonly UserManager<User, string> _userManager;

        private BootcampTrackDataContext db;
        protected BootcampTrackDataContext Db => db ?? (db = _databaseFactory.GetDataContext());

        public AuthorizationRepository(IDatabaseFactory databaseFactory, IUserStore<User, string> userStore)
        {
            _userStore = userStore;
            _databaseFactory = databaseFactory;
            _userManager = new UserManager<User, string>(userStore);
        }

        public async Task<IdentityResult> RegisterUser(RegistrationModel.SchoolAdministrator registration)
        {
            var user = new User
            {
                UserName = registration.Username,
                EmailAddress = registration.EmailAddress,
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                MiddleName = registration.MiddleName
            };

            School school = new School
            {
                SchoolName = registration.SchoolName
            };

            user.Id = school.SchoolAdministratorId;
            school.SchoolAdministrator = user;

            var result = await _userManager.CreateAsync(user, registration.Password);

            if (result.Succeeded)
            {
                SchoolBranch schoolBranch = new SchoolBranch
                {
                    City = registration.City,
                    State = registration.State
                };

                school.SchoolBranches.Add(schoolBranch);

                Db.Schools.Add(school);
                Db.SaveChanges();

                _userManager.AddToRole(user.Id, RoleConstants.SchoolAdministrator);
                _userManager.AddToRole(user.Id, RoleConstants.Instructor);
            }

            return result;
        }

        public async Task<IdentityResult> RegisterInstructor(RegistrationModel.Instructor registration)
        {
            var instructorToken = Db.InstructorInvites.FirstOrDefault(ii => ii.Token == registration.Token);

            if (instructorToken == null || Security.HasTokenExpired(registration.Token))
            {
                throw new Exception();
            }
            else
            {
                var user = new User
                {
                    UserName = registration.Username,
                    EmailAddress = registration.EmailAddress,
                    FirstName = registration.FirstName,
                    LastName = registration.LastName,
                    MiddleName = registration.MiddleName
                };

                user.SchoolBranchId = instructorToken.SchoolBranchId;
                
                var result = await _userManager.CreateAsync(user, registration.Password);

                if (result.Succeeded)
                {
                    _userManager.AddToRole(user.Id, RoleConstants.Instructor);
                    Db.InstructorInvites.Remove(instructorToken);
                    Db.SaveChanges();
                }

                return result;
            }
        }

        public async Task<IdentityResult> RegisterStudent(RegistrationModel.Student registration)
        {
            var studentToken = Db.StudentInvites.FirstOrDefault(ii => ii.Token == registration.Token);

            if (studentToken == null || Security.HasTokenExpired(registration.Token))
            {
                throw new Exception();
            }
            else
            {
                var user = new User
                {
                    UserName = registration.Username,
                    EmailAddress = registration.EmailAddress,
                    FirstName = registration.FirstName,
                    LastName = registration.LastName,
                    MiddleName = registration.MiddleName
                };
                
                var result = await _userManager.CreateAsync(user, registration.Password);

                if (result.Succeeded)
                {
                    _userManager.AddToRole(user.Id, RoleConstants.Student);

                    var course = Db.Courses.Find(studentToken.CourseId);
                    Db.Enrollments.Add(new Enrollment
                    {
                        CourseId = course.CourseId,
                        StudentId = user.Id
                    });

                    Db.StudentInvites.Remove(studentToken);
                    Db.SaveChanges();
                }

                return result;
            }
        }

        public async Task<User> FindUser(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }
    }
}
