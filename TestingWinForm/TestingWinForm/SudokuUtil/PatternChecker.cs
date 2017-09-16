using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestingWinForm.SudokuUtil
{
    class PatternChecker
    {
        SudokuMath.SudokuMathUtils mathutils = new SudokuMath.SudokuMathUtils();
        Panel SudokuboardPanel = new Panel();

        public bool checkoutSudokuBoardForErrors(Panel _panel, int dimension)
        {
            bool Correct = false;
            SudokuboardPanel = _panel;
            checkoutsboardInit(dimension);
            int groupdim = mathutils.getGroupDim(dimension);
            Task<bool> task1 = Task<bool>.Factory.StartNew(() =>
            {
                try
                {
                    checkoutMagicBox(_panel, dimension, Correct, groupdim);
                    return Correct;
                }
                catch (Exception e)
                {
                    return false;
                }
            });

            Task<bool> task2 = Task<bool>.Factory.StartNew(() =>
            {
                try
                {
                    checkoutCellsHorizontallyFunction(_panel, dimension, Correct, groupdim);
                    return Correct;
                }
                catch (Exception e)
                {
                    return false;
                }
            });

            Task<bool> task3 = Task<bool>.Factory.StartNew(() =>
            {
                try
                {
                    checkoutCellsVerticallyFunction(_panel, dimension, Correct, groupdim);
                    return Correct;
                }
                catch (Exception e)
                {
                    return false;
                }
            });

            Task<bool> task4 = Task<bool>.Factory.StartNew(() =>
            {
                try
                {
                    checkoutBlankCellsFunction(_panel, dimension, Correct, groupdim);
                    return Correct;
                }
                catch (Exception e)
                {
                    return false;
                }
            });


            return Correct;
        }


        private void checkoutsboardInit(int dimension)
        {
            ResetFontOfAllTextBox(dimension);
            ResetForeColorOfAllTextBox(dimension);
        }


        #region Magic Box Checking Function
        public void checkoutMagicBox(Panel _panel, int dimension, bool Corrector, int groupdim)
        {
            
            for (int rowspergroup = 1; rowspergroup <= dimension; rowspergroup += groupdim)
            {
                for (int colspergroup = 1; colspergroup <= dimension; colspergroup += groupdim)
                {
                    //get cell per magic box group
                    List<SudokuUI.SudokuTextBox> tbspergroup = new List<SudokuUI.SudokuTextBox>();
                    for (int row = rowspergroup; row < (rowspergroup + groupdim); row++)
                    {
                        for (int col = colspergroup; col < (colspergroup + groupdim); col++)
                        {
                            tbspergroup.Add(FindSudokuTextBox("tb{" + row + "," + col + "}", _panel));
                        }
                    }

                    Corrector = CheckUniquenessInThisTBList(tbspergroup, Corrector);
                }
            }
        }
        #endregion

        #region Check Cells Vertically
        public void checkoutCellsVerticallyFunction(Panel _panel, int dimension, bool Corrector, int groupdim)
        {
            //check errors vertically
            for (int col = 1; col <= dimension; col++)
            {
                List<SudokuUI.SudokuTextBox> tbcells = new List<SudokuUI.SudokuTextBox>();
                for (int row = 1; row <= dimension; row++)
                {
                    tbcells.Add(FindSudokuTextBox("tb{" + row + "," + col + "}", _panel));
                }
                Corrector = CheckUniquenessInThisTBList(tbcells, Corrector);
            }
        }
        #endregion

        #region Check Cells Horizontally Function
        public void checkoutCellsHorizontallyFunction(Panel _panel, int dimension, bool Corrector, int groupdim)
        {
            //check errors horizontally
            for (int row = 1; row <= dimension; row++)
            {
                List<SudokuUI.SudokuTextBox> tbcells = new List<SudokuUI.SudokuTextBox>();
                for (int col = 1; col <= dimension; col++)
                {
                    tbcells.Add(FindSudokuTextBox("tb{" + row + "," + col + "}", _panel));
                }
                Corrector = CheckUniquenessInThisTBList(tbcells, Corrector);
            }
        }
        #endregion

        #region Check for Blanks Function
        public void checkoutBlankCellsFunction(Panel _panel, int dimension, bool Corrector, int groupdim)
        {
            //check for blanks
            for (int col = 1; col <= dimension; col++)
            {
                bool stop = false;
                for (int row = 1; row <= dimension; row++)
                {
                    SudokuUI.SudokuTextBox tb = FindSudokuTextBox("tb{" + row + "," + col + "}", _panel);
                    if (tb.Text.Trim() == "")
                    {
                        Corrector = false;
                        stop = true;
                        break;
                    }
                }
                if (stop)
                    break;
            }
        }
        #endregion



        public void PrintPattern(int[,] pattern)
        {
            string str = "[";
            for(int a = 0; a < pattern.GetLength(0); a++)
            {
                for (int b = 0; b < pattern.GetLength(1); b++)
                {
                    str += pattern[a, b] + ",";
                }
                str += "\n";
            }
            Console.WriteLine(str);
        }


        bool CheckUniquenessInThisTBList(List<SudokuUI.SudokuTextBox> tbspergroup, bool Correct)
        {
            for (int a = 0; a < tbspergroup.Count; a++)
            {
                for (int b = 0; b < tbspergroup.Count; b++)
                {
                    string tblistval = tbspergroup[a].Text.Trim();
                    string tbcurval = tbspergroup[b].Text.Trim();
                    if (tblistval == tbcurval && a != b && tblistval != "" && tbcurval != "") //matches a cell in the same group
                    {
                        tbspergroup[a].ForeColor = System.Drawing.Color.Red;
                        tbspergroup[b].ForeColor = System.Drawing.Color.Red;
                        tbspergroup[a].Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        tbspergroup[b].Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        Correct = false;
                    }
                }
            }
            return Correct;
        }

        void ResetFontOfAllTextBox(int dimension)
        {
            for(int a = 1; a <= dimension; a++)
            {
                for (int b = 1; b <= dimension; b++)
                    FindSudokuTextBox("tb{" + a + "," + b + "}", SudokuboardPanel).ResetFontToDefault();
            }
        }

        void ResetForeColorOfAllTextBox(int dimension)
        {
            for (int a = 1; a <= dimension; a++)
            {
                for (int b = 1; b <= dimension; b++)
                    FindSudokuTextBox("tb{" + a + "," + b + "}", SudokuboardPanel).ResetForeColorToDefault();
            }
        }


        SudokuUI.SudokuTextBox FindSudokuTextBox(string tbname, Panel _panel)
        {
            SudokuUI.SudokuTextBox tbfind = _panel.Controls.Find(tbname, true).FirstOrDefault() as SudokuUI.SudokuTextBox;
            return tbfind;
        }

        CellIndexPairs getRowAndColumnFromName(string tbname)
        {
            string tbstr = tbname;
            CellIndexPairs pair = new CellIndexPairs();  
            tbstr = tbstr.Trim().Replace("tb{", "").Replace("}", "");
            int.TryParse(tbstr.Substring(0, tbstr.IndexOf(",")), out pair.Row);
            int.TryParse(tbstr.Substring(tbstr.IndexOf(",")+1), out pair.Column);

            return pair;
        }


        public bool patternHasNoRepitionsinGroups(int[,] pattern)
        {
            bool norep = true;
            int groupdim = mathutils.getGroupDim(pattern.GetLength(0));

            int IndexRow = 0, IndexCol = 0;

            for(int row = IndexRow; row < groupdim + IndexRow; row ++)
            {
                for (int col = IndexCol; col < groupdim + IndexCol; col ++)
                {

                    IndexCol += 1;
                }
                IndexRow += 1;
            }

            return norep;
        }

       

        public bool patternHasNoRepitionsinGrid(int[,] Pattern)
        {
            bool norep = true;


            //check horizontally
            for(int row = 0; row < Pattern.GetLength(0); row++)
            {
                bool stop1 = false;
                for (int col = 0; col < Pattern.GetLength(1); col++)
                {
                    bool stop2 = false;
                    int cellval = Pattern[row, col];
                    for(int a = 0; a < Pattern.GetLength(1); a++)
                    {
                        if(cellval == Pattern[row, a] && col != a)
                        {
                            Console.WriteLine("Horizontal: [{0},{1}] and [{2},{3}]", row, col, row, a);
                            norep = false;
                            stop1 = true;
                            stop2 = true;
                            break;
                        }
                    }
                    if (stop2)
                    {
                        break;
                    }
                }
                if (stop1)
                {
                    break;
                }
            }


            //check vertically
            for (int row = 0; row < Pattern.GetLength(0); row++)
            {
                bool stop1 = false;
                for (int col = 0; col < Pattern.GetLength(1); col++)
                {
                    bool stop2 = false;
                    int cellval = Pattern[col, row];
                    for (int a = 0; a < Pattern.GetLength(1); a++)
                    {
                        if (cellval == Pattern[col, a] && row != a)
                        {

                            Console.WriteLine("Vertical: [{0},{1}] and [{2},{3}]", row, col, col, a);
                            norep = false;
                            stop1 = true;
                            stop2 = true;
                            break;
                        }
                    }
                    if (stop2)
                    {
                        break;
                    }
                }
                if (stop1)
                {
                    break;
                }
            }



            return norep;
        }
    }
}
