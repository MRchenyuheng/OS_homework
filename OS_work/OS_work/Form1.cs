using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.Threading;

namespace OS_work
{
    
    public partial class Form1 : Form
    {
        struct mydata
        {
            public int id;
            public int times;


            public mydata(int x, int y)
            {
                id = x;
                times= y;
            }

        }

        bool[] idst = new bool[10020];

        public DataTable tb1;
        public DataTable tb2;
        public DataTable tb3;

        public int cnt1 = 0;
        public int cnt2 = 0;
        public int cnt3 = 0;

        LinkedList<mydata> que1 = new LinkedList<mydata>();
        LinkedList<mydata> que2 = new LinkedList<mydata>();
        LinkedList<mydata> que3 = new LinkedList<mydata>();

       private  bool f = true;
        public Form1()
        {
            
            InitializeComponent();
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter; // 调整对齐方式，可以根据实际需要修改
            dataGridView1.DefaultCellStyle = style;

            // 如果表头需要居中对齐，也可以添加以下代码
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void add_data(int id,int time,int op)
        {
            if (op == 1)
            {

                DataRow newRow = tb1.NewRow();
                newRow["ID"] = id;
                newRow["TIME_LEFT"] = time;
                tb1.Rows.Add(newRow);
            }

            if (op == 2)
            {

                DataRow newRow = tb2.NewRow();
                newRow["ID"] = id;
                newRow["TIME_LEFT"] = time;
                tb2.Rows.Add(newRow);
            }

            if (op == 3)
            {

                DataRow newRow = tb3.NewRow();
                newRow["ID"] = id;
                newRow["TIME_LEFT"] = time;
                tb3.Rows.Add(newRow);
            }


        }

        public int get_num(int l,int r)
        {
            Random random = new Random();
            
            int res = random.Next(l,r + 1);
            return res;

        }

        

        public void show()
        {
      

            tb1 = new DataTable("level 1");
            tb1.Columns.Add("ID",typeof(int));
            tb1.Columns.Add("TIME_LEFT", typeof(int));

            tb2 = new DataTable("level 2");
            tb2.Columns.Add("ID", typeof(int));
            tb2.Columns.Add("TIME_LEFT", typeof(int));

            tb3 = new DataTable("level 3");
            tb3.Columns.Add("ID", typeof(int));
            tb3.Columns.Add("TIME_LEFT", typeof(int));


            foreach (mydata t in que1)
            {
                add_data(t.id, t.times, 1);    
            }

            foreach (mydata t in que2)
            {
                add_data(t.id, t.times, 2);
            }

            foreach (mydata t in que3)
            {
                add_data(t.id, t.times, 3);
            }



            dataGridView1.DataSource = tb1;
            dataGridView2.DataSource = tb2;
            dataGridView3.DataSource = tb3;
        }

        public void add()
        {
          //f = false;
            

            int newid = get_num(1, 100000);
            newid %= 10007;
            if (idst[newid] == false)
            {
                int newtime = get_num(1, 100);
                newtime %= 20;

                newtime += 1;

                mydata t = new mydata();
                t.id = newid;
                t.times = newtime;
                que1.AddLast(t);

            }

            show();

        }

        public void process()
        {
            mydata tmp;

            if (que1.Count() > 0)
            {
                tmp = que1.First();
                que1.RemoveFirst();
                tmp.times--;
                if(tmp.times > 0)
                {
                    que2.AddLast(tmp);
                }
                else idst[tmp.id] = false;

            }
            else if(que2.Count > 0)
            {
                tmp = que2.First();
                que2.RemoveFirst();
                cnt2++;

                tmp.times--;

                if(tmp.times == 0)
                {
                    idst[tmp.id] = false;
                    cnt2 = 0;

                }
                else
                {
                    if (cnt2 == 2)
                    {
                        cnt2 = 0;
                        que3.AddLast(tmp);

                    }
                    else que2.AddFirst(tmp);
                }

            }
            else if(que3.Count > 0)
            {
                tmp = que3.First();
                que3.RemoveFirst();
                cnt3++;

                tmp.times--;

                if (tmp.times == 0)
                {
                    idst[tmp.id] = false;
                    cnt3 = 0;

                }
                else
                {
                    if (cnt3 == 4)
                    {
                        cnt3 = 0;
                        que3.AddLast(tmp);

                    }
                    else que3.AddFirst(tmp);
                }

            }

            show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            f = false;
            add();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f = false;
        }

   

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10010; i++) idst[i] = false;
            f = true;
            int cnt = 0;

            while (f)
            {
                cnt++;
                cnt %= 6;
                if (cnt == 0)
                {
                    add();
                    Thread.Sleep(1000);
                    Application.DoEvents();
                }
              //  add();

                process();
                Thread.Sleep(1000);
               Application.DoEvents();

            }

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
