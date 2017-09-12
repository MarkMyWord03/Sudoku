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

        int _style = 0, _borderthickness = 0;
        Color _clr = Color.Black;

        public SudokuTextBox(string text="0")
        {
            BorderStyle = System.Windows.Forms.BorderStyle.None;
            AutoSize = false;
        }

        public SudokuTextBox()
        {
            BorderStyle = System.Windows.Forms.BorderStyle.None;
            AutoSize = false;
        }




        public void SetSudokuBorderStyle(int style = 5, int borderthickness = 1)
        {
            _style = style;
            _borderthickness = borderthickness;
            setborders(style, borderthickness, Color.Black);
            
            
        }

        public void SetBorderColor(Color clr)
        {
            setborders(_style, _borderthickness, clr);
        }


        protected override void OnHandleCreated(EventArgs e)
        {
            this.OnHandleCreated(e);
            if (_style == 1) updateMarginLeft();
            else if (_style == 2) updateMarginRight();
        }

        private void updateMarginLeft()
        { 
            SendMessage(this.Handle, 0xd3, (IntPtr)0, (IntPtr)(_borderthickness << 16));
            SendMessage(this.Handle, 0xd3, (IntPtr)_borderthickness, (IntPtr)(_borderthickness));
        }

        private void updateMarginRight()
        {
            SendMessage(this.Handle, 0xd3, (IntPtr)_borderthickness, (IntPtr)(_borderthickness << 16));
            SendMessage(this.Handle, 0xd3, (IntPtr)0, (IntPtr)(_borderthickness));
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);







        private void setborders(int style, int borderthickness, Color clr)
        {
            AutoSize = false;
            Controls.Clear();
            switch (style)
            {
                case 1:
                    {
                        Controls.Add(new Label()
                        {
                            Width = borderthickness,
                            Dock = DockStyle.Left,
                            BackColor = clr
                        });
                        break;
                    }
                case 2:
                    {
                        Controls.Add(new Label()
                        {
                            Width = borderthickness,
                            Dock = DockStyle.Right,
                            BackColor = clr
                        });
                        break;
                    }
                case 3:
                    {
                        Controls.Add(new Label()
                        {
                            Height = borderthickness,
                            Dock = DockStyle.Top,
                            BackColor = clr
                        });
                        break;
                    }

                case 4:
                    {
                        Controls.Add(new Label()
                        {
                            Height = borderthickness,
                            Dock = DockStyle.Bottom,
                            BackColor = clr
                        });
                        break;
                    }
                case 5:
                    {
                        Controls.Add(new Label()
                        {
                            Height = borderthickness,
                            Dock = DockStyle.Top,
                            BackColor = clr
                        });
                        Controls.Add(new Label()
                        {
                            Height = borderthickness,
                            Dock = DockStyle.Bottom,
                            BackColor = clr
                        }); Controls.Add(new Label()
                        {
                            Width = borderthickness,
                            Dock = DockStyle.Left,
                            BackColor = clr
                        }); Controls.Add(new Label()
                        {
                            Width = borderthickness,
                            Dock = DockStyle.Right,
                            BackColor = clr
                        });
                        break;
                    }
            }
        }
    }

    public class SudokuTBStyle
    {
        public static int BORDER_LEFT = 1;
        public static int BORDER_RIGHT = 2;
        public static int BORDER_TOP = 3;
        public static int BORDER_BOTTOM = 4;
        public static int BORDER_ALL = 5;
    }
}
