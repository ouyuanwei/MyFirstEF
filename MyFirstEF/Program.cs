using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Interface;

namespace MyFirstEF
{
    class Program
    {
        static void Main(string[] args)
        {
            IStudentService service = AssemblyImplement.GenericInstance.CreateInstance<IStudentService>();
            var respones = service.GetById(5);
            var respones1 = service.GetById(5);
            Console.ReadKey();
        }
    }
}
