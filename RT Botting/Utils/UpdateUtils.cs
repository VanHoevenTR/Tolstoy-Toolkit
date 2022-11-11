using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tolstoy_Toolkit
{
    internal class UpdateUtils
    {
        public static string RemoteVersion()
        {
            try
            {
                string getupdatedata;
                WebClient wc = new WebClient();

                getupdatedata = wc.DownloadString("https://raw.githubusercontent.com/VanHoevenTR/VanHoevenTR/main/RTver.txt");

                if (!String.IsNullOrEmpty(getupdatedata))
                {
                    return getupdatedata;
                }
            }
            catch // (Exception ex)
            {
                return "ERROR";
            }
            return null;
        }
    }
}
