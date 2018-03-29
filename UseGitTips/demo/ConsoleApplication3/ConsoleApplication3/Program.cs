using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class showVm
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string pwd { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<showVm> list = new List<showVm>();
            NFineBaseEntities db = new NFineBaseEntities();
            foreach (var userFather in db.Sys_User.OrderBy(x=>x.F_RealName))
            {
                foreach (var userSon in db.Sys_UserLogOn)
                {
                    if (userFather.F_Id == userSon.F_UserId)
                    {
                        showVm vm = new showVm();
                        vm.uid = userFather.F_Id;
                        vm.name = userFather.F_RealName;
                        vm.pwd = userSon.F_UserPassword;
                        list.Add(vm);
                    }
                }
            }
            foreach (var item in list)
            {
                Console.WriteLine("{0} \t ;{1} \t;{2}\t",item.uid,item.name,item.pwd);
            }
        }
    }
}
