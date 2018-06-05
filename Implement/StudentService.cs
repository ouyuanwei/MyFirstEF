using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Interface;
using Persistence;
using AutoMapper;
using GenericCache;

namespace Implement
{
    public class StudentService :BaseService, IStudentService
    {
        public StudentModel GetById(int id)
        {
            var key = string.Format("{0}_id={1}", this.GetType().FullName, id);
            var cache = Cache.GetCache<StudentModel>(key);
            if (cache != null)
            {
                return cache;
            }

            var test = base.Get<Test_Student, StudentModel>((q) =>
                   q.StudentId > 5
               );
            var mod = unitOfWork.GetRepository<Test_Student>().dbSet.FirstOrDefault();
            var map = new MapperConfiguration(x => x.CreateMap<Test_Student, StudentModel>()).CreateMapper();
            var stu = map.Map<StudentModel>(mod);

            Cache.AddCache<StudentModel>(key, stu, DateTime.Now.Date.AddDays(1));
            Cache.RemoverCache(m => m.StartsWith(this.GetType().FullName));
            return stu;
        }
    }
}
