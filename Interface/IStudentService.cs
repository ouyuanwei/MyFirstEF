using Entity;
using Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IStudentService: IBaseService
    {
        StudentModel GetById(int id);
    }
}
