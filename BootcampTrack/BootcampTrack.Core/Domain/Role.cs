using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Domain
{
    public class Role : IRole<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserRole> Users { get; set; }

        public Role()
        {
            Users = new Collection<UserRole>();
        }
    }
}
