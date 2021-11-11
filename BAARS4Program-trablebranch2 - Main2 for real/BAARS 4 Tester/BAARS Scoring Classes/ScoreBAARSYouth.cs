using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAARS_4_Tester
{
    /*
     * This class has is for scoring the youth scores for the BAARS 4 test
     * */
    public class ScoreBAARSYouth : ScoreBAARS
    {
        private int total1, symptom1, total2, symptom2;

        private int sumTotals, sumSymptoms;

        public ScoreBAARSYouth(int[] answers) : base(answers)
        {
            total1 = TotalScore(0, 9);
            symptom1 = SymptomCount(0, 9);
            total2 = TotalScore(9, 18);
            symptom2 = SymptomCount(9, 18);

            sumTotals = total1 + total2;
            sumSymptoms = symptom2 + symptom1;
        }

        // Gets the number value of total1, total2, symptom1, symptom2, sumtotal, and sumSymptoms
        public int GetValue(string which)
        {
            switch (which)
            {
                case "total1": return total1;
                case "total2": return total2;
                case "symptom1": return symptom1;
                case "symptom2": return symptom2;
                case "sumTotal": return sumTotals;
                case "sumSymptoms": return sumSymptoms;
                default: return -999;
            }
        }
    }
}
