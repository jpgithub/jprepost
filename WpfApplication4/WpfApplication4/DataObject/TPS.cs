using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication4.DataObject
{
    public class TPS
    {
        private HashSet<ATS> atsset = new HashSet<ATS>();

        public enum TpsType
        {
            X_Group_1 = 1,
            X_Group_2 = 2,
            X_Group_3 = 3,
            X_Group_4 = 4,
            X_Group_5 = 5,
            X_Group_6 = 6,
            Y_Group_1 = 7,
            Y_Group_2 = 8,
            Y_Group_3 = 9,
            Y_Group_4 = 10,
            Y_Group_5 = 11,
            Y_Group_6 = 12,
            Unknown = 0
        }

        public string Name { get; set; }
        public TpsType ID { get; internal set; }
        public string Date { get; set; }
        public List<ATS> ATSList
        {
            get
            {
                return atsset.ToList();
            }
        }
        /// <summary>
        /// Generate a TPS according to ATS Set
        /// </summary>
        /// <param name="name"> TPS Name </param>
        /// <param name="atslist"> Ats Set</param>
        /// <returns></returns>
        public static TPS Create(string name, List<ATS> atslist)
        {
            TPS newTps = new TPS();
            int grpid = 0;
            string date = string.Empty;
            foreach (ATS ats in atslist)
            {
                newTps.atsset.Add(ats);
                date = ats.Date;
                grpid += ats.ID;
            }

            newTps.Name = name + "_" + ((TpsType)grpid).ToString();
            newTps.ID = (TpsType)grpid;
            newTps.Date = date;
            return newTps;
        }
    }
}
