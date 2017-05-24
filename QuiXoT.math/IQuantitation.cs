using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using MathNet.Numerics.LinearAlgebra;

namespace QuiXoT.math
{


    public interface IQuantitation
    {
        DataRow[] quantitate(DataRow initialData, Comb.mzI[] experimentalSpectrum);
        void config(qMethodsSchema.Quanmethods quanMethods,AminoacidList[] aminoacidList,isotList[][] isotopes, string method_id_name);
        void addDataTable(DataTable dt);
        Comb.mzI[] getFittedSpectrum(DataRow fittedDataRow, Comb.mzI[] experimentalSpectrum);
        Comb.mzI[] centroiding(Comb.mzI[] experimentalSpectrum, Comb.mzI[] mz, double width);
        double getMonoisotMass(DataRow initialData);
    }

    public class AveragineModel : IQuantitation
    {
        AminoacidList[] aminoacids;
        isotList[][] isotopes;
        //LNquantitate.quantitationStrategy qStrategy = LNquantitate.quantitationStrategy.AveragineModel;
        DataTable original_dt;
       

        #region IQuantitation Members

        public DataRow[] quantitate(DataRow initialData, 
                                    Comb.mzI[] experimentalSpectrum)
        {
            DataRow[] finalData;

            finalData = new DataRow[1];

            //Calculate the composition for a given sequence
            string sSequence = initialData["Sequence"].ToString().Trim();
            string sequence = sSequence;

            if (sSequence.Contains('.'))
            {
                string[] sSeqSplit = sSequence.Split('.');
                sequence = sSeqSplit[1].ToString();
            }

            Comb.compStrt[] composition;
            if (aminoacids.Length > 0)
            {
                composition = AminoacidList.calComposition(sequence, aminoacids);
            }
            else { return null; }

            if (composition == null) return null;


            //Calculate the Intensities and m/z for a given chemical composition.
            
            int charge = 1;

            Comb.mzI[] mz = Comb.calIntensities(composition,
                                                isotopes,
                                                charge,
                                                0,
                                                10);
                                               


            //Write the Quantification Data in the row
            finalData[0] = initialData;
            finalData[0]["q_M0"] = mz[0].mz;
            finalData[0]["q_I0"] = mz[0].I;
            finalData[0]["q_M1"] = mz[1].mz;
            finalData[0]["q_I1"] = mz[1].I;
            finalData[0]["q_M2"] = mz[2].mz;
            finalData[0]["q_I2"] = mz[2].I;
            finalData[0]["q_M3"] = mz[3].mz;
            finalData[0]["q_I3"] = mz[3].I;
            finalData[0]["q_M4"] = mz[4].mz;
            finalData[0]["q_I4"] = mz[4].I;
            finalData[0]["q_M5"] = mz[5].mz;
            finalData[0]["q_I5"] = mz[5].I;
            finalData[0]["q_M6"] = mz[6].mz;
            finalData[0]["q_I6"] = mz[6].I;
            finalData[0]["q_M7"] = mz[7].mz;
            finalData[0]["q_I7"] = mz[7].I;
            finalData[0]["q_M8"] = mz[8].mz;
            finalData[0]["q_I8"] = mz[8].I;
            finalData[0]["q_M9"] = mz[9].mz;
            finalData[0]["q_I9"] = mz[9].I;
            

            return finalData;
        }

        public void config( QuiXoT.math.qMethodsSchema.Quanmethods quanMethods, 
                            AminoacidList[] aminoacidList, 
                            isotList[][] isotopesList, 
                            string method_id_name)
        {
            aminoacids = aminoacidList;
            isotopes = isotopesList;
 
        }

        public void addDataTable(DataTable dt)
        {
            original_dt = dt.Clone();
        }

        public Comb.mzI[] centroiding(Comb.mzI[] experimentalSpectrum, Comb.mzI[] mz, double width)
        {
            throw new NotImplementedException();
        }

        public Comb.mzI[] getFittedSpectrum(DataRow fittedDataRow, 
                                            Comb.mzI[] experimentalSpectrum)
        {
            return null;
        }

        public double getMonoisotMass(DataRow initialData)
        {
            double monoisotMass = 0.0;

            //Calculate the composition for a given sequence
            string sSequence = initialData["Sequence"].ToString().Trim();
            string sequence = sSequence;

            if (sSequence.Contains('.'))
            {
                string[] sSeqSplit = sSequence.Split('.');
                sequence = sSeqSplit[1].ToString();
            }

            Comb.compStrt[] composition = AminoacidList.calComposition(sequence, aminoacids);

            //Calculate the Intensities and m/z for a given chemical composition.
            double calibrationError = 0;
            try
            {
                calibrationError = double.Parse(initialData["q_CalibrationError"].ToString());
            }
            catch { }

            int charge = int.Parse(initialData["Charge"].ToString());

            Comb.mzI[] mz = Comb.calIntensities(composition,
                                                isotopes,
                                                charge,
                                                calibrationError,
                                                1);


            monoisotMass = mz[0].mz;

            return monoisotMass;
        }

        #endregion
    }



    public class Q180ZS : IQuantitation
    {
        AminoacidList[] aminoacids;
        instrumentParamsStrt instrParams;
        isotList[][] isotopes;
        confNGfitParamsStrt configNG;
        bool fitwindowShow;
        //string methodKey = "O18_ZS";
        LNquantitate.quantitationStrategy qStrategy = LNquantitate.quantitationStrategy.O18_ZS;
        DataTable original_dt;
        double rsh = 1e-2;
        
        #region Miembros de IQuantitation

        public DataRow[] quantitate(DataRow initialData, Comb.mzI[] experimentalSpectrum) 
        {
            double calibrationError;
            int charge;
            DataRow[] finalData;
            fitwindowShow = false;
 


            finalData =new DataRow[1];

            try
            {
                calibrationError = double.Parse(initialData["q_CalibrationError"].ToString());
            }
            catch
            {
                calibrationError = 0;
                initialData["q_CalibrationError"] = calibrationError;
            }

            //Create a fit data structure
            fitDataStrt fitData = new fitDataStrt();

            //Calculate the composition for a given sequence
            string sSequence = initialData["Sequence"].ToString().Trim();
            string sequence = sSequence;

            if (sSequence.Contains('.'))
            {
                string[] sSeqSplit = sSequence.Split('.');
                sequence = sSeqSplit[1].ToString();
            }

            Comb.compStrt[] composition; 
            if (aminoacids.Length >0)
            {
                composition = AminoacidList.calComposition(sequence, aminoacids);
            }
            else { return null; }

            if (composition == null) return null;

            try
            {
                charge = int.Parse(initialData["Charge"].ToString());
            }
            catch{return null;}

            Comb.mzI[] mz = Comb.calIntensities(composition, isotopes, charge, calibrationError, configNG.kmax);

            //get initial parameters

            fitData.alpha = instrParams.alpha;
            fitData.f = instrParams.f;
            fitData.sigma = instrParams.sigma;
            fitData.A = 0;
            fitData.B = 0;
            fitData.deltaMz = instrParams.deltaMz;

            fitData = Gaussians.getInitialConditions(fitData, experimentalSpectrum, mz, instrParams.sn_f, charge, instrParams.deltaR, Resolution.LOW);

            if (instrParams.instResolution == Resolution.HIGH)
            {
                fitData.signoise = 0;
                fitData.alpha = instrParams.alpha;
            }


            fitDataStrt fitDataSweep;
            double sumSQ;


            /*
             * 1 == A
             * 2 == alpha
             * 3 == B
             * 4 == deltaMZ
             * 5 == f
             * 6 == sigma
             * 7 == signoise
             */

            int[] iterations = new int[8];
            iterations[1] = 10;
            iterations[2] = 10;
            iterations[3] = 10;
            iterations[4] = 10;
            iterations[5] = 10;
            iterations[6] = 10;
            iterations[7] = 10;
            if (instrParams.instResolution == Resolution.HIGH)
            {
                iterations[7] = 0;
            }


            fitDataSweep = LNquantitate.fitbySweep(mz,
                                                    experimentalSpectrum,
                                                    fitData,
                                                    charge,
                                                    instrParams.deltaR,
                                                    iterations,
                                                    fitwindowShow,
                                                    instrParams,
                                                    out sumSQ,
                                                    1);
            fitData = fitDataSweep;

            /* 
             *   Uncomment if you want to jump the sweep fit.
             * 
            double sumSQ=(double)dv[rowIndex].Row["q_SumSquares"];
            fitData.A = (double)dv[rowIndex].Row["q_A"];
            fitData.alpha = (double)dv[rowIndex].Row["q_Alpha"];
            fitData.B = (double)dv[rowIndex].Row["q_B"];
            fitData.deltaMz = (double)dv[rowIndex].Row["q_DeltaMZ"];
            fitData.f = (double)dv[rowIndex].Row["q_f"];
            fitData.sigma = (double)dv[rowIndex].Row["q_Sigma"];
            fitData.signoise = (double)dv[rowIndex].Row["q_background"];
            */


            fitDataStrt sdFittedData = new fitDataStrt();

            string exitFit = "";

            // First Fit (No deltaMz)

            //List of parameters used for fitting.
            int[] paramsUsed = new int[8];
            paramsUsed[1] = 1; //A (A quantity)
            paramsUsed[2] = 1; //alpha (leptokurtosis)
            paramsUsed[3] = 1; //B (B quantity)
            paramsUsed[4] = 0; //deltaMz (experimental mass deviation)
            paramsUsed[5] = 1; //f (efficiency)
            paramsUsed[6] = 1; //sigma (gaussian width)
            paramsUsed[7] = 0; //signoise (Background threshold)

            double efficiencyLimit = 0.3;
            bool lowEfficiency = false;
            if (fitData.f < efficiencyLimit)
            {
                paramsUsed[5] = 0; //We don't use the efficiency for NG fitting, 
                //and the statistical weight will be zero.
                lowEfficiency = true;
            }


            try
            {
                fitDataStrt fitDataNG = LNquantitate.fitNewtonGauss(mz,
                                                                    experimentalSpectrum,
                                                                    fitData,
                                                                    charge,
                                                                    instrParams.deltaR,
                                                                    rsh,
                                                                    fitwindowShow,
                                                                    paramsUsed,
                                                                    out sumSQ,
                                                                    out sdFittedData,
                                                                    out exitFit,
                                                                    qStrategy,
                                                                    instrParams.instResolution,
                                                                    configNG
                                                                    );
                fitData = fitDataNG;


                if (lowEfficiency)
                {
                    exitFit += " Low efficiency";
                    sdFittedData.f += 10000;
                }

            }
            catch
            {
                exitFit += "1st NG failed";
            }



            //Second Fit (try to adjust better deltaMz)

            //List of parameters used for fitting.
            paramsUsed[1] = 0; //A (A quantity)
            paramsUsed[2] = 1; //alpha (leptokurtosis)
            paramsUsed[3] = 0; //B (B quantity)
            paramsUsed[4] = 1; //deltaMz (experimental mass deviation)
            paramsUsed[5] = 1; //f (efficiency)
            paramsUsed[6] = 1; //sigma (gaussian width)
            paramsUsed[7] = 0; //signoise (Background threshold)


            try
            {
                fitDataStrt fitDataNG = LNquantitate.fitNewtonGauss(mz,
                                                                    experimentalSpectrum,
                                                                    fitData,
                                                                    charge,
                                                                    instrParams.deltaR,
                                                                    rsh,
                                                                    fitwindowShow,
                                                                    paramsUsed,
                                                                    out sumSQ,
                                                                    out sdFittedData,
                                                                    out exitFit,
                                                                    qStrategy,
                                                                    instrParams.instResolution,
                                                                    configNG
                                                                    
                                                                    );
                fitData = fitDataNG;


                if (lowEfficiency)
                {
                    exitFit += " Low efficiency";
                    sdFittedData.f += 10000;
                }

            }
            catch
            {
                exitFit += "2nd NG failed";
            }


            //Third Fit (No deltaMz)

            //List of parameters used for fitting.
            paramsUsed[1] = 1; //A (A quantity)
            paramsUsed[2] = 1; //alpha (leptokurtosis)
            paramsUsed[3] = 1; //B (B quantity)
            paramsUsed[4] = 0; //deltaMz (experimental mass deviation)
            paramsUsed[5] = 1; //f (efficiency)
            paramsUsed[6] = 1; //sigma (gaussian width)
            paramsUsed[7] = 0; //signoise (Background threshold)

            if (fitData.f < efficiencyLimit)
            {
                paramsUsed[5] = 0; //We don't use the efficiency for NG fitting, 
                //and the statistical weight will be zero.
                lowEfficiency = true;
            }


            try
            {
                fitDataStrt fitDataNG = LNquantitate.fitNewtonGauss(mz,
                                                                    experimentalSpectrum,
                                                                    fitData,
                                                                    charge,
                                                                    instrParams.deltaR,
                                                                    rsh,
                                                                    fitwindowShow,
                                                                    paramsUsed,
                                                                    out sumSQ,
                                                                    out sdFittedData,
                                                                    out exitFit,
                                                                    qStrategy,
                                                                    instrParams.instResolution,
                                                                    configNG
                                                                    );
                fitData = fitDataNG;

                if (lowEfficiency)
                {
                    exitFit += " Low efficiency";
                    sdFittedData.f += 10000;
                }

            }
            catch
            {
                exitFit += "3rd NG failed";
            }



            #region Statistical weights calculations
            
            double Imax = fitData.A >= fitData.B ? fitData.A : fitData.B;
            Comb.mzI[] fittedData;
            double sigma;
            double background = fitData.signoise;

            int LUpPoint = experimentalSpectrum.GetUpperBound(0);
            
            sigma = fitData.sigma;

            fittedData = Gaussians.calEnvelope(mz, experimentalSpectrum, fitData, charge, instrParams.deltaR, instrParams.instResolution);

            double externalLimL = experimentalSpectrum[experimentalSpectrum.GetLowerBound(0) + 1].mz;
            double externalLimR = experimentalSpectrum[experimentalSpectrum.GetUpperBound(0)].mz;

            int leftWindowRp = 0;

            for (int j = fittedData.GetLowerBound(0) + 1; j <= fittedData.GetUpperBound(0); j++)
            {
                double IntToBackGround = fittedData[j].I - background;
                if (IntToBackGround >= 0.01)
                {
                    leftWindowRp = j;
                    break;
                }
            }

            int rightWindowLp = 0;

            double[] theoreticalSpecMZ = new double[fittedData.GetLength(0)];

            for (int z = fittedData.GetLowerBound(0); z <= fittedData.GetUpperBound(0); z++)
            {
                theoreticalSpecMZ[z] = fittedData[z].mz;
            }

            double diffMZ = mz[1].mz - mz[0].mz;

            rightWindowLp = Utilities.find(theoreticalSpecMZ, mz[0].mz + 7.5*diffMZ);
            double leftWindowLmz = fittedData[leftWindowRp].mz - (2 / (double)charge);
            double rightWindowRmz = fittedData[rightWindowLp].mz + (2 / (double)charge);

            if (leftWindowLmz < externalLimL)
            {
                leftWindowLmz = externalLimL;
            }

            if (rightWindowRmz > externalLimR)
            {
                rightWindowRmz = externalLimR;
            }

            int leftWindowLp = Utilities.find(theoreticalSpecMZ, leftWindowLmz);
            int rightWindowRp = Utilities.find(theoreticalSpecMZ, rightWindowRmz);

            int numpointsLeft = (int)Math.Abs(leftWindowRp - leftWindowLp);
            int numpointsRight = (int)Math.Abs(rightWindowRp - rightWindowLp);
            int numpointsTotal = (int)Math.Abs(rightWindowRp - leftWindowLp);
            int numpointsPeptide = (int)Math.Abs(rightWindowLp - leftWindowRp);

            double leftWindow = LNquantitate.sumSquares(experimentalSpectrum, fittedData, leftWindowLp, leftWindowRp);
            double rightWindow = LNquantitate.sumSquares(experimentalSpectrum, fittedData, rightWindowLp, rightWindowRp);
            double peptideWindow = LNquantitate.sumSquares(experimentalSpectrum, fittedData, leftWindowRp, rightWindowLp);


            double normalizationWhole = (double)numpointsRight + (double)numpointsLeft;
            double normalizationR = (double)numpointsRight;
            double normalizationL = (double)numpointsLeft;
            double normalizationPeptide = (double)numpointsPeptide;
            double normalizationTotal = (double)numpointsTotal;

            double AdsumSQ = (leftWindow + rightWindow) / normalizationWhole;
            double AdsumSQright = rightWindow / normalizationR;
            double AdsumSQleft = leftWindow / normalizationL;
            double AdsumSQwh = LNquantitate.sumSquares(experimentalSpectrum, fittedData, leftWindowLp, rightWindowRp) / (normalizationTotal);
            double sumSQpeptide = LNquantitate.sumSquares(experimentalSpectrum, fittedData, leftWindowRp, rightWindowLp) / normalizationPeptide;


            //Better approach for estimating the area of A and B (for stat weights)
            //Calculate the integral under the curve of the fitted spectrum 
            fitDataStrt fitDataAB = new fitDataStrt();
            fitDataAB.A = fitData.A;
            fitDataAB.alpha = fitData.alpha;
            fitDataAB.B = 0;
            fitDataAB.deltaMz = fitData.deltaMz;
            fitDataAB.f = fitData.f;
            fitDataAB.sigma = fitData.sigma;
            fitDataAB.signoise = fitData.signoise;

            Comb.mzI[] fittedDataA = Gaussians.calEnvelope(mz, experimentalSpectrum, fitDataAB, charge, instrParams.deltaR, instrParams.instResolution);

            fitDataAB.A = 0;
            fitDataAB.B = fitData.B;

            Comb.mzI[] fittedDataB = Gaussians.calEnvelope(mz, experimentalSpectrum, fitDataAB, charge, instrParams.deltaR, instrParams.instResolution);

            double intensityA = 0;
            double intensityB = 0;
            for (int i = leftWindowRp; i < rightWindowLp - 1; i++) //Evaluated only on the peptide area
            {
                double diffA = fittedDataA[i + 1].mz - fittedDataA[i].mz;
                double diffB = fittedDataB[i + 1].mz - fittedDataB[i].mz;
                intensityA += (fittedDataA[i].I - fitDataAB.signoise) * diffA;
                intensityB += (fittedDataB[i].I - fitDataAB.signoise) * diffB;
            }


            //Weight V6
            //double weight = Utilities.calWeight(AdsumSQleft,
            //                                        sumSQpeptide,
            //                                        AdsumSQright,
            //                                        fitData.A,
            //                                        fitData.B,
            //                                        fitData.f);
            
            double weight = Utilities.calWeight(AdsumSQleft,
                                                    sumSQpeptide,
                                                    AdsumSQright,
                                                    intensityA,
                                                    intensityB,
                                                    fitData.f);

            //For controling those bad scans with very high fitted sigma  
            if (fitData.sigma > 2 * (mz[1].mz - mz[0].mz))
            {
                weight = 0;
            }
            
            #endregion


            double log2Ratio = Math.Log(fitData.A / fitData.B, 2);


            float XsNoCorrF = (float)Math.Log((fitData.A + fitData.B * (1 - fitData.f) * (1 - fitData.f)) / (fitData.B * (fitData.f * fitData.f + 2 * fitData.f * (1 - fitData.f))), 2);

            if (float.IsNaN(XsNoCorrF))
            {
                XsNoCorrF =  (float)log2Ratio;
                exitFit += "; q_f too high ";
            }

            double MHmass = LNquantitate.calMHmass(sequence, aminoacids, isotopes);

            //Write the Quantification Data in the row
            finalData[0] = initialData;
            finalData[0]["q_peptide_Mass"] = MHmass;
            finalData[0]["q_A"] = fitData.A;
            finalData[0]["q_B"] = fitData.B;
            finalData[0]["q_log2Ratio"] = log2Ratio;
            finalData[0]["Xs_NoCorrf"] = XsNoCorrF;
            finalData[0]["q_f"] = fitData.f;
            finalData[0]["q_deltaMZ"] = fitData.deltaMz;
            finalData[0]["q_Alpha"] = fitData.alpha;
            finalData[0]["q_Sigma"] = fitData.sigma;
            finalData[0]["q_DeltaR"] = instrParams.deltaR;
            finalData[0]["q_background"] = fitData.signoise;
            finalData[0]["q_SD_A"] = sdFittedData.A;
            finalData[0]["q_SD_B"] = sdFittedData.B;
            finalData[0]["q_SD_f"] = sdFittedData.f;
            finalData[0]["q_SD_Alpha"] = sdFittedData.alpha;
            finalData[0]["q_SD_Sigma"] = sdFittedData.sigma;
            finalData[0]["Vs"] = weight;
            //finalData[0]["DoubleFree1"] = weight2;
            finalData[0]["q_SQwindows"] = AdsumSQ;
            finalData[0]["q_SQwindowLeft"] = AdsumSQleft;
            finalData[0]["q_SQPeptide"] = sumSQpeptide;
            finalData[0]["q_SQwindowRight"] = AdsumSQright;
            finalData[0]["q_SQtotal"] = AdsumSQwh;


            if (finalData[0]["Label5"].ToString() != "")
            {
                finalData[0]["Label5"] = exitFit;
            }
            else
            {
                finalData[0]["Label5"] += exitFit;
            }

            

            return finalData;
        }

        public Comb.mzI[] centroiding(Comb.mzI[] experimentalSpectrum, Comb.mzI[] mz, double width)
        {
            throw new NotImplementedException();
        }

        public Comb.mzI[] getFittedSpectrum(DataRow fittedDataRow, Comb.mzI[] experimentalSpectrum)
        {

            //Create a fit data structure
            fitDataStrt fitData = new fitDataStrt();
            double deltaR = 0;

            //Calculate the composition for a given sequence
            string sSequence = fittedDataRow["Sequence"].ToString().Trim();
            string sequence = sSequence;

            if (sSequence.Contains('.'))
            {
                string[] sSeqSplit = sSequence.Split('.');
                sequence = sSeqSplit[1].ToString();
            }

            Comb.compStrt[] composition = AminoacidList.calComposition(sequence, aminoacids);


            //Calculate the Intensities and m/z for a given chemical composition.
            //double calibrationError = double.Parse(fittedDataRow["q_CalibrationError"].ToString());

            double calibrationError = 0;
            try
            {
                calibrationError = double.Parse(fittedDataRow["q_CalibrationError"].ToString());
            }
            catch { }


            int charge = int.Parse(fittedDataRow["Charge"].ToString());

            Comb.mzI[] mz = Comb.calIntensities(composition, 
                                                isotopes, 
                                                charge, 
                                                calibrationError, 
                                                (int)configNG.kmax);

            double dcalcMass = (mz[0].mz-1) * charge + 1;
            //this.label14.Text = "calc Mass : " + dcalcMass.ToString();

            fitData.A = double.Parse(fittedDataRow["q_A"].ToString());
            fitData.alpha = double.Parse(fittedDataRow["q_Alpha"].ToString());
            fitData.B = double.Parse(fittedDataRow["q_B"].ToString());
            fitData.deltaMz = double.Parse(fittedDataRow["q_deltaMZ"].ToString());
            fitData.f = double.Parse(fittedDataRow["q_f"].ToString());
            fitData.sigma = double.Parse(fittedDataRow["q_Sigma"].ToString());
            fitData.signoise = double.Parse(fittedDataRow["q_background"].ToString());
            deltaR = double.Parse(fittedDataRow["q_DeltaR"].ToString());


            //Calculate the isotopic envelope
            Comb.mzI[] envData = Gaussians.calEnvelope(mz, experimentalSpectrum, fitData, charge, deltaR, instrParams.instResolution);


            return envData;
        }
        public void config(qMethodsSchema.Quanmethods quanMethods, 
                            AminoacidList[] aminoacidList, 
                            isotList[][] isotopesList,
                            string method_id_name)
        {
            aminoacids = aminoacidList;
            isotopes = isotopesList;

            configNG.alpha = 0.1;
            configNG.kmax = 4;
            configNG.PRS = 1e-6;
            configNG.sumSQtolerance = 1e-3;
            configNG.nMaxIter = 100;

            var query =
              from meth in quanMethods.method
              join instr in quanMethods.instrument
              on meth.method_Id equals instr.method_Id
              join inFit in quanMethods.initialFitParams
              on instr.instrument_Id equals inFit.instrument_Id
              join param in quanMethods.if_parameter
              on inFit.initialFitParams_Id equals param.initialFitParams_Id
              //where meth.method_id_key == methodKey
              where meth.method_id_name == method_id_name                        
              select new
              {
                  paramId = param.IsidNull() ? "no id" : param.id,
                  paramString = param.Is_stringNull() ? "no string" : param._string,
                  paramValue = param.IsvalueNull() ? Double.NaN : param.value
              };

            foreach (var m in query)
            {
                if (m.paramId == "alpha") { instrParams.alpha = m.paramValue; }
                if (m.paramId == "sigma") { instrParams.sigma = m.paramValue; }
                if (m.paramId == "deltaR") { instrParams.deltaR = m.paramValue; }
                if (m.paramId == "efficiency") { instrParams.f = m.paramValue; }
                if (m.paramId == "deltaMZ") { instrParams.deltaMz = m.paramValue; }
                if (m.paramId == "SN_f") { instrParams.sn_f = m.paramValue; }                
            }

            var query2 =
                from meth in quanMethods.method
                join instr in quanMethods.instrument
                on meth.method_Id equals instr.method_Id
                join dfFit in quanMethods.initialFitParams
                on instr.instrument_Id equals dfFit.instrument_Id
                join param in quanMethods.df_parameter
                on dfFit.initialFitParams_Id equals param.deltaFitParams_Id
                //where meth.method_id_key == methodKey
                where meth.method_id_name == method_id_name
                select new
                {
                    paramId = param.IsidNull() ? "no id" : param.id,
                    paramString = param.Is_stringNull() ? "no string" : param._string,
                    paramValue = param.IsvalueNull() ? Double.NaN : param.value
                };

            foreach (var m in query2)
            {
                if (m.paramId == "A") { instrParams.varA = m.paramValue; }
                if (m.paramId == "B") { instrParams.varB = m.paramValue; }
                if (m.paramId == "efficiency") { instrParams.varf = m.paramValue; }
                if (m.paramId == "sigma") { instrParams.varSigma = m.paramValue; }
                if (m.paramId == "alpha") { instrParams.varAlpha = m.paramValue; }
                if (m.paramId == "SN") { instrParams.varSn = m.paramValue; }
                if (m.paramId == "rsh") { rsh = m.paramValue; }
                if (m.paramId == "alpha_NG") { configNG.alpha = m.paramValue; }
                if (m.paramId == "PRS_NG") { configNG.PRS = m.paramValue; }
                if (m.paramId == "sumSQtolerance_NG") { configNG.sumSQtolerance = m.paramValue; }
            }

            var query3 =
                from meth in quanMethods.method
                join instr in quanMethods.instrument
                on meth.method_Id equals instr.method_Id
                //where meth.method_id_key == methodKey
                where meth.method_id_name == method_id_name
                select new
                {
                    kmax = instr.IskmaxNull() ? 12 : instr.kmax,
                    resolution = instr.resolution,
                    instName = instr.instrument_id_name,
                    nMaxIter = instr.IsmaxIterationsNull() ? 12 : instr.maxIterations
                };

                        
            foreach (var m in query3)
            {
                configNG.kmax = (int)m.kmax;
                configNG.nMaxIter = (int)m.nMaxIter;
                switch(m.resolution)
                {
                    case "LOW": 
                        instrParams.instResolution = Resolution.LOW;
                        break;
                    case "HIGH":
                        instrParams.instResolution = Resolution.HIGH;
                        break;

                }
                instrParams.instName = m.instName;
            }
         
            

        }
        public void addDataTable(DataTable dt)
        {
            original_dt = dt.Clone();
        }

        public double getMonoisotMass(DataRow initialData)
        {
            double monoisotMass=0.0;

            //Calculate the composition for a given sequence
            string sSequence = initialData["Sequence"].ToString().Trim();
            string sequence = sSequence;

            if (sSequence.Contains('.'))
            {
                string[] sSeqSplit = sSequence.Split('.');
                sequence = sSeqSplit[1].ToString();
            }

            Comb.compStrt[] composition = AminoacidList.calComposition(sequence, aminoacids);

            //Calculate the Intensities and m/z for a given chemical composition.
            double calibrationError = 0;
            try
            {
                calibrationError = double.Parse(initialData["q_CalibrationError"].ToString());
            }
            catch { }
           
            int charge = int.Parse(initialData["Charge"].ToString());

            Comb.mzI[] mz = Comb.calIntensities(composition,
                                                isotopes,
                                                charge,
                                                calibrationError,
                                                1);


            monoisotMass = mz[0].mz;

            return monoisotMass;
        }

        #endregion

    }

    public class Q18OHR : IQuantitation
    {
        AminoacidList[] aminoacids;
        instrumentParamsStrt instrParams;
        isotList[][] isotopes;
        confNGfitParamsStrt configNG;
        bool fitwindowShow;
        LNquantitate.quantitationStrategy qStrategy = LNquantitate.quantitationStrategy.O18_HR;
        DataTable original_dt;
        double rsh = 1e-3;

        #region IQuantitation Members

        public DataRow[] quantitate(DataRow initialData, Comb.mzI[] experimentalSpectrum)
        {
            double calibrationError;
            int charge;
            DataRow[] finalData;
            fitwindowShow = false;

            finalData = new DataRow[1];

            try
            {
                calibrationError = double.Parse(initialData["q_CalibrationError"].ToString());
            }
            catch
            {
                calibrationError = 0;
                initialData["q_CalibrationError"] = calibrationError;
            }

            //Create a fit data structure
            fitDataStrt fitData = new fitDataStrt();

            //Calculate the composition for a given sequence
            string sSequence = initialData["Sequence"].ToString().Trim();
            string sequence = sSequence;

            if (sSequence.Contains('.'))
            {
                string[] sSeqSplit = sSequence.Split('.');
                sequence = sSeqSplit[1].ToString();
            }

            Comb.compStrt[] composition;
            if (aminoacids.Length > 0)
            {
                composition = AminoacidList.calComposition(sequence, aminoacids);
            }
            else { return null; }

            if (composition == null) return null;

            try
            {
                charge = int.Parse(initialData["Charge"].ToString());
            }
            catch { return null; }

            Comb.mzI[] mz = Comb.calIntensities(composition, isotopes, charge, calibrationError, configNG.kmax);


            //get initial parameters

            fitData.f = instrParams.f;
            fitData.A = 0;
            fitData.B = 0;
            fitData.deltaMz = instrParams.deltaMz;
            fitData.signoise = 0;
            fitData.alpha = 0;

            Comb.mzI[] clusterIntensities = Gaussians.calEnvelope(mz, null, fitData, charge, instrParams.deltaR, Resolution.HIGH);

            Comb.mzI[] expCentroidedSpectrum = centroiding(experimentalSpectrum, clusterIntensities);

            fitData = Gaussians.getInitialConditions(fitData, expCentroidedSpectrum, mz, instrParams.sn_f, charge, instrParams.deltaR, Resolution.HIGH);


            fitDataStrt fitDataSweep;
            double sumSQ;


            /*
             * 1 == A
             * 2 == alpha
             * 3 == B
             * 4 == deltaMZ
             * 5 == f
             * 6 == sigma
             * 7 == signoise
             */

            int[] iterations = new int[8];
            iterations[1] = 10;
            iterations[2] = 0;
            iterations[3] = 10;
            iterations[4] = 0;
            iterations[5] = 10;
            iterations[6] = 0;
            iterations[7] = 0;
            if (instrParams.instResolution == Resolution.HIGH)
            {
                iterations[7] = 0;
            }


            fitDataSweep = LNquantitate.fitbySweep(mz,
                                                    expCentroidedSpectrum,
                                                    fitData,
                                                    charge,
                                                    instrParams.deltaR,
                                                    iterations,
                                                    fitwindowShow,
                                                    instrParams,
                                                    out sumSQ,
                                                    0);
            fitData = fitDataSweep;

            /* 
             *   Uncomment if you want to jump the sweep fit.
             * 
            double sumSQ=(double)dv[rowIndex].Row["q_SumSquares"];
            fitData.A = (double)dv[rowIndex].Row["q_A"];
            fitData.alpha = (double)dv[rowIndex].Row["q_Alpha"];
            fitData.B = (double)dv[rowIndex].Row["q_B"];
            fitData.deltaMz = (double)dv[rowIndex].Row["q_DeltaMZ"];
            fitData.f = (double)dv[rowIndex].Row["q_f"];
            fitData.sigma = (double)dv[rowIndex].Row["q_Sigma"];
            fitData.signoise = (double)dv[rowIndex].Row["q_background"];
            */


            fitDataStrt sdFittedData = new fitDataStrt();

            string exitFit = "";

            // First Fit (No deltaMz)

            //List of parameters used for fitting.
            int[] paramsUsed = new int[8];
            paramsUsed[1] = 1; //A (A quantity)
            paramsUsed[2] = 0; //alpha (leptokurtosis)
            paramsUsed[3] = 1; //B (B quantity)
            paramsUsed[4] = 0; //deltaMz (experimental mass deviation)
            paramsUsed[5] = 1; //f (efficiency)
            paramsUsed[6] = 0; //sigma (gaussian width)
            paramsUsed[7] = 0; //signoise (Background threshold)

            double efficiencyLimit = 0.3;
            bool lowEfficiency = false;
            if (fitData.f < efficiencyLimit)
            {
                paramsUsed[5] = 0; //We don't use the efficiency for NG fitting, 
                //and the statistical weight will be zero.
                lowEfficiency = true;
            }


            try
            {
                fitDataStrt fitDataNG = LNquantitate.fitNewtonGauss(mz,
                                                                    expCentroidedSpectrum,
                                                                    fitData,
                                                                    charge,
                                                                    instrParams.deltaR,
                                                                    rsh,
                                                                    fitwindowShow,
                                                                    paramsUsed,
                                                                    out sumSQ,
                                                                    out sdFittedData,
                                                                    out exitFit,
                                                                    qStrategy,
                                                                    instrParams.instResolution,
                                                                    configNG
                                                                    );
                fitData = fitDataNG;


                if (lowEfficiency)
                {
                    exitFit += " Low efficiency";
                    sdFittedData.f += 10000;
                }

            }
            catch
            {
                exitFit += "1st NG failed";
            }

            #region Statistical weights calculations


            Comb.mzI[] theorSpectrum = Gaussians.calEnvelope(mz, expCentroidedSpectrum, fitData, charge, instrParams.deltaR, instrParams.instResolution);

            //Check dimensions
            int nPoints = LNquantitate.numOfMatchingPeaks(fitData.A, fitData.B, expCentroidedSpectrum, theorSpectrum);


            double MSQ1 = sumSQ / nPoints;   //(expCentroidedSpectrum.Length-1);
            
            double weight = Utilities.calWeight(LNquantitate.quantitationStrategy.O18_HR,
                                                fitData.A,
                                                fitData.B,
                                                MSQ1,
                                                0,
                                                0,
                                                fitData.f);
                                                

            #endregion


            double log2Ratio = Math.Log(fitData.A / fitData.B, 2);


            float XsNoCorrF = (float)Math.Log((fitData.A + fitData.B * (1 - fitData.f) * (1 - fitData.f)) / (fitData.B * (fitData.f * fitData.f + 2 * fitData.f * (1 - fitData.f))), 2);

            if (float.IsNaN(XsNoCorrF))
            {
                XsNoCorrF = (float)log2Ratio;
                exitFit += " q_f too high ";
            }

            double MHmass = LNquantitate.calMHmass(sequence, aminoacids, isotopes);

            //Write the Quantification Data in the row
            finalData[0] = initialData;
            finalData[0]["q_peptide_Mass"] = MHmass;
            finalData[0]["q_A"] = fitData.A;
            finalData[0]["q_B"] = fitData.B;
            finalData[0]["q_log2Ratio"] = log2Ratio;
            finalData[0]["Xs_NoCorrf"] = XsNoCorrF;
            finalData[0]["q_f"] = fitData.f;
            finalData[0]["q_DeltaR"] = instrParams.deltaR;
            finalData[0]["q_SD_A"] = sdFittedData.A;
            finalData[0]["q_SD_B"] = sdFittedData.B;
            finalData[0]["q_SD_f"] = sdFittedData.f;
            finalData[0]["q_Width"] = instrParams.width;
            finalData[0]["Vs"] = weight;
            finalData[0]["q_SQtotal"] = sumSQ;


            if (finalData[0]["Label5"].ToString() != "")
            {
                finalData[0]["Label5"] = exitFit;
            }
            else
            {
                finalData[0]["Label5"] += exitFit;
            }



            return finalData;
        }

        private Comb.mzI[] centroiding(Comb.mzI[] experimentalSpectrum, Comb.mzI[] mz)
        {
            // when forcedWidth = 0, then it uses the value in instrparams
            return centroiding(experimentalSpectrum, mz, 0);
        }

        public Comb.mzI[] centroiding(Comb.mzI[] experimentalSpectrum, Comb.mzI[] mz, double forcedWidth)
        {
            double width = instrParams.width;
            if (forcedWidth > 0) width = forcedWidth;
            Comb.mzI[] resultingSpectrum = new Comb.mzI[mz.Length];
            double[] peakLowLimit = new double[mz.Length];
            double[] peakHighLimit = new double[mz.Length];
            int charge = (int)Math.Round(1 / (mz[1].mz - mz[0].mz));
            double precursorMass = mz[0].mz * (double)charge - (double)charge;
            int mostIntensePeak = (int)Math.Round(precursorMass / 1000) - 1;
            if (mostIntensePeak < 0) mostIntensePeak = 0; // because it is zero based
            //double widthIncrease = 10;

            for (int i = 0; i < mz.Length; i++)
            {
                peakLowLimit[i] = mz[i].mz - width / 2;
                peakHighLimit[i] = mz[i].mz + width / 2;
            }

            for (int i = 0; i < mz.Length; i++)
            {
                resultingSpectrum[i].mz = mz[i].mz;
            }

            // old way
            for (int i = 0; i < experimentalSpectrum.Length; i++)
            {
                for (int currPeak = 0; currPeak < mz.Length; currPeak++)
                {
                    if (experimentalSpectrum[i].mz >= peakLowLimit[currPeak] && experimentalSpectrum[i].mz <= peakHighLimit[currPeak])
                    {
                        if (experimentalSpectrum[i].I > resultingSpectrum[currPeak].I)
                            resultingSpectrum[currPeak].I = experimentalSpectrum[i].I;
                        //resultingSpectrum[currPeak].I += experimentalSpectrum[i].I;
                    }
                }
            }

            //for (int i = 0; i < experimentalSpectrum.Length; i++)
            //{
            //    if (experimentalSpectrum[i].mz >= peakLowLimit[mostIntensePeak] * (double)widthIncrease
            //        && experimentalSpectrum[i].mz <= peakHighLimit[mostIntensePeak] * (double)widthIncrease)
            //    {
            //        if (experimentalSpectrum[i].I > resultingSpectrum[mostIntensePeak].I)
            //            resultingSpectrum[mostIntensePeak].I = experimentalSpectrum[i].I;
            //        //resultingSpectrum[currPeak].I += experimentalSpectrum[i].I;
            //    }
            //}

            return resultingSpectrum;
        }

        public void config(QuiXoT.math.qMethodsSchema.Quanmethods quanMethods,
                                    AminoacidList[] aminoacidList,
                                    isotList[][] isotopesList,
                                    string method_id_name)
        {
            aminoacids = aminoacidList;
            isotopes = isotopesList;

            configNG.alpha = 0.1;
            configNG.kmax = 8;
            configNG.PRS = 1e-3;
            configNG.sumSQtolerance = 1e-4;
            configNG.nMaxIter = 1000;
  
            var query =
              from meth in quanMethods.method
              join instr in quanMethods.instrument
              on meth.method_Id equals instr.method_Id
              join inFit in quanMethods.initialFitParams
              on instr.instrument_Id equals inFit.instrument_Id
              join param in quanMethods.if_parameter
              on inFit.initialFitParams_Id equals param.initialFitParams_Id
              //where meth.method_id_key == methodKey
              where meth.method_id_name == method_id_name
              select new
              {
                  paramId = param.IsidNull() ? "no id" : param.id,
                  paramString = param.Is_stringNull() ? "no string" : param._string,
                  paramValue = param.IsvalueNull() ? Double.NaN : param.value
              };

            foreach (var m in query)
            {
                if (m.paramId == "width") { instrParams.width = m.paramValue; }
                if (m.paramId == "deltaR") { instrParams.deltaR = m.paramValue; }
                if (m.paramId == "efficiency") { instrParams.f = m.paramValue; }
                if (m.paramId == "deltaMZ") { instrParams.deltaMz = m.paramValue; }
            }

            var query2 =
                from meth in quanMethods.method
                join instr in quanMethods.instrument
                on meth.method_Id equals instr.method_Id
                join dfFit in quanMethods.initialFitParams
                on instr.instrument_Id equals dfFit.instrument_Id
                join param in quanMethods.df_parameter
                on dfFit.initialFitParams_Id equals param.deltaFitParams_Id
                //where meth.method_id_key == methodKey
                where meth.method_id_name == method_id_name
                select new
                {
                    paramId = param.IsidNull() ? "no id" : param.id,
                    paramString = param.Is_stringNull() ? "no string" : param._string,
                    paramValue = param.IsvalueNull() ? Double.NaN : param.value
                };

            foreach (var m in query2)
            {
                if (m.paramId == "A") { instrParams.varA = m.paramValue; }
                if (m.paramId == "B") { instrParams.varB = m.paramValue; }
                if (m.paramId == "efficiency") { instrParams.varf = m.paramValue; }
                if (m.paramId == "rsh") { rsh = m.paramValue; }
                if (m.paramId == "alpha_NG") { configNG.alpha = m.paramValue; }
                if (m.paramId == "PRS_NG") { configNG.PRS = m.paramValue; }
                if (m.paramId == "sumSQtolerance_NG") { configNG.sumSQtolerance = m.paramValue; }
            }

            var query3 =
                from meth in quanMethods.method
                join instr in quanMethods.instrument
                on meth.method_Id equals instr.method_Id
                //where meth.method_id_key == methodKey
                where meth.method_id_name == method_id_name
                select new
                {
                    kmax = instr.IskmaxNull() ? 12 : instr.kmax,
                    resolution = instr.resolution,
                    instName = instr.instrument_id_name,
                    nMaxIter = instr.IsmaxIterationsNull() ? 12 : instr.maxIterations
                };


            foreach (var m in query3)
            {
                configNG.kmax = (int)m.kmax;
                //configNG.kmax = (int)m.kmax;
                configNG.nMaxIter = (int)m.nMaxIter;
                switch (m.resolution)
                {
                    case "LOW":
                        instrParams.instResolution = Resolution.LOW;
                        break;
                    case "HIGH":
                        instrParams.instResolution = Resolution.HIGH;
                        break;

                }
                instrParams.instName = m.instName;
            }
        }

        public void addDataTable(DataTable dt)
        {
            original_dt = dt.Clone();
        }

        public Comb.mzI[] getFittedSpectrum(DataRow fittedDataRow, Comb.mzI[] experimentalSpectrum)
        {
            //Create a fit data structure
            fitDataStrt fitData = new fitDataStrt();
            double deltaR = 0;
            
            //Calculate the composition for a given sequence
            string sSequence = fittedDataRow["Sequence"].ToString().Trim();
            string sequence = sSequence;

            if (sSequence.Contains('.'))
            {
                string[] sSeqSplit = sSequence.Split('.');
                sequence = sSeqSplit[1].ToString();
            }

            
            Comb.compStrt[] composition = AminoacidList.calComposition(sequence, aminoacids);


            //Calculate the Intensities and m/z for a given chemical composition.
            double calibrationError = 0; 
            try
            {
                calibrationError = double.Parse(fittedDataRow["q_CalibrationError"].ToString());
            }
            catch 
            { }

            int charge = int.Parse(fittedDataRow["Charge"].ToString());

            Comb.mzI[] mz = Comb.calIntensities(composition,
                                                isotopes,
                                                charge,
                                                calibrationError,
                                                (int)configNG.kmax);

            Comb.mzI[] clusterIntensities = Gaussians.calEnvelope(mz, null, fitData, charge, instrParams.deltaR, Resolution.HIGH);

            Comb.mzI[] expCentroidedSpectrum = centroiding(experimentalSpectrum, clusterIntensities);

            double dcalcMass = (mz[0].mz - 1) * charge + 1;
            //this.label14.Text = "calc Mass : " + dcalcMass.ToString();

            fitData.A = double.Parse(fittedDataRow["q_A"].ToString());
            fitData.B = double.Parse(fittedDataRow["q_B"].ToString());
            double width = double.Parse(fittedDataRow["q_Width"].ToString());
            fitData.deltaMz = 0;
            fitData.f = double.Parse(fittedDataRow["q_f"].ToString());
            deltaR = double.Parse(fittedDataRow["q_DeltaR"].ToString());


            //Calculate the isotopic envelope
            Comb.mzI[] envData = Gaussians.calEnvelope(mz, expCentroidedSpectrum, fitData, charge, deltaR, instrParams.instResolution);

            // Attention: decentroiding is no loger used!
            //Comb.mzI[] decentroidedSpectrum = decentroiding(envData, expCentroidedSpectrum, experimentalSpectrum,out experimentalSpectrum, width);
            //return decentroidedSpectrum;

            return envData;
        }

        // Old method in 2009-2010 (until release 1.3.20),
        // with decentroided spectrum;
        // it was abandoned because it was misleading
        // as it looked like there was a fit for the whole peak
        // instead of just taking into account the maximum intensity
        // THIS METHOD IS NOT USED ANY MORE, and is kept here for record only
        public Comb.mzI[] decentroiding(Comb.mzI[] thCentroidedSpectrum, 
                                        Comb.mzI[] expCentroidedSpectrum, 
                                        Comb.mzI[] expSpectrum,
                                        out Comb.mzI[] expSpectrumOut,
                                        double width)
        {
            
            Comb.mzI[] decentroidedSpectrum = (Comb.mzI[])expSpectrum.Clone();
            expSpectrumOut = (Comb.mzI[])expSpectrum.Clone();

            //set intensities of decentroidedSpectrum to zero
            for (int i = 0; i < decentroidedSpectrum.Length; i++)
            {
                decentroidedSpectrum[i].I = 0;
            }

            int currentPeak = 0;
            double minMass = thCentroidedSpectrum[currentPeak].mz - width / 2;
            double maxMass = thCentroidedSpectrum[currentPeak].mz + width / 2;
            bool[] peakFound = new bool[thCentroidedSpectrum.Length];
            int[] pointsPerPeak = new int[thCentroidedSpectrum.Length];
            double coeffRatio = 0;
            double diffPeaksTolerance = Math.Log10(10.0);

            #region fit for each experimental peak

            //detecting peaks
            for (int k = 0; k < thCentroidedSpectrum.Length; k++)
            {
                minMass = thCentroidedSpectrum[k].mz - width / 2;
                maxMass = thCentroidedSpectrum[k].mz + width / 2;

                for (int i = 0; i < expSpectrum.Length; i++)
                {
                    if (expSpectrum[i].mz >= minMass && expSpectrum[i].mz <= maxMass)
                    {
                        coeffRatio = Math.Log10(thCentroidedSpectrum[k].I) - Math.Log10(expCentroidedSpectrum[k].I);
                        if (Math.Abs(coeffRatio) < diffPeaksTolerance)
                        {
                            peakFound[k] = true;
                        }
                        pointsPerPeak[k]++;
                    }
                }
            }

            int peaksFound=0;
            int totalPointsPeak = 0;
            for(int k=0;k<thCentroidedSpectrum.Length;k++)
            {
                if (peakFound[k])
                { 
                    peaksFound++;
                    totalPointsPeak += pointsPerPeak[k];
                }

            }

            double avgPoints = Math.Floor((double)totalPointsPeak / (double)peaksFound);
            
            for (int k = 0; k < thCentroidedSpectrum.Length; k++)
            {
                minMass = thCentroidedSpectrum[k].mz - width / 2;
                maxMass = thCentroidedSpectrum[k].mz + width / 2;

                if (peakFound[k])
                {
                    coeffRatio = thCentroidedSpectrum[k].I / expCentroidedSpectrum[k].I;

                    for (int i = 0; i < expSpectrum.Length; i++)
                    {
                        if (expSpectrum[i].mz >= minMass && expSpectrum[i].mz <= maxMass)
                        {
                            decentroidedSpectrum[i].I = expSpectrum[i].I * coeffRatio;
                        }
                    }
                }
                else 
                {
                    //"create" a ficticious peak representing theoretical fitting for the peak

                    Comb.mzI[] newValues = new Comb.mzI[2];
                    newValues[0].mz = minMass;
                    newValues[1].mz = thCentroidedSpectrum[k].mz;
                    newValues[0].I = 0;
                    newValues[1].I = 0;
                    
                    expSpectrum = addPeakToSpectrum(expSpectrum, newValues);

                    double peakIntensity = thCentroidedSpectrum[k].I; 
                    if (peakIntensity < 0) peakIntensity = 0;

                    newValues[0].I = peakIntensity / avgPoints;
                    newValues[1].I = peakIntensity / avgPoints;
 
                    decentroidedSpectrum = addPeakToSpectrum(decentroidedSpectrum, newValues);
                }
 
            }

            #endregion

            expSpectrumOut =(Comb.mzI[])expSpectrum.Clone();

            return decentroidedSpectrum;
        }

        private static Comb.mzI[] addPeakToSpectrum(Comb.mzI[] expSpectrum, Comb.mzI[] peak)
        {
            Comb.mzI[] specNew = new Comb.mzI[expSpectrum.Length + 2];
            int currPoint = 0;

            //check dimension of peak[]
            if (peak.Length != 2) return expSpectrum;


            //to the first point of the peak
            for (int i = 0; i < expSpectrum.Length; i++)
            {
                specNew[i].I = expSpectrum[i].I;
                specNew[i].mz = expSpectrum[i].mz;

                if (expSpectrum[i].mz > peak[0].mz && expSpectrum[i].mz < peak[1].mz)
                {
                    specNew[i + 1].I = 0;
                    specNew[i + 1].mz = peak[0].mz;
                    specNew[i + 2].I = peak[0].I;
                    specNew[i + 2].mz = peak[0].mz;

                    currPoint = i;
                    break;
                }                
            }

            //middle of the peak and after...
            bool lastP = false;
            for(int i=currPoint+1; i<expSpectrum.Length-2; i++)
            {
                if (expSpectrum[i].mz > peak[0].mz && expSpectrum[i].mz < peak[1].mz)
                {
                    specNew[i + 2].I = peak[0].I;
                    specNew[i + 2].mz = expSpectrum[i].mz;
                }
                if (expSpectrum[i].mz > peak[1].mz)
                {
                    if (!lastP)
                    {
                        specNew[i+2].I = expSpectrum[i].I;
                        specNew[i+2].mz = expSpectrum[i].mz;

                        specNew[i + 3].I = peak[1].I;
                        specNew[i + 3].mz = peak[1].mz;

                        specNew[i + 4].I = 0;
                        specNew[i + 4].mz = peak[1].mz;

                        lastP = true;
                    }
                    else 
                    {
                        specNew[i + 4].I = expSpectrum[i+1].I;
                        specNew[i + 4].mz = expSpectrum[i+1].mz;
                    } 
                }
            }

            return specNew;
        }

        public double getMonoisotMass(DataRow initialData)
        {
            double monoisotMass = 0.0;

            //Calculate the composition for a given sequence
            string sSequence = initialData["Sequence"].ToString().Trim();
            string sequence = sSequence;

            if (sSequence.Contains('.'))
            {
                string[] sSeqSplit = sSequence.Split('.');
                sequence = sSeqSplit[1].ToString();
            }


            Comb.compStrt[] composition = AminoacidList.calComposition(sequence, aminoacids);

            //Calculate the Intensities and m/z for a given chemical composition.
            double calibrationError = 0;
            try
            {
                calibrationError = double.Parse(initialData["q_CalibrationError"].ToString());
            }
            catch { }

            int charge = int.Parse(initialData["Charge"].ToString());

            Comb.mzI[] mz = Comb.calIntensities(composition,
                                                isotopes,
                                                charge,
                                                calibrationError,
                                                1);


            monoisotMass = mz[0].mz;

            return monoisotMass;
        }

        #endregion
    }
 
    public class Q18OMSMS : IQuantitation
    {

        DataTable original_dt;

        #region Miembros de IQuantitation

        public DataRow[] quantitate(DataRow initialData, Comb.mzI[] experimentalSpectrum)
        {
            /*
            object[] tmpArray = finalData[0].ItemArray;



            DataRow newRow = original_dt.NewRow();
            finalData[1] = newRow;
            DataRow newRow2 = original_dt.NewRow();
            finalData[2] = newRow2;

            object[] quanResultArray = finalData[0].ItemArray;

            for (int i = 1; i < finalData.Length; i++)
            {
                for (int j = 0; j < quanResultArray.GetUpperBound(0); j++)
                {
                    finalData[i][j] = quanResultArray[j];
                }
            }

            finalData[1]["Label4"] = "copy 1";
            finalData[2]["Label4"] = "copy 2";
            */

            throw new NotImplementedException();
        }
        public void config(qMethodsSchema.Quanmethods quanMethods, 
                            AminoacidList[] aminoacidList, 
                            isotList[][] isotopes,
                            string method_id_name)
        {
            throw new NotImplementedException();
        }
        public void addDataTable(DataTable dt)
        {
            original_dt = dt.Clone();
        }

        public Comb.mzI[] centroiding(Comb.mzI[] experimentalSpectrum, Comb.mzI[] mz, double width)
        {
            throw new NotImplementedException();
        }

        public Comb.mzI[] getFittedSpectrum(DataRow fittedDataRow, Comb.mzI[] experimentalSpectrum)
        {
            throw new NotImplementedException();
        }

        public double getMonoisotMass(DataRow initialData)
        {
            double monoisotMass = 0.0;

            return monoisotMass;
        }


        #endregion
    }
    
    public class QSilac : IQuantitation
    {
        DataTable original_dt;
        AminoacidList[] aminoacids;
        instrumentParamsStrt instrParams;
        isotList[][] isotopes;
        confNGfitParamsStrt configNG;
        bool fitwindowShow;
        //string methodKey = "SILAC";
        LNquantitate.quantitationStrategy qStrategy = LNquantitate.quantitationStrategy.SILAC;
        char[] labeledAminoacidesList;
        double rsh = 1e-1;

        #region Miembros de IQuantitation

        public DataRow[] quantitate(DataRow initialData, Comb.mzI[] experimentalSpectrum)
        {
            double calibrationError;
            int charge;
            DataRow[] finalData;

            finalData = new DataRow[1];
            finalData[0] = initialData;

            try
            {
                calibrationError = double.Parse(initialData["q_CalibrationError"].ToString());
            }
            catch
            {
                calibrationError = 0;
                initialData["q_CalibrationError"] = calibrationError;
            }

            //Create a fit data structure
            fitDataStrt fitData = new fitDataStrt();

            //Calculate the composition for a given sequence
            string sSequence = initialData["Sequence"].ToString().Trim();
            string sequence = sSequence;

            if (sSequence.Contains('.'))
            {
                string[] sSeqSplit = sSequence.Split('.');
                sequence = sSeqSplit[1].ToString();
            }

            Comb.compStrt[] composition;
            if (aminoacids.Length > 0)
            {
                composition = AminoacidList.calComposition(sequence, aminoacids);
            }
            else { return null; }

            if (composition == null) return null;

            try
            {
                charge = int.Parse(initialData["Charge"].ToString());
            }
            catch { return null; }

            //calculate the labeling (number of aminoacides labeled in the sequence)
            int numOfAaLabeled = LNquantitate.countAminoacides(sequence,labeledAminoacidesList);
            double label = instrParams.deltaR * (double)numOfAaLabeled;

            if (label == 0)
            {
                finalData[0]["Label5"] = "peptide with no labeling";
                return finalData;
            }


            Comb.mzI[] mz = Comb.calIntensities(composition, isotopes, charge, calibrationError, configNG.kmax);


            //get initial parameters

            fitData.alpha = instrParams.alpha;
            fitData.f = instrParams.f;
            fitData.sigma = instrParams.sigma;
            fitData.A = 0;
            fitData.B = 0;
            fitData.deltaMz = instrParams.deltaMz;

            fitData = Gaussians.getInitialConditions(fitData, experimentalSpectrum, mz, instrParams.sn_f, charge, label, Resolution.LOW);

            fitDataStrt fitDataSweep;
            double sumSQ;

            int[] iterations = new int[8];
            iterations[1] = 10;   
            iterations[2] = 0;
            iterations[3] = 10;
            iterations[4] = 100;
            iterations[5] = 0;
            iterations[6] = 0;
            iterations[7] = 10;

            /*
            fitData.A = iterations[1];
            fitData.alpha = iterations[2];
            fitData.B = iterations[3];
            fitData.deltaMz = iterations[4];
            fitData.f = iterations[5];
            fitData.sigma = iterations[6];
            fitData.signoise = iterations[7];

             */
            fitwindowShow = false;

            fitDataSweep = LNquantitate.fitbySweep(mz,
                                                    experimentalSpectrum,
                                                    fitData,
                                                    charge,
                                                    label,
                                                    iterations,
                                                    fitwindowShow,
                                                    instrParams,
                                                    out sumSQ,
                                                    10);
            fitData = fitDataSweep;

            /* 
             *   Uncomment if you want to jump the sweep fit.
             * 
            double sumSQ=(double)dv[rowIndex].Row["q_SumSquares"];
            fitData.A = (double)dv[rowIndex].Row["q_A"];
            fitData.alpha = (double)dv[rowIndex].Row["q_Alpha"];
            fitData.B = (double)dv[rowIndex].Row["q_B"];
            fitData.deltaMz = (double)dv[rowIndex].Row["q_DeltaMZ"];
            fitData.f = (double)dv[rowIndex].Row["q_f"];
            fitData.sigma = (double)dv[rowIndex].Row["q_Sigma"];
            fitData.signoise = (double)dv[rowIndex].Row["q_background"];
            */


            fitDataStrt sdFittedData = new fitDataStrt();

            string exitFit = "";

            // First Fit (No deltaMz)

            //List of parameters used for fitting.
             int[] paramsUsed = new int[8];
            paramsUsed[1] = 1; //A (A quantity)
            paramsUsed[2] = 0; //alpha (leptokurtosis)
            paramsUsed[3] = 1; //B (B quantity)
            paramsUsed[4] = 0; //deltaMz (experimental mass deviation)
            paramsUsed[5] = 0; //f (efficiency)
            paramsUsed[6] = 0; //sigma (gaussian width)
            paramsUsed[7] = 0; //signoise (Background threshold)

            fitwindowShow = false;

            try
            {
                fitDataStrt fitDataNG = LNquantitate.fitNewtonGauss(mz,
                                                                    experimentalSpectrum,
                                                                    fitData,
                                                                    charge,
                                                                    label,
                                                                    rsh,
                                                                    fitwindowShow,
                                                                    paramsUsed,
                                                                    out sumSQ,
                                                                    out sdFittedData,
                                                                    out exitFit,
                                                                    qStrategy,
                                                                    instrParams.instResolution,
                                                                    configNG
                                                                    );
                fitData = fitDataNG;


            }
            catch
            {
                exitFit += "1st NG failed";
            }



            ////Second Fit (try to adjust better deltaMz)

            ////List of parameters used for fitting.
            paramsUsed[1] = 1; //A (A quantity)
            paramsUsed[2] = 0; //alpha (leptokurtosis)
            paramsUsed[3] = 1; //B (B quantity)
            paramsUsed[4] = 1; //deltaMz (experimental mass deviation)
            paramsUsed[5] = 0; //f (efficiency)
            paramsUsed[6] = 1; //sigma (gaussian width)
            paramsUsed[7] = 0; //signoise (Background threshold)

            fitwindowShow = false;

            try
            {
                fitDataStrt fitDataNG = LNquantitate.fitNewtonGauss(mz,
                                                                    experimentalSpectrum,
                                                                    fitData,
                                                                    charge,
                                                                    label,
                                                                    rsh,
                                                                    fitwindowShow,
                                                                    paramsUsed,
                                                                    out sumSQ,
                                                                    out sdFittedData,
                                                                    out exitFit,
                                                                    qStrategy,
                                                                    instrParams.instResolution,
                                                                    configNG
                                                                    );
                fitData = fitDataNG;



            }
            catch
            {
                exitFit += "2nd NG failed";
            }


            ////Third Fit (No deltaMz)

            ////List of parameters used for fitting.
            paramsUsed[1] = 1; //A (A quantity)
            paramsUsed[2] = 1; //alpha (leptokurtosis)
            paramsUsed[3] = 1; //B (B quantity)
            paramsUsed[4] = 0; //deltaMz (experimental mass deviation)
            paramsUsed[5] = 0; //f (efficiency)
            paramsUsed[6] = 1; //sigma (gaussian width)
            paramsUsed[7] = 1; //signoise (Background threshold)

            fitwindowShow = false;

            try
            {
                fitDataStrt fitDataNG = LNquantitate.fitNewtonGauss(mz,
                                                                    experimentalSpectrum,
                                                                    fitData,
                                                                    charge,
                                                                    label,
                                                                    rsh,
                                                                    fitwindowShow,
                                                                    paramsUsed,
                                                                    out sumSQ,
                                                                    out sdFittedData,
                                                                    out exitFit,
                                                                    qStrategy,
                                                                    instrParams.instResolution,
                                                                    configNG
                                                                    );
                fitData = fitDataNG;


            }
            catch
            {
                exitFit += "3rd NG failed";
            }



            #region Statistical weights calculations

            double Imax = fitData.A >= fitData.B ? fitData.A : fitData.B;
            
            double sigma =fitData.sigma;
            double background = fitData.signoise;
            double labelDistance = label * 2.0;
            
            int minPoint = experimentalSpectrum.GetLowerBound(0);
            int maxPoint = experimentalSpectrum.GetUpperBound(0);
            double minMass = experimentalSpectrum[experimentalSpectrum.GetLowerBound(0) + 1].mz;
            double maxMass = experimentalSpectrum[experimentalSpectrum.GetUpperBound(0)].mz;

            Comb.mzI[] fittedData = Gaussians.calEnvelope(mz, experimentalSpectrum, fitData, charge, label, instrParams.instResolution);
            
            double[] theoreticalSpecMZ = new double[fittedData.GetLength(0)];
            for (int z = fittedData.GetLowerBound(0); z <= fittedData.GetUpperBound(0); z++)
            {
                theoreticalSpecMZ[z] = fittedData[z].mz;
            }
            


            int pep1LeftPoint = 0;
            //double pep1LeftMass = 0;
            int pep1RightPoint = 0;
            //double pep1RightMass = 0;
            int pep2LeftPoint = 0;
            //double pep2LeftMass = 0;
            int pep2RightPoint = 0;
            //double pep2RightMass = 0;
            double SSQ1 = 0;
            double SSQ2 = 0;
            double MSQ1 = 0;
            double MSQ2 = 0;
            int nPep1 = 0;
            int nPep2 = 0;
            double weight = 0;

            //find the limits of the pep1 (light labeled peptide)
            double distanceFromPeak1 = 0.5;
            if (fitData.A >= fitData.B)
            {
                distanceFromPeak1 = 2.5;
            }

            pep1LeftPoint = Utilities.find(theoreticalSpecMZ, mz[0].mz - distanceFromPeak1 / (double)charge);
            if (pep1LeftPoint < minPoint) pep1LeftPoint = minPoint;
            if (pep1LeftPoint > maxPoint) pep1LeftPoint = maxPoint;
            pep1RightPoint = Utilities.find(theoreticalSpecMZ, mz[3].mz + distanceFromPeak1 / (double)charge);
            if (pep1RightPoint < minPoint) pep1RightPoint = minPoint;
            if (pep1RightPoint > maxPoint) pep1RightPoint = maxPoint;

            //find the limits of the pep2 (heavy labeled peptide)
            double distanceFromPeak2 = 0.5;
            if (fitData.B >= fitData.A)
            {
                distanceFromPeak2 = 2.5;

            }

            pep2LeftPoint = Utilities.find(theoreticalSpecMZ, mz[0].mz + (labelDistance - distanceFromPeak2) / (double)charge);
            if (pep2LeftPoint < minPoint) pep2LeftPoint = minPoint;
            if (pep2LeftPoint > maxPoint) pep2LeftPoint = maxPoint;
            pep2RightPoint = Utilities.find(theoreticalSpecMZ, mz[3].mz + (labelDistance + distanceFromPeak2) / (double)charge);
            if (pep2RightPoint < minPoint) pep2RightPoint = minPoint;
            if (pep2RightPoint > maxPoint) pep2RightPoint = maxPoint;


            nPep1 = (int)Math.Abs(pep1RightPoint - pep1LeftPoint);
            nPep2 = (int)Math.Abs(pep2RightPoint - pep2LeftPoint);

            SSQ1 = LNquantitate.sumSquares(experimentalSpectrum, fittedData, pep1LeftPoint, pep1RightPoint);
            SSQ2 = LNquantitate.sumSquares(experimentalSpectrum, fittedData, pep2LeftPoint, pep2RightPoint);

            MSQ1 = SSQ1 / nPep1;
            MSQ2 = SSQ2 / nPep2;


            //Better approach for estimating the area of A and B (for stat weights)
            //Calculate the integral under the curve of the fitted spectrum 
            fitDataStrt fitDataAB = new fitDataStrt();
            fitDataAB.A = fitData.A;
            fitDataAB.alpha = fitData.alpha;
            fitDataAB.B = 0;
            fitDataAB.deltaMz = fitData.deltaMz;
            fitDataAB.f = fitData.f;
            fitDataAB.sigma = fitData.sigma;
            fitDataAB.signoise = fitData.signoise;

            Comb.mzI[] fittedDataA = Gaussians.calEnvelope(mz, experimentalSpectrum, fitDataAB, charge, label, instrParams.instResolution);

            fitDataAB.A = 0;
            fitDataAB.B = fitData.B;

            Comb.mzI[] fittedDataB = Gaussians.calEnvelope(mz, experimentalSpectrum, fitDataAB, charge, label, instrParams.instResolution);

            double intensityA = 0;
            double intensityB = 0;
            for (int i = pep1LeftPoint; i < pep1RightPoint - 1; i++) //Evaluated only on the peptide area
            {
                double diffA = fittedDataA[i + 1].mz - fittedDataA[i].mz;
                intensityA += (fittedDataA[i].I - fitDataAB.signoise) * diffA;
            }
            for (int i = pep2LeftPoint; i < pep2RightPoint - 1; i++) //Evaluated only on the peptide area
            {
                double diffB = fittedDataB[i + 1].mz - fittedDataB[i].mz;
                intensityB += (fittedDataB[i].I - fitDataAB.signoise) * diffB;
            }



            //weight = Utilities.calWeight(   qStrategy,
            //                                fitData.A,
            //                                fitData.B,
            //                                MSQ1,
            //                                MSQ2,
            //                                0,
            //                                1);

            weight = Utilities.calWeight(qStrategy,
                                            intensityA,
                                            intensityB,
                                            MSQ1,
                                            MSQ2,
                                            0,
                                            1);


            //For controling those bad scans with very high fitted sigma  
            if (fitData.sigma > 2 * (mz[1].mz - mz[0].mz))
            {
                weight = 0;
            }

  
            #endregion


            #region write results
            double MHmass = LNquantitate.calMHmass(sequence, aminoacids, isotopes);
            //Write the Quantification Data in the row
            finalData[0]["q_peptide_Mass"] = MHmass;
            finalData[0]["q_A"] = fitData.A;
            finalData[0]["q_B"] = fitData.B;
            double log2Ratio = Math.Log(fitData.A / fitData.B, 2);
            finalData[0]["q_log2Ratio"] = log2Ratio;
            finalData[0]["q_f"] = fitData.f;
            finalData[0]["q_deltaMZ"] = fitData.deltaMz;
            finalData[0]["q_Alpha"] = fitData.alpha;
            finalData[0]["q_Sigma"] = fitData.sigma;
            finalData[0]["q_DeltaR"] = label*2;
            finalData[0]["q_background"] = fitData.signoise;
            finalData[0]["q_SD_A"] = sdFittedData.A;
            finalData[0]["q_SD_B"] = sdFittedData.B;
            finalData[0]["q_SD_f"] = sdFittedData.f;
            finalData[0]["q_SD_Alpha"] = sdFittedData.alpha;
            finalData[0]["q_SD_Sigma"] = sdFittedData.sigma;
            finalData[0]["q_SQtotal"] = sumSQ; ;


            //Weight V5
            finalData[0]["Vs"] = weight;

            if (finalData[0]["Label5"].ToString() != "")
            {
                finalData[0]["Label5"] = exitFit;
            }
            else
            {
                finalData[0]["Label5"] += exitFit;
            }
            #endregion



            #region NOT USED: TO IMPLEMENT LATER... Print spectra functionality
            ////print data to a file if this option is selected (writing the word "print" in label4)

            //if (dv[rowIndex].Row["Label4"].ToString().Contains("print"))
            //{
            //    try
            //    {
            //        string fileIds = this.idfileTxt.Text.Trim();
            //        char[] seps2 = new char[] { '.' };
            //        string[] sFileSplit = fileIds.Split(seps2);
            //        string spectraName = dv[rowIndex].Row["FileName"].ToString().Trim() + "_" + dv[rowIndex].Row["FirstScan"].ToString().Trim() + "_" + dv[rowIndex].Row["Charge"].ToString().Trim();
            //        string specFileName = sFileSplit[0] + "_" + spectraName + ".csv";

            //        string scanFilterPrint = "";
            //        scanFilterPrint += "FileName like '%" + dv[rowIndex].Row["FileName"].ToString().Trim() + "%'";
            //        scanFilterPrint += " AND FirstScan= " + dv[rowIndex].Row["FirstScan"].ToString().Trim();
            //        scanFilterPrint += " AND Charge= " + dv[rowIndex].Row["Charge"].ToString().Trim();

            //        DataView dvScan = new DataView(DataSetRecords.Tables["peptide_match"]);
            //        dvScan.RowFilter = scanFilterPrint;

            //        Comb.writeSpectrum(data, fittedData, specFileName, dvScan, this.lblX.Text.ToString());

            //        string lbl4 = dv[rowIndex].Row["Label4"].ToString().Trim().Replace("print", "");

            //        dv[rowIndex].Row["Label4"] = lbl4;
            //    }
            //    catch { }
            //}
            #endregion



            return finalData;

        }

        public void config(qMethodsSchema.Quanmethods quanMethods, 
                            AminoacidList[] aminoacidList, 
                            isotList[][] isotopesList,
                            string method_id_name)
        {
            aminoacids = aminoacidList;
            isotopes = isotopesList;

            configNG.alpha = 0.1;
            configNG.kmax = 4;
            configNG.PRS = 1e-6;
            configNG.sumSQtolerance = 1e-3;
            configNG.nMaxIter = 100;

            var query =
              from meth in quanMethods.method
              join instr in quanMethods.instrument
              on meth.method_Id equals instr.method_Id
              join inFit in quanMethods.initialFitParams
              on instr.instrument_Id equals inFit.instrument_Id
              join param in quanMethods.if_parameter
              on inFit.initialFitParams_Id equals param.initialFitParams_Id
              //where meth.method_id_key == methodKey
              where meth.method_id_name == method_id_name                        
              select new
              {
                  paramId = param.IsidNull() ? "no id" : param.id,
                  paramString = param.Is_stringNull() ? "no string" : param._string,
                  paramValue = param.IsvalueNull() ? Double.NaN : param.value
              };

            foreach (var m in query)
            {
                if (m.paramId == "alpha") { instrParams.alpha = m.paramValue; }
                if (m.paramId == "sigma") { instrParams.sigma = m.paramValue; }
                if (m.paramId == "efficiency") { instrParams.f = m.paramValue; }
                if (m.paramId == "deltaMZ") { instrParams.deltaMz = m.paramValue; }
                if (m.paramId == "SN_f") { instrParams.sn_f = m.paramValue; }
                if (m.paramId == "rsh") { rsh = m.paramValue; }
                if (m.paramId == "labeled amino acids")
                {
                    instrParams.deltaR = m.paramValue / 2.0;
                                        
                    string[] param_split = m.paramString.Split(',');
                    int numOfLabeledAA = param_split.GetLength(0);
                    labeledAminoacidesList = new char[numOfLabeledAA];
                    int counter = 0;
                    foreach (string aa in param_split)
                    {
                        labeledAminoacidesList[counter] = (char)aa.ToCharArray()[0];
                        counter++;
                    }
 
                }
            }

            var query2 =
                from meth in quanMethods.method
                join instr in quanMethods.instrument
                on meth.method_Id equals instr.method_Id
                join dfFit in quanMethods.initialFitParams
                on instr.instrument_Id equals dfFit.instrument_Id
                join param in quanMethods.df_parameter
                on dfFit.initialFitParams_Id equals param.deltaFitParams_Id
                //where meth.method_id_key == methodKey
                where meth.method_id_name == method_id_name
                select new
                {
                    paramId = param.IsidNull() ? "no id" : param.id,
                    paramString = param.Is_stringNull() ? "no string" : param._string,
                    paramValue = param.IsvalueNull() ? Double.NaN : param.value
                };

            foreach (var m in query2)
            {
                if (m.paramId == "A") { instrParams.varA = m.paramValue; }
                if (m.paramId == "B") { instrParams.varB = m.paramValue; }
                //if (m.paramId == "efficiency") { instrParams.varf = m.paramValue; }
                if (m.paramId == "sigma") { instrParams.varSigma = m.paramValue; }
                if (m.paramId == "alpha") { instrParams.varAlpha = m.paramValue; }
                if (m.paramId == "SN") { instrParams.varSn = m.paramValue; }
                if (m.paramId == "rsh") { rsh = m.paramValue; }
                if (m.paramId == "alpha_NG") { configNG.alpha = m.paramValue; }
                if (m.paramId == "PRS_NG") { configNG.PRS = m.paramValue; }
                if (m.paramId == "sumSQtolerance_NG") { configNG.sumSQtolerance = m.paramValue; }
            }

            var query3 =
                from meth in quanMethods.method
                join instr in quanMethods.instrument
                on meth.method_Id equals instr.method_Id
                //where meth.method_id_key == methodKey
                where meth.method_id_name == method_id_name
                select new
                {
                    kmax = instr.IskmaxNull() ? 12 : instr.kmax,
                    resolution = instr.resolution,
                    instName = instr.instrument_id_name,
                    nMaxIter = instr.IsmaxIterationsNull() ? 12 : instr.maxIterations
                };


            foreach (var m in query3)
            {
                configNG.kmax = (int)m.kmax;
                configNG.nMaxIter = (int)m.nMaxIter;
                switch (m.resolution)
                {
                    case "LOW":
                        instrParams.instResolution = Resolution.LOW;
                        break;
                    case "HIGH":
                        instrParams.instResolution = Resolution.HIGH;
                        break;

                }
                instrParams.instName = m.instName;
            }

            instrParams.f = 1.0;
            instrParams.varf = 0.01;
         
        }
        public void addDataTable(DataTable dt)
        {
            original_dt = dt.Clone();
        }

        public Comb.mzI[] centroiding(Comb.mzI[] experimentalSpectrum, Comb.mzI[] mz, double width)
        {
            throw new NotImplementedException();
        }

        public Comb.mzI[] getFittedSpectrum(DataRow fittedDataRow, Comb.mzI[] experimentalSpectrum)
        {

            //Create a fit data structure
            fitDataStrt fitData = new fitDataStrt();
            double label = 0;

            //Calculate the composition for a given sequence
            string sSequence = fittedDataRow["Sequence"].ToString().Trim();
            string sequence = sSequence;

            if (sSequence.Contains('.'))
            {
                string[] sSeqSplit = sSequence.Split('.');
                sequence = sSeqSplit[1].ToString();
            }


            Comb.compStrt[] composition = AminoacidList.calComposition(sequence, aminoacids);


            //Calculate the Intensities and m/z for a given chemical composition.
            //double calibrationError = double.Parse(fittedDataRow["q_CalibrationError"].ToString());

            double calibrationError = 0;
            try
            {
                calibrationError = double.Parse(fittedDataRow["q_CalibrationError"].ToString());
            }
            catch { }

            int charge = int.Parse(fittedDataRow["Charge"].ToString());

            Comb.mzI[] mz = Comb.calIntensities(composition,
                                                isotopes,
                                                charge,
                                                calibrationError,
                                                (int)configNG.kmax);

            double dcalcMass = (mz[0].mz - 1) * charge + 1;
            //this.label14.Text = "calc Mass : " + dcalcMass.ToString();

            fitData.A = double.Parse(fittedDataRow["q_A"].ToString());
            fitData.alpha = double.Parse(fittedDataRow["q_Alpha"].ToString());
            fitData.B = double.Parse(fittedDataRow["q_B"].ToString());
            fitData.deltaMz = double.Parse(fittedDataRow["q_deltaMZ"].ToString());
            fitData.f = double.Parse(fittedDataRow["q_f"].ToString());
            fitData.sigma = double.Parse(fittedDataRow["q_Sigma"].ToString());
            fitData.signoise = double.Parse(fittedDataRow["q_background"].ToString());
            label = double.Parse(fittedDataRow["q_DeltaR"].ToString());


            //Calculate the isotopic envelope
            Comb.mzI[] envData = Gaussians.calEnvelope(mz, experimentalSpectrum, fitData, charge, label/2, instrParams.instResolution);


            return envData;

        }

        public double getMonoisotMass(DataRow initialData)
        {
            double monoisotMass = 0.0;

            //Calculate the composition for a given sequence
            string sSequence = initialData["Sequence"].ToString().Trim();
            string sequence = sSequence;

            if (sSequence.Contains('.'))
            {
                string[] sSeqSplit = sSequence.Split('.');
                sequence = sSeqSplit[1].ToString();
            }


            Comb.compStrt[] composition = AminoacidList.calComposition(sequence, aminoacids);

            //Calculate the Intensities and m/z for a given chemical composition.
            double calibrationError = 0;
            try
            {
                calibrationError = double.Parse(initialData["q_CalibrationError"].ToString());
            }
            catch { }

            int charge = int.Parse(initialData["Charge"].ToString());

            Comb.mzI[] mz = Comb.calIntensities(composition,
                                                isotopes,
                                                charge,
                                                calibrationError,
                                                1);


            monoisotMass = mz[0].mz;

            return monoisotMass;
        }


        #endregion
    }

    public class QSilacHR : IQuantitation
    {
        AminoacidList[] aminoacids;
        instrumentParamsStrt instrParams;
        isotList[][] isotopes;
        confNGfitParamsStrt configNG;
        bool fitwindowShow;
        LNquantitate.quantitationStrategy qStrategy = LNquantitate.quantitationStrategy.SILAC_HR;
        DataTable original_dt;
        double rsh = 1e-3; // *** check this, as we did not test it
        char[] labeledAminoacidesList;

        #region IQuantitation Members

        public DataRow[] quantitate(DataRow initialData, Comb.mzI[] experimentalSpectrum)
        {
            double calibrationError;
            int charge;
            DataRow[] finalData;
            fitwindowShow = false;

            finalData = new DataRow[1];
            finalData[0] = initialData;
            Comb.mzI[] mz = new Comb.mzI[1];
            ArrayList tentativeMZmatrix = new ArrayList();
            double bestWeight=0;
            double bestMonoIsot = 0;
            fitDataStrt sdFittedData = new fitDataStrt();
            double sumSQ=0;
            double weight=0;
            bool calAveragine=false;
            string exitFit = "";

            try 
            {
                int averagineValue = int.Parse(initialData["doAveragine"].ToString());
                if (averagineValue == 1)
                    calAveragine = true;
            }
            catch 
            { }

            try
            {
                calibrationError = double.Parse(initialData["q_CalibrationError"].ToString());
            }
            catch
            {
                calibrationError = 0;
                initialData["q_CalibrationError"] = calibrationError;
            }

            try
            {
                charge = int.Parse(initialData["Charge"].ToString());
            }
            catch { return null; }


            //Create a fit data structure
            fitDataStrt fitData = new fitDataStrt();


            
            string sSequence = initialData["Sequence"].ToString().Trim();
            string sequence = sSequence;

            if (sSequence.Contains('.'))
            {
                string[] sSeqSplit = sSequence.Split('.');
                sequence = sSeqSplit[1].ToString();
            }

            //Calculate the composition for a given sequence (if not averagine)
            if (!calAveragine)
            {
                
                Comb.compStrt[] composition;
                if (aminoacids.Length > 0)
                {
                    composition = AminoacidList.calComposition(sequence, aminoacids);
                }
                else { return null; }

                if (composition == null) return null;

                mz = Comb.calIntensities(composition, isotopes, charge, calibrationError, configNG.kmax);

                tentativeMZmatrix.Add(mz);
            }

            if (calAveragine)
            {
                double MHplus=0;
                try
                {
                    MHplus = double.Parse(initialData["PrecursorMass"].ToString());
                }
                catch { return null; }

                tentativeMZmatrix = Comb.calIntensitiesAveragine(MHplus, charge, calibrationError);
 
            }
         
            //calculate the labeling (number of aminoacides labeled in the sequence)
            int extraMods = 0;

            try
            {
                bool extraModsOk = int.TryParse(initialData["extra_mods"].ToString(), out extraMods);
                if (!extraModsOk) extraMods = 0;
            }
            catch
            {
            }
                            
            int numOfAaLabeled = LNquantitate.countAminoacides(sequence, labeledAminoacidesList) + extraMods;
            
            double label = instrParams.deltaR * (double)numOfAaLabeled;

            if (label == 0)
            {
                finalData[0]["Label5"] = "peptide with no labeling";
                return finalData;
            }
            
            //get initial parameters
            fitData.f = instrParams.f;
            fitData.A = 0;
            fitData.B = 0;
            fitData.deltaMz = instrParams.deltaMz;
            fitData.signoise = 0;
            fitData.alpha = 0;


            for (int i = 0; i < tentativeMZmatrix.Count; i++)
            {

                mz = (Comb.mzI[])tentativeMZmatrix[i]; 

                // rename clusterIntensities --> clusterMasses
                //Take the cluster of masses in order to calculate a centroided experimental spectrum taken only in those masses
                Comb.mzI[] clusterIntensities = Gaussians.calEnvelope(mz, null, fitData, charge, label / 2, Resolution.HIGH);

                //This is our working experimental spectrum (after being centroided)
                Comb.mzI[] expCentroidedSpectrum = centroiding(experimentalSpectrum, clusterIntensities);

                // data for seed withdrawn
                fitData = Gaussians.getInitialConditions(fitData, expCentroidedSpectrum, mz, instrParams.sn_f, charge, label, Resolution.HIGH);

                fitDataStrt fitDataSweep;
                
                /*
                 * 1 == A
                 * 2 == alpha
                 * 3 == B
                 * 4 == deltaMZ
                 * 5 == f
                 * 6 == sigma
                 * 7 == signoise
                 */
                int[] iterations = new int[8];
                iterations[1] = 10;
                iterations[2] = 0;
                iterations[3] = 10;
                iterations[4] = 0;
                iterations[5] = 0;
                iterations[6] = 0;
                iterations[7] = 0;

                // seed improved by fit by sweep
                fitDataSweep = LNquantitate.fitbySweep(mz,
                                                        expCentroidedSpectrum,
                                                        fitData,
                                                        charge,
                                                        label / 2,
                                                        iterations,
                                                        fitwindowShow,
                                                        instrParams,
                                                        out sumSQ,
                                                        0);
                fitData = fitDataSweep;

                /* 
                 *   Uncomment if you want to jump the sweep fit.
                 * 
                double sumSQ=(double)dv[rowIndex].Row["q_SumSquares"];
                fitData.A = (double)dv[rowIndex].Row["q_A"];
                fitData.alpha = (double)dv[rowIndex].Row["q_Alpha"];
                fitData.B = (double)dv[rowIndex].Row["q_B"];
                fitData.deltaMz = (double)dv[rowIndex].Row["q_DeltaMZ"];
                fitData.f = (double)dv[rowIndex].Row["q_f"];
                fitData.sigma = (double)dv[rowIndex].Row["q_Sigma"];
                fitData.signoise = (double)dv[rowIndex].Row["q_background"];
                */



                
                // First Fit (No deltaMz)

                //List of parameters used for fitting.
                int[] paramsUsed = new int[8];
                paramsUsed[1] = 1; //A (A quantity)
                paramsUsed[2] = 0; //alpha (leptokurtosis)
                paramsUsed[3] = 1; //B (B quantity)
                paramsUsed[4] = 0; //deltaMz (experimental mass deviation)
                paramsUsed[5] = 0; //f (efficiency)
                paramsUsed[6] = 0; //sigma (gaussian width)
                paramsUsed[7] = 0; //signoise (Background threshold)

                try
                {
                    fitDataStrt fitDataNG = LNquantitate.fitNewtonGauss(mz,
                                                                        expCentroidedSpectrum,
                                                                        fitData,
                                                                        charge,
                                                                        label / 2,
                                                                        rsh,
                                                                        fitwindowShow,
                                                                        paramsUsed,
                                                                        out sumSQ,
                                                                        out sdFittedData,
                                                                        out exitFit,
                                                                        qStrategy,
                                                                        instrParams.instResolution,
                                                                        configNG
                                                                        );
                    fitData = fitDataNG;
                }
                catch
                {
                    exitFit += "1st NG failed";
                }
               
                #region Statistical weights calculations

                Comb.mzI[] theorSpectrum = Gaussians.calEnvelope(mz, expCentroidedSpectrum, fitData, charge, instrParams.deltaR, instrParams.instResolution);

                int nPoints = LNquantitate.numOfMatchingPeaks(fitData.A, fitData.B, expCentroidedSpectrum, theorSpectrum);

                double MSQ1 = sumSQ / nPoints;

                weight = Utilities.calWeight(LNquantitate.quantitationStrategy.SILAC_HR,
                                                    fitData.A,
                                                    fitData.B,
                                                    MSQ1,
                                                    0,
                                                    0,
                                                    fitData.f);


                #endregion

                //Evaluate best mz: best weight achieved.

                if (weight >= bestWeight)
                {
                    bestWeight = weight;
                    bestMonoIsot = mz[0].mz;                   
                }

            }

            double log2Ratio = Math.Log(fitData.A / fitData.B, 2);

            
            float XsNoCorrF = (float)Math.Log((fitData.A + fitData.B * (1 - fitData.f) * (1 - fitData.f)) / (fitData.B * (fitData.f * fitData.f + 2 * fitData.f * (1 - fitData.f))), 2);

            if (float.IsNaN(XsNoCorrF))
            {
                XsNoCorrF = (float)log2Ratio;
                exitFit += " q_f too high ";
            }

            double MHmass = LNquantitate.calMHmass(sequence, aminoacids, isotopes);

            //Write the Quantification Data in the row
            finalData[0]["q_peptide_Mass"] = MHmass;
            if (calAveragine)
            {
                finalData[0]["q_monoIsotMass"] = bestMonoIsot;
            }
            finalData[0]["q_A"] = fitData.A;
            finalData[0]["q_B"] = fitData.B;
            finalData[0]["q_log2Ratio"] = log2Ratio;
            //finalData[0]["Xs_NoCorrf"] = XsNoCorrF;
            finalData[0]["q_f"] = fitData.f;
            finalData[0]["q_DeltaR"] = label;
            finalData[0]["q_SD_A"] = sdFittedData.A;
            finalData[0]["q_SD_B"] = sdFittedData.B;
            finalData[0]["q_Width"] = instrParams.width;
            finalData[0]["Vs"] = bestWeight;
            finalData[0]["q_SQtotal"] = sumSQ;
            //finalData[0]["q_SQtotal"] = AdsumSQwh;


            if (finalData[0]["Label5"].ToString() != "")
            {
                finalData[0]["Label5"] = exitFit;
            }
            else
            {
                finalData[0]["Label5"] += exitFit;
            }



            return finalData;
        }

        private Comb.mzI[] centroiding(Comb.mzI[] experimentalSpectrum, Comb.mzI[] mz)
        {
            // when forcedWidth = 0, then it uses the value in instrparams
            return centroiding(experimentalSpectrum, mz, 0);
        }

        public Comb.mzI[] centroiding(Comb.mzI[] experimentalSpectrum, Comb.mzI[] mz, double forcedWidth)
        {
            double width = instrParams.width;
            if (forcedWidth > 0) width = forcedWidth;
            Comb.mzI[] resultingSpectrum = new Comb.mzI[mz.Length];
            double[] peakLowLimit= new double[mz.Length];
            double[] peakHighLimit = new double[mz.Length];
            int charge = (int)Math.Round(1 / (mz[1].mz - mz[0].mz));
            double precursorMass = mz[0].mz * (double)charge - (double)charge;
            int mostIntensePeak = (int)Math.Round(precursorMass / 1000) - 1;
            if (mostIntensePeak < 0) mostIntensePeak = 0; // because it is zero based
            //double widthIncrease = 10;

            for (int i = 0; i < mz.Length; i++)
            {
                peakLowLimit[i] = mz[i].mz - width / 2;
                peakHighLimit[i] = mz[i].mz + width / 2;
            }

            for (int i = 0; i < mz.Length; i++)
            {
                resultingSpectrum[i].mz = mz[i].mz;
            }

            // old way
            for (int i = 0; i < experimentalSpectrum.Length; i++)
            {
                for (int currPeak = 0; currPeak < mz.Length; currPeak++)
                {
                    if (experimentalSpectrum[i].mz >= peakLowLimit[currPeak] && experimentalSpectrum[i].mz <= peakHighLimit[currPeak])
                    {
                        if (experimentalSpectrum[i].I > resultingSpectrum[currPeak].I)
                            resultingSpectrum[currPeak].I = experimentalSpectrum[i].I;
                        //resultingSpectrum[currPeak].I += experimentalSpectrum[i].I;
                    }
                }
            }

            //for (int i = 0; i < experimentalSpectrum.Length; i++)
            //{
            //    if (experimentalSpectrum[i].mz >= peakLowLimit[mostIntensePeak] * (double)widthIncrease
            //        && experimentalSpectrum[i].mz <= peakHighLimit[mostIntensePeak] * (double)widthIncrease)
            //    {
            //        if (experimentalSpectrum[i].I > resultingSpectrum[mostIntensePeak].I)
            //            resultingSpectrum[mostIntensePeak].I = experimentalSpectrum[i].I;
            //        //resultingSpectrum[currPeak].I += experimentalSpectrum[i].I;
            //    }
            //}

            return resultingSpectrum;
        }

        public void config(QuiXoT.math.qMethodsSchema.Quanmethods quanMethods,
                                    AminoacidList[] aminoacidList,
                                    isotList[][] isotopesList,
                                    string method_id_name)
        {
            aminoacids = aminoacidList;
            isotopes = isotopesList;

            // to modify
            configNG.alpha = 0.1;
            configNG.kmax = 8;
            configNG.PRS = 1e-3;
            configNG.sumSQtolerance = 1e-4;
            configNG.nMaxIter = 1000;

            var query =
              from meth in quanMethods.method
              join instr in quanMethods.instrument
              on meth.method_Id equals instr.method_Id
              join inFit in quanMethods.initialFitParams
              on instr.instrument_Id equals inFit.instrument_Id
              join param in quanMethods.if_parameter
              on inFit.initialFitParams_Id equals param.initialFitParams_Id
              //where meth.method_id_key == methodKey
              where meth.method_id_name == method_id_name
              select new
              {
                  paramId = param.IsidNull() ? "no id" : param.id,
                  paramString = param.Is_stringNull() ? "no string" : param._string,
                  paramValue = param.IsvalueNull() ? Double.NaN : param.value
              };

            foreach (var m in query)
            {
                if (m.paramId == "width") { instrParams.width = m.paramValue; }
                if (m.paramId == "deltaR") { instrParams.deltaR = m.paramValue; }
                if (m.paramId == "efficiency") { instrParams.f = m.paramValue; }
                if (m.paramId == "deltaMZ") { instrParams.deltaMz = m.paramValue; }
                if (m.paramId == "labeled amino acids")
                {
                    instrParams.deltaR = m.paramValue;

                    string[] param_split = m.paramString.Split(',');
                    int numOfLabeledAA = param_split.GetLength(0);
                    labeledAminoacidesList = new char[numOfLabeledAA];
                    int counter = 0;
                    foreach (string aa in param_split)
                    {
                        labeledAminoacidesList[counter] = (char)aa.ToCharArray()[0];
                        counter++;
                    }

                }
            }

            var query2 =
                from meth in quanMethods.method
                join instr in quanMethods.instrument
                on meth.method_Id equals instr.method_Id
                join dfFit in quanMethods.initialFitParams
                on instr.instrument_Id equals dfFit.instrument_Id
                join param in quanMethods.df_parameter
                on dfFit.initialFitParams_Id equals param.deltaFitParams_Id
                where meth.method_id_name == method_id_name
                select new
                {
                    paramId = param.IsidNull() ? "no id" : param.id,
                    paramString = param.Is_stringNull() ? "no string" : param._string,
                    paramValue = param.IsvalueNull() ? Double.NaN : param.value
                };

            foreach (var m in query2)
            {
                if (m.paramId == "A") { instrParams.varA = m.paramValue; }
                if (m.paramId == "B") { instrParams.varB = m.paramValue; }
                if (m.paramId == "rsh") { rsh = m.paramValue; }
                if (m.paramId == "alpha_NG") { configNG.alpha = m.paramValue; }
                if (m.paramId == "PRS_NG") { configNG.PRS = m.paramValue; }
                if (m.paramId == "sumSQtolerance_NG") { configNG.sumSQtolerance = m.paramValue; }
            }

            var query3 =
                from meth in quanMethods.method
                join instr in quanMethods.instrument
                on meth.method_Id equals instr.method_Id
                //where meth.method_id_key == methodKey
                where meth.method_id_name == method_id_name
                select new
                {
                    kmax = instr.IskmaxNull() ? 12 : instr.kmax,
                    resolution = instr.resolution,
                    instName = instr.instrument_id_name,
                    nMaxIter = instr.IsmaxIterationsNull() ? 12 : instr.maxIterations
                };


            foreach (var m in query3)
            {
                configNG.kmax = (int)m.kmax;
                //configNG.kmax = (int)m.kmax;
                configNG.nMaxIter = (int)m.nMaxIter;
                switch (m.resolution)
                {
                    case "LOW":
                        instrParams.instResolution = Resolution.LOW;
                        break;
                    case "HIGH":
                        instrParams.instResolution = Resolution.HIGH;
                        break;
                }

                instrParams.instName = m.instName;
            }
        }

        public void addDataTable(DataTable dt)
        {
            original_dt = dt.Clone();
        }

        public Comb.mzI[] getFittedSpectrum(DataRow fittedDataRow, Comb.mzI[] experimentalSpectrum)
        {
            //Create a fit data structure
            fitDataStrt fitData = new fitDataStrt();
            double label = 0;
            Comb.mzI[] mz = new Comb.mzI[1];
            ArrayList tentativeMZmatrix = new ArrayList();

            bool calAveragine = false;

            try
            {
                int averagineValue = int.Parse(fittedDataRow["doAveragine"].ToString());
                if (averagineValue == 1)
                    calAveragine = true;
            }
            catch
            { }

            //double calibrationError = double.Parse(fittedDataRow["q_CalibrationError"].ToString());
            double calibrationError = 0;
            try
            {
                calibrationError = double.Parse(fittedDataRow["q_CalibrationError"].ToString());
            }
            catch { }

            int charge = int.Parse(fittedDataRow["Charge"].ToString());


            //Calculate the composition for a given sequence
            string sSequence = fittedDataRow["Sequence"].ToString().Trim();
            string sequence = sSequence;

            if (sSequence.Contains('.'))
            {
                string[] sSeqSplit = sSequence.Split('.');
                sequence = sSeqSplit[1].ToString();
            }



            //Calculate the composition for a given sequence (if not averagine)
            if (!calAveragine)
            {

                Comb.compStrt[] composition;
                if (aminoacids.Length > 0)
                {
                    composition = AminoacidList.calComposition(sequence, aminoacids);
                }
                else { return null; }

                if (composition == null) return null;

                mz = Comb.calIntensities(composition, 
                                        isotopes, 
                                        charge, 
                                        calibrationError, 
                                        configNG.kmax);

            }

            if (calAveragine)
            {
                double MHplus = 0;
                double monoIsotMass = 0;

                try
                {
                    MHplus = double.Parse(fittedDataRow["PrecursorMass"].ToString());
                }
                catch { return null; }

                try
                {
                    monoIsotMass = double.Parse(fittedDataRow["q_monoIsotMass"].ToString());
                }
                catch { return null; }


                tentativeMZmatrix = Comb.calIntensitiesAveragine(MHplus, charge, calibrationError);

                for (int i = 0; i < tentativeMZmatrix.Count; i++)
                {
                    Comb.mzI[] tmp_mz = (Comb.mzI[]) tentativeMZmatrix[i];
                    if (Math.Abs(tmp_mz[0].mz - monoIsotMass) < 0.001)
                    {
                        mz = tmp_mz;
                        break;
                    }
                }
            }

            label = double.Parse(fittedDataRow["q_DeltaR"].ToString());

         
            if (label == 0)
            {
                return null;
            }


            Comb.mzI[] clusterIntensities = Gaussians.calEnvelope(mz, null, fitData, charge, label/2, Resolution.HIGH);

            Comb.mzI[] expCentroidedSpectrum = centroiding(experimentalSpectrum, clusterIntensities);

            double dcalcMass = (mz[0].mz - 1) * charge + 1;
            //this.label14.Text = "calc Mass : " + dcalcMass.ToString();

            fitData.A = double.Parse(fittedDataRow["q_A"].ToString());
            fitData.B = double.Parse(fittedDataRow["q_B"].ToString());
            double width = double.Parse(fittedDataRow["q_Width"].ToString());
            fitData.deltaMz = 0;
            fitData.f = double.Parse(fittedDataRow["q_f"].ToString());
            

            //Calculate the isotopic envelope
            Comb.mzI[] envData = Gaussians.calEnvelope(mz, expCentroidedSpectrum, fitData, charge, label/2, instrParams.instResolution);

            // Attention: decentroiding is no longer used!
            //Comb.mzI[] decentroidedSpectrum = decentroiding(envData, expCentroidedSpectrum, experimentalSpectrum, out experimentalSpectrum, width);
            //return decentroidedSpectrum;

            return envData;
        }

        // Old method in 2009-2010 (until release 1.3.20),
        // with decentroided spectrum;
        // it was abandoned because it was misleading
        // as it looked like there was a fit for the whole peak
        // instead of just taking into account the maximum intensity
        // THIS METHOD IS NOT USED ANY MORE, and is kept here for record only
        public Comb.mzI[] decentroiding(Comb.mzI[] thCentroidedSpectrum,
                                        Comb.mzI[] expCentroidedSpectrum,
                                        Comb.mzI[] expSpectrum,
                                        out Comb.mzI[] expSpectrumOut,
                                        double width)
        {

            Comb.mzI[] decentroidedSpectrum = (Comb.mzI[])expSpectrum.Clone();
            expSpectrumOut = (Comb.mzI[])expSpectrum.Clone();

            //set intensities of decentroidedSpectrum to zero
            for (int i = 0; i < decentroidedSpectrum.Length; i++)
            {
                decentroidedSpectrum[i].I = 0;
            }

            int currentPeak = 0;
            double minMass = thCentroidedSpectrum[currentPeak].mz - width / 2;
            double maxMass = thCentroidedSpectrum[currentPeak].mz + width / 2;
            bool[] peakFound = new bool[thCentroidedSpectrum.Length];
            int[] pointsPerPeak = new int[thCentroidedSpectrum.Length];
            double coeffRatio = 0;
            double diffPeaksTolerance = Math.Log10(10.0);

            #region fit for each experimental peak

            //detecting peaks
            for (int k = 0; k < thCentroidedSpectrum.Length; k++)
            {
                minMass = thCentroidedSpectrum[k].mz - width / 2;
                maxMass = thCentroidedSpectrum[k].mz + width / 2;

                for (int i = 0; i < expSpectrum.Length; i++)
                {
                    if (expSpectrum[i].mz >= minMass && expSpectrum[i].mz <= maxMass)
                    {
                        coeffRatio = Math.Log10(thCentroidedSpectrum[k].I) - Math.Log10(expCentroidedSpectrum[k].I);
                        if (Math.Abs(coeffRatio) < diffPeaksTolerance)
                        {
                            peakFound[k] = true;
                        }
                        pointsPerPeak[k]++;
                    }
                }
            }

            int peaksFound = 0;
            int totalPointsPeak = 0;
            for (int k = 0; k < thCentroidedSpectrum.Length; k++)
            {
                if (peakFound[k])
                {
                    peaksFound++;
                    totalPointsPeak += pointsPerPeak[k];
                }

            }

            double avgPoints = Math.Floor((double)totalPointsPeak / (double)peaksFound);

            for (int k = 0; k < thCentroidedSpectrum.Length; k++)
            {
                minMass = thCentroidedSpectrum[k].mz - width / 2;
                maxMass = thCentroidedSpectrum[k].mz + width / 2;

                if (peakFound[k])
                {
                    coeffRatio = thCentroidedSpectrum[k].I / expCentroidedSpectrum[k].I;

                    for (int i = 0; i < expSpectrum.Length; i++)
                    {
                        if (expSpectrum[i].mz >= minMass && expSpectrum[i].mz <= maxMass)
                        {
                            decentroidedSpectrum[i].I = expSpectrum[i].I * coeffRatio;
                        }
                    }
                }
                else
                {
                    //"create" a ficticious peak representing theoretical fitting for the peak

                    Comb.mzI[] newValues = new Comb.mzI[2];
                    newValues[0].mz = minMass;
                    newValues[1].mz = thCentroidedSpectrum[k].mz;
                    newValues[0].I = 0;
                    newValues[1].I = 0;

                    expSpectrum = addPeakToSpectrum(expSpectrum, newValues);

                    double peakIntensity = thCentroidedSpectrum[k].I;
                    if (peakIntensity < 0) peakIntensity = 0;

                    newValues[0].I = peakIntensity / avgPoints;
                    newValues[1].I = peakIntensity / avgPoints;

                    decentroidedSpectrum = addPeakToSpectrum(decentroidedSpectrum, newValues);
                }

            }

            #endregion

            expSpectrumOut = (Comb.mzI[])expSpectrum.Clone();

            return decentroidedSpectrum;
        }

        // in the future this method should be taken out
        private static Comb.mzI[] addPeakToSpectrum(Comb.mzI[] expSpectrum, Comb.mzI[] peak)
        {
            Comb.mzI[] specNew = new Comb.mzI[expSpectrum.Length + 2];
            int currPoint = 0;

            //check dimension of peak[]
            if (peak.Length != 2) return expSpectrum;


            //to the first point of the peak
            for (int i = 0; i < expSpectrum.Length; i++)
            {
                specNew[i].I = expSpectrum[i].I;
                specNew[i].mz = expSpectrum[i].mz;

                if (expSpectrum[i].mz > peak[0].mz && expSpectrum[i].mz < peak[1].mz)
                {
                    specNew[i + 1].I = 0;
                    specNew[i + 1].mz = peak[0].mz;
                    specNew[i + 2].I = peak[0].I;
                    specNew[i + 2].mz = peak[0].mz;

                    currPoint = i;
                    break;
                }
            }

            //middle of the peak and after...
            bool lastP = false;
            for (int i = currPoint + 1; i < expSpectrum.Length - 2; i++)
            {
                if (expSpectrum[i].mz > peak[0].mz && expSpectrum[i].mz < peak[1].mz)
                {
                    specNew[i + 2].I = peak[0].I;
                    specNew[i + 2].mz = expSpectrum[i].mz;
                }
                if (expSpectrum[i].mz > peak[1].mz)
                {
                    if (!lastP)
                    {
                        specNew[i + 2].I = expSpectrum[i].I;
                        specNew[i + 2].mz = expSpectrum[i].mz;

                        specNew[i + 3].I = peak[1].I;
                        specNew[i + 3].mz = peak[1].mz;

                        specNew[i + 4].I = 0;
                        specNew[i + 4].mz = peak[1].mz;

                        lastP = true;
                    }
                    else
                    {
                        specNew[i + 4].I = expSpectrum[i + 1].I;
                        specNew[i + 4].mz = expSpectrum[i + 1].mz;
                    }
                }
            }

            return specNew;
        }

        public double getMonoisotMass(DataRow initialData)
        {
            double monoisotMass = 0.0;

            //Calculate the composition for a given sequence
            string sSequence = initialData["Sequence"].ToString().Trim();
            string sequence = sSequence;

            if (sSequence.Contains('.'))
            {
                string[] sSeqSplit = sSequence.Split('.');
                sequence = sSeqSplit[1].ToString();
            }


            Comb.compStrt[] composition = AminoacidList.calComposition(sequence, aminoacids);

            //Calculate the Intensities and m/z for a given chemical composition.
            double calibrationError = 0;
            try
            {
                calibrationError = double.Parse(initialData["q_CalibrationError"].ToString());
            }
            catch { }

            int charge = int.Parse(initialData["Charge"].ToString());

            Comb.mzI[] mz = Comb.calIntensities(composition,
                                                isotopes,
                                                charge,
                                                calibrationError,
                                                1);


            monoisotMass = mz[0].mz;

            return monoisotMass;
        }

        #endregion
    }

    public class QiTraq : IQuantitation
    {
        AminoacidList[] aminoacids;
        isotList[][] isotopes;
        LNquantitate.quantitationStrategy qStrategy = LNquantitate.quantitationStrategy.iTRAQ;
        //string methodKey = "iTRAQ";
        LNquantitate.reporterMethodType ionReporterMethod;
        ArrayList massTags = new ArrayList();

        double intensityThreshold=0.0;
        double deltaMZ = 0.0;

        Comb.mzI[] repIons;

        bool useCorrection = true;
        
        double[,] corr;
        double[] I0;
        double[] I;
        double[] T;
        Matrix C;
        double[,] Cmat;
        double[,] delta_i;
        double Cdet;

        #region Miembros de IQuantitation

        public DataRow[] quantitate(DataRow initialData, Comb.mzI[] experimentalSpectrum)
        {

            DataRow[] finalData = new DataRow[1];
            finalData[0] = initialData;

            ArrayList Xs_columnsInXML = new ArrayList();
            ArrayList Vs_columnsInXML = new ArrayList();
            for (int i = 0; i < initialData.Table.Columns.Count; i++)
            {
                string col = initialData.Table.Columns[i].ToString();
                if (col.StartsWith("q_Xs_"))
                    Xs_columnsInXML.Add(col);
                if (col.StartsWith("q_Vs_"))
                    Vs_columnsInXML.Add(col);
            }

            //Calculate the composition for a given sequence
            string sSequence = initialData["Sequence"].ToString().Trim();
            string sequence = sSequence;

            if (sSequence.Contains('.'))
            {
                string[] sSeqSplit = sSequence.Split('.');
                sequence = sSeqSplit[1].ToString();
            }

            double MHmass = LNquantitate.calMHmass(sequence, aminoacids, isotopes); 
            finalData[0]["q_peptide_Mass"] = MHmass;


            if (useCorrection)
            {

                I = new double[I0.Length];
                T = new double[I0.Length];
                repIons = new Comb.mzI[I0.Length];
                delta_i = new double[I0.Length, I0.Length];


                int counter = 0;

                foreach (LNquantitate.MassTag mt in massTags)
                {

                    repIons[counter] = LNquantitate.calIonReporter(experimentalSpectrum,
                                                                mt.exactMass,
                                                                deltaMZ,
                                                                intensityThreshold,
                                                                ionReporterMethod);

                    I[counter] = repIons[counter].I;
                    
                    counter++;
                }



                counter = 0;
                
                foreach(LNquantitate.MassTag mt in massTags)
                {


                    //Transpose matrix
                    for (int i = 0; i < I.Length; i++)
                    {
                        for (int j = 0; j < I.Length; j++)
                        {
                            delta_i[i, j] = Cmat[j, i];
                        }
                    }

                    //Concrete values of delta_i
                    for (int i = 0; i < I.Length; i++)
                    {
                        delta_i[i, counter] = I[i];
                    }


                    Matrix delta_im = Matrix.Create(delta_i);
                    double delta_im_det = delta_im.Determinant();

                    if (Cdet != 0)
                    {
                        T[counter] = delta_im_det / Cdet;
                    }
                    else { T[counter] = double.NaN; }

                    string repIonLabel = "q_reporterIon_" + mt.label;
                    string fittedMassLabel = "q_fittedMass_" + mt.label;

                    double intensity = T[counter];

                    if (intensity <= 0)
                    {
                        intensity = 1e-4;
                        finalData[0]["Label5"] += "Intensity of the " + fittedMassLabel + " too low";
                    }

                    finalData[0][repIonLabel] = intensity;
                    
                    
                    finalData[0][fittedMassLabel] = repIons[counter].mz;
                                        

                    counter++;
                }


            }
            else
            {
                foreach (LNquantitate.MassTag mt in massTags)
                {

                    mt.repIon = LNquantitate.calIonReporter(experimentalSpectrum,
                                                                mt.exactMass,
                                                                deltaMZ,
                                                                intensityThreshold,
                                                                ionReporterMethod);

                    string repIonLabel = "q_reporterIon_" + mt.label;
                    string fittedMassLabel = "q_fittedMass_" + mt.label;

                    double intensity = mt.repIon.I;

                    if (intensity <= 0)
                    {
                        intensity = 1e-4;
                        finalData[0]["Label5"] += "Intensity of the " + fittedMassLabel + " too low";
                    }

                    finalData[0][repIonLabel] = intensity;
                    finalData[0][fittedMassLabel] = mt.repIon.mz;
                    
                }
            }


            int countRef = 0;
            foreach (LNquantitate.MassTag rf in massTags)
            {                
                if (rf.reference) 
                {
                    int counter = 0;
                    foreach (LNquantitate.MassTag mt in massTags)
                    {
                        if (mt.label != rf.label)
                        {
                            string logRlabel = "q_Xs_" + mt.label + "_" + rf.label;
                            string Vslabel = "q_Vs_" + mt.label + "_" + rf.label;

                            if (Xs_columnsInXML.Contains(logRlabel) && Vs_columnsInXML.Contains(Vslabel))
                            {
                                double logRatio = 0;
                                double weight = 0;

                                if (useCorrection)
                                {

                                    double qA = T[counter];
                                    double qB = T[countRef];

                                    if (qA <= 0) qA = 1e-4;
                                    if (qB <= 0) qB = 1e-4;


                                    logRatio = Math.Log(qA / qB, 2);
                                    weight = Utilities.calWeight(qStrategy,
                                                                        qA,
                                                                        qB,
                                                                        0, 0, 0, 0);

                                }
                                else
                                {

                                    double qA = mt.repIon.I;
                                    double qB = rf.repIon.I;

                                    if (qA <= 0) qA = 1e-4;
                                    if (qB <= 0) qB = 1e-4;


                                    logRatio = Math.Log(mt.repIon.I / rf.repIon.I, 2);


                                    weight = Utilities.calWeight(qStrategy,
                                                                        mt.repIon.I,
                                                                        rf.repIon.I,
                                                                        0, 0, 0, 0);

                                }


                                finalData[0][logRlabel] = logRatio;
                                finalData[0][Vslabel] = weight;

                                
                            }
                        }
                        counter++;
                    }
                }
                countRef++;
            }
           

            return finalData;            
        }

        public void config(qMethodsSchema.Quanmethods quanMethods, 
                            AminoacidList[] aminoacidList, 
                            isotList[][] isotopesList,
                            string method_id_name)
        {

            aminoacids = (AminoacidList[])aminoacidList.Clone();
            isotopes = (isotList[][])isotopesList.Clone();

            var query =
                        from meth in quanMethods.method
                        join instr in quanMethods.instrument
                        on meth.method_Id equals instr.method_Id
                        join inFit in quanMethods.initialFitParams
                        on instr.instrument_Id equals inFit.instrument_Id
                        join param in quanMethods.if_parameter
                        on inFit.initialFitParams_Id equals param.initialFitParams_Id
                        //where meth.method_id_key == methodKey 
                        where meth.method_id_name == method_id_name
                        select new
                        {
                            paramId = param.IsidNull() ? "no id" : param.id, 
                            paramString = param.Is_stringNull() ? "no string" : param._string,
                            paramValue =  param.IsvalueNull() ? Double.NaN : param.value
                        };

            foreach (var m in query)
            {
                if (m.paramId == "deltaMZ") { deltaMZ = m.paramValue; }
                if (m.paramId == "intensityThreshold") { intensityThreshold = m.paramValue; }
                if (m.paramId == "reporterMethod")
                {
                    if (m.paramString == "leastDeltaMZ") { ionReporterMethod = LNquantitate.reporterMethodType.leastDeltaMZ; }
                    if (m.paramString == "mostIntense") { ionReporterMethod = LNquantitate.reporterMethodType.mostIntense; }
                    if (m.paramString == "sumI") { ionReporterMethod = LNquantitate.reporterMethodType.sumI; }
                }
            }


            var query2 =
                    from meth in quanMethods.method
                    join massTs in quanMethods.MassTags
                    on meth.method_Id equals massTs.method_Id
                    join massT in quanMethods.MassTag
                    on massTs.MassTags_Id equals massT.MassTags_Id
                    //where meth.method_id_key == methodKey
                    where meth.method_id_name == method_id_name
                    select new
                    {

                        primKey = meth.method_Id,
                        idKey = meth.method_id_key,
                        insKey = massTs.MassTags_Id,
                        label = massT.id,
                        paramMass = massT.Mass,
                        paramRef = massT.IsreferenceNull() ? false : massT.reference,
                        paramCorr_1 = massT.Is_Corr_1Null() ? 0.0 : massT._Corr_1,
                        paramCorr_2 = massT.Is_Corr_2Null() ? 0.0 : massT._Corr_2,
                        paramCorr1 = massT.IsCorr1Null() ? 0.0 : massT.Corr1,
                        paramCorr2 = massT.IsCorr1Null() ? 0.0 : massT.Corr2,
                        
                    };

            int massTagCounter = 0;
            foreach (var m in query2)
            {
                LNquantitate.MassTag mt = new LNquantitate.MassTag();
                float[] corrections = new float[4];

                corrections[0] = (float)m.paramCorr_2/100;
                corrections[1] = (float)m.paramCorr_1/100;
                corrections[2] = (float)m.paramCorr1/100;
                corrections[3] = (float)m.paramCorr2/100;
                
                mt.label = m.label;
                mt.reference = m.paramRef;
                mt.exactMass = m.paramMass;
                mt.corrections = (float[])corrections.Clone();

                massTags.Add(mt);
                massTagCounter++;
            }


          
            //Setting up matrixes for isotopic correction
            if (useCorrection)
            {
                I0 = new double[massTagCounter];
                corr = new double[massTagCounter, 4];

                int im = 0;
                foreach (LNquantitate.MassTag mt in massTags)
                {
                    I0[im] = (double)1.0;
                    for (int j = 0; j < 4; j++)
                    {
                        I0[im] -= (double)mt.corrections[j];
                        corr[im, j] = (double)mt.corrections[j];
                    }
                    im++;
                }

                Cmat = new double[massTagCounter, massTagCounter];

                for (int i = 0; i < massTagCounter; i++)
                {
                    Cmat[i, i] = I0[i];

                    if (i - 2 >= 0) Cmat[i, i - 2] = corr[i, 0];
                    if (i - 1 >= 0) Cmat[i, i - 1] = corr[i, 1];
                    if (i + 1 < massTagCounter) Cmat[i, i + 1] = corr[i, 2];
                    if (i + 2 < massTagCounter) Cmat[i, i + 2] = corr[i, 3];
                }

                C = Matrix.Create(Cmat);

                Cdet = C.Determinant();

            }           


        }

        public void addDataTable(DataTable dt)
        {
            //original_dt = dt.Clone();
        }

        public Comb.mzI[] centroiding(Comb.mzI[] experimentalSpectrum, Comb.mzI[] mz, double width)
        {
            throw new NotImplementedException();
        }

        public Comb.mzI[] getFittedSpectrum(DataRow fittedDataRow, Comb.mzI[] experimentalSpectrum)
        {


            Comb.mzI[] fittedSpectrum = new Comb.mzI[massTags.Count+2];

            int counter = 1;
            foreach(LNquantitate.MassTag mt in massTags)
            {
                try
                {
                    string fittedMassLbl = "q_fittedMass_" + mt.label;
                    string repIonLbl = "q_reporterIon_"+mt.label;
                    fittedSpectrum[counter].mz = Double.Parse(fittedDataRow[fittedMassLbl].ToString());
                    fittedSpectrum[counter].I = Double.Parse(fittedDataRow[repIonLbl].ToString());
                    counter++;
                }
                catch { }
            }




            return fittedSpectrum;

        }

        public double getMonoisotMass(DataRow initialData)
        {
            double monoisotMass = 0.0;

            //Calculate the composition for a given sequence
            string sSequence = initialData["Sequence"].ToString().Trim();
            string sequence = sSequence;

            if (sSequence.Contains('.'))
            {
                string[] sSeqSplit = sSequence.Split('.');
                sequence = sSeqSplit[1].ToString();
            }


            Comb.compStrt[] composition = AminoacidList.calComposition(sequence.ToString(), aminoacids);

            //Calculate the Intensities and m/z for a given chemical composition.
            double calibrationError = 0; //there is no calibration error for itraq
            int charge = int.Parse(initialData["Charge"].ToString());

            Comb.mzI[] mz = Comb.calIntensities(composition,
                                                isotopes,
                                                charge,
                                                calibrationError,
                                                1);


            monoisotMass = mz[0].mz;

            return monoisotMass;
        }


        #endregion

    }

    public class LNquantitate
    {

        public class MassTag
        {
            private string labelVal;
            private double exactMassVal;
            private bool referenceVal;
            private float[] correctionsVal;  //=new float[4];
            private Comb.mzI repIonVal;

                        //corrections[0] == correction over -2 Da 
                        //corrections[1] == correction over -1 Da 
                        //corrections[2] == correction over +1 Da 
                        //corrections[3] == correction over +2 Da 

            public string label
            {
                get { return labelVal; }
                set { labelVal = value; }
            }

            public double exactMass
            {
                get { return exactMassVal; }
                set { exactMassVal = value; }
            }
            public bool reference
            {
                get { return referenceVal; }
                set { referenceVal = value; }
            }
            public float[] corrections
            {
                get { return correctionsVal; }
                set { correctionsVal = value; }
            }
            public Comb.mzI repIon
            {
                get { return repIonVal; }
                set { repIonVal = value; }
            }

            
        }

        public enum quantitationStrategy
        {
            O18_MSMS,
            O18_ZS,
            O18_HR,
            SILAC,
            SILAC_HR,
            iTRAQ,
            AveragineModel

        }

        public enum ionType
        {
            a,
            y,
            b
        }

        public enum reporterMethodType 
        {
            mostIntense,
            leastDeltaMZ,
            sumI
        }

        public enum spectrumType
        {
            centroid,
            profile
        }

        public struct ionSeriesStrt
        {
            private string sequenceVal;
            private int zVal;
            private string ionVal;

            public string sequence 
            {
                get { return sequenceVal; }
                set { sequenceVal = value; }
            }
            public int z 
            {
                get { return zVal; }
                set { zVal = value; }
            }
            public string ion 
            {
                get { return ionVal; }
                set { ionVal = value; }
            }
        }


        #region sweep fit

        public static fitDataStrt fitbySweep(Comb.mzI[] intensities,
                                                Comb.mzI[] expData,
                                                fitDataStrt fitData,
                                                int z,
                                                double deltaR,
                                                int[] iterations,
                                                bool fitWindowShow,
                                                //Comb.mzI[] peaks,
                                                //double sigmaForSQ,
                                                instrumentParamsStrt instrParams,
                                                out double sumSQfinal,
                                                double deltaMZmargin
                                                )
        {

            fitDataStrt fitDataAdj = new fitDataStrt();

            Comb.mzI[] envelope;
            OPfitWindow fitWindow = new OPfitWindow();

            if (fitWindowShow)
            {
                fitWindow.Show();
                fitWindow.Activate();
                fitWindow.init(expData);
            }


            envelope = Gaussians.calEnvelope(intensities, expData, fitData, z, deltaR, instrParams.instResolution);

            //Calculate the isotopic envelope
            double[] fitDataMtx = new double[8];
            double[] fitDataMtxOrigin = new double[8];
            fitDataMtxOrigin = LNquantitate.fitDatatoMatrix(fitData);
            fitDataMtxOrigin[5] = 0;

            fitDataAdj = fitData;
            double sumSQ = double.MaxValue;
            double sumSQold = double.MaxValue;


            double decrement = 0.6;

            double[] percent_init = new double[fitDataMtx.GetUpperBound(0) + 1];
            percent_init[1] = 0.2;
            percent_init[2] = 0.2;
            percent_init[3] = 0.2;
            percent_init[4] = 0.2;
            percent_init[5] = 0.2;
            percent_init[6] = 0.2;
            percent_init[7] = 0.2;

            double[] percent = new double[fitDataMtx.GetUpperBound(0) + 1];

            //Sweep
            for (int k = 1; k <= 4; k++)
            {
                fitData = fitDataAdj;

                for (int i = 1; i <= percent.GetUpperBound(0); i++)
                {
                    percent[i] = percent_init[i] * Math.Pow(decrement, (double)(k - 1));
                }


                for (int i = 1; i <= 7; i++)
                {

                    fitData = fitDataAdj;
                    fitDataMtx = fitDatatoMatrix(fitData);


                    for (int j = 0; j <= iterations[i]; j++)
                    {

                        DateTime timeInit = DateTime.Now;

                        if (i == 4) // deltaMZ
                        {
                            //fitDataMtx[i] = fitDataMtxOrigin[i]+(double)((-3*fitData.sigma/(iterations[i]/2) + fitData.sigma * (double)j / ((double)iterations[i])) * (Math.Pow(10,(double)(1-k))));
                            //fitDataMtx[i] = fitDataMtxOrigin[i] + Math.Pow(10, (double)(1 - k)) * ((double)((-50 * fitData.sigma / z) + (double)(100 * j * fitData.sigma / (iterations[i] * z))));
                            fitDataMtx[i] = fitDataMtxOrigin[i] + deltaMZmargin * Math.Pow(10, (double)(1 - k)) * ((double)((-5 * fitData.sigma / z) + (double)(10 * j * fitData.sigma / (iterations[i] * z))));
                        }
                        else
                        {
                            fitDataMtx[i] = (fitDataMtxOrigin[i] * (1 - percent[i] + 2 * percent[i] * (double)j / (double)iterations[i]));
                        }

                        Comb.mzI[] envelopeOld_test = (Comb.mzI[])envelope.Clone();


                        fitData = MatrixtofitData(fitDataMtx);

                        envelope = null;
                        envelope = Gaussians.calEnvelope(intensities, expData, fitData, z, deltaR, instrParams.instResolution);
                        fitDataMtx = fitDatatoMatrix(fitData);


                        double testingEnvelopeSQ = 0;

                        for (int tt = 0; tt <= envelope.GetUpperBound(0); tt++)
                        {
                            testingEnvelopeSQ += (envelope[tt].I - envelopeOld_test[tt].I);
                        }

                        sumSQ = LNquantitate.sumSquares(expData, envelope);

                        if (sumSQ < sumSQold && fitData.f <= 1.0)
                        {
                            fitDataAdj = fitData;
                            sumSQold = sumSQ;
                            fitDataMtxOrigin = fitDatatoMatrix(fitDataAdj);
                        }


                        TimeSpan time = DateTime.Now - timeInit;
                        fitWindow.addStep(fitData, sumSQ, time);

                    }




                }



            }

            envelope = Gaussians.calEnvelope(intensities, expData, fitDataAdj, z, deltaR, instrParams.instResolution);




            sumSQfinal = sumSQ;

            fitWindow = null;

            return fitDataAdj;
        }



        #endregion


        #region Newton-Gauss fit


        private static Comb.mzI[] fittingFunction(  Comb.mzI[] intensities,
                                                    Comb.mzI[] data,
                                                    fitDataStrt fitData,
                                                    int z,
                                                    double deltaR, 
                                                    Resolution res)
        {
            return Gaussians.calEnvelope(intensities, data, fitData, z, deltaR, res);
        }

       
        public static fitDataStrt fitNewtonGauss(Comb.mzI[] intensities,
                                                Comb.mzI[] expData,
                                                fitDataStrt fitData,
                                                int z,
                                                double label,
                                                double _rsh,
                                                bool fitWindowShow,
                                                int[] paramsUsed,
                                                out double sumSQfinal,
                                                out fitDataStrt SDfitData,
                                                out string exit,
                                                quantitationStrategy strategy,
                                                Resolution resolution,
                                                confNGfitParamsStrt configNG
                                                )
        {

            
            //Save the initial parameters
            fitDataStrt originalFitData = new fitDataStrt();
            originalFitData.A = fitData.A;
            originalFitData.alpha = fitData.alpha;
            originalFitData.B = fitData.B;
            originalFitData.deltaMz = fitData.deltaMz;
            originalFitData.f = fitData.f;
            originalFitData.sigma = fitData.sigma;
            originalFitData.signoise = fitData.signoise;

            /*
            //List of parameters used for fitting.
            paramsUsed[1]  //A (A quantity)
            paramsUsed[2]  //alpha (leptokurtosis)
            paramsUsed[3]  //B (B quantity)
            paramsUsed[4]  //deltaMz (experimental mass deviation)
            paramsUsed[5]  //f (efficiency)
            paramsUsed[6]  //sigma (gaussian width)
            paramsUsed[7]  //signoise (Background threshold)
            */

            int indexOfEfficiency = 0;
            for (int i = 1; i <= 5; i++)
            {
                indexOfEfficiency += paramsUsed[i];
            }

            int nParams = 0;
            for (int i = 1; i <= paramsUsed.GetUpperBound(0); i++)
            {
                nParams += paramsUsed[i];
            }

            fitDataStrt fittedData = new fitDataStrt();
            fitDataStrt fittedDataBest = new fitDataStrt();
            exit = "";

            //init the fit window
            DateTime timeInit = DateTime.Now;
            OPfitWindow fitWindow = new OPfitWindow();
            if (fitWindowShow)
            {
                fitWindow.Show();
                fitWindow.Activate();
                fitWindow.init(expData);
            }


            double[] RSH = new double[nParams + 1];

            for (int i = 1; i <= RSH.GetUpperBound(0); i++)
            {
                RSH[i] = _rsh; 
            }
             
            //RSH[1] = 1e-1; //A (A quantity)
            //RSH[2] = 1e-2; //alpha (leptokurtosis)
            //RSH[3] = 1e-1; //B (B quantity)
            //RSH[4] = 1e-2; //deltaMz (experimental mass deviation)
            //RSH[5] = 1e-2; //efficiency f
            //RSH[6] = 1e-2; //sigma (gaussian width)
            //RSH[7] = 1e-2; //signoise (Background threshold) 

            //declare variables for NG fitting

            double[] B = new double[nParams + 1];
            double[] A = new double[nParams + 1];
            B = fitDatatoMatrix(fitData, paramsUsed);
            A = (double[])B.Clone();

            double[] BANT = new double[nParams + 1];
            double[] DX = new double[nParams + 1];
            double[] R1 = new double[nParams + 1];
            double[] P = new double[nParams + 1];
            double[,] R = new double[nParams + 1, nParams + 1];
            double[,] RINV = new double[nParams + 1, nParams + 1];
            double[,] RINVbest = new double[nParams + 1, nParams + 1];
            double[,] H = new double[nParams + 1, nParams + 1];
            double[,] H1 = new double[nParams + 1, 2 * (nParams + 1)];
            double[] S = new double[expData.Length];

            int nIter = 1;
            double sumSQ;
            double sumSQ1;
            double sumSQ2 = 0;
            double sumSQbest;
            int nPointsSQ = 0;
            int sign;
            double Dpar;
            double DsumSQ;
            bool convergence = false;
            bool stepCorr = false;  //it is true when divergence must be corrected 
            //(one iteration without adjusting)

            Comb.mzI[] envelope = fittingFunction(intensities, expData, fitData, z, label, resolution);

            switch (resolution)
            {
                case Resolution.LOW:
                    sumSQ2 = sumSquares(expData, envelope, fitData.signoise, configNG.sumSQtolerance, out nPointsSQ);
                    break;

                case Resolution.HIGH:
                    //sumSQ1 = sumSquares(expData, Gaussians.calEnvelope(intensities, expData, fitData, z, deltaR, resolution), fitData.signoise, sumSQtolerance, out nPointsSQ);
                    sumSQ2 = sumSquares(expData, envelope, 0, expData.Length-1);
                    nPointsSQ = expData.Length;
                    break;
            }

            sumSQ1 = sumSQ2;
            writeFitWindowIfNeeded(fitData, fitWindowShow, timeInit, fitWindow, sumSQ2);

            //Adjust NG

            clearRandP(nParams, ref P, ref R);

            Comb.mzI[] fitSpectrum; 
            
            fitSpectrum = fittingFunction(intensities, expData, fitData, z, label,resolution);
                    

            
            for (int j = 0; j <= fitSpectrum.GetUpperBound(0); j++)
            {
                S[j] = expData[j].I - fitSpectrum[j].I;
            }

            double[,] derivMtx = derivative(RSH, B, paramsUsed, originalFitData, intensities, expData, z, label, resolution);

            //Matrix construction
            constructionOfRandP(expData, nParams, ref P, ref R, S, derivMtx);


            RINV = InvMatrix(R);


            for (int i = 1; i <= nParams; i++)
            {
                DX[i] = 0;
                for (int j = 1; j <= nParams; j++)
                {
                    DX[i] += RINV[i, j] * P[j];
                }
                A[i] = DX[i] + B[i];
            }


            // End of Adjust NG



            //Save preview data (if the iterations don't get a best value, we use these preview data)
            RINVbest = (double[,])RINV.Clone();
            sumSQbest = sumSQ2;
            fittedDataBest.A = fitData.A;
            fittedDataBest.alpha = fitData.alpha;
            fittedDataBest.B = fitData.B;
            fittedDataBest.deltaMz = fitData.deltaMz;
            fittedDataBest.f = fitData.f;
            fittedDataBest.sigma = fitData.sigma;
            fittedDataBest.signoise = fitData.signoise;

            int counter = 0;
                while (nIter < configNG.nMaxIter && !convergence && counter < configNG.nMaxIter)
                {
                    counter++;

                    if (!stepCorr)
                    {
                        //Adjust NG

                        clearRandP(nParams, ref P, ref R);

                        fitSpectrum = fittingFunction(intensities, expData, MatrixtofitData(B, paramsUsed, originalFitData), z, label, resolution);
                       
                        for (int j = 0; j <= fitSpectrum.GetUpperBound(0); j++)
                        {
                            S[j] = expData[j].I - fitSpectrum[j].I;
                        }

                        derivMtx = derivative(RSH, B, paramsUsed, originalFitData, intensities, expData, z, label, resolution);

                        //Matrix construction
                        constructionOfRandP(expData, nParams, ref P, ref R, S, derivMtx);

                        RINV = InvMatrix(R);
                        for (int i = 1; i <= nParams; i++) 
                        {
                            for (int j = 1; j <= nParams; j++)
                            {
                                if (double.IsNaN(RINV[i, j]))
                                {
                                    RINV =(double[,])RINVbest.Clone();
                                }
                            }
                        }

                        for (int i = 1; i <= nParams; i++)
                        {
                            DX[i] = 0;
                            for (int j = 1; j <= nParams; j++)
                            {
                                DX[i] += RINV[i, j] * P[j];
                            }
                            A[i] = DX[i] + B[i];

                        }



                        // End of Adjust NG

                        BANT = (double[])B.Clone();

                    }

                    B = (double[])A.Clone();
                   

                    fitDataStrt fitDataIter = new fitDataStrt();
                    fitDataIter = MatrixtofitData(B, paramsUsed, originalFitData);

                    fitSpectrum = fittingFunction(intensities, expData, fitDataIter, z, label, resolution);
                                      
                    sumSQ = 0;
                    switch (resolution)
                    {
                        case Resolution.LOW:
                            sumSQ = sumSquares(expData, fitSpectrum, fitDataIter.signoise, configNG.sumSQtolerance, out nPointsSQ);
                            break;

                        case Resolution.HIGH:
                            sumSQ = sumSquares(expData, fitSpectrum, 0, expData.Length - 1);                            
                            if (double.IsNaN(sumSQ))
                                sumSQ = double.MaxValue;
                            nPointsSQ = expData.Length;
                            break;
                    }
                    
                    
                    sumSQ2 = sumSQ;
                    sign = correctIfNeeded(ref configNG, ref A, BANT, ref DX, sumSQ1, sumSQ2, ref stepCorr);

                    Dpar = 0;
                    for (int i = 1; i <= nParams; i++)
                    {
                        Dpar += Math.Abs(DX[i] / BANT[i]);
                    }
                    DsumSQ = Math.Abs((sumSQ1 - sumSQ2) / sumSQ1);

                    // convergence is reached only when the last SQ improves, AND the difference
                    // between the last SQ's is small enough.
                    convergence = checkConvergence(Dpar, DsumSQ, configNG.PRS, out exit) && (sign == 1);

                    fittedData = MatrixtofitData(B, paramsUsed, originalFitData);

                    writeFitWindowIfNeeded(fittedData, fitWindowShow, timeInit, fitWindow, sumSQ2);

                    sumSQ1 = sumSQ2;
                    //check if we have encountered a minimum sumSQ
                     if (sumSQ2 <= sumSQbest)
                    {
                        nIter++;
                        sumSQbest = sumSQ2;
                        RINVbest = (double[,])RINV.Clone();

                        fittedDataBest.A = fittedData.A;
                        fittedDataBest.alpha = fittedData.alpha;
                        fittedDataBest.B = fittedData.B;
                        fittedDataBest.deltaMz = fittedData.deltaMz;
                        fittedDataBest.f = fittedData.f;
                        fittedDataBest.sigma = fittedData.sigma;
                        fittedDataBest.signoise = fittedData.signoise;

                    }

                }


            if (nIter >= configNG.nMaxIter)
            {
                exit = "Max iterations reached";
            }

            if (counter >= configNG.nMaxIter)
            {
                exit = "Max counter reached at iter = " + nIter;
            }


            //return the results
            //fittedData = MatrixtofitData(B);

            SDfitData = stDeviations(nPointsSQ, sumSQbest, RINVbest, paramsUsed);

            sumSQfinal = sumSQbest;

            if (fitWindowShow)
            {
                fitWindow = null;
            }

            return fittedDataBest;

        }

        private static void constructionOfRandP(Comb.mzI[] expData, int nParams, ref double[] P, ref double[,] R, double[] S, double[,] derivMtx)
        {
            for (int j = 0; j <= expData.GetUpperBound(0); j++)
            {
                for (int i = 1; i <= nParams; i++)
                {
                    for (int q = 1; q <= nParams; q++)
                    {

                        R[i, q] += derivMtx[j, i] * derivMtx[j, q];

                    }
                }
                for (int i = 1; i <= nParams; i++)
                {
                    P[i] += derivMtx[j, i] * S[j];
                }
            }
        }

        private static void writeFitWindowIfNeeded(fitDataStrt fitData,
                                                    bool fitWindowShow,
                                                    DateTime timeInit,
                                                    OPfitWindow fitWindow,
                                                    double sumSQ2)
        {
            if (fitWindowShow)
            {
                TimeSpan time = DateTime.Now - timeInit;
                fitWindow.addStep(fitData, sumSQ2, time);
            }
        }

        private static int correctIfNeeded(ref confNGfitParamsStrt configNG, ref double[] A, double[] BANT, ref double[] DX, double sumSQ1, double sumSQ2, ref bool stepCorr)
        {
            int sign;
            try
            {
                sign = Math.Sign(sumSQ1 - sumSQ2);
            }
            catch
            {
                sign = 0;
            }
            if (sign < 0)
            {
                corrDivergence(DX, BANT, configNG.alpha, out DX, out A);
                stepCorr = true;
            }
            else
            {
                stepCorr = false;
            }
            return sign;
        }

        private static void clearRandP(int nParams, ref double[] P, ref double[,] R)
        {
            for (int i = 1; i <= nParams; i++)
            {
                for (int j = 1; j <= nParams; j++)
                {
                    R[i, j] = 0;
                }
                P[i] = 0;
            }
        }

              
        
        private static fitDataStrt stDeviations(int nPoints, double sumSQ, double[,] RINV, int[] paramsUsed)
        {
            double[] sd = new double[RINV.GetUpperBound(0) + 1];

            double sdAdjust = Math.Sqrt(sumSQ / (nPoints - 1));

            for (int i = 1; i <= RINV.GetUpperBound(0); i++)
            {
                sd[i] = sdAdjust * Math.Sqrt(RINV[i, i]);
            }

            fitDataStrt SDforNotUsedParams = new fitDataStrt();
            SDforNotUsedParams.A = 0;
            SDforNotUsedParams.alpha = 0;
            SDforNotUsedParams.B = 0;
            SDforNotUsedParams.deltaMz = 0;
            SDforNotUsedParams.f = 0;
            SDforNotUsedParams.sigma = 0;
            SDforNotUsedParams.signoise = 0;

            fitDataStrt stDev = MatrixtofitData(sd, paramsUsed, SDforNotUsedParams);

            return stDev;

        }


        private static double[,] derivative(double[] RSH,
                                        double[] B,
                                        int[] paramsUsed,
                                        fitDataStrt originalFitData,
                                        Comb.mzI[] intensities,
                                        Comb.mzI[] expData,
                                        int z,
                                        double deltaR,
                                        Resolution res)
        {

            double PP;
            double[,] derivMtx = new double[expData.GetLength(0), B.GetLength(0)];
            double[] H = new double[B.GetLength(0)];

            for (int i = 1; i <= B.GetUpperBound(0); i++)
            {
                if (H[i] != 0)
                {
                    H[i] = RSH[i] * Math.Abs(B[i]);
                }
                else
                {
                    H[i] = RSH[i];
                }
            }

            for (int i = 1; i <= B.GetUpperBound(0); i++)
            {
                //if (H[i] != 0)
                //{
                //    H[i] = RSH[i] * Math.Abs(B[i]);
                //}
                //else
                //{
                //    H[i] = RSH[i];
                //}

                PP = B[i];
                B[i] = PP + H[i];
                //B[i] = PP * (1 + RSH[i]);

                fitDataStrt ftDtDerivative = new fitDataStrt();
                ftDtDerivative = MatrixtofitData(B, paramsUsed, originalFitData);
                Comb.mzI[] dtaDerivPlus = Gaussians.calEnvelope(intensities, 
                                                                expData, 
                                                                ftDtDerivative, 
                                                                z, 
                                                                deltaR,
                                                                res);
                B[i] = PP - H[i];
                //B[i] = PP * (1 - RSH[i]);
                 ftDtDerivative = MatrixtofitData(B, paramsUsed, originalFitData);
                Comb.mzI[] dtaDerivMinus = Gaussians.calEnvelope(intensities, 
                                                                    expData, 
                                                                    ftDtDerivative, 
                                                                    z,
                                                                    deltaR,
                                                                    res);

                for (int j = 1; j <= dtaDerivPlus.GetUpperBound(0); j++)
                {
                    derivMtx[j, i] = (dtaDerivPlus[j].I - dtaDerivMinus[j].I) / (2 * H[i]);
                }
                
                B[i] = PP;
            }



            return derivMtx;

        }




        private static double[,] InvMatrix(double[,] Matrix)
        {
            //check whether the dimensions are correct
            int dimI = Matrix.GetUpperBound(0);
            int dimJ = Matrix.GetUpperBound(1);
            if (dimI != dimJ)
            {
                return null;
            }

            double[,] H = new double[dimI + 1, dimJ + 1];
            double[,] H1 = new double[dimI + 1, (dimJ + 1) * 2];
            double A;
            double B;
            double[,] invMat = new double[dimI + 1, dimJ + 1];


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
                    if ((dimJ + i) == j)
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


        private static void corrDivergence(double[] DXold,
                                           double[] Aold,
                                           double alpha,
                                           out double[] DX,
                                           out double[] A)
        {

            DX = (double[])DXold.Clone();
            A = (double[])Aold.Clone();

            for (int i = 1; i <= DXold.GetUpperBound(0); i++)
            {
                DX[i] = DX[i] * alpha;
                A[i] += DX[i];
            }

            return;
        }

        private static bool checkConvergence(double DPar, double DsumSQ, double PRS, out string exit)
        {
            bool conv = false;

            exit = "";

            if (DPar > PRS && DsumSQ > PRS)
            {
                conv = false;
            }
            else
            {
                if (DPar <= PRS)
                {
                    exit = "parameters stable";
                    conv = true;
                }
                if (DsumSQ <= PRS)
                {
                    exit = "SumSQ stable";
                    conv = true;
                }
            }

            return conv;
        }


        #endregion


        #region sumSquares


        public static double sumSquares(Comb.mzI[] expData, 
                                        Comb.mzI[] fittedData)
        {
            double sumSQ = 0;

            for (int j = 0; j <= expData.GetUpperBound(0); j++)
            {
                sumSQ += Math.Pow(expData[j].I - fittedData[j].I, 2);
            }

            return sumSQ;

        }

        public static double sumSquares(Comb.mzI[] expData, 
                                        Comb.mzI[] fittedData, 
                                        double background, 
                                        double tolerance, 
                                        out int nPointsUsed)
        {
            double sumSQ = 0;

            double backminus = background * (1.0 - tolerance);
            double backplus = background * (1.0 + tolerance);

            int pointLeft = 0;
            int pointRight = expData.GetUpperBound(0);

            // this is to exclude points before the proper spectrum
            for (int j = 0; j <= Math.Floor((float)expData.GetUpperBound(0) / 2); j++)
            {
                if (fittedData[j].I < (backminus) || fittedData[j].I > (backplus))
                {
                    pointLeft = j;
                    break;
                }
            }

            // this is toi exclude points after the proper spectrum
            for (int j = expData.GetUpperBound(0); j > Math.Floor((float)expData.GetUpperBound(0) / 2); j--)
            {
                if (fittedData[j].I < (backminus) || fittedData[j].I > (backplus))
                {
                    pointRight = j;
                    break;
                }
            }

            // once we know where the proper spectrum is...
            for (int j = pointLeft; j <= pointRight; j++)
            {
                sumSQ += Math.Pow(expData[j].I - fittedData[j].I, 2);
            }

            nPointsUsed = pointRight - pointLeft + 1;

            return sumSQ;

        }

        public static double sumSquares(Comb.mzI[] expData, 
                                        Comb.mzI[] fittedData, 
                                        int rangeMin, 
                                        int rangeMax)
        {

            double sumSQ = 0;
            if (rangeMax <= rangeMin)
            {
                sumSQ = double.MaxValue;
                return sumSQ;
            }

            for (int j = rangeMin; j <= rangeMax; j++)
            {
                if (fittedData[j].I >= 0)
                {
                    sumSQ += Math.Pow((expData[j].I - fittedData[j].I), 2);
                }
            }

            return sumSQ;

        }

        public static double sumSquares(Comb.mzI[] expData,
                                        Comb.mzI[] fittedData,
                                        Comb.mzI[] peaks,
                                        double sigmaPeak,
                                        out int nPointsUsed)
        {

            double sumSQ = 0.0;
            bool peakFound = false;
            int peaksFound=0;
            nPointsUsed = 0;
            double alpha = 2;
            double weight = 0.0;


            //for each fitted peak
            for (int i = 0; i < peaks.GetUpperBound(0); i++)
            {
                peakFound = false;
                //declare a minimum and a maximum m/z
                double minMZ = peaks[i].mz - sigmaPeak / 2;
                double maxMZ = peaks[i].mz + sigmaPeak / 2;

                for (int j = 0; j <= fittedData.GetUpperBound(0); j++)
                {
                    if (fittedData[j].mz >= minMZ && fittedData[j].mz <= maxMZ)
                    {
                        if (!peakFound)
                        {
                            peaksFound++;
                            peakFound = true;
                        }
                        sumSQ += Math.Pow(expData[j].I, alpha) * ((expData[j].I - fittedData[j].I) * (expData[j].I - fittedData[j].I));

                        weight += Math.Pow(expData[j].I, alpha);

                        nPointsUsed++;
                    }

                }
                
            }

            sumSQ = sumSQ / weight;

            return sumSQ;
        }


        #endregion


        #region data conversors

        public static double[] fitDatatoMatrix(fitDataStrt fitData)
        {
            double[] mtxData = new double[8];

            mtxData[1] = fitData.A;
            mtxData[2] = fitData.alpha;
            mtxData[3] = fitData.B;
            mtxData[4] = fitData.deltaMz;
            mtxData[5] = fitData.f;
            mtxData[6] = fitData.sigma;
            mtxData[7] = fitData.signoise;

            return mtxData;
        }
        public static fitDataStrt MatrixtofitData(double[] mtxData)
        {
            fitDataStrt fitData = new fitDataStrt();

            fitData.A = mtxData[1];
            fitData.alpha = mtxData[2];
            fitData.B = mtxData[3];
            fitData.deltaMz = mtxData[4];
            fitData.f = mtxData[5];
            fitData.sigma = mtxData[6];
            fitData.signoise = mtxData[7];

            return fitData;
        }

        public static double[] fitDatatoMatrix(fitDataStrt fitData, int[] paramsUsed)
        {

            int totalParams = 0;
            for (int i = 1; i <= paramsUsed.GetUpperBound(0); i++)
            {
                totalParams += paramsUsed[i];
            }

            double[] mtxData = new double[8];

            mtxData[1] = fitData.A;
            mtxData[2] = fitData.alpha;
            mtxData[3] = fitData.B;
            mtxData[4] = fitData.deltaMz;
            mtxData[5] = fitData.f;
            mtxData[6] = fitData.sigma;
            mtxData[7] = fitData.signoise;


            double[] mtxDataCut = new double[totalParams + 1];

            int j = 1;
            for (int i = 1; i <= mtxData.GetUpperBound(0); i++)
            {
                if (paramsUsed[i] != 0)
                {
                    mtxDataCut[j] = mtxData[i];
                    j++;
                }
            }

            return mtxDataCut;
        }

        public static fitDataStrt MatrixtofitData(double[] mtxDataCut, int[] paramsUsed, fitDataStrt originalFitData)
        {

            double[] mtxData = new double[8];

            mtxData[1] = originalFitData.A;
            mtxData[2] = originalFitData.alpha;
            mtxData[3] = originalFitData.B;
            mtxData[4] = originalFitData.deltaMz;
            mtxData[5] = originalFitData.f;
            mtxData[6] = originalFitData.sigma;
            mtxData[7] = originalFitData.signoise;

            int j = 1;
            for (int i = 1; i <= paramsUsed.GetUpperBound(0); i++)
            {
                if (paramsUsed[i] != 0)
                {
                    mtxData[i] = mtxDataCut[j];
                    j++;
                }
            }

            return MatrixtofitData(mtxData);

        }

        #endregion


        #region utilities

        public static int numOfMatchingPeaks(double A, double B, Comb.mzI[] expCentroidedSpectrum, Comb.mzI[] theorSpectrum)
        {
            int dimTh = theorSpectrum.Length;
            int dimExp = expCentroidedSpectrum.Length;
            int dimMin = dimTh < dimExp ? dimTh : dimExp;
            int nPoints = 0;
            double tolPeak = (A + B) / 100;
            for (int i = 0; i < dimMin; i++)
            {
                if (theorSpectrum[i].I > tolPeak || expCentroidedSpectrum[i].I > tolPeak)
                {
                    nPoints++;
                }
            }
            return nPoints;
        }

        /// <summary>
        /// Count up the number of aminoacides from a given list (aminoacidesList) present in a given sequence
        /// </summary>
        /// <param name="sequence">target sequence</param>
        /// <param name="aminoacidesList">aminoacides to be searched</param>
        /// <returns>number of aminoacides from the aminoacidesList in sequence</returns>
        public static int countAminoacides(string sequence, char[] aminoacidesList)
        {
            int counter=0;
            char[] sequenceArray = sequence.ToCharArray();

            for (int i = 0; i < aminoacidesList.GetLength(0); i++)
            {
                for (int j = 0; j < sequenceArray.GetLength(0); j++)
                {
                    if (sequenceArray[j] == aminoacidesList[i])
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }


        /// <summary>
        /// Report the ion reporter requested, without any correction
        /// </summary>
        /// <param name="spectrum">experimental spectrum</param>
        /// <param name="labelMass">mass of the reporter to be acquired</param>
        /// <param name="tolerance">total mass range allowed</param>
        /// <param name="threshold">a certain threshold for the reporter ion</param>
        /// <param name="repMethod">acquisition method</param>
        /// <returns>intensity/Area of the reporter ion</returns>
        public static Comb.mzI calIonReporter(Comb.mzI[] spectrum,
                                            double labelMass,
                                            double tolerance,
                                            double threshold,
                                            LNquantitate.reporterMethodType repMethod)                                            
        {

            Comb.mzI reporterIon=new Comb.mzI();

            double[] specMZ = new double[spectrum.GetLength(0)];

            for (int z = spectrum.GetLowerBound(0); z <= spectrum.GetUpperBound(0); z++)
            {
                specMZ[z] = spectrum[z].mz;
            }


            double minMass = labelMass - tolerance / 2;
            double maxMass = labelMass + tolerance / 2;

            // prior to v1.3.31 this line was:
            // int exactMassPos = Utilities.find(specMZ, labelMass);
            // it was giving problems with iTRAQ when only one or no spectral lines were present
            // this line seemed to have no effect in the quantification, so it was changed
            int exactMassPos = 0;// Utilities.find(specMZ, labelMass);
            int minMassPos = 0; //Utilities.find(specMZ, minMass);
            int maxMassPos = spectrum.GetLength(0)-1; //Utilities.find(specMZ, maxMass);

            for (int i = 1; i < spectrum.GetLength(0); i++)
            {
                if (spectrum[i].mz < minMass)
                {
                    minMassPos = i+1;
                }

                if (spectrum[i].mz > maxMass)
                {
                    maxMassPos = i - 1;
                    break;
                }
            }

            for (int i = 1; i < spectrum.GetLength(0); i++)
            {
                if (spectrum[i].mz < labelMass)
                {
                    exactMassPos = i;
                }
                if (spectrum[i].mz >= labelMass)
                {
                    if(Math.Abs(labelMass-spectrum[i].mz)<Math.Abs(labelMass-spectrum[exactMassPos].mz))
                    {
                        exactMassPos =i;
                    }
                    break;
                }
            }


            switch (repMethod)
            {
                case reporterMethodType.leastDeltaMZ:
                    for (int i = 0; i < minMassPos; i++)
                    {
                        if (spectrum[i].mz >= minMass && spectrum[i].mz <= maxMass)
                        {
                            if (spectrum[exactMassPos - i].I >= threshold)
                            {
                                reporterIon.I = spectrum[exactMassPos - i].I;
                                reporterIon.mz = spectrum[exactMassPos - i].mz;
                                break;
                            }
                            if (spectrum[exactMassPos + i].I >= threshold)
                            {
                                reporterIon.I = spectrum[exactMassPos + i].I;
                                reporterIon.mz = spectrum[exactMassPos + i].mz;
                                break;
                            }
                        }
                    }
                    break;
                case reporterMethodType.mostIntense:
                    for (int i = minMassPos; i <= maxMassPos; i++)
                    {
                        if (spectrum[i].mz >= minMass && spectrum[i].mz <= maxMass)
                        {
                            if (spectrum[i].I >= reporterIon.I && spectrum[i].I >= threshold)
                            {
                                reporterIon.I = spectrum[i].I;
                                reporterIon.mz = spectrum[i].mz;
                            }
                        }
                    }
                    break;
                case reporterMethodType.sumI:
                    
                    for (int i = minMassPos; i <= maxMassPos; i++)
                    {
                        if (spectrum[i].mz >= minMass && spectrum[i].mz <= maxMass)
                        {
                            reporterIon.I += spectrum[i].I;
                        }
                    }
                   
                    int middlePos = (int)((maxMassPos - minMassPos) / 2);
                    reporterIon.mz = spectrum[middlePos].mz;

                    if (reporterIon.I < threshold) reporterIon.I = 0.0; 
                    break;
            }

            return reporterIon;
        }



        /// <summary>
        /// NOT YET IMPLEMENTED
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="z"></param>
        /// <param name="ionType"></param>
        /// <param name="aminoacidList"></param>
        /// <returns></returns>
        public static ionSeriesStrt[] calIonSeries( string sequence, 
                                                    int z, 
                                                    ionType ionType, 
                                                    AminoacidList[] aminoacidList)
        {


            sequence = sequence.ToUpper();

            ArrayList modifList = new ArrayList();

            for (int i = aminoacidList.GetLowerBound(0); i <= aminoacidList.GetUpperBound(0); i++)
            {
                if (aminoacidList[i].equivalent != null)
                {
                    aminoacidStrt amod = new aminoacidStrt(aminoacidList[i].code1, aminoacidList[i].equivalent, null);
                    modifList.Add(amod);
                }
            }
                        

            foreach (aminoacidStrt aa in modifList)
            {
                if (sequence.Contains(aa.code1))
                {
                    sequence = sequence.Replace(aa.code1, aa.equivalent);
                }
            }

            ionSeriesStrt[] ionSerie = new ionSeriesStrt[5];

            switch (ionType)
            {
                case ionType.b:                    
                    break;
                case ionType.y:
                    break;
                case ionType.a:
                    break;
                default:
                    return null;
            }
            
        
            return ionSerie;
        }

        
        public static double calMHmass(string _sequence, 
                                        AminoacidList[] _aminoacids, 
                                        isotList[][] _isotopes)
        {
            double protonMass = 1.007276466812;  // source: http://physics.nist.gov/cgi-bin/cuu/Value?mpu

            double mass=0.0;


            Comb.compStrt[] composition;
            if (_aminoacids.Length > 0)
            {
                composition = AminoacidList.calComposition(_sequence, _aminoacids);
            }
            else { return 0.0; }


            foreach (Comb.compStrt element in composition)
            {
                for (int i = 0; i < _isotopes.GetUpperBound(0); i++)
                {
                    if (element.Elem == _isotopes[i][0].Elem)
                    {
                        mass += (double)element.Nats * _isotopes[i][0].Mass;
                        break;
                    }
                }           
            
            }



            //Add a proton
            
            // Not correct
            //string H = "H";
            //for (int i = 0; i < _isotopes.GetUpperBound(0); i++)
            //{
            //    if (_isotopes[i][0].Elem==H)
            //    {
            //        mass +=  _isotopes[i][0].Mass;
            //        break;
            //    }
            //}
            mass += protonMass;



            return mass;
        }




        public static string getAminoacidesFileName(string _methodChosen, 
                                                    qMethodsSchema.Quanmethods quanMethods)
        {
            string fileName="";

            string methodKey = _methodChosen;

            var query =
                          from meth in quanMethods.method
                          //where meth.method_id_key.Trim() == methodKey
                          select new
                          {
                             aminoacidsFile = meth.aminoacids_file,
                             method = meth.method_id_name
                         };


            foreach (var m in query)
            {   
                if(m.method.Trim()==methodKey.Trim()) fileName = m.aminoacidsFile.ToString();
            }

            return fileName;
        }

        #endregion


    }
}
