using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int seconds;
        DateTime dt = new DateTime();

        string[] questionLines;
        string[] answerLines;
        bool enterFourthButton = false;
        int size;
        int answersClicked = 0;
        int correctAnswers;
        Random rnd = new Random();
        string answers = @"..\..\..\Answers.txt";
        string questions = @"..\..\..\Questions.txt";
        int numberOfline;
        private void button1_Click(object sender, EventArgs e)
        {
            
            textBox2.Text = textBox3.Text = string.Empty;
            EnableButtons();
            timer1.Start();
            seconds = 1200;
            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = true;
            label6.Text = "Успех ! ";
            answersClicked = 0;
            correctAnswers = 0;
            questionLines = File.ReadAllLines(questions, Encoding.GetEncoding("windows-1251"));
            answerLines = File.ReadAllLines(answers, Encoding.GetEncoding("windows-1251"));
            size = questionLines.Length;

            int random = rnd.Next(0, (size-1));
            string temp =questionLines[random];
            numberOfline = Convert.ToInt32(char.GetNumericValue(temp[0]));
            temp = temp.Substring(1, temp.Length-1);
            richTextBox1.Text = temp;
            loadAnswers();
            randomNumber(random);
            button1.Enabled = false;
            button4.Enabled = false;
                       
        }

       

        void randomNumber(int random)
        {
            if (random!=size)
             questionLines[random] = questionLines[size-1];
           
            size= size - 1;
        }
        void loadAnswers()
        {
            int value = numberOfline * 5;
                radioButton1.Text = answerLines[value];
                radioButton2.Text = answerLines[value + 1];
                radioButton3.Text = answerLines[value + 2];
                radioButton4.Text = answerLines[value + 3];
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;
                   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(enterFourthButton==false)
              button2.Enabled = true;

            label6.Text = "Успех";
            answersClicked++;
            UncheckButtons();
            button4.Enabled = false;
            int random = rnd.Next(0, (size -1));
            string temp = questionLines[random];
            numberOfline = Convert.ToInt32(char.GetNumericValue(temp[0]));
            temp = temp.Substring(1, temp.Length-1);
            loadAnswers();
            richTextBox1.Text = temp;
            
            randomNumber(random);
      

            button3.Enabled = false;
            if (size == 0)
                button3.Enabled = false;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            enterFourthButton = true;   
            if(numberOfline==0 || numberOfline==4)
            {
                radioButton2.Enabled = false;
                radioButton4.Enabled = false;
            }
            if (numberOfline == 1)
            {
                radioButton1.Enabled = false;
                radioButton3.Enabled = false;
            }
            if (numberOfline == 2)
            {
                radioButton2.Enabled = false;
                radioButton4.Enabled = false;
            }
            if (numberOfline == 3)
            {
                radioButton1.Enabled = false;
                radioButton3.Enabled = false;
            }
            button2.Enabled = false;
            
        }
        void UncheckButtons()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }
        void EnableButtons()
        {
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton3.Enabled = true;
            radioButton4.Enabled = true;
            
        }
        private void button4_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            EnableButtons();
            int size = questionLines.Length-1;

            button3.Enabled = true;
            bool hasEnteredIfs = false;
            if (radioButton1.Checked == true && ((numberOfline == 0)))
            {
                label6.Text = "Правилно :) !";
                hasEnteredIfs = true;
                correctAnswers++;
            }
            if (radioButton2.Checked == true && numberOfline == 1)
            {
                label6.Text = "Правилно :) !";
                hasEnteredIfs = true;
                correctAnswers++;
            }
            if (radioButton3.Checked == true && numberOfline == 2)
            {
                label6.Text = "Правилно :) !";
                hasEnteredIfs = true;
                correctAnswers++;
            }
            if (radioButton4.Checked == true && numberOfline == 3)
            {
                label6.Text = "Правилно :) !";
                hasEnteredIfs = true;
                correctAnswers++; 
            }
            if (radioButton1.Checked == true && numberOfline == 4)
            {
                label6.Text = "Правилно :) !";
                hasEnteredIfs = true;
                correctAnswers++;
            }
            if (hasEnteredIfs == false)
                label6.Text = "Неее ...";

            button4.Enabled = false;
            textBox2.Text = correctAnswers.ToString() + '/' + questionLines.Length.ToString();

            if (answersClicked == size)
            {
                label6.Text = "Край :)";
                button3.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = false;
                timer1.Stop();

                double  maxTotalSize = questionLines.Length;
                double  mark = ((correctAnswers* 4) / maxTotalSize )  +2;
                string print = mark.ToString("N2");
                textBox3.Text = print;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds--;
             textBox1.Text = dt.AddSeconds(seconds).ToString("mm:ss");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button4.Enabled = true;
        }

    }
}

