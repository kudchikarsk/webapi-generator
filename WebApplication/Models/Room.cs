using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication;

namespace WebApplication.Models
{
    public class Room : Entity<long>
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

        public int Capacity
        {
            get;
            set;
        }

        public Company Company
        {
            get;
            set;
        }

        public long CompanyId
        {
            get;
            set;
        }

        public ICollection<Event> Events
        {
            get;
            set;
        }

        public Room()
        {
        }

        public static Room Create(CreateRoom value)
        {
            return new Room()
            {Id = value.Id, Name = value.Name, Capacity = value.Capacity, CompanyId = value.Company.Id, };
        }

        public void Update(UpdateRoom value)
        {
            Id = value.Id;
            Name = value.Name;
            Capacity = value.Capacity;
            CompanyId = value.Company.Id;
        }
    }

    public class CreateRoom
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

        public int Capacity
        {
            get;
            set;
        }

        public CompactCompany Company
        {
            get;
            set;
        }
    }

    public class UpdateRoom
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

        public int Capacity
        {
            get;
            set;
        }

        public CompactCompany Company
        {
            get;
            set;
        }
    }

    public class GetRoom
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

        public int Capacity
        {
            get;
            set;
        }

        public CompactCompany Company
        {
            get;
            set;
        }

        public ICollection<CompactEvent> Events
        {
            get;
            set;
        }
    }

    public class CompactRoom
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

        public int Capacity
        {
            get;
            set;
        }
    }
}
