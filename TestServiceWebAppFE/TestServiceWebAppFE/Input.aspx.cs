using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestServiceWebAppFE.ServiceReference1;

namespace TestServiceWebAppFE
{
    public partial class Input : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TestTable tb1 = new TestTable();
            string msg;
            tb1.Row = Convert.ToInt32(TextBox1.Text);
            tb1.Column = (float)Convert.ToDecimal(TextBox2.Text);
            tb1.Name = TextBox3.Text;
            tb1.Length = Convert.ToInt32(TextBox4.Text);
            tb1.Unit = TextBox5.Text;

            msg = client.PutData(tb1);
            TextBox12.Text = msg;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TestTable tb2 = new TestTable();
            tb2 = client.GetData();
            TextBox6.Text = tb2.ID.ToString();
            TextBox7.Text = tb2.Row.ToString();
            TextBox8.Text = tb2.Column.ToString();
            TextBox9.Text = tb2.Name;
            TextBox10.Text = tb2.Length.ToString();
            TextBox11.Text = tb2.Unit;

        }
    }
}