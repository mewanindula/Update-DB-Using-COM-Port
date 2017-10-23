using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace showData
{
    public partial class showData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder table = new StringBuilder();

            SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Indula\Desktop\insertData\insertData\App_Data\Detail.mdf;Integrated Security=True");

            con1.Open();

            String str1 = "SELECT * FROM Location";

            SqlCommand cmd = new SqlCommand(str1, con1);

            SqlDataReader rd = cmd.ExecuteReader();

            Response.AppendHeader("refresh", "2");

            table.Append("<table border='1'>");
            table.Append("<th>Latitude</th><th>Longitude</th><th>Location</th><th>Date</th><th>Time</th><th>Mobile</th></tr>");

            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    table.Append("<tr>");
                    table.Append("<td>" + rd[1] + "</td>");
                    table.Append("<td>" + rd[2] + "</td>");
                    table.Append("<td><a href = 'http://maps.google.com/maps?q=" + rd[1] + "," + rd[2] + "' target='_blank'>Location</a></td>");
                    table.Append("<td>" + rd[3] + "</td>");
                    table.Append("<td>" + rd[4] + "</td>");
                    table.Append("<td>" + rd[5] + "</td>");
                    table.Append("</tr>");
                }
            }
            table.Append("</table>");

            PlaceHolder1.Controls.Add(new Literal { Text = table.ToString() });

            rd.Close();
            rd.Dispose();

            con1.Close();
        }
    }
}