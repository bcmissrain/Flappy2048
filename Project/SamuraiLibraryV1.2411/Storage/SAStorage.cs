using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace Samurai
{
    public static class SAStorage
    {
        static IsolatedStorageSettings settings;
        static SAStorage()
        {
            settings = IsolatedStorageSettings.ApplicationSettings;
        }
        public static bool Contains(string key)
        {
            return settings.Contains(key);
        }
        public static void SaveData(string key, string value)
        {
            settings[key] = value;
        }
        /// <summary>
        /// 判断是否为Null然后再使用数据
        /// </summary>
        /// <param name="key"></param>
        public static string ReadData(string key)
        {
            if (settings.Contains(key))
            {
                return settings[key].ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
