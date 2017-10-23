using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace insertData
{
    public partial class insertData : System.Web.UI.Page
    {

        SerialPort sps = new SerialPort();

        private SerialPort myport;

        private string in_data;
        private string lat;
        private string lon;
        private string date;
        private string time;
        private string tp;


        protected void Page_Load(object sender, EventArgs e)
        {
            myport = new SerialPort();
            myport.BaudRate = 9600;
            myport.PortName = "COM3";
            myport.Parity = Parity.None;
            myport.DataBits = 8;
            myport.StopBits = StopBits.One;
            myport.DataReceived += myport_DataReceived;

            try
            {
                myport.Open();
                TextBox1.Text = "";


            }
            catch (Exception ex)
            {
                TextBox1.Text = ex.Message + "error";
            }
        }


        void myport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            in_data = myport.ReadLine();

            string v = in_data.ToString();

            if (v[0] == 'V' && v[1] == 'T' && v[2] == 'D')
            {
                string[] data = v.Split(',');

                lat = data[1];
                lon = data[2];
                date = data[3];
                time = data[4];
                tp = data[5];
                tp = tp.Replace("\r", string.Empty);


                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Indula\Desktop\insertData\insertData\App_Data\Detail.mdf;Integrated Security=True");

                con.Open();

                String str = "INSERT INTO Location(Latitude,Longitude,Date,Time,Mobile) VALUES('" + lat + "','" + lon + "','" + date + "','" + time + "','" + tp + "') ";

                SqlCommand cmd = new SqlCommand(str, con);

                int OBJ = Convert.ToInt32(cmd.ExecuteNonQuery());

                if (OBJ > 0)
                {
                    //Sucess
                }
                else
                {
                    //Error
                }

                con.Close();
            }

        }
    }
}