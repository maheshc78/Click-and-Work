//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClickAndWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class hirer
    {
        [Display(Name = "First name")]
        public string hfname { get; set; }
        [Display(Name = "Last name")]
        public string hlname { get; set; }
        [Display(Name = "Phone")]
        public string hphone { get; set; }
        [Display(Name = "Mail Address")]
        public string hmailid { get; set; }
        [Display(Name = "Password")]
        public string hpwd { get; set; }
    }
}
