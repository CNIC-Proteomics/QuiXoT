using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QuiXoT.math
{
    public class Utilities
    {
        public static int find(double[] matrix, double evaluated) 
        {
            int ranking=0;

            int low=1;
            int high=matrix.GetUpperBound(0);
            int diff=high - low;
            
            int actEval=(int)Math.Truncate((double)((high-low)/2));
           

            if (evaluated < matrix[1]) 
            {
                ranking = 0;
                return ranking;
            }
            if (evaluated == matrix[1])
            {
                ranking = 1;
                return ranking;
            }
            if (evaluated == matrix[matrix.GetUpperBound(0)])
            {
                ranking = matrix.GetUpperBound(0)-1;
                return ranking;
            }
            if (evaluated > matrix[matrix.GetUpperBound(0)])
            {
                ranking = matrix.GetUpperBound(0) - 1;
                return ranking;
            }

            while(diff>3)
            {

                if (evaluated == matrix[actEval])
                {
                    ranking = actEval;
                    return ranking;
                }

                if (evaluated < matrix[actEval])
                {
                    high = actEval;
                    diff = high-low;
                    actEval = low + (int)Math.Truncate((double)((high - low) / 2));
                }
                else 
                {
                    low = actEval;
                    diff = high - low;
                    actEval = low + (int)Math.Truncate((double)((high - low) / 2));
                }

            }

            if (evaluated >= matrix[actEval])
            {
                ranking = actEval - 1;
            }
            else
            {
                ranking = actEval;
            }

            return ranking;

        }

        public static double median(double[] matrix)
        {

            double median = 0;
            
            ArrayList al = new ArrayList(matrix.Length);
           
            for(int i=0;i<=matrix.GetUpperBound(0);i++)
            {
                al.Add(matrix[i]);            
            }

            al.Sort();

            int pos = (int)Math.Floor((double)(matrix.Length/2)) ;

            if (pos == matrix.Length / 2)
            { 
                median = ((double)al[pos] + (double)al[pos - 1]) / 2; 
            }
            else 
            {
                median = (double)al[pos]; 
            }



            return median;
        }

        public static double median(ArrayList matrix)
        {

            double median = 0;
            matrix.Sort();

            int pos = (int)Math.Floor((double)(matrix.Count/2));

            if (pos == matrix.Count / 2)
            {
                median = (double)matrix[pos];                
            }
            else
            {
                median = ((double)matrix[pos] + (double)matrix[pos - 1]) / 2;
            }

            return median;
        }

        public static double mean(ArrayList matrix)
        {
            double mean = 0;
            double tot = 0;

            foreach (double i in matrix)
            {
                tot += i;
            }

            mean = tot / matrix.Count;
            return mean;
        }

        public static double standardDev(ArrayList matrix)
        {
            double var = 0;
            double mu = mean(matrix);

            foreach (double i in matrix)
            {
                var += (i - mu) * (i - mu);
            }

            var = var / matrix.Count;
            double sd = Math.Sqrt(var);
            return sd;
        }


        #region Weight calculation methods


        /// <summary>
        /// Weight version 6
        /// </summary>
        /// <param name="quanMethod">method used to quantify</param>
        /// <param name="A">quantity of not labeled sample</param>
        /// <param name="B">quantity of labeled sample</param>
        /// <param name="MSQ1">Mean of the sum of squares 1</param>
        /// <param name="MSQ2">Mean of the sum of squares 2</param>
        /// <param name="MSQ3">Mean of the sum of squares 3</param>
        /// <param name="efficiency">labeling efficiency detected</param>
        /// <returns></returns>
        public static double calWeight(LNquantitate.quantitationStrategy qStrategy,
                                        double A,
                                        double B,
                                        double MSQ1,
                                        double MSQ2,
                                        double MSQ3,
                                        double efficiency)
        {


            double weight=0.0;
            double zeroCorr = 0.00000000001;

            if (A + B == 0) return 0.0;

            A += zeroCorr;
            B += zeroCorr;

            double B0 = B * (1 - efficiency) * (1 - efficiency);
            double B1 = 2 * B * efficiency * (1 - efficiency);
            double B2 = B * efficiency * efficiency;

            double AB0 = A + B0;
            double B1B2 = B1 + B2;
    
            switch (qStrategy)
            {
                case LNquantitate.quantitationStrategy.iTRAQ:

                    // Old weight used until October 2010
                    //weight = A > B ? Math.Pow(A, 2) : Math.Pow(B, 2);
                    weight = Math.Max(A, B);
                    break;

                case LNquantitate.quantitationStrategy.SILAC:
                    double Imax = A >= B ? A : B;
                    weight = (Imax * Imax) / (MSQ1 + MSQ2);
                    break;

                case LNquantitate.quantitationStrategy.O18_ZS:
                    
                    if (AB0 >= B1B2)
                    {
                        weight = A * A / (MSQ1 + MSQ2);
                    }
                    else
                    {
                        weight = B * B / (MSQ3 + MSQ2);
                    }

                    break;

                case LNquantitate.quantitationStrategy.O18_HR:
                    
                    if (AB0 >= B1B2)
                    {
                        weight = A * A / MSQ1;
                    }
                    else
                    {
                        weight = B * B / MSQ1;
                    }

                    break;

                case LNquantitate.quantitationStrategy.SILAC_HR:

                    if (AB0 >= B1B2)
                    {
                        weight = A * A / MSQ1;
                    }
                    else
                    {
                        weight = B * B / MSQ1;
                    }

                    break;

                default:
                    weight = 0.0;
                    break;
            }
            
            return weight;
 
        }

        /// <summary>
        /// Weight version 5
        /// </summary>
        /// <param name="MSQLeft">Mean of the sum of squares of the left window</param>
        /// <param name="MSQPept">Mean of the sum of squares of the peptide</param>
        /// <param name="MSQRight">Mean of the sum of squares of the right window</param>
        /// <param name="A">quantity of not labeled sample</param>
        /// <param name="B">quantity of labeled sample</param>
        /// <param name="efficiency">labeling efficiency detected</param>
        /// <returns>weight</returns>
        public static double calWeight(double MSQLeft,
                                        double MSQPept,
                                        double MSQRight,
                                        double A,
                                        double B,
                                        double efficiency)
        {
            double zeroCorr = 0.00000000001;

            A += zeroCorr;
            B += zeroCorr;

            double weight = 0;

            double B0 = B * (1 - efficiency) * (1 - efficiency);
            double B1 = 2 * B * efficiency * (1 - efficiency);
            double B2 = B * efficiency * efficiency;

            double AB0 = A + B0;
            double B1B2 = B1 + B2;


            if (AB0 >= B1B2)
            {
                weight = 1 / ((MSQLeft + MSQPept) / (AB0 * AB0));
            }
            else
            {
                weight = 1 / ((MSQRight + MSQPept) / (B1B2 * B1B2));
            }


            return weight;
        }

        /// <summary>
        /// Weight version 4
        /// </summary>
        /// <param name="MSQLeft">Mean of the sum of squares of the left window</param>
        /// <param name="MSQPept">Mean of the sum of squares of the peptide</param>
        /// <param name="MSQRight">Mean of the sum of squares of the right window</param>
        /// <param name="A">quantity of not labeled sample</param>
        /// <param name="B">quantity of labeled sample</param>
        /// <returns>weight</returns>
        public static double calWeight(double MSQLeft,
                                        double MSQPept,
                                        double MSQRight,
                                        double A,
                                        double B)
        {
            double zeroCorr = 0.00000000001;

            A += zeroCorr;
            B += zeroCorr;

            double weight = 0;
            if (A >= B)
            {
                weight = 1 / ((MSQLeft + MSQPept) / (A * A));
            }
            else
            {
                weight = 1 / ((MSQRight + MSQPept) / (B * B));
            }


            return weight;
        }

        /// <summary>
        /// Weight, version 2
        /// </summary>
        /// <param name="A"></param>
        /// <param name="SD_A"></param>
        /// <param name="B"></param>
        /// <param name="SD_B"></param>
        /// <returns></returns>
        public static double calWeight(double A, double SD_A, double B, double SD_B)
        {
            double zeroCorr = 0.0001;

            A += zeroCorr;
            B += zeroCorr;

            double maxAB = Math.Max(A, B);
            double maxDADB = A > B ? SD_A : SD_B;

            //double weight = 1 / (( Math.Abs(SD_A /A)*Math.Abs(SD_A/A) + Math.Abs(SD_B / B) * Math.Abs(SD_B / B))*Math.Log(2)*Math.Log(2));
            double weight = 1 / ((Math.Abs(maxDADB / maxAB) * Math.Abs(maxDADB / maxAB)));

            return weight;
        }

        public static double calWeight(double A, double SD_A,
                                        double B, double SD_B,
                                        double sigma, double SD_sigma,
                                        double f, double SD_f,
                                        double alpha, double SD_alpha,
                                        double deltaMZ, double SD_deltaMZ)
        {

            double zeroCorr = 0.0001;

            A += zeroCorr;
            B += zeroCorr;
            sigma += zeroCorr;
            f += zeroCorr;
            alpha += zeroCorr;
            deltaMZ += zeroCorr;

            double factors = Math.Abs(SD_A / A) + Math.Abs(SD_B / B) + Math.Abs(SD_sigma / sigma) + Math.Abs(SD_f / f) + Math.Abs(SD_alpha / alpha) + Math.Abs(SD_deltaMZ / deltaMZ);

            double weight = 1 / (factors * factors);

            return weight;

        }


        public static double calWeight(double A, double SD_A,
                                        double B, double SD_B,
                                        double sigma, double SD_sigma,
                                        double f, double SD_f,
                                        double deltaMZ, double SD_deltaMZ)
        {

            double zeroCorr = 0.0001;

            A += zeroCorr;
            B += zeroCorr;
            sigma += zeroCorr;
            f += zeroCorr;
            deltaMZ += zeroCorr;

            double factors = Math.Abs(SD_A / A) + Math.Abs(SD_B / B) + Math.Abs(SD_sigma / sigma) + Math.Abs(SD_f / f) + Math.Abs(SD_deltaMZ / deltaMZ);

            double weight = 1 / (factors * factors);

            return weight;

        }

        #endregion

    
    }
}
