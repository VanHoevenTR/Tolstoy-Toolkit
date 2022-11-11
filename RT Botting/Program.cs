using Bluegrams.Application;
using Tolstoy_Toolkit.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Tolstoy_Toolkit
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PortableSettingsProvider.SettingsFileName = "config.xml";
            PortableSettingsProvider.ApplyProvider(Settings.Default);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


        public static readonly string tokensFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "tokens.txt");
        public static readonly string usrIdMassMsgFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "useridstomassmessage.txt");
        public static readonly string friendsFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "friends.txt");
        public static readonly string favUsersFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "favoriteusers.txt");
        public static readonly string historyFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "votehistory.csv");
        public static readonly string notesFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "notes.txt");
        public static readonly string usersCsvPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "users.csv");
        public static readonly string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static readonly string tempDir = Path.Combine(Path.GetTempPath(), "RT");
        public static readonly string usrInfoDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "User Info");
    }
}
