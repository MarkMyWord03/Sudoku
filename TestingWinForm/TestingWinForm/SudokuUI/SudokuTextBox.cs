using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestingWinForm.SudokuUI
{
    public class SudokuTextBox: TextBox
    {

        int _borderthickness = 0;
        SudokuBorderStyle borderstyle;
        Label Left_Border = new Label();
        Label Right_Border = new Label();
        Label Bottom_Border = new Label();
        Label Top_Border = new Label();

        Color _clr = Color.DarkGray;
        Color gridcolor = Color.DarkGray;
        string _cellstate = "";

        int Default_MagicBoxBorderThickness = 0;
        Font Default_Font;
        Color Default_TextColor;


        public SudokuTextBox(string text="0")
        {
            BorderStyle = System.Windows.Forms.BorderStyle.None;
            AutoSize = false;
            setborders(1, gridcolor);
            Default_Font = Font;
            Default_TextColor = ForeColor;
        }


        public SudokuTextBox()
        {
            BorderStyle = System.Windows.Forms.BorderStyle.None;
            AutoSize = false;
            setborders(1, gridcolor);
            Default_Font = Font;
            Default_TextColor = ForeColor;
        }

        public void SetDefaultColor(Color defcolor)
        {
            Default_TextColor = defcolor;
        }

        public void SetDefaultFont(Font font)
        {
            Default_Font = font;
        }


        public void SetSudokuBorderStyle(SudokuBorderStyle _borderstyle, Color DefaultGridColor, int borderthickness = 1)
        {
            borderstyle = _borderstyle;
            _borderthickness = borderthickness;
            gridcolor = DefaultGridColor;
            SetBorderContraints(_borderstyle, borderthickness, gridcolor);
        }

        void SetBorderContraints(SudokuBorderStyle _borderstyle, int borderthickness, Color clr)
        {
            Left_Border.BackColor = gridcolor;
            Right_Border.BackColor = gridcolor;
            Top_Border.BackColor = gridcolor;
            Bottom_Border.BackColor = gridcolor;

            if (_borderstyle.TOP_BORDERED)
            {
                Top_Border.BackColor = clr;
                Top_Border.Height = borderthickness;
            }

            if (_borderstyle.BOTTOM_BORDERED)
            {
                Bottom_Border.BackColor = clr;
                Bottom_Border.Height = borderthickness;
            }

            if (_borderstyle.LEFT_BORDERED)
            {
                Left_Border.BackColor = clr;
                Left_Border.Width = borderthickness;
            }

            if (_borderstyle.RIGHT_BORDERED)
            {
                Right_Border.BackColor = clr;
                Right_Border.Width = borderthickness;
            }
        }
        
        public void SetGridColor(Color _gridcolor)
        {
            gridcolor = _gridcolor;
            Left_Border.BackColor = gridcolor;
            Right_Border.BackColor = gridcolor;
            Top_Border.BackColor = gridcolor;
            Bottom_Border.BackColor = gridcolor;
        }



        public void ResetFontToDefault()
        {
            Font = Default_Font;
        }

        public void ResetForeColorToDefault()
        {
            ForeColor = Default_TextColor;
        }


        public void setTopBorderSize(int thickness)
        {
            
            Top_Border.Height = thickness;
        }
        public void setBottomBorderSize(int thickness)
        {
            Bottom_Border.Height = thickness;
        }
        public void setLeftBorderSize(int thickness)
        {
            Left_Border.Width = thickness;
        }
        public void setRightBorderSize(int thickness)
        {
            Right_Border.Width = thickness;
        }

        public void setTopBorderColor(Color clr)
        {
            Top_Border.BackColor = clr;
        }
        public void setBottomBorderColor(Color clr)
        {
            Bottom_Border.BackColor = clr;
        }
        public void setLeftBorderColor(Color clr)
        {
            Left_Border.BackColor = clr;
        }
        public void setRightBorderColor(Color clr)
        {
            Right_Border.BackColor = clr;
        }


        public void setCellMagicBoxGrid(string cellstate, int thickness)
        {
            Default_MagicBoxBorderThickness = thickness;
            _cellstate = cellstate;
            SetGridColor(gridcolor);

            if (cellstate.Contains("TOP"))
            {
                setTopBorderSize(thickness);
            }
                
            if (cellstate.Contains("RIGHT"))
            {
                setRightBorderSize(thickness);
            }
            if (cellstate.Contains("LEFT"))
            {
                setLeftBorderSize(thickness);
            }
            if (cellstate.Contains("BOTTOM"))
            {
                setBottomBorderSize(thickness);
            }
            if (cellstate.Contains("ALL"))
            {

                setTopBorderSize(thickness);
                setRightBorderSize(thickness);
                setLeftBorderSize(thickness);
                setBottomBorderSize(thickness);
            }
        }




        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            //if (borderstyle.LEFT_BORDERED && !borderstyle.RIGHT_BORDERED) updateMarginLeft();
            //else if (!borderstyle.LEFT_BORDERED && borderstyle.RIGHT_BORDERED) updateMarginRight();
            //else if (borderstyle.LEFT_BORDERED && borderstyle.RIGHT_BORDERED) updateMarginLeftRight();
            updateMarginLeftRight();
        }

        //private void updateMarginLeft()
        //{ 
        //    SendMessage(this.Handle, 0xd3, (IntPtr)0, (IntPtr)(_borderthickness << 16));
        //    SendMessage(this.Handle, 0xd3, (IntPtr)_borderthickness, (IntPtr)(_borderthickness));
        //}

        //private void updateMarginRight()
        //{
        //    SendMessage(this.Handle, 0xd3, (IntPtr)_borderthickness, (IntPtr)(_borderthickness << 16));
        //    SendMessage(this.Handle, 0xd3, (IntPtr)0, (IntPtr)(_borderthickness));
        //}

        private void updateMarginLeftRight()
        {
            SendMessage(this.Handle, 0xd3, (IntPtr)_borderthickness, (IntPtr)(_borderthickness << 16));
            SendMessage(this.Handle, 0xd3, (IntPtr)_borderthickness, (IntPtr)(_borderthickness));
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        


        private void setborders(int borderthickness, Color clr)
        {
            Left_Border.BackColor = clr;
            Right_Border.BackColor = clr;
            Top_Border.BackColor = clr;
            Bottom_Border.BackColor = clr;

            Left_Border.Width = borderthickness;
            Right_Border.Width = borderthickness;
            Top_Border.Height = borderthickness;
            Bottom_Border.Height = borderthickness;


            Top_Border.Dock = DockStyle.Top;
            Bottom_Border.Dock = DockStyle.Bottom;
             Left_Border.Dock = DockStyle.Left;
           // Right_Border.SetBounds(CurWidth - borderthickness, CurHeight - borderthickness, borderthickness, CurHeight - (borderthickness * 2));
            Right_Border.Dock = DockStyle.Right;
           // Left_Border.SetBounds(0, borderthickness, borderthickness, CurHeight - (borderthickness * 2));

            Controls.Add(Top_Border);
            Controls.Add(Bottom_Border);
            Controls.Add(Left_Border);
            Controls.Add(Right_Border);

           // Top_Border.SendToBack();
           // Bottom_Border.SendToBack();
        }
    }

    public class SudokuBorderStyle
    {
        public bool LEFT_BORDERED = false;
        public bool RIGHT_BORDERED = false;
        public bool TOP_BORDERED = false;
        public bool BOTTOM_BORDERED = false;

        public SudokuBorderStyle(bool LeftLined = false, bool RightLined = false, bool TopLined = false, bool BottomLined = false)
        {
            LEFT_BORDERED = LeftLined;
            RIGHT_BORDERED = RightLined;
            TOP_BORDERED = TopLined;
            BOTTOM_BORDERED = BottomLined;
        }
    }
}
