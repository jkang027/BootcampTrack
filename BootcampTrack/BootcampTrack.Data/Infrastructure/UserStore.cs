using BootcampTrack.Core.Domain;
using BootcampTrack.Core.Infrastructure;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Data.Infrastructure
{
    public class UserStore : Disposable,
                            IUserPasswordStore<User, string>,
                            IUserSecurityStampStore<User, string>,
                            IUserRoleStore<User, string>
    {
        private readonly IDatabaseFactory _databaseFactory;

        private BootcampTrackDataContext _dataContext;
        protected BootcampTrackDataContext DataContext
        {
            get
            {
                return _dataContext ?? (_dataContext = _databaseFactory.GetDataContext());
            }
        }

        public UserStore(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        #region IUserPasswordStore
        public Task CreateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() => {
                user.Id = Guid.NewGuid().ToString();
                DataContext.Users.Add(user);
                DataContext.SaveChanges();
            });
        }

        public Task DeleteAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() => {
                DataContext.Users.Remove(user);
                DataContext.SaveChanges();
            });
        }

        public Task<User> FindByIdAsync(string userId)
        {
            return Task.Factory.StartNew(() => {
                return DataContext.Users.FirstOrDefault(u => u.Id == userId);
            });
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return Task.Factory.StartNew(() =>
            {
                return DataContext.Users.FirstOrDefault(u => u.UserName == userName);
            });
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>
            {
                return user.PasswordHash;
            });
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        public Task UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>
            {
                DataContext.Users.Attach(user);
                DataContext.Entry(user).State = EntityState.Modified;

                DataContext.SaveChanges();
            });
        }
        #endregion

        #region ISecurityStampStore
        public Task<string> GetSecurityStampAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(User user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }
        #endregion

        #region IUserRoleStore
        public Task AddToRoleAsync(User user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            return Task.Factory.StartNew(() =>
            {
                if (!DataContext.Roles.Any(r => r.Name == roleName))
                {
                    DataContext.Roles.Add(new Role
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = roleName
                    });
                }

                DataContext.UserRoles.Add(new UserRole
                {
                    Role = DataContext.Roles.FirstOrDefault(r => r.Name == roleName),
                    User = user
                });

                DataContext.SaveChanges();
            });
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            return Task.Factory.StartNew(() =>
            {
                var userRole = user.Roles.FirstOrDefault(r => r.Role.Name == roleName);

                if (userRole == null)
                {
                    throw new InvalidOperationException("User does not have that role");
                }

                DataContext.UserRoles.Remove(userRole);

                DataContext.SaveChanges();
            });
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            return Task.Factory.StartNew(() =>
            {
                return (IList<string>)user.Roles.Select(ur => ur.Role.Name);
            });
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            return Task.Factory.StartNew(() =>
            {
                return user.Roles.Any(r => r.Role.Name == roleName);
            });
        }
        #endregion
    }
}
