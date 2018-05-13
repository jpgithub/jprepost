using System.Collections.Generic;
using System.IO;

namespace BConsoleApp
{
    public class Scanner : IScanner
    {
        private string filename;
        public Scanner(string file)
        {
            this.filename = file;
        }

        public static IScanner Create(string file)
        {
            return new Scanner(file);
        }

        public IEnumerable<Item> ReadItems()
        {
            using (FileStream reader = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                int counter = 0;
                byte[] datarray = new byte[4410];

                while (reader.CanRead && counter < 1000)
                {
                    reader.Read(datarray, 0, datarray.Length);
                    yield return new Item() { Name = string.Format("Item_{0}", counter), Id = counter++, PayLoad = datarray };
                }
            }
        }
    }
}
