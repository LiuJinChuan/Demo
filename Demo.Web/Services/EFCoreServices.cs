using Demo.Web.Data;
using Demo.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Web.Services
{
    public class EFCoreServices : IServices<Student>
    {
        /// <summary>
        /// 数据库实例
        /// </summary>
        private readonly DataContext _context;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public EFCoreServices(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 获取Student集合的全部数据
        /// </summary>
        /// <returns>返回集合</returns>
        public IEnumerable<Student> GetAll()
        {
            return _context.students.ToList();
        }
        /// <summary>
        /// 创建一个学生
        /// </summary>
        /// <param name="newModel">学生对象</param>
        public Student Add(Student newModel)
        {
            _context.students.Add(newModel);
            _context.SaveChanges();
            return newModel;
        }
        /// <summary>
        /// 根据Id获取Student对象
        /// </summary>
        /// <param name="id">Student的Id</param>
        /// <returns>返回Student对象</returns>
        public Student GetById(int id)
        {
            return _context.students.Find(id);
        }
    }
}
