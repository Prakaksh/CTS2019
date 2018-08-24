using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTS2019
{
    public class AppUtility
    {
        public static string getStatus(int val)
        {
            string str = "";
            switch (val)
            {
                case 0:
                    str = "failure";
                    break;
                case 1:
                    str = "success";
                    break;
                case 2:
                    str = "exist";
                    break;
                case 3:
                    str = "update";
                    break;
                default:
                    str = "failure";
                    break;
            }
            return str;
        }
    }
}