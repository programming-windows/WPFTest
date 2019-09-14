using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public class MyStringUtil
    {
        public static Boolean isEmpty(String str)
        {
            if ((str==null) || "".Equals(str.Trim()) || "null".Equals(str.Trim()))
                return true;

            return false;
        }


        public static String getGuidStr()
        {
            return System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 生成22位唯一的数字 并发可用
        /// </summary>
        /// <returns></returns>
        public static string getGuidStr22()
        {
            System.Threading.Thread.Sleep(1); //保证yyyyMMddHHmmssffff唯一
            Random d = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            string strUnique = DateTime.Now.ToString("yyyyMMddHHmmssffff") + d.Next(1000, 9999);
            return strUnique;
        }


        /**
         * 将下划线大写方式命名的字符串转换为驼峰式。如果转换前的下划线大写方式命名的字符串为空，则返回空字符串。</br>
         * 例如：HELLO_WORLD->helloWorld
         * @param name 转换前的下划线大写方式命名的字符串
         * @return 转换后的驼峰式命名的字符串
         */
        public static string getCamelName(string name) {  
            StringBuilder result = new StringBuilder();  
            // 快速检查  
            if (isEmpty(name)) {  
                // 没必要转换  
                return "";  
            } else if (!name.Contains("_")) {  
                // 不含下划线，直接转换成小写  
                return name.ToLower() ;  
            }  
            // 用下划线将原始字符串分割  
            string[] camels = name.Split('_');
 
            foreach (string camel in  camels) {  
                // 跳过原始字符串中开头、结尾的下换线或双重下划线  
                if (isEmpty(camel))
                {  
                    continue;  
                }  
                // 处理真正的驼峰片段  
                if (result.Length == 0) {  
                    // 第一个驼峰片段，全部字母都小写  
                    result.Append(camel.ToLower());  
                } else {  
                    // 其他的驼峰片段，首字母大写  
                    result.Append(camel.Substring(0, 1).ToUpper());
                    result.Append(camel.Substring(1).ToLower());  
                }  
            }  
            return result.ToString();  
        }

        /**
         * 将驼峰式命名的字符串转换为下划线大写方式。如果转换前的驼峰式命名的字符串为空，则返回空字符串。</br>
         * 例如：helloWorld->HELLO_WORLD
         * @param name 转换前的驼峰式命名的字符串
         * @return 转换后下划线大写方式命名的字符串
         */
        public static string getUnderscoreName(string name)
        {
            StringBuilder result = new StringBuilder();
            if (!isEmpty(name))
            {
                // 将第一个字符处理成大写
                result.Append(name.Substring(0, 1).ToUpper());
                // 循环处理其余字符
                for (int i = 1; i < name.Length; i++)
                {
                    String s = name.Substring(i, i + 1);
                    // 在大写字母前添加下划线
                    if (s.Equals(s.ToUpper()) &&  char.IsDigit(s,0))
                    {
                        result.Append("_");
                    }
                    // 其他字符直接转成大写
                    result.Append(s.ToUpper());
                }
            }
            return result.ToString();
        }

       

    }
}
