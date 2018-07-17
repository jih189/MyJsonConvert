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
using System.Diagnostics;

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
            var model3 = new TestModel3
            {
                Id = 1,
                listint = new List<int> { 1, 23, 4, 32 },
                liststr = new List<string> { "111", "22", "33" }
            };

            var list = new TestModel
            {
                Id = 1,
                listint = new List<int> { 1, 23, 4, 32 },
                liststr = new List<string> { "111", "22", "33" },
                model3 = model3
            };

            var list1 = new TestModel1
            {
                intlist = new List<TestModel> { list, list },
                testmodel = list
            };

            var json = JsonConvert.SerializeObject(list1);

            textBox1.Text = json;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = JsonConvert.DeserializeObject<TestModel>(textBox1.Text.Trim());
        }

        private void button3_Click(object sender, EventArgs e)
        {

            var result = MyJsonConvert.MyProcess(MyJsonConvert.MyKeyValue(textBox1.Text.Trim()));
            TestModel1 res = new TestModel1();
            Stopwatch sw = new Stopwatch();

            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                MyJsonConvert.MyDtoO(res, result);
            }
            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed);


            Stopwatch sw1 = new Stopwatch();

            sw1.Start();
            for (int i = 0; i < 10000; i++)
            {
               var value = JsonConvert.DeserializeObject<TestModel1>(textBox1.Text.Trim());
            }
            sw1.Stop();

            Console.WriteLine("Elapsed1={0}", sw1.Elapsed);


         


        }
    }

    public class TestModel
    {
        public int Id { set; get; }
        public List<int> listint { set; get; }

        public List<string> liststr { set; get; }

        public TestModel3 model3 { set; get; }

    }
    public class TestModel1
    {
        public TestModel testmodel { set; get; }

        public List<TestModel> intlist { set; get; }
    }

    public class TestModel3
    {
        public int Id { set; get; }
        public List<int> listint { set; get; }

        public List<string> liststr { set; get; }
    }
}
