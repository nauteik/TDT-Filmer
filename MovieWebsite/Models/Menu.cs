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
    
    public partial class Menu
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Meta { get; set; }
        public Nullable<bool> Hide { get; set; }
        public Nullable<int> C_Order { get; set; }
        public Nullable<System.DateTime> InitDate { get; set; }
    }
}
