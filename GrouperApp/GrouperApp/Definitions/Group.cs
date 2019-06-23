using System.Collections.Generic;
using System.Xml.Serialization;

namespace GrouperApp.Definitions
{
    public class Group
    {
        [XmlAttribute]
        /// <summary>
        /// Group Division
        /// </summary>
        public int Division { get; set; }

        [XmlAttribute]
        public int ID { get; set; }

        [XmlIgnore]
        public int Size
        {
            get
            {
                if(Individuals != null)
                {
                    return Individuals.Count;
                }
                return -1;
            }
        }

        public string Description { get; set; }
        public List<Individual> Individuals { get; set; }
    }
}
