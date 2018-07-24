using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClickAndWork.Models;
using System.ComponentModel.DataAnnotations;
namespace ClickAndWork.ViewModels
{
    public class WorkerHirer
    {
        [Key]
        public hirer hirer { get; set; }
        [Key]
        public user user { get; set; }
        [Key]
        public List<Jobpost> jobposts { get; set; }
        [Key]
        public List<Profile> profiles { get; set; }
        [Key]
        public List<Jobapply> applies { get; set; }
      
    }
}