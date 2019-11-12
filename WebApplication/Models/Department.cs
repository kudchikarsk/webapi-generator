using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication;

namespace WebApplication.Models
{
    public class Department : Entity<long>
    {
        public Int32 Id
        {
            get;
            set;
        }

        public Int32 Name
        {
            get;
            set;
        }

        public ICollection<Employee> Employees
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
        public Int32 Id
        {
            get;
            set;
        }

        public Int32 Name
        {
            get;
            set;
        }
    }

    public class UpdateDepartment
    {
        public Int32 Id
        {
            get;
            set;
        }

        public Int32 Name
        {
            get;
            set;
        }
    }

    public class GetDepartment
    {
        public Int32 Id
        {
            get;
            set;
        }

        public Int32 Name
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

    public class CompactDepartment
    {
        public Int32 Id
        {
            get;
            set;
        }

        public Int32 Name
        {
            get;
            set;
        }
    }
}
