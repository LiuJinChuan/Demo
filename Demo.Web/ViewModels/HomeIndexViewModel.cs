using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Web.ViewModels
{
    public class HomeIndexViewModel
    {
        /// <summary>
        /// Model实体指定返回集合
        /// </summary>
        public IEnumerable<StudentViewModel> Students { get; set; }
    }
}
