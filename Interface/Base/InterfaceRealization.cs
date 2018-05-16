using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public static class InterfaceRealization
    {
        public static T GetInterfaceRealization<T>()
        {
            Type type = typeof(T);
            var assemblyFile = ConfigurationManager.AppSettings["AssemblyFile"];
            Assembly ass = Assembly.Load(assemblyFile);
            Type[] asstype = ass.GetTypes();
            if (asstype.Length <= 0)
                throw new Exception("接口管理器的异常：该程序集没有任何实现类");
            for (int i = 0; i < asstype.Length; i++)
            {
                //获取该实现类的整个继承链中是否有传入的接口类型；
                Type oddinterfacetype = asstype[i].GetInterface(type.Name);
                if (oddinterfacetype != null)
                {
                    T t = (T)System.Activator.CreateInstance(asstype[i]);
                    return t;//返回动态实例化的接口实现类；
                }
            }
            return default(T);
            //throw new Exception("接口管理器的异常：没有该接口的实现类，必须先实现接口类才能查找");
        }

    }
}
