using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Configuration;
using Interface;
using Interface.Base;
using System.IO;

namespace AssemblyImplement
{
    public class GenericInstance
    {
        private static Dictionary<Type, IBaseService> _dicService = new Dictionary<Type, IBaseService>();
        private static Dictionary<Type, string> _dicServiceAddress = new Dictionary<Type, String>();
        private static string _assemblyStr = ConfigurationManager.AppSettings["AssemblyIServer"];
        private static readonly object _lock = new object();

        static GenericInstance()
        {
            _dicServiceAddress.Add(typeof(IStudentService), "Implement,Implement.StudentService");


        }
        public static T CreateInstance<T>() where T : IBaseService
        {
            if (!_dicService.ContainsKey(typeof(T)))
            {
                lock (_lock)
                {
                    if (!_dicService.ContainsKey(typeof(T)))
                    {
                        InstanceByConfig<T>();
                    }
                }
            }
            return (T)_dicService[typeof(T)];
        }
        private static void InstanceByConfig<T>() where T : IBaseService
        {
            lock (_lock)
            {
                //var path = Path.GetFullPath(string.Format(@"..\..\..\{0}\bin\Debug\{1}.dll", _assemblyStr, _assemblyStr));
                try
                {
                    Assembly assembly = Assembly.Load(_assemblyStr);
                    Type[] types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        Type fType = type.GetInterface(typeof(T).Name);
                        if (fType != null)
                        {
                            T service = (T)Activator.CreateInstance(type);
                            _dicService.Add(typeof(T), service);
                            return ;
                        }
                    }
                }
                catch (Exception ex)
                {
                    InstanceByDic<T>();
                }
                InstanceByDic<T>();
            }
        }
        private static void InstanceByDic<T>() where T:IBaseService
        {
            lock(_lock)
            {
                try
                {
                    string[] assArr = _dicServiceAddress[typeof(T)].Split(',');
                    //var path = Path.GetFullPath(string.Format(@"..\..\..\{0}\bin\Debug\{1}.dll", assArr[0], assArr[0]));
                    Type type = Assembly.Load(assArr[0]).GetType(assArr[1]);
                    T service = (T)Activator.CreateInstance(type);
                    _dicService.Add(typeof(T), service);
                }
                catch(Exception ex)
                {
                    throw new Exception("Not Find InterFace Address In Dictionary");
                }
            }
        }
    }
}
