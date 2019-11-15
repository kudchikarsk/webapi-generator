using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace WebApplication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Id
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

        public int DepartmentId
        {
            get;
            set;
        }

        public Branch Branch
        {
            get;
            set;
        }

        public int BranchId
        {
            get;
            set;
        }

        public ICollection<Event> Events
        {
            get;
            set;
        }

        public ApplicationUser()
        {
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public static ApplicationUser Create(CreateApplicationUser value)
        {
            return new ApplicationUser()
            {Id = value.Id, CompanyId = value.Company.Id, DepartmentId = value.Department.Id, BranchId = value.Branch.Id, };
        }

        public void Update(UpdateApplicationUser value)
        {
            Id = value.Id;
            CompanyId = value.Company.Id;
            DepartmentId = value.Department.Id;
            BranchId = value.Branch.Id;
        }
    }

    public class CreateApplicationUser
    {
        public string Id
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
    }

    public class UpdateApplicationUser
    {
        public string Id
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
    }

    public class GetApplicationUser
    {
        public string Id
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

        public ICollection<CompactEvent> Events
        {
            get;
            set;
        }
    }

    public class CompactApplicationUser
    {
        public string Id
        {
            get;
            set;
        }
    }
}
