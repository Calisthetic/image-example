//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReadDataConsoleApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employees
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string ThirdName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> LastEntryTime { get; set; }
        public Nullable<bool> LastEntryType { get; set; }
        public byte[] ImageFile { get; set; }
    
        public virtual Roles Roles { get; set; }
    }
}
