using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.Backend
{
    interface IDisplay
    {
        
        void Play();
        void StopPlayBack();
        bool Pause { get; set; }
        /// <summary>
        /// Fast Forward Functionality
        /// </summary>
        void Forward();
        void Rewind();
        int Marker
        {
            get;
        }
        
    }
}
