using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp1
{
    class MyJsonConvert
    {
        // 转换boolean，就是把true变成false， false变成true
        public static bool Flip(bool value)
        {
            if (value == true)
                return false;
            else
                return true;
        }
        // 将一个object string 分成段， 通过每发现一个分割号（，）就分一段来分组
        public static List<String> MySplit(string value)
        {
            List<string> res = new List<string>();
            int first = value.IndexOf('{');
            int last = value.LastIndexOf('}');
            string crop = value.Substring(first + 1, last - first - 1);
            int pos = 0;
            int length = 0;
            int numOfBracket = 0;
            int numOfSquare = 0;
            bool outOfQuotation = true;// 此值用于判断是否不在字符串里
            while (true)
            {

                if (crop[pos + length] == '"')
                {
                    if ((pos + length) == 0)  // 处理第一个就是 “
                    {
                        outOfQuotation = Flip(outOfQuotation);
                    }
                    else
                    {
                        int checkback = 1;
                        while (crop[pos + length - checkback] == '\\') //检查斜杠的数目来确定”是不是个字符
                        {
                            checkback++;
                        }
                        if (checkback % 2 == 1)
                        {
                            outOfQuotation = Flip(outOfQuotation);
                        }
                    }
                }
                // 处理【】{}的成对问题，若是字符便直接忽视
                if (outOfQuotation == true && crop[pos + length] == '[') numOfSquare++;
                if (outOfQuotation == true && crop[pos + length] == ']') numOfSquare--;
                if (outOfQuotation == true && crop[pos + length] == '{') numOfBracket++;
                if (outOfQuotation == true && crop[pos + length] == '}') numOfBracket--;
                // 若发现‘，’是在同等级的分割号且不是字符， 便把从上个分割好或开头到现在这个分割号的字符串储蓄起来
                if (crop[pos + length] == ',' && numOfSquare == 0 && numOfBracket == 0 && outOfQuotation == true)
                {
                    string put = crop.Substring(pos, length);
                    res.Add(put);
                    pos = pos + length + 1;
                    length = 0;
                    continue;
                }
                length++;
                // 若发现已经到底， 便把从上个分割好或开头到现在这个分割号的字符串储蓄起来
                if (pos + length == crop.Length)
                {
                    string put = crop.Substring(pos, length);
                    res.Add(put);
                    break;
                }
            }
            return res;
        }
        // 将set string分成段， 通过每发现一个分割号（，）就分一段来分组
        public static List<String> MySplitForSquare(string value)
        {
            List<string> res = new List<string>();
            int first = value.IndexOf('[');
            int last = value.LastIndexOf(']');
            string crop = value.Substring(first + 1, last - first - 1);
            int pos = 0;
            int length = 0;
            int numOfBracket = 0;
            int numOfSquare = 0;
            bool outOfQuotation = true;// 此值用于判断是否不在字符串里
            while (true)
            {
                if (crop[pos + length] == '"')
                {
                    if ((pos + length) == 0)
                    {
                        outOfQuotation = Flip(outOfQuotation);
                    }
                    else
                    {
                        int checkback = 1;
                        while (crop[pos + length - checkback] == '\\')//检查斜杠的数目来确定”是不是个字符
                        {
                            checkback++;
                        }
                        if (checkback % 2 == 1)
                        {
                            outOfQuotation = Flip(outOfQuotation);
                        }
                    }
                }
                // 处理【】{}的成对问题，若是字符便直接忽视
                if (outOfQuotation == true && crop[pos + length] == '[') numOfSquare++;
                if (outOfQuotation == true && crop[pos + length] == ']') numOfSquare--;
                if (outOfQuotation == true && crop[pos + length] == '{') numOfBracket++;
                if (outOfQuotation == true && crop[pos + length] == '}') numOfBracket--;
                // 若发现‘，’是在同等级的分割号且不是字符， 便把从上个分割好或开头到现在这个分割号的字符串储蓄起来
                if (crop[pos + length] == ',' && numOfSquare == 0 && numOfBracket == 0 && outOfQuotation == true)
                {
                    string put = crop.Substring(pos, length);
                    res.Add(put);
                    pos = pos + length + 1;
                    length = 0;
                    continue;
                }
                length++;
                // 若发现已经到底， 便把从上个分割好或开头到现在这个分割号的字符串储蓄起来
                if (pos + length == crop.Length)
                {
                    string put = crop.Substring(pos, length);
                    res.Add(put);
                    break;
                }
            }
            return res;
        }
        // 将object string分成key, value段组
        public static Dictionary<string, string> MyKeyValue(string value)
        {
            List<string> splited = MySplit(value);//先将object字符串分段
            Dictionary<String, String> res = new Dictionary<string, string>();
            foreach (string item in splited)// 处理每个分段
            {
                // get the key
                // 找key
                string KeyPart, ValuePart;
                int pos = 0, length = 0;
                bool outOfQuotation = true; //此值用于判断是否不在字符串里
                while (true)
                {
                    if (item[pos + length] == '"')
                    {
                        if ((pos + length) == 0)
                        {
                            outOfQuotation = Flip(outOfQuotation);
                        }
                        else
                        {
                            int checkback = 1;
                            while (item[pos + length - checkback] == '\\')
                            {
                                checkback++;
                            }
                            if (checkback % 2 == 1)
                            {
                                outOfQuotation = Flip(outOfQuotation);
                            }
                        }
                    }
                    length++;
                    if (outOfQuotation)
                    {
                        // 找到key的结尾就把key储蓄
                        KeyPart = item.Substring(pos, length);
                        pos = pos + length + 1;
                        break;
                    }
                }
                // 将剩下的字符串以value来储蓄
                ValuePart = item.Substring(pos, item.Length - pos);
                // 将key, value成对储蓄
                res.Add(KeyPart, ValuePart);
            }
            return res;
        }
        // 将分好段的string set转换成object set
        public static List<object> MyProcessForList(List<string> list)
        {
            List<object> res = new List<object>();
            foreach (string item in list)//处理每个分段
            {
                if (item[0] == '[')//发现这个分段是集合set
                {
                    res.Add(MyProcessForList(MySplitForSquare(item.Trim())));
                }
                else if (item[0] == '{')//发现这个分段是对象object
                {
                    res.Add(MyProcess(MyKeyValue(item)));
                }
                else if (item[0] == '"')//发现这个分段是字符串
                {
                    res.Add(item);
                }
                else // 发现这个分段可以是数字
                {
                    res.Add(Convert.ToInt32(item));
                }
            }
            return res;
        }
        // 将分好的key, value组中的value从string格式换成object格式
        public static Dictionary<string, object> MyProcess(Dictionary<string, string> list)
        {
            Dictionary<String, Object> res = new Dictionary<string, Object>();
            foreach (var item in list)//处理每个分段
            {
                string KeyPart = item.Key;//先拿到key并储蓄
                object ValuePart;
                if (item.Value[0] == '{') // object， 发现这个分段是对象
                {
                    ValuePart = MyProcess(MyKeyValue(item.Value));
                }
                else if (item.Value[0] == '[') // list， 发现这个分段是集合
                {

                    var temp1 = MyProcessForList(MySplitForSquare(item.Value.Trim()));
                    ValuePart = temp1;
                }
                else if (item.Value[0] == '"') // string， 发现这个分段是字符串
                {
                    ValuePart = item.Value;
                }
                else // 发现这个分段可以是数字
                {
                    ValuePart = Convert.ToInt32(item.Value);
                }

                res.Add(KeyPart, ValuePart);
            }
            return res;
        }
        public static T MyDtoO<T>(Dictionary<string, object> list) where T : new()
        {
            var MyType = typeof(T);
            T res = new T();
            foreach (System.Reflection.PropertyInfo item in MyType.GetProperties())
            {
                var propname = '"' + item.Name + '"';
                if (!list.ContainsKey(propname))
                {
                    continue;
                }
                var check = item.PropertyType.Name;
                if (check == "Int32")
                {
                    item.SetValue(res, list[propname]);
                }
                else if (check == "String")
                {
                    item.SetValue(res, list[propname]);
                }
                else if (check == "List`1")
                {
                    var checktype = (list[propname] as List<object>)[0].GetType();
                    var some = list[propname] as List<object>;

                    if (checktype.Name == "Int32")
                    {
                        var target = some.ConvertAll(x => (int)x);
                        item.SetValue(res, target, null);
                    }
                    else if (checktype.Name == "String")
                    {
                        var target = some.ConvertAll(x => (String)x);
                        item.SetValue(res, target, null);
                    }
                }
            }
            return default(T);
        }
        public static List<object> MyDtoOForList(List<object> list)
        {

            List<object> res = list;
            //foreach (var data in list)
            //{
            //    if (data is int)
            //    {
            //        res.Add(data);
            //    }
            //    else if (data is string)
            //    {
            //        res.Add(data);
            //    }
            //    else if (data is List<object>)
            //    {
            //        res.Add(MyDtoOForList(data as List<object>));
            //    }
            //    else
            //    {
            //        res.Add(MyDtoO<object>(data as Dictionary<string, object>));
            //    }
            //}
            return res;
        }
    }
}
