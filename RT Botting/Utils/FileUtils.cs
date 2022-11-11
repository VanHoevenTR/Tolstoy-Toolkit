using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tolstoy_Toolkit.Utils
{
    internal class FileUtils
    {
        public static bool IsFileLocked(string file)
        {
            FileInfo fi = new FileInfo(file);
            try
            {
                using (FileStream stream = fi.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }
    }
}
