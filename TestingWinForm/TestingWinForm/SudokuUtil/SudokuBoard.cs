﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestingWinForm.SudokuUtil
{
    public class SudokuBoard
    {
        int MaxTextLen = 0;
        Panel tbpanel2;
        int cellwidth = 0, cellheight = 0;
        int MainDimension = 0;
        SudokuMath.SudokuMathUtils mathutils = new SudokuMath.SudokuMathUtils();
        SudokuUtil.SudokuPattern patternutil = new SudokuUtil.SudokuPattern();
        PatternChecker patternchecker = new PatternChecker();

        public SudokuBoard(Panel _tbpanel, int _cellwidth, int _cellheight)
        {
            this.tbpanel2 = _tbpanel;
            cellwidth = _cellwidth;
            cellheight = _cellheight;
        }


        public void generateBoard(int gridrootcount)
        {
            MainDimension = gridrootcount;
            tbpanel2.Controls.Clear();

            MaxTextLen = (gridrootcount + "").Length;

            for (int a = 1; a <= gridrootcount; a++)
            {
                for (int b = 1; b <= gridrootcount; b++)
                {
                    SudokuUI.SudokuTextBox tb = createTextbox(a, b);
                    tbpanel2.Controls.Add(tb);
                    tb.Location = new System.Drawing.Point((b - 1) * cellwidth, (a - 1) * cellheight);
                    string cellstate = patternutil.getCellStateinMagicBox(a, b, gridrootcount);
                    tb.setCellMagicBoxGrid(cellstate,2);
                    tb.TextChanged += Tb_TextChanged;
                    if (a == 1) //top sides
                        tb.setTopBorderSize(4);
                    if (b == 1) //left sides
                        tb.setLeftBorderSize(4);
                    if (b == gridrootcount) //right sides
                        tb.setRightBorderSize(4);
                    if (a == gridrootcount) //bottom sides
                        tb.setBottomBorderSize(4);
                }
            }
        }
        

        private void Tb_TextChanged(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse((sender as TextBox).Text, out parsedValue))
            {
                (sender as TextBox).Text = "";
            }
            else
            {
                if(parsedValue > MainDimension)
                    (sender as TextBox).Text = MainDimension+"";
                else if(parsedValue < 1)
                    (sender as TextBox).Text = "";

            }
        }

        void SetRowTableStyles(TableLayoutPanel _tbpanel, int height)
        {

            for (int ctr = 0; ctr < _tbpanel.RowCount; ctr++)
            {
                _tbpanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / MainDimension));
            }
        }

        void SetColTableStyles(TableLayoutPanel _tbpanel, int width)
        {
            for (int ctr = 0; ctr < _tbpanel.RowCount; ctr++)
            {
                _tbpanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100/ MainDimension));

            }
        }

        SudokuUI.SudokuTextBox createTextbox(int row, int col)
        {
            SudokuUI.SudokuTextBox tb = new SudokuUI.SudokuTextBox();
            tb.Text = row + "" + col;
            tb.Name = ("tb{" + row + "," + col + "}");
            tb.Multiline = false;
            tb.TextAlign = HorizontalAlignment.Center;
            tb.MaxLength = MaxTextLen;tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb.Width = cellwidth;
            tb.Height = cellheight;
            tb.Font = new System.Drawing.Font("Century Gothic", 10F,
                    System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tb.SetDefaultFont(new System.Drawing.Font("Century Gothic", 10F,
                    System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            tb.Margin = new Padding(0);

            tb.SetSudokuBorderStyle(new SudokuUI.SudokuBorderStyle(false, false, false, false),System.Drawing.Color.BurlyWood, 1);
            
            tb.TabIndex = 0;

            return tb;
        }
    }
}
