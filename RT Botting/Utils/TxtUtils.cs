using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tolstoy_Toolkit.Classes
{
    internal class TxtUtils
    {
        public static string[] LoadToken()
        {
            if (File.Exists(Program.tokensFilePath))
            {
                var tokens = File.ReadAllLines(Program.tokensFilePath).OrderBy(line => Guid.NewGuid()).ToArray();
                var rnd = new Random();
                tokens = tokens.OrderBy(line => rnd.Next()).ToArray();
                var newTokens = new List<string>();

                foreach (string t in tokens)
                {
                    if (t.Contains("#") || String.IsNullOrEmpty(t))
                        continue;

                    newTokens.Add(t);
                }

                return newTokens.ToArray();
            }
            return null;
        }

        public static string[] LoadFavUsers()
        {
            if (File.Exists(Program.favUsersFilePath))
            {
                var text = File.ReadAllLines(Program.favUsersFilePath);
                var newText = new List<string>();

                foreach (string t in text)
                {
                    if (t.Contains("#") || String.IsNullOrEmpty(t))
                        continue;

                    newText.Add(t);
                }

                return newText.ToArray();
            }
            return null;
        }
    }
}
