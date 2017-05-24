using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
 

namespace QuiXoT.math
{

    public enum Resolution { LOW, HIGH }


    /// <summary>
    /// structure for the fit parameters
    /// </summary>
    public struct fitDataStrt
    {
        
        private double AVal;
        private double BVal;
        private double fVal;
        private double deltaMzVal;
        private double alphaVal;
        private double sigmaVal;
        private double signoiseVal;
        
        /// <summary>
        /// constructor of the structure for the fit parameters
        /// </summary>
        /// <param name="AValue">(double)concentration of A</param>
        /// <param name="BValue">(double)concentration of B</param>
        /// <param name="deltaMzValue">(double)experimental deviation of m/z</param>
        /// <param name="alphaValue">(double)Leptokurtosis</param>
        /// <param name="sigmaValue">(double)gaussian width</param>
        /// <param name="signoiseValue">(double)Signal to Noise relation</param>
        public fitDataStrt(double AValue, double BValue, double fValue ,double deltaMzValue, double alphaValue,
                           double sigmaValue, double signoiseValue)
        {
            AVal = AValue;
            BVal = BValue;
            fVal = fValue;
            deltaMzVal = deltaMzValue;
            alphaVal = alphaValue;
            sigmaVal = sigmaValue;
            signoiseVal = signoiseValue;
                       
        }

        public double A
        {
            get{return AVal;}
            set{AVal = Math.Abs(value);}
        }

        public double B
        {
            get{return BVal;}
            set{BVal = Math.Abs(value);}
        }
        public double f
        {
            get{return fVal;}
            set{fVal = Math.Abs(value);}
        }
        public double deltaMz
        {
            get{return deltaMzVal;}
            set{deltaMzVal = value;}
        }
        public double alpha
        {
            get{return alphaVal;}
            set{alphaVal = Math.Abs(value);}
        }
        public double sigma
        {
            get{return sigmaVal;}
            set{sigmaVal = value;}
        }
        public double signoise
        {
            get{return signoiseVal;}
            set{signoiseVal = value;}
        }

    }
    

    

    /// <summary>
    /// Structure for reading the fit parameters XML file.
    /// </summary>
    public struct instrumentParamsStrt 
    {
        private double widthVal;
        private string instNameVal;
        private Resolution instResolutionVal;
        private int kmaxVal;
        private double alphaVal;
        private double sigmaVal;
        private double deltaRVal;
        private double fVal;
        private double deltaMzVal;
        private double sn_fVal;
        private double varAVal;
        private double varBVal;
        private double varfVal;
        private double varSigmaVal;
        private double varAlphaVal;
        private double varSnVal;


        //public instrumentParamsStrt(string instNameValue, Resolution instResolutionValue, int kmaxValue,
        //                            double alphaValue, double sigmaValue, double deltaRValue, double fValue,
        //                            double deltaMzValue, double sn_fValue, double varAValue, double varBValue,
        //                            double varfValue, double varSigmaValue, double varAlphaValue, double varSnValue)
        //{
        //    widthVal = widthValue;
        //    instNameVal = instNameValue;
        //    instResolutionVal = instResolutionValue;
        //    kmaxVal = kmaxValue;
        //    alphaVal = alphaValue;
        //    sigmaVal = sigmaValue;
        //    deltaRVal = deltaRValue;
        //    fVal = fValue;
        //    deltaMzVal = deltaMzValue;
        //    sn_fVal = sn_fValue;
        //    varAVal = varAValue;
        //    varBVal = varBValue;
        //    varfVal = varfValue;
        //    varSigmaVal = varSigmaValue;
        //    varAlphaVal = varAlphaValue;
        //    varSnVal = varSnValue;
        //}

        public double width
        {
            get { return widthVal; }
            set { widthVal = value; }
        }
        public string instName
        {
            get{return instNameVal;}
            set{instNameVal = value;}
        }
        public Resolution instResolution
        {
            get{return instResolutionVal;}
            set{instResolutionVal = value;}
        }
        public int kmax
        {
            get{return kmaxVal;}
            set{kmaxVal = value;}
        }
        public double alpha
        {
            get{return alphaVal;}
            set{alphaVal = value;}
        }        
        public double sigma
        {
            get{return sigmaVal;}
            set{sigmaVal = value;}
        }        
        public double deltaR
        {
            get{return deltaRVal;}
            set{deltaRVal  = value;}
        }
        public double f
        {
            get{return fVal;}
            set{fVal = value;}
        }
        public double deltaMz
        {
            get{return deltaMzVal;}
            set{deltaMzVal = value;}
        }
        
        public double sn_f
        {
            get{return sn_fVal;}
            set{sn_fVal = value;}
        }
        public double varA
        {
            get{return varAVal;}
            set{varAVal = value;}
        }
        public double varB
        {
            get{return varBVal;}
            set{varBVal = value;}
        }
        public double varf
        {
            get{return varfVal;}
            set{varfVal = value;}
        }
                    
        public double varSigma
        {
            get{return varSigmaVal;}
            set{varSigmaVal = value;}
        }
        public double varAlpha
        {
            get{return varAlphaVal;}
            set{varAlphaVal = value;}
        }

        public double varSn
        {
            get{return varSnVal;}
            set{varSnVal = value;}
        }
    }


    public struct confNGfitParamsStrt
    {
        private int kmaxVal;
        private int nMaxIterVal;
        private double sumSQtoleranceVal;
        private double alphaVal;
        private double PRSVal;

        public int kmax
        {
            get { return kmaxVal; }
            set { kmaxVal = value; }
        }
        public int nMaxIter
        {
            get { return nMaxIterVal; }
            set { nMaxIterVal = value; }
        }
        public double sumSQtolerance
        {
            get { return sumSQtoleranceVal; }
            set { sumSQtoleranceVal = value; }
        }
        public double alpha
        {
            get { return alphaVal; }
            set { alphaVal = value; }
        }
        public double PRS
        {
            get { return PRSVal; }
            set { PRSVal = value; }
        }


    }

    public class Gaussians
    {

        /// <summary>
        /// Calculates the isotopic envelope.
        /// </summary>
        /// <param name="intensities">(Comb.mzI[])array of isotopic peaks</param>
        /// <param name="expData">(Comb.mzI[])array of the experimental data</param>
        /// <param name="fitData">(fitDataStr)Fit parameters</param>
        /// <param name="charge">(int)Charge</param>
        /// <param name="deltaR">(double)Shift due to the labeling (in 18O, deltaR=2.004245778)</param>
        /// <returns>(Comb.mzI[])array of intensities representing the envelope</returns>
        /// 


//#region normal distribution

//        public static double normalDistribution(double x, double mean, double std, bool cumulative)
//        {
//            if (cumulative)
//            {
//                return Phi(x, mean, std);
//            }
//            else
//            {
//                double tmp = 1 / ((Math.Sqrt(2 * Math.PI) * std));
//                return tmp * Math.Exp(-.5 * Math.Pow((x - mean) / std, 2));
//            }
//        }
//        //from http://www.cs.princeton.edu/introcs/...Math.java.html
//        // fractional error less than 1.2 * 10 ^ -7.
//        static double erf(double z)
//        {
//            double t = 1.0 / (1.0 + 0.5 * Math.Abs(z));

//            // use Horner's method
//            double ans = 1 - t * Math.Exp(-z * z - 1.26551223 +
//            t * (1.00002368 +
//            t * (0.37409196 +
//            t * (0.09678418 +
//            t * (-0.18628806 +
//            t * (0.27886807 +
//            t * (-1.13520398 +
//            t * (1.48851587 +
//            t * (-0.82215223 +
//            t * (0.17087277))))))))));
//            if (z >= 0) return ans;
//            else return -ans;
//        }

//        // cumulative normal distribution
//        static double Phi(double z)
//        {
//            return 0.5 * (1.0 + erf(z / (Math.Sqrt(2.0))));
//        }

//        // cumulative normal distribution with mean mu and std deviation sigma
//        static double Phi(double z, double mu, double sigma)
//        {
//            return Phi((z - mu) / sigma);
//        }

//        #endregion

        public static Comb.mzI[] calEnvelope(   Comb.mzI[] intensities, 
                                                Comb.mzI[] expData, 
                                                fitDataStrt fitData, 
                                                int charge,
                                                double label,
                                                Resolution res)
        {
            double A = fitData.A;
            double B = fitData.B;
            double f=fitData.f;
            double deltaMz= fitData.deltaMz;
            double alpha=fitData.alpha;
            double sigma=fitData.sigma;
            double signoise=fitData.signoise;


            double Coeff1 = A + B * (1 - f) * (1 - f);
            double Coeff2 = 2 * B * f * (1 - f);
            double Coeff3 = B * f * f;




            //Modify this to obtain beatiful figures for your posters
            //Coeff1 = 0;//A + B * (1 - f) * (1 - f);
            //Coeff2 = 0;// 2 * B * f * (1 - f);
            //Coeff3 = 0; //B * f * f;
            //

            int nGaussians = intensities.Length;

            Comb.mzI[] intensitiesCoeff1 = new Comb.mzI[nGaussians];
            Comb.mzI[] intensitiesCoeff2 = new Comb.mzI[nGaussians];
            Comb.mzI[] intensitiesCoeff3 = new Comb.mzI[nGaussians];
            double ddd = 0;

            for (int i = 0; i <= intensitiesCoeff1.GetUpperBound(0); i++)
            {
                intensitiesCoeff1[i].mz = intensities[i].mz;
                intensitiesCoeff2[i].mz = intensities[i].mz + label / charge;
                intensitiesCoeff3[i].mz = intensities[i].mz + 2 * label / charge;

                ddd = (intensitiesCoeff2[i].mz - intensitiesCoeff1[i].mz) * 2;
            }

            for (int i = 0; i < nGaussians; i++)
            {
                intensitiesCoeff1[i].I = Coeff1 * intensities[i].I;
                intensitiesCoeff2[i].I = Coeff2 * intensities[i].I;
                intensitiesCoeff3[i].I = Coeff3 * intensities[i].I;
            }
        


            // 13July2009 Pedro : Modification for SILAC (and generalization for all isotopic labelings)
            int maxNumOfPeaks = (int)Math.Floor(2 * label) + nGaussians;
            Comb.mzI[] intensitiesTotal = new Comb.mzI[maxNumOfPeaks];

            int beginCoeff1 = 0;
            int beginCoeff2 = (int)Math.Floor(label);
            int beginCoeff3 = (int)Math.Floor(2 * label);

            for (int i = 0; i < maxNumOfPeaks; i++)
            {
                intensitiesTotal[i].I = 0;
            }
           
            for (int i = 0; i < nGaussians; i++)
            {
                intensitiesTotal[i + beginCoeff1].mz = intensitiesCoeff1[i].mz;
                intensitiesTotal[i + beginCoeff2].mz = intensitiesCoeff2[i].mz;
                intensitiesTotal[i + beginCoeff3].mz = intensitiesCoeff3[i].mz;

                intensitiesTotal[i + beginCoeff1].I += intensitiesCoeff1[i].I;
                intensitiesTotal[i + beginCoeff2].I += intensitiesCoeff2[i].I;
                intensitiesTotal[i + beginCoeff3].I += intensitiesCoeff3[i].I;
            }

            ddd = (intensitiesTotal[0].mz - intensitiesTotal[2].mz) * 2;

            if (res == Resolution.HIGH)
            {
                int counter = 0;
                for(int k=0;k<intensitiesTotal.Length;k++)
                {
                    if (intensitiesTotal[k].mz != 0) counter++;
                }


                //If the labeling distance is high, then we must clean empty peak positions
                Comb.mzI[] intensitiesTotalClean = new Comb.mzI[counter];
                counter = 0;
                for (int k = 0; k < intensitiesTotal.Length; k++)
                {
                    if (intensitiesTotal[k].mz != 0)
                    {
                        intensitiesTotalClean[counter].mz =intensitiesTotal[k].mz;
                        intensitiesTotalClean[counter].I = intensitiesTotal[k].I;
                        counter++;
                    }
                }

                return intensitiesTotalClean;
            }
            Comb.mzI[] envelope = new Comb.mzI[expData.Length];


            //search the positions (m/z) of the peaks on the experimental data
            //Assuming that expData is sorted by m/z!!!
            double[] expmz = new double[expData.Length];
            int[] intensitiesMzPos = new int[intensitiesTotal.Length];


            for (int i = 0; i <= expData.GetUpperBound(0); i++)
            {
                expmz[i] = expData[i].mz;
            }
            for (int i = 0; i <= intensitiesTotal.GetUpperBound(0); i++)
            {
                intensitiesMzPos[i] = Utilities.find(expmz, intensitiesTotal[i].mz);
            }


            //Add background to theoretical envelope
            for (int i = 0; i <= envelope.GetUpperBound(0); i++)
            {
                envelope[i].mz = expData[i].mz;
                envelope[i].I = signoise;
            }

            double maxContribSigma = 3 * sigma;

            for (int i = 0; i <= intensitiesTotal.GetUpperBound(0); i++)
            {
                if (intensitiesTotal[i].I != 0)
                {
                    double diff = 0;
                    int idx = 0;
                    // calculation for the right side of the gaussian
                    while (diff < maxContribSigma + Math.Abs(deltaMz) && intensitiesMzPos[i] + idx < envelope.Length) //Meter el parámetro a la hoja XML de parámetros 
                    {
                        double contrib;
                        contrib = intensitiesTotal[i].I * gaussDblExp(envelope[intensitiesMzPos[i] + idx].mz - deltaMz,
                            intensitiesTotal[i].mz, sigma, alpha).I;
                        envelope[intensitiesMzPos[i] + idx].I += contrib;
                        //diff = contrib / (envelope[intensitiesMzPos[i] + idx].I+0.00001);
                        diff = Math.Abs(envelope[intensitiesMzPos[i] + idx].mz - envelope[intensitiesMzPos[i]].mz);
                        idx++;
                    }

                    diff = 0;
                    idx = 1;
                    // calculation for the left side of the gaussian
                    while (diff < maxContribSigma + Math.Abs(deltaMz) && intensitiesMzPos[i] - idx > 0) //Meter el parámetro a la hoja XML de parámetros 
                    {
                        double contrib;
                        contrib = intensitiesTotal[i].I * gaussDblExp(envelope[intensitiesMzPos[i] - idx].mz - deltaMz,
                            intensitiesTotal[i].mz, sigma, alpha).I;
                        envelope[intensitiesMzPos[i] - idx].I += contrib;
                        //diff = contrib / (envelope[intensitiesMzPos[i] - idx].I+0.0001);
                        diff = Math.Abs(envelope[intensitiesMzPos[i] - idx].mz - envelope[intensitiesMzPos[i]].mz);
                        idx++;
                    }
                }
            }



            ////Correct deltaMZ 
            ////Comb.mzI[] envelopeCorrected = (Comb.mzI[])envelope.Clone();
            //Comb.mzI[] envelopeCorrected =new Comb.mzI[envelope.Length];

            //for (int i = 0; i <= envelopeCorrected.GetUpperBound(0); i++)
            //{
            //    envelopeCorrected[i].mz = envelope[i].mz;
            //    envelopeCorrected[i].I = signoise; //signoise
            //}

            //int deltaMZpos = 0;
            //double deltaMZq = 0;
            //double initPos = expData[deltaMZpos].mz;
            //while ((double)deltaMZq < Math.Abs(deltaMz))
            //{
            //    deltaMZpos++;
            //    deltaMZq = expData[deltaMZpos].mz - initPos;
            //}

            //if (deltaMZq > Math.Abs(deltaMz) * 2 && deltaMZpos == 1)
            //    deltaMZpos = 0;

            //if (deltaMz < 0)
            //    deltaMZpos = -deltaMZpos;

            //if (deltaMZpos >= 0)
            //{
            //    // bug warning! nothing is copied before deltaMSpos ****
            //    for (int i = 0; i <= envelopeCorrected.GetUpperBound(0) - deltaMZpos; i++)
            //    {
            //        envelopeCorrected[i + deltaMZpos].I = envelope[i].I;
            //    }
            //}
            //else
            //{
            //    // bug warning! nothing is copied from deltaMZpos to the end ****
            //    for (int i = -deltaMZpos; i <= envelopeCorrected.GetUpperBound(0) + deltaMZpos; i++)
            //    {
            //        envelopeCorrected[i + deltaMZpos].I = envelope[i].I;
            //    }
            //}

            return envelope;// Corrected;
        }

        public static Comb.mzI[] calFunction(Comb.mzI[] expData, fitDataStrt fitData)
        {
            double a = fitData.A;
            double b = fitData.alpha;
            double c = fitData.B;
            
            Comb.mzI[] fdata = new Comb.mzI[expData.Length];

            for (int i = 1; i <= expData.GetUpperBound(0); i++)
            {
                fdata[i].mz = expData[i].mz;
                fdata[i].I = a + b * fdata[i].mz + c * fdata[i].mz * fdata[i].mz;
            }


            return fdata;
        }


        public static Comb.mzI gaussDblExp(double x, double mu, double sigma, double alpha) 
        {
            Comb.mzI gDblExp = new Comb.mzI();
            
            //testing: alpha can not be higher than one
            if (alpha > 1.4) alpha = 1.4;
            gDblExp.mz = x;
            gDblExp.I = (1-alpha)* gaussian(x, mu, sigma).I+ alpha * dblExp(x,mu,sigma).I;

            return gDblExp;
        }

        /// <summary>
        ///  gaussian(x,mu,sigma)
        /// </summary>
        /// <param name="x">(double)x</param>
        /// <param name="mu">(double)gaussian center</param>
        /// <param name="sigma">(double)gaussian width</param>
        /// <returns>(Comb.mzI)Gaussian height</returns>
        public static Comb.mzI gaussian(double x, double mu,double sigma) 
        {


            Comb.mzI gauss = new Comb.mzI();
            sigma = Math.Abs(sigma);

                gauss.mz = x;
                gauss.I = (1 / (Math.Sqrt(2 * Math.PI)*sigma))*Math.Exp(-((x-mu)*(x-mu))/(2*sigma*sigma));
                //gauss.I = Math.Exp(-((x - mu) * (x - mu)) / (2 * sigma * sigma));
            return gauss;
        }

        /// <summary>
        /// Double exponential(x,mu,sigma)
        /// </summary>
        /// <param name="x">(double)x</param>
        /// <param name="mu">(double)Double exponential center</param>
        /// <param name="sigma">(double)double exponential width</param>
        /// <returns>(Comb.mzI)double exponential height</returns>
        public static Comb.mzI dblExp(double x, double mu, double sigma)
        {
            Comb.mzI dEx = new Comb.mzI();
            sigma = Math.Abs(sigma);

            dEx.mz = x;
            dEx.I = (1/(2*sigma)) * Math.Exp(-Math.Abs(x - mu) / sigma);

            return dEx;
        }


        #region initial conditions


        public static fitDataStrt getInitialConditions(fitDataStrt initialConds,
                                                        Comb.mzI[] expData,
                                                        Comb.mzI[] intensities,
                                                        double sn_f,
                                                        int charge,
                                                        double deltaR,
                                                        Resolution res)
        {
            if (res == Resolution.LOW)
            {
                double h0 = getMax(expData, intensities[0].mz + (initialConds.deltaMz - 1) / charge, intensities[0].mz + (initialConds.deltaMz + 1) / charge);
                double hmark = getMax(expData, intensities[0].mz + (initialConds.deltaMz + 2 * deltaR - 1) / charge, intensities[0].mz + (initialConds.deltaMz + 2 * deltaR + 1) / charge);

                double alpha = initialConds.alpha;
                double sigma = initialConds.sigma;
                double efficiency = initialConds.f;
                double PI = Math.PI;

                initialConds.B = hmark / ((1 - alpha) / (Math.Sqrt(2 * PI) * sigma) + alpha / (2 * sigma)) / intensities[0].I;
                initialConds.A = h0 / ((1 - alpha) / (Math.Sqrt(2 * PI) * sigma) + alpha / (2 * sigma)) / intensities[0].I;
                //initialConds.B = hmark / ((1 - alpha) + alpha / (2 * sigma)) / intensities[0].I;
                //initialConds.A = h0 / ((1 - alpha) + alpha / (2 * sigma)) / intensities[0].I;
                initialConds.signoise = averagef(expData, sn_f);
            }
            else
            {
                initialConds.A = 0;

                // we just need to know the order of magnitude, so we don't need to know accurately the sum in A and B,
                for (int peak = 0; peak < expData.Length; peak++)
                {
                    initialConds.A += expData[peak].I;
                }

                initialConds.A = initialConds.A / 2;
                initialConds.B = initialConds.A;
            }

            return initialConds;
        }

        public static double getMax(Comb.mzI[] expData, double rangeMin, double rangeMax) 
        {
            double max = 0;
            try
            {
                for (int i = 0; i <= expData.GetUpperBound(0); i++)
                {
                    if (expData[i].mz >= rangeMin && expData[i].mz <= rangeMax && expData[i].I > max)
                        max = expData[i].I;
                }
            }
            catch { }

            return max;
        }

        public static double getMin(Comb.mzI[] expData, double rangeMin, double rangeMax) 
        {
            double min = getMax(expData, rangeMin, rangeMax);
            try
            {
                for (int i = 0; i <= expData.GetUpperBound(0); i++)
                {
                    if (expData[i].mz >= rangeMin && expData[i].mz <= rangeMax && expData[i].I < min) min = expData[i].I;
                }
            }
            catch { }

            return min;
        }

        private static double averagef(Comb.mzI[] expData, double sn_f) 
        {
            double avg = 0;
            try
            {
                for (int i = 0; i <= expData.GetUpperBound(0); i++)
                {
                    avg += Math.Pow(expData[i].I, sn_f);
                }
                avg = Math.Pow(avg / (expData.GetUpperBound(0) + 1), 1 / sn_f);
            }
            catch { }

            return avg;
        }

        #endregion

    }
}
