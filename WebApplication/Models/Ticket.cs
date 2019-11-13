using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication;

namespace WebApplication.Models
{
    public class Ticket : Entity<long>
    {
        public Int32 Id
        {
            get;
            set;
        }

        public Employee Employee
        {
            get;
            set;
        }

        public long EmployeeId
        {
            get;
            set;
        }

        public Ticket()
        {
        }

        public static Ticket Create(CreateTicket value)
        {
            return new Ticket()
            {Id = value.Id, EmployeeId = value.Employee.Id, };
        }

        public void Update(UpdateTicket value)
        {
            Id = value.Id;
            EmployeeId = value.Employee.Id;
        }
    }

    public class CreateTicket
    {
        public Int32 Id
        {
            get;
            set;
        }

        public CompactEmployee Employee
        {
            get;
            set;
        }
    }

    public class UpdateTicket
    {
        public Int32 Id
        {
            get;
            set;
        }

        public CompactEmployee Employee
        {
            get;
            set;
        }
    }

    public class GetTicket
    {
        public Int32 Id
        {
            get;
            set;
        }

        public CompactEmployee Employee
        {
            get;
            set;
        }
    }

    public class CompactTicket
    {
        public Int32 Id
        {
            get;
            set;
        }
    }
}
