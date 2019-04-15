using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Web.Services
{
    public interface IServices<T> where T : class
    {
        /// <summary>
        /// 获取所有集合数据
        /// </summary>
        /// <returns>返回集合数据</returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// 根据id获取对象数据
        /// </summary>
        /// <param name="id">对象id</param>
        /// <returns>返回对象数据</returns>
        T GetById(int id);
        /// <summary>
        /// 创建一个学生
        /// </summary>
        /// <param name="newModel">学生对象</param>
        T Add(T newModel);
    }
}
