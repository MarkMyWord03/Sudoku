using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWinForm.SudokuMath
{
    class SudokuMathUtils
    {
        Random random = new Random();


        //public int getCascadedGroupColIndexByGroupRowAndCol(int MaxDim, int grouprow, int groupcol)
        //{
        //    int groupdim = getGroupDim(MaxDim);

        //    if( row%(groupdim-1) == 0)
        //    {
        //        return col;
        //    }
        //    else
        //    {
        //        int 
        //    }
            
        //}

        public int[] pushArrayAndLastToFirst(int[] arr)
        {
            int[] pushedarr = new int[arr.Length];

            pushedarr[0] = arr[arr.Length - 1];
            for(int a = 0; a < arr.Length - 1; a++)
            {
                pushedarr[a + 1] = arr[a];
            }

            return pushedarr;
        }


        public int randomNumber(int min, int Max)
        {
            int number = -1;
            number = ((random.Next(min, Max) + random.Next(min, Max)) % Max) + min;

            return number;
        }

        //public int getFirstGroupRowIndex(int groupNum, int _Dimension)
        //{
        //    int index = -1;
        //    int groupings = this.getGroupDim(_Dimension);


        //    return index;
        //}


        public int getStartingGroupRowIndex(int posrow, int _groupings)
        {
            if(posrow < _groupings)
            {
                return 0;
            }
            else
            {
                return (posrow % _groupings) == 0 ? posrow - _groupings : posrow - (posrow % _groupings);
            }
        }

        public int getStartingGroupColIndex(int poscol, int _groupings)
        {
            if (poscol < _groupings)
            {
                return 0;
            }
            else
            {
                return (poscol % _groupings) == 0 ? poscol - _groupings : poscol - (poscol % _groupings);
            }
        }


        public int getGroupDim(int Num)
        {
            int Dimn = 0;
            Dimn = Convert.ToInt32(Math.Sqrt(Num));
            return Dimn;
        }


    }
}
