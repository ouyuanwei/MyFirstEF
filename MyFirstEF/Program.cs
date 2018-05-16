using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Persistence;
using Interface;

namespace MyFirstEF
{
    class Program
    {
        static void Main(string[] args)
        {
            IStudentService service = InterfaceRealization.GetInterfaceRealization<IStudentService>();
            service.GetById(5);

            Console.ReadKey();
        }
    }
}
