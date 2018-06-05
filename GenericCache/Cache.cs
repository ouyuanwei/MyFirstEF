using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entity.BaseEntity;
using System.Configuration;

namespace GenericCache
{
    public class Cache
    {
        private static Dictionary<string, BaseCache> _dicCache = new Dictionary<string, BaseCache>();
        private static int _clearSpan = 10;//默认缓存清理时间 单位s
        private static readonly object _lock = new object();

        static Cache()
        {
            #region 从配置中获取缓存清理时间间隔
            string spanStr = ConfigurationManager.AppSettings["CacheSpan"];
            int span = 0;
            if (int.TryParse(spanStr, out span))
            {
                _clearSpan = span;
            } 
            #endregion
            //启动一个线程,固定间隔清理过期缓存
            Task.Run(() =>
            {
                while (true)
                {
                    lock (_lock)
                    {
                        List<string> keyList = new List<string>();
                        foreach (var item in _dicCache)
                        {
                            keyList.Add(item.Key);
                        }
                        foreach (var key in keyList)
                        {
                            if (_dicCache[key].OverdueTime < DateTime.Now)
                            {
                                _dicCache.Remove(key);
                            }
                        }
                    }
                    Thread.Sleep(1000 * _clearSpan);//清理 间隔时间
                }
            });
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">关键字key</param>
        /// <param name="data">缓存数据</param>
        /// <param name="overdunSeconds">过期时间(单位秒) 默认30分钟有效</param>
        public static void AddCache<T>(string key, T data, int overdunSeconds = 60 * 30)
        {
            lock (_lock)
            {
                if (_dicCache.ContainsKey(key))
                {
                    _dicCache.Remove(key);
                }
                _dicCache.Add(key, new BaseCache<T>
                {
                    OverdueTime = DateTime.Now.AddSeconds(overdunSeconds),
                    Data = data
                });
            }
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">关键字key</param>
        /// <param name="data">缓存数据</param>
        /// <param name="overdunSeconds">设置过期时间</param>
        public static void AddCache<T>(string key, T data, DateTime dateTime)
        {
            lock (_lock)
            {
                if (_dicCache.ContainsKey(key))
                {
                    _dicCache.Remove(key);
                }
                _dicCache.Add(key, new BaseCache<T>
                {
                    OverdueTime = dateTime,
                    Data = data
                });
            }
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">关键字key</param>
        /// <returns></returns>
        public static T GetCache<T>(string key)
        {
            if (_dicCache.ContainsKey(key))
            {
                lock (_lock)
                {
                    if (_dicCache.ContainsKey(key) && _dicCache[key].OverdueTime > DateTime.Now && _dicCache[key].GetType() == typeof(BaseCache<T>))
                    {
                        BaseCache<T> cache = (BaseCache<T>)_dicCache[key];
                        return cache.Data;
                    }
                }
            }
            return default(T);
        }
        /// <summary>
        /// 主动移除缓存
        /// </summary>
        /// <param name="key">按照key移除</param>
        public static void RemoverCache(string key)
        {
            if (_dicCache.ContainsKey(key))
            {
                lock (_lock)
                {
                    _dicCache.Remove(key);
                }
            }
        }
        /// <summary>
        /// 主动移除缓存
        /// </summary>
        /// <param name="func">按照规定规则移除</param>
        public static void RemoverCache(Func<string, bool> func)
        {
            lock (_lock)
            {
                List<string> keyList = new List<string>();
                foreach (var item in _dicCache)
                {
                    keyList.Add(item.Key);
                }
                foreach (var key in keyList)
                {
                    if (func.Invoke(key))
                    {
                        _dicCache.Remove(key);
                    }
                }
            }
        }
        /// <summary>
        /// 全部移除缓存
        /// </summary>
        public static void RemoverCache()
        {
            lock (_lock)
            {
                List<string> keyList = new List<string>();
                foreach (var item in _dicCache)
                {
                    keyList.Add(item.Key);
                }
                keyList.ForEach(m => _dicCache.Remove(m));
            }
        }
    }
}
