using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public class ObjectUtils
    {
        /// <summary>
        /// 通过反射机制，获取类对象中的某个属性的值
        /// </summary>
        /// <param name="columnName">属性</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string GetModelValue(object obj, string columnName)
        {
            try
            {
                Type Ts = obj.GetType();
                string propertyName = MyStringUtil.getCamelName(columnName);
                System.Reflection.PropertyInfo propertyInfo = Ts.GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    object o = propertyInfo.GetValue(obj, null);
                    string Value = Convert.ToString(o);
                    if (string.IsNullOrEmpty(Value)) return null;
                    return Value;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 通过反射机制，获取类对象中的所有简单类型的 属性名称列表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string[] GetModelProperties(object obj)
        {
            List<string> result = new List<string>();

            try
            {
                Type Ts = obj.GetType();
                System.Reflection.PropertyInfo[] ps = Ts.GetProperties();
                foreach (System.Reflection.PropertyInfo propertyInfo in ps)
                {
                    if (propertyInfo.PropertyType == typeof(string)
                        || propertyInfo.PropertyType == typeof(bool)
                        || propertyInfo.PropertyType == typeof(int)
                        || propertyInfo.PropertyType == typeof(long)
                        || propertyInfo.PropertyType == typeof(double))
                        result.Add(propertyInfo.Name);
                }

            }
            catch
            {
                return null;
            }

            return result.ToArray();
        }

        public static object[] GetModelValues(object obj)
        {
            List<object> result = new List<object>();

            try
            {
                Type Ts = obj.GetType();
                System.Reflection.PropertyInfo[] ps = Ts.GetProperties();
                foreach (System.Reflection.PropertyInfo propertyInfo in ps)
                {
                    if (propertyInfo.PropertyType == typeof(string)
                        || propertyInfo.PropertyType == typeof(bool)
                        || propertyInfo.PropertyType == typeof(int)
                        || propertyInfo.PropertyType == typeof(long)
                        || propertyInfo.PropertyType == typeof(double))
                    {
                        object temp = propertyInfo.GetValue(obj, null);
                        result.Add(temp);
                    }

                }

            }
            catch
            {
                return null;
            }

            return result.ToArray();
        }

        public static bool SetModelValue(object obj, string columnName, string Value)
        {
            try
            {
                Type Ts = obj.GetType();
                string propertyName = MyStringUtil.getCamelName(columnName);
                System.Reflection.PropertyInfo propertyInfo = Ts.GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    object v = Convert.ChangeType(Value, propertyInfo.PropertyType);
                    propertyInfo.SetValue(obj, v, null);
                    return true;
                }
                else
                    return false;

            }
            catch
            {
                return false;
            }
        }

        public static bool copyObjectValues(object src, object dest)
        {
            try
            {
                Type Ts = src.GetType();

                System.Reflection.PropertyInfo[] ps = Ts.GetProperties();
                foreach (System.Reflection.PropertyInfo propertyInfo in ps)
                {
                    if (propertyInfo.PropertyType == typeof(string)
                        || propertyInfo.PropertyType == typeof(bool)
                        || propertyInfo.PropertyType == typeof(int)
                        || propertyInfo.PropertyType == typeof(long)
                        || propertyInfo.PropertyType == typeof(double))
                    {
                        object value = propertyInfo.GetValue(src, null);

                        object v = Convert.ChangeType(value, propertyInfo.PropertyType);
                        propertyInfo.SetValue(dest, v, null);

                    }

                }

                return true;

            }
            catch
            {
                return false;
            }
        }

        
    }
}
