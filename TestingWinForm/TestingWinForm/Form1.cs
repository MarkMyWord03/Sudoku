using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestingWinForm.SudokuUtil;

namespace TestingWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // this.Paint += Form1_Paint;

            mypane.Paint += mypane_Paint;
        }

        private void mypane_Paint(object sender, PaintEventArgs e)
        {
            
        }

        //private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        //{
        //    var rectangle = e.CellBounds;
        //    rectangle.Inflate(-1, -1);

        //  //  ControlPaint.DrawBorder3D(e.Graphics, rectangle, Border3DStyle.Raised, Border3DSide.All); // 3D border
        //  //  ControlPaint.DrawBorder(e.Graphics, rectangle, Color.Red, ButtonBorderStyle.Dotted); // dotted border
        //    e.Graphics.DrawRectangle(new Pen(Color.Blue), e.CellBounds);
        //}



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SudokuBoard sdgen = new SudokuBoard(this.mypane, 29, 29);
            int[,] pattern = new SudokuPattern(Convert.ToInt32(comboBox1.Text)).generateNumberPattern();


            sdgen.generateBoard(Convert.ToInt32(comboBox1.Text));
            for (int row = 1; row <= Convert.ToInt32(comboBox1.Text); row++)
            {
                for (int col = 1; col <= Convert.ToInt32(comboBox1.Text); col++)
                {
                    TextBox tb = mypane.Controls.Find("tb{"+row+","+col+"}", true).FirstOrDefault() as TextBox;
                    tb.Text =( pattern[row-1, col-1]+"");
                }
            }
            //MessageBox.Show(tb.Text);
        }
    }

}
