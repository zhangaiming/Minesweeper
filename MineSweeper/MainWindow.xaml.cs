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
using System.Text.RegularExpressions;

namespace MineSweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameWin gamewin = new GameWin();
            int num = 1, sizex = 1, sizey = 1;
            bool run = true;
            switch (DifficultyBox.SelectedIndex)
            {
                case 0: num = 15;sizey = 8; sizex = 12;break;
                case 1: num = 25;sizey = 10; sizex = 15;break;
                case 2: num = 50; sizey = 10; sizex = 15; break;
                case 3:
                    if (Regex.IsMatch(Text_Num.Text + Text_SizeX.Text + Text_SizeY.Text, @"^(\d{1,})$"))
                    {
                        num = Convert.ToInt32(Text_Num.Text);
                        sizex = Convert.ToInt32(Text_SizeX.Text);
                        sizey = Convert.ToInt32(Text_SizeY.Text);
                    }
                    else
                    {
                        MessageBox.Show("应该输入数字!");
                        run = false;
                    }
                    break;
            }
            if (run)
            {
                gamewin.init(num, sizex, sizey);
                gamewin.Show();
                this.Close();
            }
        }

        private void Box_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            if (box.SelectedIndex == 3)
            {
                this.Height = 140;
                this.Width = 205;
            }
            else
            {
                this.Height = 92;
                this.Width = 205;
            }
        }
    }
}
