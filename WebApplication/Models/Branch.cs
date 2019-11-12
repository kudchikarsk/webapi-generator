using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication;

namespace WebApplication.Models
{
    public class Branch : Entity<long>
    {
        public Int32 Id
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public ICollection<Employee> Employees
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
        public Int32 Id
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }
    }

    public class UpdateBranch
    {
        public Int32 Id
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }
    }

    public class GetBranch
    {
        public Int32 Id
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public ICollection<CompactEmployee> Employees
        {
            get;
            set;
        }
    }

    public class CompactBranch
    {
        public Int32 Id
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }
    }
}
