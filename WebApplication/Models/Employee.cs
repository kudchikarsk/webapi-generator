using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication;

namespace WebApplication.Models
{
    public class Employee : Entity<long>
    {
        public Int32 Id
        {
            get;
            set;
        }

        public String FirstName
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

        public Department Department
        {
            get;
            set;
        }

        public long DepartmentId
        {
            get;
            set;
        }

        public Branch Branch
        {
            get;
            set;
        }

        public long BranchId
        {
            get;
            set;
        }

        public Ticket Ticket
        {
            get;
            set;
        }

        public long TicketId
        {
            get;
            set;
        }

        public Employee()
        {
        }

        public static Employee Create(CreateEmployee value)
        {
            return new Employee()
            {Id = value.Id, FirstName = value.FirstName, CompanyId = value.Company.Id, DepartmentId = value.Department.Id, BranchId = value.Branch.Id, TicketId = value.Ticket.Id, };
        }

        public void Update(UpdateEmployee value)
        {
            Id = value.Id;
            FirstName = value.FirstName;
            CompanyId = value.Company.Id;
            DepartmentId = value.Department.Id;
            BranchId = value.Branch.Id;
            TicketId = value.Ticket.Id;
        }
    }

    public class CreateEmployee
    {
        public Int32 Id
        {
            get;
            set;
        }

        public String FirstName
        {
            get;
            set;
        }

        public CompactCompany Company
        {
            get;
            set;
        }

        public CompactDepartment Department
        {
            get;
            set;
        }

        public CompactBranch Branch
        {
            get;
            set;
        }

        public CompactTicket Ticket
        {
            get;
            set;
        }
    }

    public class UpdateEmployee
    {
        public Int32 Id
        {
            get;
            set;
        }

        public String FirstName
        {
            get;
            set;
        }

        public CompactCompany Company
        {
            get;
            set;
        }

        public CompactDepartment Department
        {
            get;
            set;
        }

        public CompactBranch Branch
        {
            get;
            set;
        }

        public CompactTicket Ticket
        {
            get;
            set;
        }
    }

    public class GetEmployee
    {
        public Int32 Id
        {
            get;
            set;
        }

        public String FirstName
        {
            get;
            set;
        }

        public CompactCompany Company
        {
            get;
            set;
        }

        public CompactDepartment Department
        {
            get;
            set;
        }

        public CompactBranch Branch
        {
            get;
            set;
        }

        public CompactTicket Ticket
        {
            get;
            set;
        }
    }

    public class CompactEmployee
    {
        public Int32 Id
        {
            get;
            set;
        }

        public String FirstName
        {
            get;
            set;
        }
    }
}
