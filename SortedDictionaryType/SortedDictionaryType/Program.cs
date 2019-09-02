using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedDictionaryType
{
    class Program
    {

        static void Main(string[] args)
        {
            Dictionary<string, string> table = new Dictionary<string, string>();
            List<string> sortedorderList = new List<string>();
            table.Add("fp", "kindle book");
            table.Add("ab1", "kindle book");
            table.Add("mj1", "echo kindle book");

            var sortedByValue = table.OrderBy(d => d.Value);
            var sortedByKey = sortedByValue.OrderBy(d => d.Key);

            var listTable = sortedByKey.ToList();
            foreach(var  order in sortedByValue)
            {
                sortedorderList.Add(string.Format("{0} {1}", order.Key, order.Value));
            }
            //SortedDictionaryExample();
        }

        private static void SortedDictionaryExample()
        {
            // Create a new sorted dictionary of strings, with string
            // keys.
            SortedDictionary<string, string> openWith =
                new SortedDictionary<string, string>();

            // Add some elements to the dictionary. There are no 
            // duplicate keys, but some of the values are duplicates.
            openWith.Add("kindle book", "fp");
            openWith.Add("echo kindle book", "ab1");

            Console.WriteLine();
            foreach (var kvp in openWith)
            {
                Console.WriteLine("Key = {0}, Value = {1}",
                    kvp.Key, kvp.Value);
            }
        }
    }
}
