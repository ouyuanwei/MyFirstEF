using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Interface;
using Persistence;
using AutoMapper;

namespace Implement
{
    public class StudentService :BaseSerice, IStudentService
    {
        public void GetById(int id)
        {

            var test = base.Get<Test_Student, StudentModel>((q) =>
                   q.StudentId > 5
               );


            var mod = unitOfWork.GetRepository<Test_Student>().dbSet.FirstOrDefault();
            Mapper.Initialize(x => x.CreateMap<Test_Student, StudentModel>());
            var stu = Mapper.Map<Test_Student, StudentModel>(mod);
        }
    }
}
