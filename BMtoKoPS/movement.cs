using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMtoKOPS
{
    public class Movement
    {
        /// <summary>
        /// Round -> Record -> [NS pair #, EW pair #, (table), (round)]
        /// </summary>
        private List<List<List<int>>> movement;
        private int tournamentBase;
        private int mitchellSections;

        public Movement(int tourBase, int mitchells)
        {
            movement = new List<List<List<int>>>();
            tournamentBase = tourBase;
            mitchellSections = mitchells;
        }

        public int Rounds()
        {
            return movement.Count;
        }

        public int Deals(int round)
        {
            return movement[round].Count;
        }

        public int GetNS(int round, int deal)
        {
            return movement[round][deal][0];
        }

        public int GetEW(int round, int deal)
        {
            return movement[round][deal][1];
        }

        public void Generate(int rounds, int dealingRound, int roundUntilLineChanged, List<string> data, int pairs, int appendix)
        {
            for (int j = 0; j < mitchellSections; j++)
            {
                GenerateMitchellMovement(j, rounds, dealingRound, roundUntilLineChanged);
            }

            if (appendix > 0)
            {
                ReadAppendixMovement(data, pairs, appendix);
            }
        }

        public void GenerateMitchellMovement(int section, int rounds, int dealingRound, int roundUntilLineChanged)
        {
            int skippedPairs = tournamentBase * section * 2;

            for (int i = 0; i < tournamentBase; i++)
            {
                List<List<int>> numbers = new List<List<int>>();

                for (int j = 0; j < rounds + dealingRound; j++)
                {
                    int ns = (tournamentBase - j + i) % tournamentBase; 
                    int ew = (tournamentBase * 2 - j * 2 + i) % tournamentBase;

                    //Jump
                    if (tournamentBase % 2 == 0 && j > tournamentBase / 2 - 1)
                    {
                        ew = (tournamentBase * 2 - j * 2 + i - 1) % tournamentBase;
                    }

                    List<int> deal = new List<int>();

                    //if line changed
                    if (j < roundUntilLineChanged + dealingRound) 
                    {
                        deal.Add(ns + skippedPairs + 1); //pair NS
                        deal.Add(ew + tournamentBase + skippedPairs + 1); //pair EW
                    }
                    else
                    {
                        deal.Add(ew + tournamentBase + skippedPairs + 1); //pair NS
                        deal.Add(ns + skippedPairs + 1); //pair EW
                    }
                    deal.Add(ns + tournamentBase * section + 1); //table
                    deal.Add(j + 1); //round

                    if (dealingRound != 1 || j != 0)
                    {
                        numbers.Add(deal);
                    }
                }

                if (section == 0)
                {
                    movement.Add(numbers);
                }
                else
                {
                    movement[i].AddRange(numbers);
                }
            }
        }

        /// <summary>
        /// Setup appendix move
        /// </summary>
        /// <param name="data"></param>
        public void ReadAppendixMovement(List<string> data, int pairs, int appendix)
        {
            //List<int> pauses = new List<int>();
            List<List<List<int>>> apMovement = new List<List<List<int>>>();

            /*for (int i = 0; i < tournamentBase; i++)
            {
                pauses.Add(0);
            }*/

            //read pairs numbers
            for (int i = 0; i < tournamentBase; i++)
            {
                List<List<int>> numbers = new List<List<int>>();

                for (int j = 0; j < Math.Min(appendix, int.Parse(data[(appendix + 1) * 2 * i])); j++)
                {
                    int ns = int.Parse(data[2 + (appendix + 1) * 2 * i + j * 2].Substring(0, 3));
                    int ew = int.Parse(data[2 + (appendix + 1) * 2 * i + j * 2 + 1].Substring(0, 3));

                    List<int> deal = new List<int>();
                    deal.Add(ns + mitchellSections * tournamentBase * 2);
                    deal.Add(ew + mitchellSections * tournamentBase * 2);

            /*        //put stationary pair position to pauses
                    if (ns == pairs || ew == pairs)
                    {
                        pauses[i] = j;
                    }
                */
                    numbers.Add(deal);
                }

                apMovement.Add(numbers);
            }

            /*
            //normalize pauses
            for (int i = 1; i < tournamentBase; i++)
            {
                pauses[i] = (pauses[i] - pauses[0] + appendix) % appendix;
            }
            pauses[0] = 0;
            
            
            //setup tables and rounds
            for (int i = 0; i < tournamentBase; i++)
            {
                List<List<int>> numbers = new List<List<int>>();

                for (int j = 0; j < int.Parse(data[15 + pairs + (appendix + 1) * 2 * i]); j++)
                {

                    int table = (pauses[i] - j + appendix) % appendix;
                    
                    apMovement[i][j].Add(table + 1 + mitchellSections * tournamentBase);

                    int round = 0;

                    int curPos = i;

                    for (int k = 0; k < j + 1; k++)
                    {
                        while (pauses[curPos] == pauses[dec(curPos, tournamentBase)])
                        {
                            round++;
                            curPos = dec(curPos, tournamentBase);
                        }
                        round++;
                        curPos = dec(curPos, tournamentBase);
                    }

                    apMovement[i][j].Add(round);
                }
            }
             */

            if (mitchellSections == 0)
            {
                movement = apMovement;
            }
            else
            {
                for (int i = 0; i < movement.Count(); i++)
                {
                    movement[i].AddRange(apMovement[i]);
                }
            }
        }

    }
}
