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
    
    public partial class NewCommentReply
    {
        public int Id { get; set; }
        public Nullable<int> NewCommentId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Content { get; set; }
        public string Meta { get; set; }
        public Nullable<bool> Hide { get; set; }
        public Nullable<int> C_ORDER { get; set; }
        public Nullable<System.DateTime> InitDate { get; set; }
    
        public virtual NewComment NewComment { get; set; }
        public virtual User User { get; set; }
    }
}
