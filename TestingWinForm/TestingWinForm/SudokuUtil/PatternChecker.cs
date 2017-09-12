using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWinForm.SudokuUtil
{
    class PatternChecker
    {
        SudokuMath.SudokuMathUtils mathutils = new SudokuMath.SudokuMathUtils();

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
