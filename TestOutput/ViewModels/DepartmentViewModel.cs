//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    
    
    public class DepartmentViewModel
    {
        
        private int _Id;
        
        private int _Name;
        
        private ICollection<CompactEmployeeViewModel> _Employees;
        
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }
        
        public int Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }
        
        public ICollection<CompactEmployeeViewModel> Employees
        {
            get
            {
                return this._Employees;
            }
            set
            {
                this._Employees = value;
            }
        }
    }
}
