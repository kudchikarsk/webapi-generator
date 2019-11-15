// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace WebApplication.DependencyResolution
{
    using AutoMapper;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using System.Data.Entity;
    using WebApplication.Areas.HelpPage.Controllers;
    using WebApplication.BaseHelpers;
    using WebApplication.Interfaces;
    using WebApplication.Models;

    public class DefaultRegistry : Registry
    {
#region Constructors and Destructors
        public DefaultRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
                scan.AssemblyContainingType<ApplicationDbContext>();
                scan.ConnectImplementationsToTypesClosing(typeof(IRepository<>));
                scan.ConnectImplementationsToTypesClosing(typeof(IHandle<>));
            }

            );
            //For<IExample>().Use<Example>();
            For<HelpController>().Use(ctx => new HelpController());
            ForConcreteType<ApplicationDbContext>().Configure.Singleton();
            For<DbContext>().Use(ctx => new ApplicationDbContext());
            For(typeof(IRepository<>)).Use(typeof(BaseRepository<>));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Company, GetCompany>();
                cfg.CreateMap<Company, CompactCompany>();
                cfg.CreateMap<Room, GetRoom>();
                cfg.CreateMap<Room, CompactRoom>();
                cfg.CreateMap<Department, GetDepartment>();
                cfg.CreateMap<Department, CompactDepartment>();
                cfg.CreateMap<Branch, GetBranch>();
                cfg.CreateMap<Branch, CompactBranch>();
                cfg.CreateMap<ApplicationUser, GetApplicationUser>();
                cfg.CreateMap<ApplicationUser, CompactApplicationUser>();
                cfg.CreateMap<Event, GetEvent>();
                cfg.CreateMap<Event, CompactEvent>();
            }

            );
            For<Mapper>().Use(ctx => new Mapper(config));
        }
#endregion
    }
}
