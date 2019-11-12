using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication;

namespace WebApplication.Models
{
    public class Company : Entity<long>
    {
        public Int64 Id
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public ICollection<Room> Rooms
        {
            get;
            set;
        }

        public ICollection<Employee> Employees
        {
            get;
            set;
        }

        public Company()
        {
        }

        public static Company Create(CreateCompany value)
        {
            return new Company()
            {Id = value.Id, Name = value.Name, };
        }

        public void Update(UpdateCompany value)
        {
            Id = value.Id;
            Name = value.Name;
        }
    }

    public class CreateCompany
    {
        public Int64 Id
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

    public class UpdateCompany
    {
        public Int64 Id
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

    public class GetCompany
    {
        public Int64 Id
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public ICollection<CompactRoom> Rooms
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

    public class CompactCompany
    {
        public Int64 Id
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
