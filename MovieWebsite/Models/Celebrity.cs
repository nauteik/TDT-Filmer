//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MovieWebsite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Celebrity
    {
        public int Id { get; set; }
        public string NAME { get; set; }
        public string Avatar { get; set; }
        public string Role { get; set; }
        public string Meta { get; set; }
        public Nullable<bool> Hide { get; set; }
        public Nullable<int> C_ORDER { get; set; }
        public Nullable<System.DateTime> InitDate { get; set; }
    }
}
