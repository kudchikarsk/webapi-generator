using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication;

namespace WebApplication.Models
{
    public class Department : Entity<long>
    {
        public int Id
        {
            get;
            set;
        }

        public int Name
        {
            get;
            set;
        }

        public ICollection<ApplicationUser> ApplicationUsers
        {
            get;
            set;
        }

        public Department()
        {
        }

        public static Department Create(CreateDepartment value)
        {
            return new Department()
            {Id = value.Id, Name = value.Name, };
        }

        public void Update(UpdateDepartment value)
        {
            Id = value.Id;
            Name = value.Name;
        }
    }

    public class CreateDepartment
    {
        public int Id
        {
            get;
            set;
        }

        public int Name
        {
            get;
            set;
        }
    }

    public class UpdateDepartment
    {
        public int Id
        {
            get;
            set;
        }

        public int Name
        {
            get;
            set;
        }
    }

    public class GetDepartment
    {
        public int Id
        {
            get;
            set;
        }

        public int Name
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

    public class CompactDepartment
    {
        public int Id
        {
            get;
            set;
        }

        public int Name
        {
            get;
            set;
        }
    }
}
