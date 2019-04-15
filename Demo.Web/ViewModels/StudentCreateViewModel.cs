using Demo.Web.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Web.ViewModels
{
    public class StudentCreateViewModel
    {
        /// <summary>
        /// 名
        /// </summary>
        [Display(Name = "名"), Required]
        public string FirstName { get; set; }
        /// <summary>
        /// 姓
        /// </summary>
        [Display(Name = "姓"), Required, MaxLength(10)]
        public string LastName { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [Display(Name = "出生日期")]
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public Gender Gender { get; set; }
    }
}
