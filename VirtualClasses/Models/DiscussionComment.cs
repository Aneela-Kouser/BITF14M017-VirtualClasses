//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VirtualClasses.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DiscussionComment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int DiscussionId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Time { get; set; }
    
        public virtual Discussion Discussion { get; set; }
    }
}