using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;    //反射名称空间
using System.Windows.Forms;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Form stu = new Form();    //实例化一个类对象
            Type t = stu.GetType();         //获取对象类型
            foreach (PropertyInfo item in t.GetProperties().OrderBy(x=>x.Name))        //遍历类型属性列表
            {
                Console.WriteLine(item.Name);                       //打印属性名称
            }
        }
    }
    class student
    {
        public string name { get; set; }
        public bool sex { get; set; }
        public int age { get; set; }
        public double height { get; set; }
    }
}
