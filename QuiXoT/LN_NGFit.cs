using System;
using System.Collections.Generic;
using System.Text;
using math_QuiXoT;

namespace scanViewer
{


    class LN_NGFit
    {
        int nIterations;
        
        double alpha;   //Convergence factor 
        double PRS;
        double RSH;

        Comb.mzI[] expData;
        int iCharge;
        double dDeltaR;
        Comb.mzI[] intensPeaks;

        Comb.mzI[] fitData;
        double sumSQ1;
        double sumSQ2;
        bool SignPositive;

        double[] mA=new double[8];
        double[] mB=new double[8];
        double[] mBant=new double[8];
        
        double[] DX=new double[8];

        double[,] R=new double[8,8];
        double[,] RInv=new double[8,8];


        fitDataStrt fitParams=new fitDataStrt();

        OPctrFit controlFit = new OPctrFit();


        public void NewtonGaussFit(Comb.mzI[] experimentalData,Comb.mzI[] intensities, int charge, double deltaR ,fitDataStrt initialParams) 
        {
            //Clone the data to the class
            expData = (Comb.mzI[])experimentalData.Clone();
            iCharge = charge;
            dDeltaR = deltaR;
            intensPeaks = (Comb.mzI[])intensities.Clone();

            //Assign parameters  HAY QUE CAMBIAR ESTO PARA QUE SEA AJUSTABLE DESDE FUERA
            alpha = 0.01;
            PRS = 1e-8;
            RSH = 1e-4;

            //assign initial parameters to the parameters' matrix
            mB[1] = initialParams.A;        //Amount of A
            mB[2] = initialParams.alpha;    //leptokurtosis
            mB[3] = initialParams.B;        //Amount of B
            mB[4] = initialParams.deltaMz;  //experimental mass/charge deviation
            mB[5] = initialParams.f;        //efficiency
            mB[6] = initialParams.sigma;    //Gaussian width
            mB[7] = initialParams.signoise; //signal/noise relation

            nIterations = 0;

            //assign initial parameters as new parameters
            for (int i = 1; i <= mB.GetUpperBound(0); i++) 
            {
                mA[i] = mB[i];
            }
            
            //Calculate the initial fit
            fitData = Gaussians.calEnvelope(intensities, expData, initialParams, charge, deltaR);

            //first chi^2 test.
            sumSQ1 = sumSquares(expData, fitData);



            //control form of the iterations
            if (controlFit.IsDisposed)
            {
                controlFit = null;
                controlFit = new OPctrFit();
            }
            controlFit.Show();
            controlFit.Activate();
            //controlFit.Owner = this;
            controlFit.iterAdd(nIterations,initialParams,sumSQ1,false);


     

        
        }

        public void NewtonGaussFit(double[,] expData, double[] fitParameters, double[] constants)
        {
            nIterations = 0;


 
        }

        public double[,] Function() 
        {
            double[,] f=new double[3,2];

            f[1,1]=0;

            return f;
        }


        public void divCorrection()
        {
            for (int i = 1; i <= DX.GetUpperBound(0); i++)
            {
                DX[i] = DX[i] * alpha;
                mA[i] = mBant[i] + DX[i];
            }
        }

        /// <summary>
        /// First partial derivative (for each fit parameter) of the fitted data  
        /// </summary>
        /// <returns>First partial derivative (for each fit parameter) of the fitted data</returns>
        public double[] derivative()
        {
            double[] dv = null;


            return dv;
        }

        public void adjust() 
        {
            for (int i = 1; i <= R.GetUpperBound(0); i++) 
            {
                for (int j = 1; j <= R.GetUpperBound(1); j++)
                {
                    R[i, j] = 0;
                }
            }

            for (int j = 1; j <= fitData.GetUpperBound(0); j++)
            {
 
            }


        }

        public static double sumSquares(Comb.mzI[] expData, Comb.mzI[] fittedData)
        {
            double sumSQ = 0;

            for (int j = 1; j <= expData.GetUpperBound(0); j++)
            {
                sumSQ += (expData[j].I - fittedData[j].I) * (expData[j].I - fittedData[j].I);
            }

            return sumSQ;
            
        }

        public static double sumSquares(double[] expData, double[] fittedData)
        {
            double sumSQ = 0;

            for (int j = 1; j <= expData.GetUpperBound(0); j++)
            {
                sumSQ += (expData[j] - fittedData[j]) * (expData[j] - fittedData[j]);
            }

            return sumSQ;
 
        }
                
        public static double[,] InvMatrix(double[,] Matrix)
        {
            //check whether the dimensions are correct
            int dimI = Matrix.GetUpperBound(0);
            int dimJ = Matrix.GetUpperBound(1);
            if (dimI != dimJ) 
            {
                return null;
            }

            double[,] H = new double[dimI+1, dimJ+1];
            double[,] H1 = new double[dimI+1, (dimJ+1)*2];
            double A;
            double B;
            double[,] invMat = new double[dimI+1, dimJ+1];


            for (int i = 1; i <= dimI; i++) 
            {
                for (int j = 1; j <= dimJ; j++)
                {
                    H[i, j] = Matrix[i, j]; 
                }
            }

            for (int i = 1; i <= dimI; i++)
            {
                for (int j = 1; j <= dimJ; j++)
                {
                    H1[i, j] = H[i, j];
                }

                for (int j = dimJ + 1; j <= 2 * dimJ; j++)
                {
                    if ( (dimJ + i) == j)
                    {
                        H1[i, j] = 1;
                    }
                    else
                    {
                        H1[i, j] = 0; 
                    }
                }

            }

            for (int l = 1; l <= dimI; l++)
            {
                A = H1[l, l];

                for (int j = 1; j <= 2 * dimJ; j++)
                {
                    H1[l, j] = H1[l, j] / A;
                }
                for (int i = 1; i <= dimI; i++)
                {
                    B = H1[i, l];
                
                    for (int j = 1; j <= 2 * dimJ; j++)
                    {
                        if (i != l)
                        {
                            H1[i, j] = H1[i, j] - H1[l, j] * B; 
                        }
                    }
                }
            }


            for (int i = 1; i <= dimI; i++)
            {
                for (int j = dimJ + 1; j <= 2 * dimJ; j++)
                {
                    invMat[i, j - dimJ] = H1[i, j];
                }
            }

            return invMat;
        }
    }
}
