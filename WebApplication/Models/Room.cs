using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication;

namespace WebApplication.Models
{
    public class Room : Entity<long>
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

        public Int32 Capacity
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

        public Int32 Capacity
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

        public Int32 Capacity
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

        public Int32 Capacity
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

    public class CompactRoom
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

        public Int32 Capacity
        {
            get;
            set;
        }
    }
}
