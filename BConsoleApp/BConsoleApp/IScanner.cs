using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BConsoleApp
{
    /// <summary>
    /// Item Scanner Interface
    /// </summary>
    public interface IScanner
    {
        IEnumerable<Item> ReadItems();
    }
}
