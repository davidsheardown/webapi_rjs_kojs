//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public System.Guid UserGuid { get; set; }
        public bool UserActive { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
    }
}
