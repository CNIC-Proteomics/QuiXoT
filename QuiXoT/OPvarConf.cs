using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace QuiXoT
{
    public partial class OPvarConf : Form
    {
        double[] Vvalues;
        double[] Xvalues;
        int[] NsSim;
        int[] NpSim;
        int NqSim = 600;
        int NmaxS;
        int NmaxP;
        double Kconstant;
        double sigma2S = 0;
        double sigma2P = 0;
        double sigma2Q = 0;
        double mu = 0;
        //double[,,] V;
        //double[,,] X;
        DataSet dset;
        double percent = 0.05;
        string dataToSave = "";
        string XMLfile = "";

        bool saveFirstSimulation = false;

        // medianConstant = 1/DISTR.NORM.ESTAND.INV(3/4)
        double medianConstant = 1.4826022185056;

        public OPvarConf()
        {
            InitializeComponent();
        }

        public OPvarConf(DataSet dsetOriginal, double sigma2S, double sigma2P, double sigma2Q, string filePath)
        {
            InitializeComponent();

            tbxFilter.Text = "st_excluded=''";
            cbxNexp.SelectedIndex = 4;
            dset = dsetOriginal.Copy();
            XMLfile = filePath;
            try
            {
                DataRow[] dset3 = dsetOriginal.Copy().Tables[3].Select("st_excluded=''");

                NqSim = int.Parse(dset.Tables[1].Select()[0]["Nq"].ToString());
                NsSim = doubleToIntArray(getArrayPerP(dset3, "scan_per_peptide"));
                NpSim = doubleToIntArray(getArrayPerQ(dset3, "pep_per_protein"));
                NmaxS = NsSim.Max();
                NmaxP = NpSim.Max();
            }
            catch { /* otherwise, the default value is used */ }

            tbxMaxScanPerPep.Text = NmaxS.ToString();
            tbxMaxPepPerProt.Text = NmaxP.ToString();
            tbxNumProts.Text = NqSim.ToString();
        }

        private ArrayList calculateOneExperiment(DataSet dsetOriginal,
                                                int Nq,
                                                bool saveAsXML,
                                                method sigmaCalculationMethod)
        {
            ArrayList resultingVars = new ArrayList();

            DataRow[] dset1 = dsetOriginal.Copy().Tables[1].Select();
            DataRow[] dset3 = new DataRow[0];

            string message = "";
            string filter = tbxFilter.Text.Trim();
            tbxFilter.ForeColor = Color.Black;

            if (filter == "")
                dset3 = dsetOriginal.Copy().Tables[3].Select("st_excluded=''");
            else
            {
                try
                {
                    dset3 = dsetOriginal.Copy().Tables[3].Select(filter);
                }
                catch
                {
                    tbxFilter.ForeColor = Color.Red;
                    dset3 = dsetOriginal.Copy().Tables[3].Select();
                }
            }

            try
            {
                Xvalues = getArrayPerS(dset3, "Xs");
                Vvalues = getArrayPerS(dset3, "Vs");
                NsSim = doubleToIntArray(getArrayPerP(dset3, "scan_per_peptide"));
                NpSim = doubleToIntArray(getArrayPerQ(dset3, "pep_per_protein"));
                NmaxS = int.Parse(tbxMaxScanPerPep.Text);
                NmaxP = int.Parse(tbxMaxPepPerProt.Text);
            }
            catch
            {
                tbxFilter.ForeColor = Color.Blue;
                dset3 = dsetOriginal.Copy().Tables[3].Select();

                Xvalues = getArrayPerS(dset3, "Xs");
                Vvalues = getArrayPerS(dset3, "Vs");
                NsSim = doubleToIntArray(getArrayPerP(dset3, "scan_per_peptide"));
                NpSim = doubleToIntArray(getArrayPerQ(dset3, "pep_per_protein"));
                NmaxS = NsSim.Max();
                NmaxP = NpSim.Max();
            }

            mu = 0; // calculated from original data would be avgXqCalculation(dset3)
            tbxKconstant.ForeColor = Color.Black;

            try
            {
                Kconstant = double.Parse(tbxKconstant.Text);
            }
            catch
            {
                tbxKconstant.ForeColor = Color.Red;
                Kconstant = double.Parse(dset1[0]["ct_K"].ToString());
            }

            if (Kconstant<=0)
            {
                tbxKconstant.ForeColor = Color.Red;
                Kconstant = double.Parse(dset1[0]["ct_K"].ToString());
            }

            sigma2S = double.Parse(dset1[0]["ct_sigma2S"].ToString());
            sigma2P = double.Parse(dset1[0]["ct_sigma2P"].ToString());
            sigma2Q = double.Parse(dset1[0]["ct_sigma2Q"].ToString());

            double[, ,] Vs;
            double[, ,] Xs;
            simulateExperiment(out Vs, out Xs, Nq);

            if (Vs == null || Xs == null)
                return null;

            #region gather parameters from form

            double sigma2Sstart = 0;
            double sigma2Sfinal = 0.2;
            double sigma2Sincrement = 0.001;

            double sigma2Pstart = 0;
            double sigma2Pfinal = 0.2;
            double sigma2Pincrement = 0.001;

            double sigma2Qstart = 0;
            double sigma2Qfinal = 0.2;
            double sigma2Qincrement = 0.001;

            // gather parameters for scans
            try
            {
                sigma2Sstart = double.Parse(txbSigma2Sstart.Text);
            }
            catch
            {
                sigma2Sstart = 0; //default value
            }

            try
            {
                sigma2Sfinal = double.Parse(txbSigma2Send.Text);
            }
            catch
            {
                sigma2Sfinal = 0.2; //default value
            }

            try
            {
                sigma2Sincrement = double.Parse(txbSigma2Sstep.Text);
            }
            catch
            {
                sigma2Sincrement = 0.001; //default value
            }

            // gather parameters for peptides
            try
            {
                sigma2Pstart = double.Parse(txbSigma2Pstart.Text);
            }
            catch
            {
                sigma2Pstart = 0; //default value
            }

            try
            {
                sigma2Pfinal = double.Parse(txbSigma2Pend.Text);
            }
            catch
            {
                sigma2Pfinal = 0.2; //default value
            }

            try
            {
                sigma2Pincrement = double.Parse(txbSigma2Pstep.Text);
            }
            catch
            {
                sigma2Pincrement = 0.001; //default value
            }

            // gather parameters for proteins
            try
            {
                sigma2Qstart = double.Parse(txbSigma2Qstart.Text);
            }
            catch
            {
                sigma2Qstart = 0; //default value
            }

            try
            {
                sigma2Qfinal = double.Parse(txbSigma2Qend.Text);
            }
            catch
            {
                sigma2Qfinal = 0.2; //default value
            }

            try
            {
                sigma2Qincrement = double.Parse(txbSigma2Qstep.Text);
            }
            catch
            {
                sigma2Qincrement = 0.001; //default value
            }

            try
            {
                percent = double.Parse(tbxPercent.Text) / 100;
            }
            catch
            {
                sigma2Qincrement = 0.001; //default value
            }

            if (dataToSave == "\n1")
            {
                dataToSave = "XMLfile = " + XMLfile + "\n";
                dataToSave += "NQ = " + Nq + "\n";
                dataToSave += "percent = " + percent + "\n";
                dataToSave += "filter = " + filter + "\n";
                dataToSave += "Kconstant = " + Kconstant + "\n";
                dataToSave += "sigma2Sstart = " + sigma2Sstart + "\n";
                dataToSave += "sigma2Sfinal = " + sigma2Sfinal + "\n";
                dataToSave += "sigma2Sincrement = " + sigma2Sincrement + "\n";
                dataToSave += "sigma2Pstart = " + sigma2Pstart + "\n";
                dataToSave += "sigma2Pfinal = " + sigma2Pfinal + "\n";
                dataToSave += "sigma2Pincrement = " + sigma2Pincrement + "\n";
                dataToSave += "sigma2Qstart = " + sigma2Qstart + "\n";
                dataToSave += "sigma2Qfinal = " + sigma2Qfinal + "\n";
                dataToSave += "sigma2Qincrement = " + sigma2Qincrement + "\n";
                dataToSave += "\n".PadLeft(78, '*') + "\n1";
            }
#endregion

            double[,] Vp = new double[Vs.GetUpperBound(0) + 1, Vs.GetUpperBound(1) + 1];
            double[,] Xp = new double[Xs.GetUpperBound(0) + 1, Xs.GetUpperBound(1) + 1];
            double[] Vq = new double[Vs.GetUpperBound(0) + 1];
            double[] Xq = new double[Xs.GetUpperBound(0) + 1];
            double[] Np = new double[Vs.GetUpperBound(0) + 1];
            double[,] Ns = new double[Vs.GetUpperBound(0) + 1, Vs.GetUpperBound(1) + 1];
            // Nq is in the input
            double Vsup = 0;
            double Xsup = 0;

            double bestSSigma = 0;
            double oldSVariance = double.MaxValue;

            double bestPSigma = 0;
            double oldPVariance = double.MaxValue;
            bool scansPerPeptideOK = false;
            bool pepPerProteinOK = false;

            double bestQSigma = 0;
            double oldQVariance = double.MaxValue;

            // calculates variance at SCAN LEVEL
            for (double sigmaTentative = sigma2Sstart;
                sigmaTentative <= sigma2Sfinal;
                sigmaTentative += sigma2Sincrement)
            {
                ArrayList medianArray = new ArrayList();
                Vp.Initialize();
                Xp.Initialize();
                Vq.Initialize();
                Xq.Initialize();
                Np.Initialize();
                Ns.Initialize();

                // weighted averages calculation
                weightedAveragesScanLevel(Vs, Xs, Vp, Xp, Vq, Xq, Np, Ns, sigmaTentative);

                // Ns must be > 1 for at least one peptide,
                // otherwise, robust statistics can not be performed
                scansPerPeptideOK = scansPerPeptideOKmethod(Ns);
                pepPerProteinOK = pepPerProteinOKmethod(Np);

                if (scansPerPeptideOK && pepPerProteinOK)
                    bestSSigma = getScanVariance(Vs, Xs, Xp, Ns,
                                                    sigmaTentative,
                                                    ref oldSVariance,
                                                    bestSSigma,
                                                    medianArray,
                                                    sigmaCalculationMethod);
            }

            double NsTot;
            double NpTot;
            getTotalNumbers(Ns, out NsTot, out NpTot);

            message = "";
            
            if (scansPerPeptideOK && pepPerProteinOK)
            {
                message = "scan = " + bestSSigma.ToString() + "\tNS = " + NsTot.ToString();
                dataToSave += "\t" + message;
                rtbOutScan.Text += "\n" + message;
            }
            
            if(!scansPerPeptideOK)
                lblStat.Text += " (spp not OK)";

            // calculates variance at PEPTIDE LEVEL
            for (double sigmaTentative = sigma2Pstart;
                sigmaTentative <= sigma2Pfinal;
                sigmaTentative += sigma2Pincrement)
            {
                ArrayList medianArray = new ArrayList();
                Vp.Initialize();
                Xp.Initialize();
                Vq.Initialize();
                Xq.Initialize();
                Np.Initialize();
                Ns.Initialize();

                // weighted averages calculation
                weightedAveragesPepLevel(Vs, Xs, Vp, Xp, Vq, Xq, Np, Ns, sigmaTentative, bestSSigma);
                scansPerPeptideOK = scansPerPeptideOKmethod(Ns);
                pepPerProteinOK = pepPerProteinOKmethod(Np);

                if (scansPerPeptideOK && pepPerProteinOK)
                    bestPSigma = getPepVariance(Vs, Xs, Vp, Xp, Xq, Ns, Np,
                                                sigmaTentative,
                                                ref oldPVariance,
                                                bestPSigma,
                                                medianArray,
                                                sigmaCalculationMethod);
            }

            if (!pepPerProteinOK)
                lblStat.Text += " (ppp not OK)";

            if (scansPerPeptideOK && pepPerProteinOK)
            {
                message = "pep = " + bestPSigma.ToString() + "\tNP = " + NpTot.ToString();
                dataToSave += "\t" + message;
                rtbOutPep.Text += "\n" + message;
            }

            // calculates variance at PROTEIN LEVEL
            for (double sigmaTentative = sigma2Qstart;
                sigmaTentative <= sigma2Qfinal;
                sigmaTentative += sigma2Qincrement)
            {
                ArrayList medianArray = new ArrayList();
                Vp.Initialize();
                Xp.Initialize();
                Vq.Initialize();
                Xq.Initialize();
                Np.Initialize();
                Ns.Initialize();

                // weighted averages calculation
                weightedAveragesProtLevel(Vs, Xs, Vp, Xp, Vq, Xq, Vsup, Xsup, Np, Ns,
                                        sigmaTentative,
                                        bestPSigma,
                                        bestSSigma);
                scansPerPeptideOK = scansPerPeptideOKmethod(Ns);
                pepPerProteinOK = pepPerProteinOKmethod(Np);

                if (scansPerPeptideOK && pepPerProteinOK)
                    bestQSigma = getProtVariance(Vs, Xs, Vp, Xp, Vq, Xq, Xsup, Ns, Np, Nq,
                                                sigmaTentative,
                                                ref oldQVariance,
                                                bestQSigma,
                                                medianArray,
                                                sigmaCalculationMethod);
            }

            if (scansPerPeptideOK && pepPerProteinOK)
            {
                message = "prot = " + bestQSigma.ToString() + "\tNQ = " + Nq.ToString();
                dataToSave += "\t" + message;
                rtbOutProt.Text += "\n" + message;
                resultingVars.Add(bestSSigma);
                resultingVars.Add(bestPSigma);
                resultingVars.Add(bestQSigma);

                if (saveAsXML)
                    saveSimulationAsQuiXML(filter, Vs, Xs, Vp, Xp, Vq, Xq,
                        bestSSigma, bestPSigma, bestQSigma, NsTot, NpTot);
            }

            return resultingVars;
        }

        private void saveSimulationAsQuiXML(string filter, double[, ,] Vs, double[, ,] Xs, double[,] Vp, double[,] Xp, double[] Vq, double[] Xq, double bestSSigma, double bestPSigma, double bestQSigma, double NsTot, double NpTot)
        {
            // Warning: this method will probably give errors with iTRAQ
            // because its Xs and Vs columns depend on the reporter ions

            string simulationFile = Path.GetDirectoryName(XMLfile);
            simulationFile += "\\" + Path.GetFileNameWithoutExtension(XMLfile);
            string textDate = DateTime.Today.ToString("yyyyMMmdd");
            string textTime = DateTime.Now.ToString("HHmmss");
            simulationFile += "_simulation" + textDate + "-" + textTime + ".xml";

            // insert structure in dsSimulation
            DataSet dsSimulation = dset.Clone();

            DataView dvSimIdArchive = dsSimulation.Tables["IdentificationArchive"].DefaultView;
            DataView dvSimIds = dsSimulation.Tables["Identifications"].DefaultView;
            DataView dvSimData = dsSimulation.Tables["peptide_match"].DefaultView;

            dvSimIdArchive.Table.Rows.Add(dvSimIds);
            dvSimIds.Table.Rows.Add();

            dvSimIds.Table.Rows[0][1] = (int)dvSimIdArchive.Table.Rows[0][19];

            dvSimIdArchive[0].Row["Filter"] = filter;
            dvSimIdArchive[0].Row["col_Xs"] = "q_log2Ratio";
            dvSimIdArchive[0].Row["col_Vs"] = "Vs";

            dvSimIdArchive[0].Row["ct_k"] = Kconstant;
            dvSimIdArchive[0].Row["Ns"] = NsTot;
            dvSimIdArchive[0].Row["Np"] = NpTot;
            dvSimIdArchive[0].Row["Nq"] = NqSim;
            dvSimIdArchive[0].Row["X"] = mu;
            dvSimIdArchive[0].Row["Ns_varCalc"] = NsTot;
            dvSimIdArchive[0].Row["Np_varCalc"] = NpTot;
            dvSimIdArchive[0].Row["Nq_varCalc"] = NqSim;
            dvSimIdArchive[0].Row["X_varCalc"] = mu;
            dvSimIdArchive[0].Row["ct_sigma2S"] = bestSSigma.ToString();
            dvSimIdArchive[0].Row["ct_sigma2P"] = bestPSigma.ToString();
            dvSimIdArchive[0].Row["ct_sigma2Q"] = bestQSigma.ToString();


            int primKey = 0;
            Random rnd = new Random();
            for (int q = 0; q <= Vs.GetUpperBound(0); q++)
            {
                string fastaProteinDescription = "Protein_" + rnd.NextDouble().ToString().Substring(2);

                for (int p = 0; p <= Vs.GetUpperBound(1); p++)
                {
                    if (Vs[q, p, 0] > 0)
                    {
                        string Sequence = "Sequence_" + rnd.NextDouble().ToString().Substring(2);

                        for (int s = 0; s <= Vs.GetUpperBound(2); s++)
                        {
                            if (Vs[q, p, s] > 0)
                            {
                                object[] rowToCopy = new object[dvSimData.Table.Columns.Count];
                                int primKeydir = dvSimData.Table.Columns.IndexOf(dvSimData.Table.TableName + "_Id");
                                int primKeyIdentifdir = dvSimData.Table.Columns.IndexOf(dvSimIds.Table.TableName + "_Id");
                                int fileNameDir = dvSimData.Table.Columns.IndexOf("FileName");
                                int RAWFileNameDir = dvSimData.Table.Columns.IndexOf("RAWFileName");
                                int FirstScanDir = dvSimData.Table.Columns.IndexOf("FirstScan");
                                int LastScanDir = dvSimData.Table.Columns.IndexOf("LastScan");
                                int ChargeDir = dvSimData.Table.Columns.IndexOf("Charge");
                                int FastaProteinDescriptionDir = dvSimData.Table.Columns.IndexOf("FASTAProteinDescription");
                                int SequenceDir = dvSimData.Table.Columns.IndexOf("Sequence");


                                rowToCopy[primKeydir] = primKey;
                                rowToCopy[primKeyIdentifdir] = 1;
                                rowToCopy[fileNameDir] = "DummyFile";
                                rowToCopy[RAWFileNameDir] = "DummyRAWFile";
                                rowToCopy[FirstScanDir] = primKey;
                                rowToCopy[LastScanDir] = primKey;
                                rowToCopy[ChargeDir] = 0;
                                rowToCopy[FastaProteinDescriptionDir] = fastaProteinDescription;
                                rowToCopy[SequenceDir] = Sequence;


                                dvSimData.Table.Rows.Add(rowToCopy);
                                dvSimData[primKey].Row["q_log2Ratio"] = Xs[q, p, s];
                                dvSimData[primKey].Row["Xs"] = Xs[q, p, s];
                                dvSimData[primKey].Row["Vs"] = Vs[q, p, s];
                                dvSimData[primKey].Row["Xp"] = Xp[q, p];
                                dvSimData[primKey].Row["Wp"] = Vp[q, p];
                                dvSimData[primKey].Row["Xq"] = Xq[q];
                                dvSimData[primKey].Row["Wq"] = Vq[q];
                                primKey++;
                            }
                        }

                    }
                }


            }


            // save xml with simulation name, which appends date ant time to filename
            // warning: will give problems if filename is longer than 255 chars
            dsSimulation.WriteXml(simulationFile);
        }

        private static void getTotalNumbers(double[,] Ns, out double NsTot, out double NpTot)
        {
            NsTot = 0;
            NpTot = 0;
            for (int q = 0; q <= Ns.GetUpperBound(0); q++)
            {
                for (int p = 0; p <= Ns.GetUpperBound(1); p++)
                {
                    NsTot += Ns[q, p];
                    if (Ns[q, p] != 0) NpTot++;
                }
            }
        }

        private static bool scansPerPeptideOKmethod(double[,] Ns)
        {
            bool scansOK = false;
            for (int q = 0; q < Ns.GetUpperBound(0); q++)
            {
                for (int p = 0; p < Ns.GetUpperBound(1); p++)
                {
                    if (Ns[q, p] > 1)
                    {
                        scansOK = true;
                        break;
                    }
                }
                if (scansOK) break;
            }
            return scansOK;
        }

        private static bool pepPerProteinOKmethod(double[] Np)
        {
            bool peptideOK = false;
            for (int q = 0; q < Np.GetUpperBound(0); q++)
            {
                    if (Np[q] > 1)
                    {
                        peptideOK = true;
                        break;
                    }
            }
            return peptideOK;
        }



        private void weightedAveragesScanLevel(double[, ,] Vs,
                                        double[, ,] Xs,
                                        double[,] Vp,
                                        double[,] Xp,
                                        double[] Vq,
                                        double[] Xq,
                                        double[] Np,
                                        double[,] Ns,
                                        double sigmaTentative)
        {
            for (int q = 0; q <= Vs.GetUpperBound(0); q++)
            {
                Np[q] = 0;
                Vq[q] = 0;
                Xq[q] = 0;
                for (int p = 0; p <= Vs.GetUpperBound(1); p++)
                {
                    if (Vs[q, p, 0] > 0)
                    {
                        Np[q]++;
                        Ns[q, p] = 0;
                        Vp[q, p] = 0;
                        Xp[q, p] = 0;
                        for (int s = 0; s <= Vs.GetUpperBound(2); s++)
                        {
                            if (Vs[q, p, s] > 0)
                            {
                                Ns[q, p]++;
                                Vp[q, p] += 1 / (sigmaTentative + Kconstant / Vs[q, p, s]);
                                Xp[q, p] += Xs[q, p, s] / (sigmaTentative + Kconstant / Vs[q, p, s]);
                            }
                        }
                        Xp[q, p] = Xp[q, p] / Vp[q, p];
                        Vq[q] += Vp[q, p];
                        Xq[q] += Vp[q, p] * Xp[q, p];
                    }
                }
                Xq[q] = Xq[q] / Vq[q];
            }
        }

        private void weightedAveragesPepLevel(double[, ,] Vs,
                                        double[, ,] Xs,
                                        double[,] Vp,
                                        double[,] Xp,
                                        double[] Vq,
                                        double[] Xq,
                                        double[] Np,
                                        double[,] Ns,
                                        double sigmaTentative,
                                        double sigma2Scalculated)
        {
            for (int q = 0; q <= Vs.GetUpperBound(0); q++)
            {
                Np[q] = 0;
                Vq[q] = 0;
                Xq[q] = 0;
                for (int p = 0; p <= Vs.GetUpperBound(1); p++)
                {
                    if (Vs[q, p, 0] > 0)
                    {
                        Np[q]++;
                        Ns[q, p] = 0;
                        Vp[q, p] = 0;
                        Xp[q, p] = 0;
                        for (int s = 0; s <= Vs.GetUpperBound(2); s++)
                        {
                            if (Vs[q, p, s] > 0)
                            {
                                Ns[q, p]++;
                                Vp[q, p] += 1 / (sigma2Scalculated + Kconstant / Vs[q, p, s]);
                                Xp[q, p] += Xs[q, p, s] / (sigma2Scalculated + Kconstant / Vs[q, p, s]);
                            }
                        }
                        Xp[q, p] = Xp[q, p] / Vp[q, p];
                        Vq[q] += 1 / (sigmaTentative + 1 / Vp[q, p]);
                        Xq[q] += Xp[q, p] / (sigmaTentative + 1 / Vp[q, p]);
                    }
                }
                Xq[q] = Xq[q] / Vq[q];
            }
        }

        private void weightedAveragesProtLevel(double[, ,] Vs,
                                double[, ,] Xs,
                                double[,] Vp,
                                double[,] Xp,
                                double[] Vq,
                                double[] Xq,
                                double Vsup,
                                double Xsup,
                                double[] Np,
                                double[,] Ns,
                                double sigmaTentative,
                                double sigma2Pcalculated,
                                double sigma2Scalculated)
        {
            for (int q = 0; q <= Vs.GetUpperBound(0); q++)
            {
                Np[q] = 0;
                Vq[q] = 0;
                Xq[q] = 0;
                for (int p = 0; p <= Vs.GetUpperBound(1); p++)
                {
                    if (Vs[q, p, 0] > 0)
                    {
                        Np[q]++;
                        Ns[q, p] = 0;
                        Vp[q, p] = 0;
                        Xp[q, p] = 0;
                        for (int s = 0; s <= Vs.GetUpperBound(2); s++)
                        {
                            if (Vs[q, p, s] > 0)
                            {
                                Ns[q, p]++;
                                Vp[q, p] += 1 / (sigma2Scalculated + Kconstant / Vs[q, p, s]);
                                Xp[q, p] += Xs[q, p, s] / (sigma2Scalculated + Kconstant / Vs[q, p, s]);
                            }
                        }
                        Xp[q, p] = Xp[q, p] / Vp[q, p];
                        Vq[q] += 1 / (sigma2Pcalculated + 1 / Vp[q, p]);
                        Xq[q] += Xp[q, p] / (sigma2Pcalculated + 1 / Vp[q, p]);
                    }
                }
                Xq[q] = Xq[q] / Vq[q];
                Vsup += 1 / (sigmaTentative + 1 / Vq[q]);
                Xsup += Xq[q] / (sigmaTentative + 1 / Vq[q]);
            }

            Xsup = Xsup / Vsup;
        }

        private double getScanVariance(double[, ,] Vs,
                                        double[, ,] Xs,
                                        double[,] Xp,
                                        double[,] Ns,
                                        double sigma2Sinitial,
                                        ref double oldVariance,
                                        double bestSigma,
                                        ArrayList medianArray,
                                        method calculationMethod)
        {
            for (int q = 0; q <= Vs.GetUpperBound(0); q++)
            {
                for (int p = 0; p <= Vs.GetUpperBound(1); p++)
                {
                    if (Vs[q, p, 0] > 0)
                    {
                        for (int s = 0; s <= Vs.GetUpperBound(2); s++)
                        {
                            if (Vs[q, p, s] > 0)
                            {
                                if (Ns[q, p] > 1)
                                {
                                    // degrees of freedom
                                    double gcorr = Ns[q, p] / (Ns[q, p] - 1);
                                    double prov = Math.Sqrt(Math.Pow((Xs[q, p, s] - Xp[q, p]), 2) * gcorr
                                        / (sigma2Sinitial + Kconstant / Vs[q, p, s]));
                                    medianArray.Add(prov);
                                }
                            }
                        }
                    }
                }
            }
            double newVariance = getMiddleValue(medianArray, calculationMethod);
            newVariance = Math.Pow(newVariance, 2);
            if (calculationMethod == method.median) newVariance *= Math.Pow(medianConstant, 2);

            if (Math.Abs(newVariance - 1) < Math.Abs(oldVariance - 1))
            {
                oldVariance = newVariance;
                bestSigma = sigma2Sinitial;
            }
            return bestSigma;
        }

        private double getPepVariance(double[, ,] Vs,
                                        double[, ,] Xs,
                                        double[,] Vp,
                                        double[,] Xp,
                                        double[] Xq,
                                        double[,] Ns,
                                        double[] Np,
                                        double sigma2Pinitial,
                                        ref double oldVariance,
                                        double bestSigma,
                                        ArrayList medianArray,
                                        method calculationMethod)
        {
            for (int q = 0; q <= Vs.GetUpperBound(0); q++)
            {
                for (int p = 0; p <= Vs.GetUpperBound(1); p++)
                {
                    if (Vs[q, p, 0] > 0)
                    {
                        if (Np[q] > 1)
                        {
                            // degrees of freedom
                            double gcorr = Np[q] / (Np[q] - 1);
                            double prov = Math.Sqrt(Math.Pow((Xp[q, p] - Xq[q]), 2) * gcorr / (sigma2Pinitial + 1 / Vp[q, p]));
                            medianArray.Add(prov);
                        }
                    }
                }
            }
            double newVariance = getMiddleValue(medianArray, calculationMethod);
            newVariance = Math.Pow(newVariance, 2);
            if (calculationMethod == method.median) newVariance *= Math.Pow(medianConstant, 2);

            if (Math.Abs(newVariance - 1) < Math.Abs(oldVariance - 1))
            {
                oldVariance = newVariance;
                bestSigma = sigma2Pinitial;
            }
            return bestSigma;
        }

        private double getProtVariance(double[, ,] Vs,
                                double[, ,] Xs,
                                double[,] Vp,
                                double[,] Xp,
                                double[] Vq,
                                double[] Xq,
                                double Xsup,
                                double[,] Ns,
                                double[] Np,
                                double Nq,
                                double sigma2Qinitial,
                                ref double oldVariance,
                                double bestSigma,
                                ArrayList medianArray,
                                method calculationMethod)
        {
            for (int q = 0; q <= Vs.GetUpperBound(0); q++)
            {
                // degrees of freedom, thoug they are not really needed for proteins
                // as there are many
                double gcorr = Nq / (Nq - 1);
                double prov = Math.Sqrt(Math.Pow((Xq[q] - Xsup), 2) * gcorr / (sigma2Qinitial + 1 / Vq[q]));
                medianArray.Add(prov);
            }
            double newVariance = getMiddleValue(medianArray, calculationMethod);
            newVariance = Math.Pow(newVariance, 2);
            if (calculationMethod == method.median) newVariance *= Math.Pow(medianConstant, 2);

            if (Math.Abs(newVariance - 1) < Math.Abs(oldVariance - 1))
            {
                oldVariance = newVariance;
                bestSigma = sigma2Qinitial;
            }
            return bestSigma;
        }

        private static double getMedian(ArrayList medianArray)
        {
            medianArray.Sort();
            double median;
            int medianIndex = medianArray.Count;
            if (medianIndex % 2 == 1)
                median = (double)medianArray[(medianIndex + 1) / 2];
            else
                median = ((double)medianArray[medianIndex / 2 - 1] + (double)medianArray[medianIndex / 2]) / 2;
            return median;
        }

        private static double getAverage(ArrayList averageArray)
        {
            double average;
            double total = 0;
            foreach (double element in averageArray)
                total += element;

            average = total / averageArray.Count;

            return average;
        }

        private static double getMiddleValue(ArrayList myArray, method how)
        {
            double answer = 0;

            if (how == method.average)
                answer = getAverage(myArray);
            if (how == method.median)
                answer = getMedian(myArray);

            // if how is none of the above, the result will be zero
            return answer;
        }

        private bool simulateExperiment(out double[, ,] V, out double[, ,] X, int Nq)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            bool routineWasFine = true;

            try
            {
                V = new double[Nq, NmaxP, NmaxS];
                X = new double[Nq, NmaxP, NmaxS];
                Random rnd = new Random();
                int NSsim = 0;
                int NPsim = 0;
                int NQsim = 0;

                for (int q = 0; q < Nq; q++)
                {
                    NQsim++;
                    int Nptot = NpSim[rnd.Next(NpSim.Length)];
                    if (Nptot > NmaxP) Nptot = NmaxP;

                    double SDprot = Math.Sqrt(sigma2Q);
                    MathNet.Numerics.Distributions.NormalDistribution ndistQ =
                            new MathNet.Numerics.Distributions.NormalDistribution(0, SDprot);
                    double epsiloQ = ndistQ.InverseCumulativeDistribution(rnd.NextDouble());

                    for (int p = 0; p < Nptot; p++)
                    {
                        NPsim++;
                        int Nstot = NsSim[rnd.Next(NsSim.Length)];
                        if (Nstot > NmaxS) Nstot = NmaxS;

                        double SDpep = Math.Sqrt(sigma2P);
                        MathNet.Numerics.Distributions.NormalDistribution ndistP =
                            new MathNet.Numerics.Distributions.NormalDistribution(0, SDpep);
                        double epsiloP = ndistP.InverseCumulativeDistribution(rnd.NextDouble());

                        for (int s = 0; s < Nstot; s++)
                        {
                            NSsim++;
                            int Vrnd = rnd.Next(Vvalues.Length);
                            V[q, p, s] = Vvalues[Vrnd];

                            double SDscan = Math.Sqrt(sigma2S + Kconstant / V[q, p, s]);
                            MathNet.Numerics.Distributions.NormalDistribution ndistS =
                                new MathNet.Numerics.Distributions.NormalDistribution(0, SDscan);
                            double epsiloS = ndistS.InverseCumulativeDistribution(rnd.NextDouble());
                            X[q, p, s] = epsiloQ + epsiloP + epsiloS;// +mu;
                        }
                    }
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();

                routineWasFine = true; // true = everything was OK
            }
            catch(Exception ex)
            {
                string errorMessage = ex.ToString();
                if (errorMessage == "System.OutOfMemoryException: Exception of type 'System.OutOfMemoryException' was thrown.\r\n   at QuiXoT.OPvarConf.simulateExperiment(Double[,,]& V, Double[,,]& X, Int32 Nq)")
                {
                    MessageBox.Show("Error: apparently the maximum number of scans per peptide is too high," +
                        "\nplease, make it lower than " + NmaxS.ToString() + " and try again." +
                        "\n\nYou can as well try to lower the maximum number of peptides per protein," +
                        "\nwhich is currently set at " + NmaxP.ToString() + ".");
                }
                else
                {
                    MessageBox.Show("Unexpected error.\nMessage was: " + ex.ToString());
                }

                V = null;
                X = null;
                routineWasFine = false; // false = probably memory problem
            }

            return routineWasFine;
        }

        private double avgXqCalculation(DataRow[] dset)
        {
            double[] XqValues = getArrayPerQ(dset, "Xq");
            double[] WqValues = getArrayPerQ(dset, "Wq");
            double sumXqWq = 0;
            double sumWq = 0;
            for (int i = 0; i < XqValues.Length; i++)
            {
                sumXqWq += XqValues[i] * WqValues[i];
                sumWq += WqValues[i];
            }

            double mu = sumXqWq / sumWq;
            return mu;
        }

        private int[] doubleToIntArray(double[] array)
        {
            int[] outArray = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
                outArray[i] = (int)array[i];

            return outArray;
        }

        private double[] getArrayPerS(DataRow[] dset, string outColumn)
        {
            int dataLength = dset.Length;
            double[] list = new double[dataLength];
            for (int i = 0; i < dataLength; i++)
                list[i] = (double)dset[i][outColumn];

            return list;
        }

        private double[] getArrayPerP(DataRow[] dset, string outColumn)
        {
            int dataLength = dset.Length;
            double[] listOriginal = new double[dataLength];
            int tot = 0;

            for (int i = 0; i < dataLength; i++)
            {
                double s_index = double.Parse(dset[i]["s_index"].ToString());

                if (s_index == 1)
                {
                    listOriginal[i] = double.Parse(dset[i][outColumn].ToString());
                    tot++;
                }
            }

            double[] list = new double[tot];
            tot = 0;
            for (int i = 0; i < dataLength; i++)
            {
                if (listOriginal[i] != 0)
                {
                    list[tot] = listOriginal[i];
                    tot++;
                }
            }
            return list;
        }

        private double[] getArrayPerQ(DataRow[] dset, string outColumn)
        {
            int dataLength = dset.Length;
            double[] listOriginal = new double[dataLength];
            int tot = 0;

            for (int i = 0; i < dataLength; i++)
            {
                double s_index = double.Parse(dset[i]["s_index"].ToString());
                double p_index = double.Parse(dset[i]["p_index"].ToString());

                if (s_index == 1 && p_index == 1)
                {
                    listOriginal[i] = double.Parse(dset[i][outColumn].ToString());
                    tot++;
                }
            }

            double[] list = new double[tot];
            tot = 0;

            for (int i = 0; i < dataLength; i++)
            {
                if (listOriginal[i] != 0)
                {
                    list[tot] = listOriginal[i];
                    tot++;
                }
            }

            return list;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            dataToSave = "";
            int Nexp = int.Parse(cbxNexp.Text.ToString());

            bool useAverage = cbxUseAverage.Checked;
            method sigmaCalculation = method.average;
            if (!useAverage) sigmaCalculation = method.median;

            try
            {
                NqSim = int.Parse(tbxNumProts.Text.Trim().ToString());
            }
            catch { };

            saveFirstSimulation = cbxSaveFirst.Checked;

            ArrayList variances = calculateAllExperiments(Nexp, NqSim, sigmaCalculation);

            if (variances == null) // this means there was an error, probably about memory
                enableOrDisableForms(true);
            else // no error then...
            {
                if (dataToSave.Length > 0)
                    btnSave.Enabled = true;
            }
        }

        private ArrayList calculateAllExperiments(int _Nexp, int _Nq, method _sigmaCalculation)
        {
            ArrayList variances = new ArrayList();
            ArrayList variancesS = new ArrayList();
            ArrayList variancesP = new ArrayList();
            ArrayList variancesQ = new ArrayList();

            rtbOutScan.Text = "scan info";
            rtbOutPep.Text = "peptide info";
            rtbOutProt.Text = "protein info";

            enableOrDisableForms(false);

            int errCounter = 0;

            bool tooManyErrors = false;
            bool saveExperimentAsXML = saveFirstSimulation;

            for (int i = 1; i <= _Nexp; i++)
            {
                if ((errCounter > Math.Floor((double)_Nexp / 2) && _Nexp > 10) ||
                    (_Nexp <= 10 && errCounter > _Nexp && errCounter >= 10))
                {
                    MessageBox.Show("Apparently this set of data is not suitable for statistics." +
                        "\nPlease, check:" +
                        "\n1) the number of proteins per experiment (NQ, which might be too low)," +
                        "\n2) the filter, which might bee too restrictive or invalid," +
                        "\n3) the size of the dataset.");
                    tooManyErrors = true;
                    break;
                }

                lblStat.Text = "Calculating experiment " + i + "/" + _Nexp + ".";
                Application.DoEvents();
                dataToSave += "\n" + i.ToString();

                if (i > 1) saveExperimentAsXML = false;

                ArrayList variancesOneExp = calculateOneExperiment(dset, _Nq, saveExperimentAsXML, _sigmaCalculation);
                if (variancesOneExp == null)
                    return null;

                if (variancesOneExp.Count == 3)
                {
                    variancesS.Add((double)variancesOneExp[0]);
                    variancesP.Add((double)variancesOneExp[1]);
                    variancesQ.Add((double)variancesOneExp[2]);
                }
                else
                {
                    // if it is not 3, this means there was an error
                    errCounter++;
                    if (i == 1)
                        dataToSave = "";
                    else
                    {
                        int charsToRemove = 2 + (int)Math.Floor(Math.Log10(i));
                        dataToSave = dataToSave.Substring(0, dataToSave.Length - charsToRemove);
                    }

                    i--;
                }
            }



            if (!tooManyErrors)
            {
                variances.Add(variancesS);
                variances.Add(variancesP);
                variances.Add(variancesQ);

                if (_Nexp > 8)
                {
                    string valuesS = getLimits(_Nexp, variancesS, percent, "scan:\n");
                    string valuesP = getLimits(_Nexp, variancesP, percent, "peptide:\n");
                    string valuesQ = getLimits(_Nexp, variancesQ, percent, "protein:\n");
                    string message = "Nexp = " + _Nexp.ToString() + "\n\n" +
                                        valuesS + "\n\n" +
                                        valuesP + "\n\n" +
                                        valuesQ;

                    MessageBox.Show(message);
                    dataToSave += "\n".PadLeft(78, '*') + "\n" + message;
                }
            }
            else
            {
                dataToSave = "";
            }

            enableOrDisableForms(true);

            if (variances != null)
                lblStat.Text = "Calculation completed.";
            else
                lblStat.Text = "Error found.";

            Application.DoEvents();

            return variances;
        }

        private static string getLimits(int _Nexp, ArrayList variancesS, double perc, string header)
        {
            variancesS.Sort();
            int lowerPart = (int)Math.Max(0, (int)Math.Floor(_Nexp * perc - 1));
            int upperPart = (int)Math.Min(_Nexp - 1, (int)Math.Ceiling(_Nexp * (1 - perc) - 1));

            double first = (double)variancesS[0];
            double last = (double)variancesS[_Nexp - 1];
            double median = getMedian(variancesS);
            double lower = (double)variancesS[lowerPart];
            double upper = (double)variancesS[upperPart];
            //double first5percent
            string values = header + "first = " + first +
                                "\nlower " + perc * 100 + "% (" + (lowerPart + 1).ToString() + "th) = " + lower +
                                "\nmedian = " + median +
                                "\nupper " + perc * 100 + "% (" + (upperPart + 1).ToString() + "th) = " + upper +
                                "\nlast = " + last;
            return values;
        }

        private void enableOrDisableForms(bool formEnabled)
        {
            tbxNumProts.Enabled = formEnabled;
            tbxMaxPepPerProt.Enabled = formEnabled;
            tbxMaxScanPerPep.Enabled = formEnabled;

            txbSigma2Sstart.Enabled = formEnabled;
            txbSigma2Send.Enabled = formEnabled;
            txbSigma2Sstep.Enabled = formEnabled;

            txbSigma2Pstart.Enabled = formEnabled;
            txbSigma2Pend.Enabled = formEnabled;
            txbSigma2Pstep.Enabled = formEnabled;

            txbSigma2Qstart.Enabled = formEnabled;
            txbSigma2Qend.Enabled = formEnabled;
            txbSigma2Qstep.Enabled = formEnabled;

            tbxFilter.Enabled = formEnabled;
            tbxKconstant.Enabled = formEnabled;

            cbxNexp.Enabled = formEnabled;
            cbxUseAverage.Enabled = formEnabled;
            tbxPercent.Enabled = formEnabled;

            btnCalculate.Enabled = formEnabled;
            btnLoadPrevFilter.Enabled = formEnabled;
            btnFilter.Enabled = formEnabled;
            btnSave.Enabled = formEnabled;

            if (dataToSave == "") btnSave.Enabled = false;
        }

        private void btnLoadPrevFilter_Click(object sender, EventArgs e)
        {
            try
            {
                tbxKconstant.Text =
                    dset.Copy().Tables[1].Select()[0]["ct_K"].ToString();
            }
            catch
            {
                tbxKconstant.Text = "";
            }

            try
            {
                tbxFilter.Text =
                    dset.Copy().Tables[1].Select()[0]["filter"].ToString();
            }
            catch
            {
                tbxFilter.Text = "";
            }

            btnFilter_Click(sender, e);
        }

        private void tbxFilter_TextChanged(object sender, EventArgs e)
        {
            //***
        }

        private void tbxKconstant_TextChanged(object sender, EventArgs e)
        {
            tbxKconstant.ForeColor = Color.Black;
        }

        private void rtbOutScan_TextChanged(object sender, EventArgs e)
        {
            rtbOutScan.SelectionStart = rtbOutScan.TextLength;
            rtbOutScan.ScrollToCaret();
        }

        private void rtbOutPep_TextChanged(object sender, EventArgs e)
        {
            rtbOutPep.SelectionStart = rtbOutPep.TextLength;
            rtbOutPep.ScrollToCaret();
        }

        private void rtbOutProt_TextChanged(object sender, EventArgs e)
        {
            rtbOutProt.SelectionStart = rtbOutProt.TextLength;
            rtbOutProt.ScrollToCaret();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string resultsFilePath = "";
            if (XMLfile.EndsWith(".xml"))
            {
                resultsFilePath = XMLfile.Substring(0, XMLfile.Length - 4) + "_varConf.txt";
                StreamWriter writer = new StreamWriter(resultsFilePath);
                writer.Write(dataToSave);
                writer.Close();
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            btnFilter.Enabled = false;
            btnLoadPrevFilter.Enabled = false;

            tbxFilter.ForeColor = Color.Black;
            int totalRows = 0;
            DataRow[] dset3;

            try
            {
                string filter = "";
                string filterProteins = "";
                if (tbxFilter.Text.Trim().Length > 0)
                {
                    filter = "(" + tbxFilter.Text + ") and st_excluded = ''";
                    filterProteins = "(" + tbxFilter.Text + ") and s_index = 1 and p_index = 1 and st_excluded = ''";
                }
                else
                {
                    filter = "st_excluded = ''";
                    filterProteins = "s_index = 1 and p_index = 1 and st_excluded = ''";
                }

                dset3 = dset.Copy().Tables[3].Select(filter);


                NqSim = dset.Copy().Tables[3].Select(filterProteins).Length;
                NsSim = doubleToIntArray(getArrayPerP(dset3, "scan_per_peptide"));
                NpSim = doubleToIntArray(getArrayPerQ(dset3, "pep_per_protein"));
                NmaxS = NsSim.Max();
                NmaxP = NpSim.Max();

                tbxNumProts.Text = NqSim.ToString();
                tbxMaxPepPerProt.Text = NmaxP.ToString();
                tbxMaxScanPerPep.Text = NmaxS.ToString();
            }
            catch
            {
                tbxFilter.ForeColor = Color.Red;
                dset3 = dset.Copy().Tables[3].Select();
            }

            totalRows = dset3.Length;
            if (tbxFilter.Text.Trim().Length > 0)
                lblStartInfo.Text = totalRows.ToString() + " datarows to use with this filter.";
            else
                lblStartInfo.Text = "";

            if (tbxFilter.ForeColor == Color.Red)
                lblStartInfo.Text += " (filter not valid, all rows will be used).";
            else
            {
                if (totalRows == 0)
                {
                    tbxFilter.ForeColor = Color.Blue;
                    lblStartInfo.Text += " (filter is valid, but gives no rows, so all rows will be used).";
                }
            }

            btnFilter.Enabled = true;
            btnLoadPrevFilter.Enabled = true;
        }

        private enum method
        {
            average,
            median
        }
    }
}