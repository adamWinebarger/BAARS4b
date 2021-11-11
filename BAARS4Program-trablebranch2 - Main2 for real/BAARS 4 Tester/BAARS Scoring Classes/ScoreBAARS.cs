using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAARS_4_Tester
{
    /*
     * This class is the scoring system for the BAARS 4 Test
     * */
    public class ScoreBAARS
    {
        protected int[] answers;

        public ScoreBAARS(int[] answers)
        {
            this.answers = answers;
        }

        // Adds up all the answer scores to the total score
        protected int TotalScore(int start, int end)
        {
            int total = 0;

            for (int i = start; i < end; i++)
            {
                total += answers[i];
            }

            return total;
        }

        // Determines the total number of symptoms
        // Remember Symptoms count only increments if the value of a symptom is 3 or 4
        protected int SymptomCount(int start, int end)
        {
            int count = 0;

            for (int i = start; i < end; i++)
            {
                if (answers[i] >= 3)
                {
                    count++;
                }
            }

            return count;
        }

    }
}
