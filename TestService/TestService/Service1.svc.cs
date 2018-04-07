using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string PutData(TestTable tb)
        {
            string msg;
            
            SqlConnection dbconnection = new SqlConnection("Data Source=(LocalDB)\\v11.0;AttachDbFilename='c:\\users\\jp\\documents\\visual studio 2012\\Projects\\TestService\\TestService\\App_Data\\CsvDatabase.mdf';Integrated Security=True");
            SqlCommand cmd = new SqlCommand("INSERT INTO TestTable(Id,Name,Length,Unit,Rows,Columns)  VALUES(@Id,@Name,@Length,@Unit,@Rows,@Columns)", dbconnection);
            cmd.Parameters.AddWithValue("@Id", tb.ID);
            cmd.Parameters.AddWithValue("@Name", tb.Name);
            cmd.Parameters.AddWithValue("@Length", tb.Length);
            cmd.Parameters.AddWithValue("@Unit", tb.Unit);
            cmd.Parameters.AddWithValue("@Columns", tb.Column);
            cmd.Parameters.AddWithValue("@Rows", tb.Row);
            
            
            dbconnection.Open();
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                msg = "Success!";
            }
            else
            {
                msg = "Failure!";
            }
            dbconnection.Close();
            return msg;
        }

        public TestTable GetData()
        {
            TestTable tb1 = new TestTable();
            SqlConnection dbconnection = new SqlConnection("Data Source=(LocalDB)\\v11.0;AttachDbFilename='c:\\users\\jp\\documents\\visual studio 2012\\Projects\\TestService\\TestService\\App_Data\\CsvDatabase.mdf';Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM TestTable", dbconnection);

            dbconnection.Open();
            SqlDataReader reader;
            reader = cmd.ExecuteReader();

            if (reader != null)
            {
                if (reader.Read())
                {
                    tb1.ID = (int)reader["Id"];
                    tb1.Length = (int)reader["Length"];
                    tb1.Row = (int)reader["Rows"];
                    tb1.Column = (float)Convert.ToDouble(reader["Columns"].ToString());
                    tb1.Name = reader["Name"].ToString();
                    tb1.Unit = reader["Unit"].ToString();
                }
                else
                {
                    tb1 = null;
                }
            }

            dbconnection.Close();

            return tb1; 
        }
    }
}
