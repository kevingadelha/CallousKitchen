//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Capstone
{
    using System;
    using System.Collections.Generic;
    
    public partial class DietTag
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public int DietId { get; set; }
    
        public virtual Diet Diet { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
