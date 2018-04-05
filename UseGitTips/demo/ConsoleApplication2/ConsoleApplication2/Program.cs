using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "123456789";
            if (str[str.Length-1] == ',')
            {
                str = str.Substring(0, str.Length - 1);
            }
            Console.WriteLine(str);
            //NFineBaseEntities db = new NFineBaseEntities();
            //var entity = db.Message_Center.First(x => x.Id > 0);
            //string s = entity.MessageContent;
            //int i = s.IndexOf("id=")+3;
            
            //string str = s.Substring(i, entity.MessageContent.Length-i);
            //Console.WriteLine(str);
            //int len = 0;
            //foreach (var item in str)
            //{
            //    if (Convert.ToInt32(item) >= 48 && Convert.ToInt32(item) <= 57)
            //    {
            //        len++;
            //        Console.WriteLine(item);
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}
            //Console.WriteLine("len=");
            //Console.WriteLine(len);
            //str = str.Substring(0, len);
            //string result = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
            //Console.WriteLine(result);
        }
    }
}
