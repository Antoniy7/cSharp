using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream;
        const string fileName = "data.xml";
        private Random rnd = new Random();
       

        private bool Check()
        {
            if ((Math.Log10(Int64.Parse(txtNumber.Text))+1)!=16)
            {
                MessageBox.Show("Not enough digits in number");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNumber.Text) || string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Text)) ;
            {
                MessageBox.Show("Information not full");
                return false;
            }
            return true;
        }

        public Form1()
        {
           
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            clientSocket.Connect("127.0.0.1", 8888);
           // label1.Text = "Client Socket Program - Server Connected ...";

        }
        private bool Validation()
        {

            return true;
        }
 private void button1_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            button3.Visible = true;
            button1.Visible = false;
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Message from Client");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[1024];
            serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
          
            button1.Enabled=false;
        }
            private long tokken;

        private bool CheckOfSum(long sum)
        {
            if (sum % 10 == 0)
                return false;
            else
                return true;
        }

          protected void MakeTokken(string number)
        {
              Random rnd = new Random();
              long randomDigit = rnd.Next(0, 10);

              long sumOfDigits = 0;
              long scalar = 10;
              long temp = Int64.Parse(number);
              tokken = (temp % 10) *scalar; // first digit;
              sumOfDigits += (temp % 10);
              scalar *= 10;
              temp /= 10;
              
              tokken += (temp % 10) *scalar; // second digit;
              sumOfDigits += (temp % 10);
              scalar *= 10;
              temp /= 10;

              tokken += (temp % 10) * scalar; // third digit;
              sumOfDigits += (temp % 10);
              scalar *= 10;
              temp /= 10;

              tokken += (temp % 10) * scalar; // forth digit;
              scalar *= 10;
              sumOfDigits += (temp % 10);
              temp /= 10;

              for (int i=0;i<12;i++)
              {
                  if (i==11)
                  {
                      while (true)
                      {
                          randomDigit = rnd.Next(1, 10);
                          if (randomDigit != 3 && randomDigit != 4 && randomDigit != 5 && randomDigit != 6 && randomDigit!=temp)
                              break;
                      }    
                  }

                  randomDigit = rnd.Next(0, 10);
                  while (true)
                  {
                      if (randomDigit != temp && CheckOfSum(sumOfDigits) == true)
                          break;
                  }
              }
           }

        private void button3_Click(object sender, EventArgs e)
        {
            do
            {
                Check();
            } while (Check() != true);

            MakeTokken(txtNumber.ToString()); 

                File.WriteAllText(fileName, 
                   '<'+ "userName= "+ txtUserName.ToString()+
                   " Password= " + txtPassword.ToString() +
                   " Tokken= "+ tokken.ToString()+
                   "/>");
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var xml = XDocument.Load(fileName);

            var query = from c in xml.Root.Descendants("Tokken")
                        where (int)c.Attribute("Tokken") == Int64.Parse(txtNumber.Text)
                        select c.Element("userName").Value + " ";
                               //c.Element("Password").Value;

            foreach (string name in query)
            {
                richTextBox1.AppendText(String.Format("{0} Name : ", name));
            }
        }
            
       
    }
}

