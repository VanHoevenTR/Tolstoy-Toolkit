using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tolstoy_Toolkit.Reader
{
    public class HistoryReader
    {
        public string historyList = "";

        public HistoryReader()
        {
            if (File.Exists(Program.historyFilePath))
                historyList = File.ReadAllText(Program.historyFilePath);
        }

        public void Save()
        {
            //Debug.WriteLine("Save history " + DateTime.Now);
            if (!String.IsNullOrEmpty(historyList))
                File.WriteAllText(Program.historyFilePath, historyList);
        }
    }
}
