using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JW.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "";
            JW.API.Sanse.GetCustomerOnhandByStyleList("data.sanse.com.cn:9000", "84d1cf38-de5a-4283-902e-a12448cf3bd7", "X31", "", out str);
        }
    }
}
