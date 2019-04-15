using Demo.Web.Model;
using Demo.Web.Services;
using Demo.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 全局实体Student集合
        /// </summary>
        private readonly IServices<Student> _repository;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository">接口</param>
        public HomeController(IServices<Student> repository)
        {
            this._repository = repository;
        }
        /// <summary>
        /// 实现首页
        /// </summary>
        /// <returns>返回Index.chtml页面</returns>
        public IActionResult Index()
        {
            var list = _repository.GetAll();//获取实体Student集合
            var vms = list.Select(o => new StudentViewModel
            {
                Id = o.Id,
                Name = $"{o.LastName}{o.FirstName}",
                Age = DateTime.Now.Subtract(o.BirthDate).Days / 365
            });
            var vm = new HomeIndexViewModel
            {
                Students = vms
            };
            return View(vm);
        }
        /// <summary>
        /// 详情信息
        /// </summary>
        /// <param name="id">传入Student的Id</param>
        /// <returns>返回Index.chtml页面</returns>
        public IActionResult Detail(int id)
        {
            var student = _repository.GetById(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Detail));//定向到此controller下的action
            }
            return View(student);
        }
        /// <summary>
        /// 创建学生页
        /// </summary>
        /// <returns>返回Create.chtml页面</returns>
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// 通过post方式创建一个学生
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentCreateViewModel student)
        {
            if (ModelState.IsValid)
            {
                var newStudent = new Student()
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    BirthDate = student.BirthDate,
                    Gender = student.Gender
                };
                var newModel = _repository.Add(newStudent);
                return RedirectToAction(nameof(Detail), new { id = newModel.Id });//便于重构
            }
            return View();
        }
    }
}
