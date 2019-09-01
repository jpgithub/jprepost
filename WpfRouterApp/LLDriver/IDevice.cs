using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLDriver
{
    public interface IDevice
    {
        void Init();
        void ExecuteTask(int tasknumber);
        void Close();
    }
}
