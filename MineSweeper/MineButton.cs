using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MineSweeper
{
    class MineButton : Button
    {
        public bool IsClicked { 
            get { return (bool)GetValue(IsClickedProperty); } 
            set { SetValue(IsClickedProperty, value); } 
        }
        private int num = 1;
        public int Num
        {
            get { return num; }
            set { num = value; }
        }
        public static readonly DependencyProperty IsClickedProperty=DependencyProperty.Register("IsClicked",typeof(bool),typeof(MineButton));

        public MineButton(int _num, int px, int py)
        {
            Num = _num;
        }


    }
}
