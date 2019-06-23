using System;
using System.IO;
using System.Xml.Serialization;
using GrouperApp.Definitions;

namespace GrouperApp
{
    /// <summary>
    /// Serialize and Deserialize Grouper xml file
    /// </summary>
    public class Grouper
    {
        public static void CreateGrouperFile(string filename, Func<Group> method)
        {
            // Create an instance of the XmlSerializer class;
            // specify the type of object to serialize.
            XmlSerializer serializer =
            new XmlSerializer(typeof(Group));
            TextWriter writer = new StreamWriter(filename);

            Group grp =  method.Invoke(); 

            // Serialize the purchase order, and close the TextWriter.
            serializer.Serialize(writer, grp);
            writer.Close();
        }

        public Func<Group> CreateDataMethod()
        {
            return () =>
            {
                Group grp = new Group() { ID = 1000, Division = 1, Description = "Team A" };

                int taskid1 = 1;
                int taskid2 = 2;
                int taskid3 = 3;

                grp.Individuals = new System.Collections.Generic.List<Individual>()
                {
                    new Individual()
                    {
                        ID = taskid1.ToString(),
                        GroupID = grp.ID.ToString(),
                        Shifts = 1,
                        Description = string.Format("Working on {0}st task",taskid1.ToString())
                    },
                    new Individual()
                    {
                        ID = taskid2.ToString(),
                        GroupID = grp.ID.ToString(),
                        Shifts = 1,
                        Description = string.Format("Working on {0}nd task",taskid2.ToString())
                    },
                    new Individual()
                    {
                        ID = taskid3.ToString(),
                        GroupID = grp.ID.ToString(),
                        Shifts = 1,
                        Description = string.Format("Working on {0}rd task",taskid3.ToString())
                    },
                    new Individual()
                    {
                        ID = taskid1.ToString(),
                        GroupID = grp.ID.ToString(),
                        Shifts = 2,
                        Description = string.Format("Working on {0}st task",taskid1.ToString())
                    },
                    new Individual()
                    {
                        ID = taskid2.ToString(),
                        GroupID = grp.ID.ToString(),
                        Shifts = 2,
                        Description = string.Format("Working on {0}nd task",taskid2.ToString())
                    },
                    new Individual()
                    {
                        ID = taskid3.ToString(),
                        GroupID = grp.ID.ToString(),
                        Shifts = 2,
                        Description = string.Format("Working on {0}rd task",taskid3.ToString())
                    }
                };
                return grp;
            };            
        }

        public static void ReadGrouperFile(string filename)
        {
            // Create an instance of the XmlSerializer class;
            // specify the type of object to be deserialized.
            XmlSerializer serializer = new XmlSerializer(typeof(Group));
            /* If the XML document has been altered with unknown 
            nodes or attributes, handle them with the 
            UnknownNode and UnknownAttribute events.*/
            serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
            serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

            // A FileStream is needed to read the XML document.
            FileStream fs = new FileStream(filename, FileMode.Open);
            // Declare an object variable of the type to be deserialized.
            Group grp;
            /* Use the Deserialize method to restore the object's state with
            data from the XML document. */
            grp = (Group)serializer.Deserialize(fs);
            // Read the order date.
            Console.WriteLine("OrderDate: " + grp.ID);
        }

        private static void serializer_UnknownNode (object sender, XmlNodeEventArgs e)
        {
            Console.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
        }

        private static void serializer_UnknownAttribute (object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            Console.WriteLine("Unknown attribute " +
            attr.Name + "='" + attr.Value + "'");
        }
    }
}
