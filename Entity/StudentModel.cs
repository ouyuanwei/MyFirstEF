using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class StudentModel
    {
        public int StudentId { set; get; }
        public string StudentName { set; get; }
        public string StudentSex { set; get; }
        public int? StudentAge { set; get; }
    }
}
