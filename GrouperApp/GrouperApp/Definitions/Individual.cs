using System.Xml.Serialization;

namespace GrouperApp.Definitions
{
    public class Individual
    {
        public string ID { get; set; }

        /// <summary>
        /// Shift Number decide what time to start work order
        /// </summary>
        public int Shifts { get; set; }

        [XmlIgnore]
        public string GroupID { get; set; }
        
        /// <summary>
        /// Work Order Location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Work Order Description 
        /// </summary>
        public string Description { get; set; }

        [XmlIgnore]
        /// <summary>
        /// Work Order 
        /// </summary>
        public WorkOrder WrkOrder { get; set; } 

    }
}
