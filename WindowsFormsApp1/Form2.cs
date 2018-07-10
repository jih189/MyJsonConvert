using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var list = new TestModel {
            Id=1,str="字符串",listid=new List<int> {1,2,3 },
            liststr=new List<string> { "s,1","s2","s3","s4\\\\"}
            };

            var json = JsonConvert.SerializeObject(list);

            textBox1.Text = json;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = JsonConvert.DeserializeObject<TestModel>(textBox1.Text.Trim());
        }

        private void button3_Click(object sender, EventArgs e)
        {

            var result = MyJsonConvert.MyProcess(MyJsonConvert.MyKeyValue(textBox1.Text.Trim()));
            var final = MyJsonConvert.MyDtoO<TestModel>(result);
            //var result = MyJsonConvert.MySplitForSquare(textBox1.Text.Trim());
        }
    }

    public class TestModel {

        public int Id { set; get; }

        public string str { set; get; }

        public List<int> listid { set; get; }

        public List<string> liststr { set; get; }
    }
}
