using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:8011/Event/PostRequest");

            var postData = "EmployeeID=" + textBox1.Text.Trim();
            postData += "&Date=" + string.Format("{0:yyyy-MM-dd}",dateTimePicker1.Value);
          //  postData += "&TimeOfDay=1234";


            //var postData = "\"EmployeeID\":" + textBox1.Text.Trim();
            //postData += ",\"time\":\"2018/07/07\"";

            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            label2.Text= textBox1.Text.Trim() + " " + responseString;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://127.0.0.1:8011/");

          
            request.Method = "GET";
        

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }


    }
}
