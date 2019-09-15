using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
//using Game.ServiceReference1;
//using Game.ServiceReference3;

namespace Game
{
    /// <summary>
    /// Interaction logic for GameUserControl.xaml
    /// </summary>
    public partial class GameUserControl : UserControl
    {
        //Service211Client client = new Service211Client();
        //ServiceReference3.Service211Client client = new Service211Client();



        private bool playerOne;
        private List<int> p1;
        private List<int> p2;
        private List<int> allData;
        private List<List<int>> victoryData;
        private bool flagFirstTimeSecondPlayer;
        public GameUserControl()
        {
            InitializeComponent();
            PopulateComboBox();
            PopulateData();
            lblPlayerTwo.Content = "";
            spAllNumbers.IsEnabled = false;
            p1 = new List<int>();
            p2 = new List<int>();
            flagFirstTimeSecondPlayer = false;
            allData = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            victoryData = new List<List<int>>{ new List<int> { 1, 2, 3 ,4, 5},
                new List<int> { 4, 5, 6 }, new List<int> { 1, 5,9 },
                new List<int> { 3, 5, 7 }, new List<int> { 2, 5, 8 },
                new List<int> { 1, 3, 5,6 },new List<int> { 1, 2, 5,7 },
                new List<int> { 1, 2, 4,8 },new List<int> { 1, 3, 4,7 },
                new List<int> { 6,9 },new List<int> { 7,8},
                new List<int> { 2,3,4,6}};
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string data = cbNumberOfElements.SelectedItem.ToString();

            txtSumToWin.Text = GenerateSumToWin
                (cbNumberOfElements.SelectedItem.ToString()).ToString();
        }

        private void PopulateComboBox()
        {
            for (int i = 10; i <= 30; ++i)
            {
                cbNumberOfElements.Items.Add(i);
            }
        }
        int GenerateSumToWin(string val)
        {
            int sum = 0;
            int value = int.Parse(val);
            for (int i = 1; i < value; ++i) { sum += i; }


            return ((sum / value + 1) * 3);
        }
        void PopulateData()
        {
            string data = cbNumberOfElements.SelectedItem.ToString();
            int length = int.Parse(data);
            for (int i = 1; i < length; i++)
            {
                System.Windows.Controls.Button newBtn = new Button();

                newBtn.Content = i.ToString();
                newBtn.Name = "Button" + i.ToString();
                newBtn.Click += (s, e) => ButtonEvent(newBtn.Name);
                //newBtn.Click += (s, e) => print();


                spAllNumbers.Children.Add(newBtn);
            }
        }
        public void ButtonEvent(string s)
        {
            int val = s[6] - '0';
            //player1 logic 
            if (playerOne)
            {

                MakeEmptyButton(val, PlayerOneNumbers);
                p1.Add(val);
                RemoveNumber(val);
                //playerOne = false;
                if (CheckWinner(p1))
                {
                    lblPlayerOne.Content = "WInner";
                }


                int elem = Position();
                if (elem < 0) return;
                p2.Add(elem);
                RemoveNumber(elem);

                MakeEmptyButton(elem, PlayerTwoNumbers);

                if (CheckWinner(p2))
                {
                    lblPlayerTwo.Content = "WInner";
                }
            }
            else
            {

                int elem = Position();
                if (elem < 0) return;
                p1.Add(elem);
                RemoveNumber(elem);

                MakeEmptyButton(elem, PlayerOneNumbers);


                if (CheckWinner(p2))
                {
                    lblPlayerTwo.Content = "WInner";
                }

                MakeEmptyButton(val, PlayerTwoNumbers);
                p2.Add(val);
                RemoveNumber(val);

                if (CheckWinner(p1))
                {
                    lblPlayerOne.Content = "WInner";
                }

                //playerOne = true;
            }
        }
        void MakeEmptyButton(int val, StackPanel sp)
        {
            System.Windows.Controls.Button newBtn = new Button();
            newBtn.Content = val.ToString();
            newBtn.Name = "Button" + val.ToString();
            sp.Children.Add(newBtn);
        }

        void PopulateData2()
        {
            string data = cbNumberOfElements.SelectedItem.ToString();
            int length = int.Parse(data);
            for (int i = 1; i < length; i++)
            {
                System.Windows.Controls.Button newBtn = new Button();

                newBtn.Content = i.ToString();
                newBtn.Name = "Button" + i.ToString();

                spAllNumbers.Children.Add(newBtn);
            }


            UniformGrid g = new UniformGrid() { Rows = 4, Columns = 4 };
            g.Children.Add(new Button());
        }

        void RemoveNumber(int num)
        {
            int poz = allData.IndexOf(num);
            allData.Remove(num);
            spAllNumbers.Children.RemoveAt(poz);
        }
        void GameOver()
        {
            spAllNumbers.IsEnabled = false;
        }

        private void btnPlayer2_Click(object sender, RoutedEventArgs e)
        {
            lblPlayerTwo.Content = "Current";
            lblPlayerOne.Content = "";
            ButtonsDisable();
            spAllNumbers.IsEnabled = true;
        }

        private void btnPlayer1_Click(object sender, RoutedEventArgs e)
        {
            playerOne = true;
            lblPlayerOne.Content = "Current";
            ButtonsDisable();
            spAllNumbers.IsEnabled = true;

        }

        bool CheckWinner(List<int> list)
        {
            list.Sort();
            foreach (List<int> data in victoryData)
            {
                if (data.SequenceEqual(list))
                {
                    GameOver();
                    return true;
                }
            }
            return false;
        }
        int Position()
        {
            int add = 0;
            if (allData.Count == 1)
            { add = 0; }
            if (!flagFirstTimeSecondPlayer) add = 0;
            //return allData.ElementAt(((allData.Count+add) / 2 ));

            int val = 0;
            for (int i = 0; i < allData.Count; ++i)
            {
                val += allData.ElementAt(i);
            }

            if (allData.Count == 0) return -1;
            else if (allData.Count / 2 == 0 && allData.Count == 1) return allData.ElementAt(1);
            else if (allData.Count / 2 != 0)
                return allData.ElementAt(allData.Count / 2);
            else return allData.ElementAt((allData.Count + 1) / 2);
        }

        void ButtonsDisable()
        {
            btnPlayer1.IsEnabled = false;
            btnPlayer2.IsEnabled = false;

        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
