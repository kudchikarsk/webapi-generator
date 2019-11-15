using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication;

namespace WebApplication.Models
{
    public class Company : Entity<long>
    {
        public long Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ICollection<Room> Rooms
        {
            get;
            set;
        }

        public ICollection<ApplicationUser> ApplicationUsers
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
        public long Id
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

    public class UpdateCompany
    {
        public long Id
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

    public class GetCompany
    {
        public long Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ICollection<CompactRoom> Rooms
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

    public class CompactCompany
    {
        public long Id
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
