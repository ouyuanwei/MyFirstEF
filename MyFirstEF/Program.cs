using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Persistence;
using Repository.Base;
using Interface;
using Implement;

namespace MyFirstEF
{
    class Program
    {
        static void Main(string[] args)
        {
             IStudentService service =new StudentService();
            service.GetById(5);

            Console.ReadKey();
        }
    }
}
