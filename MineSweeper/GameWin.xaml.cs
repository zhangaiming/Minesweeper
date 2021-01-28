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
using System.Windows.Shapes;

namespace MineSweeper
{
    /// <summary>
    /// Interaction logic for GameWin.xaml
    /// </summary>
    public partial class GameWin : Window
    {
        int[,] dir = new int[8, 2] { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 } };
        int Map_SizeX, Map_SizeY;
        bool[,] vis;
        bool[,] map;
        bool[,] clickedMap;
        MineButton[,] buttonMap;
        bool Gaming = true;
        int[] mistakePos = new int[2];

        public GameWin()
        {
            InitializeComponent();
        }

        bool check(int x,int y)
        {
            if (y >= 0 && y < Map_SizeY && x >= 0 && x < Map_SizeX)
            {
                return true;
            }
            return false;
        }

        public void init(int mine_num, int map_sizex, int map_sizey)
        {
            mine_num = Math.Max(1, mine_num);
            mine_num = Math.Min(map_sizex * map_sizey - 1, mine_num);
            Map_SizeX = map_sizex;
            Map_SizeY = map_sizey;
            map = new bool[map_sizex, map_sizey];
            clickedMap = new bool[map_sizex, map_sizey];
            buttonMap = new MineButton[map_sizex, map_sizey];

            List<int[]> lp = new List<int[]>();
            
            for(int i = 0; i < Map_SizeY; i++)
            {
                RowDefinition r = new RowDefinition
                {
                    Height = new GridLength()
                };
                GameGrid.RowDefinitions.Add(r);
            }
            for(int i = 0; i < Map_SizeX; i++)
            {
                ColumnDefinition cd = new ColumnDefinition
                {
                    Width = new GridLength()
                };
                GameGrid.ColumnDefinitions.Add(cd);
            }

            for(int i = 0; i < Map_SizeX; i++)
            {
                for(int j = 0; j < Map_SizeY; j++)
                {
                    lp.Add(new int[2] { i, j });
                }
            }

            Random rd;
            rd = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < mine_num; i++)
            {
                
                int[] res = lp[rd.Next(0, lp.Count) * i % lp.Count];
                map[res[0], res[1]] = true;
                lp.Remove(new int[2] { res[0], res[1] });
            }

            for (int y = 0; y < Map_SizeY; y++)
            {
                for (int x = 0; x < Map_SizeX; x++)
                {
                    int tn = GetAdjacentMineNum(x, y);
                    CreateButton(tn, x, y);
                    //EnableButton(buttonMap[x, y]);
                }
            }
        }

        int GetAdjacentMineNum(int x, int y)
        {
            int res= 0;
            for (int i = 0; i < 8; i++)
            {
                if (check(x + dir[i, 0], y + dir[i, 1]))
                {
                    if (map[x + dir[i, 0], y + dir[i, 1]])
                        res++;
                }
            }
            return res;
        }

        int[] GetPos(int seed)
        {
            int[] res = new int[2];
            
            return res;
        }

        private void CreateButton(int num, int x, int y)
        {

            MineButton button = new MineButton(num,x,y);
            GameGrid.Children.Add(button);
            button.SetValue(Grid.RowProperty, y);
            button.SetValue(Grid.ColumnProperty, x);
            button.Num = num;
            buttonMap[x, y] = button;
        }

        private void EnableButton(MineButton mb)
        {
            mb.Content = mb.Num;
            int x = Grid.GetColumn(mb);
            int y = Grid.GetRow(mb);

            if (map[x, y])
            {
                mb.SetValue(ContentProperty, "X");
                mb.Background = Brushes.Red;
                return;
            }

            SolidColorBrush color;
            switch (mb.Num)
            {
                case 0: color = Brushes.Black; break;
                case 1: color = Brushes.Green; break;
                case 2: color = Brushes.YellowGreen; break;
                case 3: color = Brushes.Orange; break;
                default: color = Brushes.OrangeRed; break;
            }
            mb.Foreground = color;
            if (mb.Num == 0)
            {
                mb.Background = Brushes.DarkGray;
            }
            else
                mb.Background = Brushes.WhiteSmoke;
            clickedMap[x, y] = true;
            CheckGameState();
        }

        private void MineButton_Click(object sender, RoutedEventArgs e)
        {
            MineButton bt = (MineButton)sender;
            int x = Grid.GetColumn(bt);
            int y = Grid.GetRow(bt);
            if (clickedMap[x, y]) return;
            clickedMap[x, y] = true;
            if (map[x, y])
            {
                if (Gaming)
                {
                    bt.SetValue(ContentProperty, "X");
                    bt.Background = Brushes.Red;
                    MessageBox.Show("你输了!");
                    Gaming = false;
                    Button_Back.IsEnabled = true;
                    GameGrid.IsEnabled = false;
                    mistakePos = new int[] { x, y };
                }
            }
            else
            {
                EnableButton(bt);
                if (bt.Num == 0)
                {
                    Queue<int[]> q = new Queue<int[]>();
                    q.Enqueue(new int[] { x, y});
                    vis = new bool[Map_SizeX, Map_SizeY];
                    while (q.Count > 0)
                    {
                        int[] n = q.Dequeue();
                        EnableButton(buttonMap[n[0], n[1]]);                       
                        
                        //buttonMap[n[0], n[1]].IsEnabled = false;
                        if(buttonMap[n[0],n[1]].Num == 0)
                            for(int i = 0; i < 8; i++)
                            {
                                int nx = n[0] + dir[i, 0];
                                int ny = n[1] + dir[i, 1];
                                if(check(nx,ny) && !vis[nx, ny] && !clickedMap[nx,ny])
                                {
                                    q.Enqueue(new int[] { nx, ny});
                                    vis[nx, ny] = true;
                                }
                            }
                    }
                }
            }
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            Button_Back.IsEnabled = false;
            GameGrid.IsEnabled = true;
            clickedMap[mistakePos[0], mistakePos[1]] = false;
            buttonMap[mistakePos[0], mistakePos[1]].Background = Brushes.AntiqueWhite;
            buttonMap[mistakePos[0], mistakePos[1]].Content = " ";
            Gaming = true;
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Gaming = false;
            MainWindow w = new MainWindow();
            w.Show();
            this.Close();
        }

        private void CheckGameState()
        {
            bool win = true;
            for (int i = 0; i < Map_SizeX; i++)
            {
                for (int j = 0; j < Map_SizeY; j++)
                {
                    if (!clickedMap[i, j])
                    {
                        if (map[i, j])
                        {
                            continue;
                        }
                        if (GetAdjacentMineNum(i, j) != 0)
                        {
                            win = false;
                            break;
                        }
                    }
                }
            }
            if (win && Gaming)
            {
                MessageBox.Show("你赢了!");
                Gaming = false;
                MainWindow w = new MainWindow();
                w.Show();
                this.Close();
            }
        }
    }
}
