using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWinForm.SudokuUtil
{
    class SudokuPattern
    {
        int _Dimension = 0;
        int[,] _Pattern; //2D array pattern
        SudokuMath.SudokuMathUtils mathutils = new SudokuMath.SudokuMathUtils();

        List<List<GroupLineSet>> rumblingHistory = new List<List<GroupLineSet>>();

        public SudokuPattern(int _dimension)
        {
            _Dimension = _dimension;
        }

        public int[,] generateNumberPattern()
        {
            _Pattern = setDefaultValuesTo2DArray(-1);
            int[] full_linenumset = generateRandomFullSet();
            int groupDim = mathutils.getGroupDim(_Dimension);

            rumblingHistory = new List<List<GroupLineSet>>();
            List<GroupLineSet> grouplinesets = getGroupedSets(full_linenumset);

            int rowPatternIndex = 0, colPatternIndex = 0;

            for (int gRows = 0; gRows < groupDim; gRows++) // main groupings row indeces
            {
                int[] setToGroupIndeces = getDefaultGroupingIndeces(groupDim);
                if(gRows > 0)
                {
                    grouplinesets = rumbleGroupSet(grouplinesets);
                    rumblingHistory.Add(grouplinesets);
                }
                else
                    rumblingHistory.Add(grouplinesets);

                for (int subgRows = 0; subgRows < groupDim; subgRows++)  //row indeces to mini groupings
                {

                    for(int aa = 0; aa < setToGroupIndeces.Length; aa ++)
                    {
                        GroupLineSet miniset = grouplinesets[setToGroupIndeces[aa]];
                        for (int ctr = 0; ctr < miniset.lineset.Length; ctr++)
                        {
                            _Pattern[rowPatternIndex, colPatternIndex] = miniset.lineset[ctr];
                            colPatternIndex += 1;
                        }

                    }

                    setToGroupIndeces = mathutils.pushArrayAndLastToFirst(setToGroupIndeces);
                    rowPatternIndex += 1;
                    colPatternIndex = 0;
                }
                
            }
            Console.WriteLine("Norep: " + new PatternChecker().patternHasNoRepitionsinGrid(_Pattern));
            
            return _Pattern;
        }


        private int[] getDefaultGroupingIndeces(int groupDim)
        {
            int[] defgs = new int[groupDim];
            for(int a = 0; a < groupDim; a++)
            {
                defgs[a] = a;
            }
            return defgs;
        }



        private List<GroupLineSet> rumbleGroupSet(List<GroupLineSet> Gset)
        {
            List<GroupLineSet> rumbledSet = new List<GroupLineSet>();

            int gcolindex = 0;
            foreach(GroupLineSet set in Gset)
            {
                int[] rumblearr = null;
                bool loop = true;
                while (loop)
                {
                    loop = false;
                    rumblearr = rumbleGroupMiniSet(set.lineset);
                    if(rumbledAlreadyInHistory(rumblearr)){
                        loop = true;
                    }
                    
                }

                rumbledSet.Add(new GroupLineSet { lineset = rumblearr });
                gcolindex += 1;
            }
            

            return rumbledSet;
        }

        private bool APositionIsAlreadySetted(int[] rumbled, int[] hist)
        {
            bool already = false;
            if(rumbled.Length == hist.Length)
            {
                for (int a = 0; a < rumbled.Length; a++)
                {
                    if(rumbled[a] == hist[a]) //position already done from past rumbles
                    {
                        already = true;
                        break;
                    }
                }
            }
            else
            {
                return true;
            }

            return already;
        }

        private bool rumbledAlreadyInHistory(int[] rumbled)  //groupcol must start in 0
        {
            bool alreadyinhist = false;

            foreach(List<GroupLineSet> gset in rumblingHistory)
            {
                foreach(GroupLineSet miniset in gset)
                {
                    int[] histset = miniset.lineset;
                    if (ArrayIsEquals(histset, rumbled) || APositionIsAlreadySetted(rumbled, histset))
                    {
                        alreadyinhist = true;
                    }
                }
                
            }


            return alreadyinhist;
        }

        private string ArrtoString(int[] arr)
        {
            string str = "[";
            foreach(int d in arr)
            {
                str += d + ",";
            }
            str += "]";
            return str;
        }

        private bool ArrayIsEquals(int[] arr1, int[] arr2)
        {
            bool equal = false;

            if(arr1.Length > arr2.Length)
            {
                return false;
            }
            else if (arr1.Length < arr2.Length)
            {
                return false;
            }
            else
            {
                int MaxSame = arr1.Length;
                int SamesFound = 0;
                for(int a = 0; a < MaxSame; a++)
                {
                    if(arr1[a] == arr2[a])
                    {
                        SamesFound += 1;
                    }
                }
                if(MaxSame == SamesFound)
                {
                    equal = true;
                }
            }

                    return equal;
        }



        private int[,] setDefaultValuesTo2DArray(int defvalue)
        {
            int[,] array = new int[_Dimension, _Dimension];
            for(int a = 0; a < _Dimension; a++)
            {
                for (int b = 0; b < _Dimension; b++)
                {
                    array[a, b] = defvalue;
                }
            }

            return array;
        }


        private int[] generateRandomFullSet()
        {
            int[] set = new int[_Dimension];

            for(int a = 0; a < _Dimension; a++)
            {
                int generatednum = -1;
                bool gen = true;
                while (gen)
                {
                    gen = false;
                    generatednum = mathutils.randomNumber(1, _Dimension);
                    for (int check = 0; check < a; check++)
                    {
                        if(set[check] == generatednum)
                        {
                            gen = true;
                            break;
                        }
                    }
                }
                set[a] = generatednum;
            }

            return set;
        }


        private int[] rumbleGroupMiniSet(int[] rawset)
        {
            int[] rumbledset = new int[rawset.Length];
            int[] randomedindeces = new int[rawset.Length];

            for (int a = 0; a < rawset.Length; a++)
            {
                int generatednum = -1;
                bool gen = true;
                while (gen)
                {
                    gen = false;
                    generatednum = mathutils.randomNumber(0, rawset.Length);
                    for (int check = 0; check < a; check++)
                    {
                        if (randomedindeces[check] == generatednum)
                        {
                            gen = true;
                            break;
                        }
                    }
                }
                randomedindeces[a] = generatednum;
            }

            int ctr = 0;
            foreach(int index in randomedindeces)
            {
                rumbledset[ctr] = rawset[index];
                ctr++;
            }

            return rumbledset;
        }


        private List<GroupLineSet> getGroupedSets(int[] randomfullset)
        {
            List<GroupLineSet> glineset = new List<GroupLineSet>();
            int groupdim = mathutils.getGroupDim(_Dimension);

            int index = 0;
            for(int a = 0; a < groupdim; a++) //group by group
            {
                int[] eachgset = new int[groupdim];
                for (int b = 0; b < groupdim; b++) //cell by each group
                {
                    eachgset[b] = randomfullset[index];
                    index += 1;
                }

                glineset.Add(new GroupLineSet { lineset = eachgset });
            }

            return glineset;
        }
        
       

    }

    class GroupLineSet
    {
        public int[] lineset = new int[1];
    }
}
