using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication;

namespace WebApplication.Models
{
    public class Branch : Entity<long>
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ICollection<ApplicationUser> ApplicationUsers
        {
            get;
            set;
        }

        public Branch()
        {
        }

        public static Branch Create(CreateBranch value)
        {
            return new Branch()
            {Id = value.Id, Name = value.Name, };
        }

        public void Update(UpdateBranch value)
        {
            Id = value.Id;
            Name = value.Name;
        }
    }

    public class CreateBranch
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public class UpdateBranch
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public class GetBranch
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ICollection<CompactApplicationUser> ApplicationUsers
        {
            get;
            set;
        }
    }

    public class CompactBranch
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
