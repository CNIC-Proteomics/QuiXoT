using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using QuiXoT.math;

namespace QuiXoT.statistics
{

    /// <summary>
    /// Structure of the options selected by the user for doing statistics 
    /// </summary>
    public struct statOptionsStrt
    {

        //Xs and Vs columns
        private string colXsVal;
        private string colVsVal;
        public string colXs { get { return colXsVal; } set { colXsVal = value; } }
        public string colVs { get { return colVsVal; } set { colVsVal = value; } }


        //Data filter
        private string filterVal;
        public string filter { get { return filterVal; } set { filterVal = value; } }


        //SILAC Arg-->Pro correction
        private bool silacCorrectionVal;
        public bool silacCorrection { get { return silacCorrectionVal; } set { silacCorrectionVal = value; } }
        private double silacFDRqVal;
        public double silacFDRq { get { return silacFDRqVal; } set { silacFDRqVal = value; } }
        
        //Force supermean
        private bool forceXVal;
        public bool forceX { get { return forceXVal; } set { forceXVal = value; } }
        private double forcedXVal;
        public double forcedX { get { return forcedXVal; } set { forcedXVal = value; } }
        
        //ignore scans
        private bool ignScansVal;
        public bool ignScans { get { return ignScansVal; } set { ignScansVal = value; } }
        private string colWpVal;
        public string colWp { get { return colWpVal; } set { colWpVal = value; } }
        private double ignScans_s2pVal;
        public double ignScans_s2p { get { return ignScans_s2pVal; } set { ignScans_s2pVal = value; } }
        public double sigmas_defaultVal;
        public double sigmas_default { get { return sigmas_defaultVal; } set { sigmas_defaultVal = value; } }
        
        //ignore peptides
        private bool ignPeptidesVal;
        public bool ignPeptides { get { return ignPeptidesVal; } set { ignPeptidesVal = value; } }
        private string colWqVal;
        public string colWq { get { return colWqVal; } set { colWqVal = value; } }
        public double sigmap_defaultVal;
        public double sigmap_default { get { return sigmap_defaultVal; } set { sigmap_defaultVal = value; } }
              

        //only used to calculate the general variances
        private bool calVariancesVal;
        private double vs_thresVal;
        private double wp_thresVal;
        private double wq_thresVal;
        private double kVal;
        private bool calSigmasVal;
        private bool calSigmapVal;
        private bool calSigmaqVal;
        public double sigmaq_defaultVal;
        public double sigmaq_default { get { return sigmaq_defaultVal; } set { sigmaq_defaultVal = value; } }
        private double s2sminVal;
        private double s2smaxVal;
        private double s2sdeltaVal;
        private int s2sCiclesVal;
        private double s2pminVal;
        private double s2pmaxVal;
        private double s2pdeltaVal;
        private int s2pCiclesVal;
        private double s2qminVal;
        private double s2qmaxVal;
        private double s2qdeltaVal;
        private int s2qCiclesVal;

        public double s2smin { get { return s2sminVal; } set { s2sminVal = value; } }
        public double s2smax { get { return s2smaxVal; } set { s2smaxVal = value; } }
        public double s2sdelta { get { return s2sdeltaVal; } set { s2sdeltaVal = value; } }
        public int s2sCicles { get { return s2sCiclesVal; } set { s2sCiclesVal = value; } }
        public double s2pmin { get { return s2pminVal; } set { s2pminVal = value; } }
        public double s2pmax { get { return s2pmaxVal; } set { s2pmaxVal = value; } }
        public double s2pdelta { get { return s2pdeltaVal; } set { s2pdeltaVal = value; } }
        public int s2pCicles { get { return s2pCiclesVal; } set { s2pCiclesVal = value; } }
        public double s2qmin { get { return s2qminVal; } set { s2qminVal = value; } }
        public double s2qmax { get { return s2qmaxVal; } set { s2qmaxVal = value; } }
        public double s2qdelta { get { return s2qdeltaVal; } set { s2qdeltaVal = value; } }
        public int s2qCicles { get { return s2qCiclesVal; } set { s2qCiclesVal = value; } }


        public bool calVariances { get { return calVariancesVal; } set { calVariancesVal = value; } }
        public double vs_thres { get { return vs_thresVal; } set { vs_thresVal = value; } }
        public double wp_thres { get { return wp_thresVal; } set { wp_thresVal = value; } }
        public double wq_thres { get { return wq_thresVal; } set { wq_thresVal = value; } }
        public double k { get { return kVal; } set { kVal = value; } }
        public bool calSigmas { get { return calSigmasVal; } set { calSigmasVal = value; } }
        public bool calSigmap { get { return calSigmapVal; } set { calSigmapVal = value; } }
        public bool calSigmaq { get { return calSigmaqVal; } set { calSigmaqVal = value; } }
        
        
        //peptide properties
        private int methioninesVal;
        private bool cTermVal;
        private bool partialDigVal;
        private bool subPeptidePartialDigVal;
        public int methionines { get { return methioninesVal; } set { methioninesVal = value; } }
        public bool cTerm { get { return cTermVal; } set { cTermVal = value; } }
        public bool partialDig { get { return partialDigVal; } set { partialDigVal = value; } }
        public bool subPeptidePartialDig { get { return subPeptidePartialDigVal; } set { subPeptidePartialDigVal = value; } }
        
        
        //scan properties
        private bool efficiencyVal;
        private double n_efficiencyVal;
        private bool wsVal;
        private double n_wsVal;
        private bool chargeLowVal;
        private int n_chargeLowVal;
        private bool chargeHiVal;
        private int n_chargeHiVal;
        private bool FDRVal;
        private double n_FDRVal;
        private bool totSumSQVal;
        private double n_totSumSQVal;
        private bool adSumSQVal;
        private double n_adSumSQVal;
        public bool efficiency{get{return efficiencyVal;}set{efficiencyVal = value;}}
        public double n_efficiency{get{return n_efficiencyVal;}set{n_efficiencyVal = value;}}
        public bool ws{get{return wsVal;}set{wsVal = value;}}
        public double n_ws{get{return n_wsVal;}set{n_wsVal = value;}}
        public bool chargeLow{get{return chargeLowVal;}set{chargeLowVal = value;}}
        public int n_chargeLow{get{return n_chargeLowVal;}set{n_chargeLowVal = value;}}
        public bool chargeHi{get{return chargeHiVal;}set{chargeHiVal = value;}}
        public int n_chargeHi{get{return n_chargeHiVal;}set{n_chargeHiVal = value;}}
        public bool FDR{get{return FDRVal;}set{FDRVal = value;}}
        public double n_FDR{get{return n_FDRVal;}set{n_FDRVal = value;}}
        public bool totSumSQ{get{return totSumSQVal;}set{totSumSQVal = value;}}
        public double n_totSumSQ{get{return n_totSumSQVal;}set{n_totSumSQVal = value;}}
        public bool adSumSQ{get{return adSumSQVal;}set{adSumSQVal = value;}}
        public double n_adSumSQ{get{return n_adSumSQVal;}set{n_adSumSQVal = value;}}

    }

    /// <summary>
    /// Structure of the calculated general variances, and the parameters used to calculate them
    /// </summary>
    public struct variancesStrt 
    {

        //Grandmean calculated
        private double XVal;

        //Ns scans, Np peptides, Nq proteins used in the calculus
        private int NsVal;
        private int NpVal;
        private int NqVal;

        //Variance at the scan level
        private double sigma2SVal;
        //constant K
        private double kVal;
        //Variance at the peptide level
        private double sigma2PVal;
        //Variance at the protein level        
        private double sigma2QVal;

        
        //Best value of the function F (for scan, peptide, and protein variance calculation)
        private double FsVal;
        private double FpVal;
        private double FqVal;

        //Do the the Fvalues have passed across 1?
        private bool FscutVal;
        private bool FpcutVal;
        private bool FqcutVal;

        //Thresholds used
        private double vs_thresVal;
        private double wp_thresVal;
        private double wq_thresVal;

        //Filter used
        private string filterVal;
        

        public double X { get { return XVal; } set { XVal = value; } }
        public int Ns { get { return NsVal; } set { NsVal = value; } }
        public int Np { get { return NpVal; } set { NpVal = value; } }
        public int Nq { get { return NqVal; } set { NqVal = value; } }
       

        public double sigma2S { get { return sigma2SVal; } set { sigma2SVal = value; } }
        public double sigma2P { get { return sigma2PVal; } set { sigma2PVal = value; } }
        public double sigma2Q { get { return sigma2QVal; } set { sigma2QVal = value; } }
        public double k { get { return kVal; } set { kVal = value; } }

        public double Fs { get { return FsVal; } set { FsVal = value; } }
        public double Fp { get { return FpVal; } set { FpVal = value; } }
        public double Fq { get { return FqVal; } set { FqVal = value; } }

        public bool Fscut { get { return FscutVal; } set { FscutVal = value; } }
        public bool Fpcut { get { return FpcutVal; } set { FpcutVal = value; } }
        public bool Fqcut { get { return FqcutVal; } set { FqcutVal = value; } }

        public double vs_thres { get { return vs_thresVal; } set { vs_thresVal = value; } }
        public double wp_thres { get { return wp_thresVal; } set { wp_thresVal = value; } }
        public double wq_thres { get { return wq_thresVal; } set { wq_thresVal = value; } }

        public string filter { get { return filterVal; } set { filterVal = value; } }


    }
    
    /// <summary>
    /// Structure for row ranges.
    /// </summary>
    public struct rowRangesStrt
    {
        private int minVal;
        private int maxVal;

        public rowRangesStrt(int minValue, int maxValue)
        {
            minVal = minValue;
            maxVal = maxValue;
        }

        public int min
        {
            get
            {
                return minVal;
            }
            set
            {
                minVal = value;
            }
        }

        public int max
        {
            get
            {
                return maxVal;
            }
            set
            {
                maxVal = value;
            }
        }
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
    public class MCleavProduct 
     {
  
        public string sequence;
        public int length;

        public MCleavProduct() {}

         public MCleavProduct(string sequence) 
        {
           this.sequence = sequence;
           this.length = sequence.Length;
        }

    }
    public class MCleavComparer : IComparer
    {
        #region Miembros de IComparer

        private SortDirection m_direction = SortDirection.Ascending;

        public MCleavComparer() : base() { }

        public MCleavComparer(SortDirection direction) 
        {
            this.m_direction = direction;
        }

        int IComparer.Compare(object x, object y) 
        {

           MCleavProduct productX = (MCleavProduct) x;
           MCleavProduct productY = (MCleavProduct) y;

           if (productX == null && productY == null) 
           {
            return 0;
           } 
           else if (productX == null && productY != null) 
           {
            return (this.m_direction == SortDirection.Ascending) ? -1 : 1;
           } 
           else if (productX != null && productY == null) 
           {
            return (this.m_direction == SortDirection.Ascending) ? 1 : -1;
           } 
           else 
            {
                return (this.m_direction == SortDirection.Ascending) ?
                productX.length.CompareTo(productY.length) :
                productY.length.CompareTo(productX.length);
            }
        }
    

        #endregion
    }

    public class Q_scan
    {

        public int row;
        
        public double Vs;
 
        public double Xs;
        public double Xs_noCorrP;

        public double SD_Xs;
        public double Ws;
        public float Zs;
           
        public int z;
        public double quality;
        public bool outlier;
        public double f;
        
        public double adSumSquares;
        public double totSumSquares;

        public double q_SQwindowLeft;
        public double q_SQPeptide;
        public double q_SQwindowRight;
        public double q_SQtotal;

        public double FDR;
        public float FDRs;
       
        public int included;
        public string excludedReasons="";
        
    }

    public class Q_peptide
    {

        //declare the class properties
        protected int start, end, theSize;
        public Q_scan[] scan;
        public rowRangesStrt rowRanges;
        
        public double Xp;
        public double Xp_noCorrP;
        public double Wp;
        public double SD_Xp;
        // public double Ppq;

        public double sumWs=0;
        public double sumWsXs = 0;
        public float Zp;
        public float FDRp;

        public string sequence;
        public int methionines;
        public bool partialDig;
        public bool subpepPartialDig;
        public bool cTerm;
        public bool outlier;
        public int included;
        public string excludedReasons="";
        public int validScans;
        public int used=0;

       
        #region general methods of Q_peptide class
       
        /// <summary>
        /// construct a new list given the capacity
        /// </summary>
        /// <param name="capacity">(int)number of scans in peptide</param>
        public Q_peptide(int capacity)
        {
            //allocate memory for components' list
            scan = new Q_scan[capacity];

            //start, end and size ar 0 (list is empty)
            start = end = theSize = 0;  

            //initial number of valid scans
            validScans = 0;
                       
        }
        /// <summary>
        /// check wether this list is empty
        /// </summary>
        /// <returns>(bool)true if the list is empty</returns>
        public bool isEmpty()
        {
            return theSize == 0;
        }
        /// <summary>
        /// check wether this list is full
        /// </summary>
        /// <returns>(bool)true if the list is full</returns>
        public bool isFull() 
        {
            return theSize >= scan.Length;
        }
        /// <summary>
        /// get the size of this list
        /// </summary>
        /// <returns>(int)size of list</returns>
        public int size() 
        {
            return theSize;
        }
        /// <summary>
        /// insert a new peptide into the list
        /// </summary>
        /// <param name="newScan">(QuiXoT.statistics.Q_scan)scan</param>
        public void insert(Q_scan newScan)
        {

            // if insert won't overflow list
            if (theSize < scan.Length)
            {

                // increment start and set element
                scan[start = (start + 1) % scan.Length] = newScan;

                // increment list size (we've added an element)
                theSize++;
            }
 
        }
        /// <summary>
        /// peek at an element in the list 
        /// </summary>
        /// <param name="offset">(int)array index to point</param>
        /// <returns>(QuiXoT.Statistics.Q_scan)selected scan</returns>
        public Q_scan peek(int offset)
        {
            Q_scan ret = new Q_scan();

            // is someone trying to peek beyond our size?
            if (offset >= theSize)
                return ret;

            // get object we're peeking at (do not remove it)
            return scan[(end + offset + 1) % scan.Length];
        }
        #endregion
         
    }

    public class Q_protein
    {
        //declare the class properties
        protected int start, end, theSize;
        public Q_peptide[] peptide;
        public rowRangesStrt rowRanges;
        
        public double Xq;
        public double SD_Xq;
        public double Wq;
        public double Pq;
        public float  Zq;
        public float FDRq;
        public string excludedReasons;

        public double SumWp;
        public double SumWpXp;
                
        public bool outlier;
        public int validPeptides;
        public int used=0;
        public int included;
        
        #region general methods of Q_protein class
       
        /// <summary>
        /// construct a new list given the capacity
        /// </summary>
        /// <param name="capacity">(int)number of peptiddes in protein</param>
        public Q_protein(int capacity)
        {
            //allocate memory for components' list
            peptide = new Q_peptide[capacity];

            //start, end and size ar 0 (list is empty)
            start = end = theSize = 0;  

            //initial number of valid peptides
            validPeptides = 0;
                       
        }
        /// <summary>
        /// check wether this list is empty
        /// </summary>
        /// <returns>(bool)true if the list is empty</returns>
        public bool isEmpty()
        {
            return theSize == 0;
        }
        /// <summary>
        /// check wether this list is full
        /// </summary>
        /// <returns>(bool)true if the list is full</returns>
        public bool isFull() 
        {
            return theSize >= peptide.Length;
        }
        /// <summary>
        /// get the size of this list
        /// </summary>
        /// <returns>(int)size of list</returns>
        public int size() 
        {
            return theSize;
        }
        /// <summary>
        /// insert a new peptide into the list
        /// </summary>
        /// <param name="newScan">(QuiXoT.statistics.Q_peptide)peptide</param>
        public void insert(Q_peptide newPeptide)
        {

            // if insert won't overflow list
            if (theSize < peptide.Length)
            {

                // increment start and set element
                peptide[start = (start + 1) % peptide.Length] = newPeptide;

                // increment list size (we've added an element)
                theSize++;
            }
 
        }
        /// <summary>
        /// peek at an element in the list 
        /// </summary>
        /// <param name="offset">(int)array index to point</param>
        /// <returns>(QuiXoT.DA_Raw.scanStrt)selected scan</returns>
        public Q_peptide peek(int offset)
        {
            Q_peptide ret = new Q_peptide(1);

            // is someone trying to peek beyond our size?
            if (offset >= theSize)
                return ret;

            // get object we're peeking at (do not remove it)
            return peptide[(end + offset + 1) % peptide.Length];
        }
        #endregion
    
        

    }
    
    public class Stats
    {

        public static void setDataGrid(DataView dv,
                                        AminoacidList[] aas,
                                        out int Ns,
                                        out int Np,
                                        out int Nq)
        {

            #region initial changes in the data grid


            for (int iRows = 0; iRows < dv.Count; iRows++)
            {
                Type ty = dv[iRows].Row["NumLabel1"].GetType();
                Type ty2 = dv[iRows].Row["index"].GetType();

                if (ty.FullName == "System.DBNull")
                {
                    dv[iRows].Row["NumLabel1"] = 1;
                }

                //Get the equivalent sequences
                if (aas != null)
                {
                    string sSeq = dv[iRows].Row["Sequence"].ToString().Trim();
                    dv[iRows].Row["eq_Sequence"] = AminoacidList.getEquivalent(sSeq, aas);
                }

                if (ty2.FullName == "System.DBNull")
                {
                    dv[iRows].Row["index"] = iRows + 1;
                }


                ty = dv[iRows].Row["protLabel"].GetType();
                if (ty.FullName == "System.DBNull")
                {
                    dv[iRows].Row["protLabel"] = 1;
                }

                ty = dv[iRows].Row["peptLabel"].GetType();
                if (ty.FullName == "System.DBNull")
                {
                    dv[iRows].Row["peptLabel"] = 1;
                }
                                

                //The redundances have been cutted by the SEQUEST, so we must cut all the references to compare them 
                //(we use FASTAshort field instead of FASTAProteinDescription)
                string pr1 = dv[iRows].Row["FASTAshort"].ToString();
                string pr2 = dv[iRows].Row["FASTAProteinDescription"].ToString();
                if (pr2 == "")
                {
                    dv[iRows].Row["FASTAProteinDescription"] = "Unknown protein (deployment error)";
                    pr2 = dv[iRows].Row["FASTAProteinDescription"].ToString();
                }
                if (pr2.Length >= 31)
                {
                    dv[iRows].Row["FASTAshort"] = dv[iRows].Row["FASTAProteinDescription"].ToString().Substring(0, 30);
                }
                else
                {
                    dv[iRows].Row["FASTAshort"] = dv[iRows].Row["FASTAProteinDescription"].ToString().Substring(0, pr2.Length - 1);
                }

            }
            # endregion

            // this must be kept like this, as otherwise would not tell the difference
            // between proteins and peptides
            dv.Sort = "FASTAshort, protLabel, eq_Sequence, peptLabel";
            

            #           region Build Protein structure
            //Build the protein structure

            rowRangesStrt[] proteinRanges;
            int numProteins = countProteins(dv, out proteinRanges);

            // protein contains the proteins' tree
            Q_protein[] protein = new Q_protein[numProteins + 1];


            //Build protein structure
            for (int i = 1; i <= numProteins; i++)
            {

                int peptidesProtein;

                rowRangesStrt[] peptideRange;
                peptidesProtein = countPeptides(dv, proteinRanges[i], out peptideRange);

                // protein[i] : the ith protein
                protein[i] = new Q_protein(peptidesProtein + 1);
                protein[i].rowRanges = proteinRanges[i];


                //Build peptide structure
                for (int j = 1; j <= peptidesProtein; j++)
                {

                    int numScans = peptideRange[j].max - peptideRange[j].min + 1;

                    Q_peptide peptide = new Q_peptide(numScans + 1);
                    peptide.rowRanges.min = peptideRange[j].min;
                    peptide.rowRanges.max = peptideRange[j].max;


                    //Build scan structure
                    for (int k = 0; k < numScans; k++)
                    {
                        Q_scan scan = new Q_scan();
                        scan.row = peptide.rowRanges.min + k;
                        peptide.insert(scan);

                    }

                    protein[i].insert(peptide);

                }

            }

            #endregion

            #region Fill the protein structure and look for partial digested peptide candidates

            //ArrayList containing all the sub-peptides produced by partial digested peptides
            ArrayList partialDigCandidates = new ArrayList();

            //fill the structure 

            //for each protein
            for (int i = 1; i <= protein.GetUpperBound(0); i++)
            {
                protein[i].included = 1;


                //for each peptide belonging to the protein
                for (int j = 1; j <= protein[i].peptide.GetUpperBound(0); j++)
                {
                    protein[i].peptide[j].included = 1;
                    int firstRow = protein[i].peptide[j].rowRanges.min;
                    string sSequence = dv[firstRow].Row["eq_Sequence"].ToString();

                    protein[i].peptide[j].sequence = sSequence;


                    bool missDetected;
                    try
                    {
                        ArrayList tempList = detectMCleaveage(sSequence, out missDetected, 5);

                        for (int z = 0; z < tempList.Count; z++)
                        {
                            partialDigCandidates.Add(tempList[z]);
                        }

                        protein[i].peptide[j].partialDig = missDetected;


                        for (int t = protein[i].peptide[j].rowRanges.min; t <= protein[i].peptide[j].rowRanges.max; t++)
                        {
                            if (missDetected)
                            {
                                dv[t].Row["st_PartialDig"] = 1; //missed cleaveage detected
                            }
                            else { dv[t].Row["st_PartialDig"] = 0; }
                        }


                    }
                    catch
                    {
                    }


                    protein[i].peptide[j].methionines = detectMethionines(sSequence);
                    protein[i].peptide[j].cTerm = detectCTerminal(sSequence);


                    //for each scan belonging to the peptide
                    for (int k = 1; k <= protein[i].peptide[j].scan.GetUpperBound(0); k++)
                    {
                        protein[i].peptide[j].scan[k].included = 1;
                        int row = protein[i].peptide[j].scan[k].row;

                        int iZ;
                        bool bParseZ = int.TryParse(dv[row].Row["Charge"].ToString(), out iZ);
                        if (bParseZ)
                        {
                            protein[i].peptide[j].scan[k].z = iZ;
                        }
                        else
                        {
                            protein[i].peptide[j].scan[k].included = 0;
                            protein[i].peptide[j].scan[k].excludedReasons += " scan-NoCharge ";
                        }
                        
                    }

                }
            }
            #endregion

            #region Discard sub-products of partially digested peptides

            MCleavComparer digComparer = new MCleavComparer(SortDirection.Ascending);
            partialDigCandidates.Sort(digComparer);


            ArrayList partialPeptidesDetected = new ArrayList();

            //for each protein i
            for (int i = 1; i <= protein.GetUpperBound(0); i++)
            {
                //for each peptide j belonging to the protein i
                for (int j = 1; j <= protein[i].peptide.GetUpperBound(0); j++)
                {
                    bool isPartialPeptide = detectPartialPeptide(protein[i].peptide[j].sequence, partialDigCandidates);

                    if (isPartialPeptide)
                    {
                        for (int t = protein[i].peptide[j].rowRanges.min; t <= protein[i].peptide[j].rowRanges.max; t++)
                        {
                            dv[t].Row["st_PartialDig"] = 2; //partial digestion subpeptide
                        }
                        partialPeptidesDetected.Add(protein[i].peptide[j].sequence);
                    }


                    if (!protein[i].peptide[j].partialDig)
                    {
                        protein[i].peptide[j].subpepPartialDig = isPartialPeptide;
                    }

                }
            }


            //Detection of those missed cleavages with subpeptides already detected in the sample

            //for each protein i
            for (int i = 1; i <= protein.GetUpperBound(0); i++)
            {
                //for each peptide j belonging to the protein i
                for (int j = 1; j <= protein[i].peptide.GetUpperBound(0); j++)
                {
                    if (protein[i].peptide[j].partialDig)
                    {
                        if (isPartialPeptideDetected(protein[i].peptide[j].sequence, partialPeptidesDetected))
                        {
                            for (int t = protein[i].peptide[j].rowRanges.min; t <= protein[i].peptide[j].rowRanges.max; t++)
                            {
                                dv[t].Row["st_PartialDig"] = 3; //Missed cleavage with subpeptides detected
                            }
                        }
                    }
                }
            }


            #endregion


            #region count prolines
            //If necessary, write the number of prolines in the peptide
            //for each protein
            if (dv.Table.Columns.Contains("numOfProlines"))
            {
                for (int i = 1; i <= protein.GetUpperBound(0); i++)
                {
                    //for each peptide belonging to the protein
                    for (int j = 1; j <= protein[i].peptide.GetUpperBound(0); j++)
                    {

                        int numProlines = countProlines(protein[i].peptide[j].sequence);

                        //for each scan belonging to the peptide
                        for (int k = 1; k <= protein[i].peptide[j].scan.GetUpperBound(0); k++)
                        {
                            int row = protein[i].peptide[j].scan[k].row;

                            dv[row]["numOfProlines"] = numProlines;

                        }

                    }
                }
            }
            #endregion

            #region if no spectrumIndex was assigned, copy from FirstScan

            Type typ = dv[0].Row["spectrumIndex"].GetType();
            if (typ.FullName == "System.DBNull")
            {
                //Copy from column FirstScan
                for (int iRows = 0; iRows < dv.Count; iRows++)
                {
                    int tmpSpecIndex = 0;
                     bool tmpSpecIndext = int.TryParse(dv[iRows]["FirstScan"].ToString(), out tmpSpecIndex);
                     if (tmpSpecIndext)
                     {
                         dv[iRows]["spectrumIndex"] = tmpSpecIndex;
                     }
                }                                    
            }


            #endregion
            

            #region calc of Ns, Np, Nq

            Ns = 0;
            Np = 0;
            Nq = 0;

            for (int q = 1; q <= protein.GetUpperBound(0); q++)
            {
                for (int p = 1; p <= protein[q].peptide.GetUpperBound(0); p++)
                {
                    for (int s = 1; s <= protein[q].peptide[p].scan.GetUpperBound(0); s++)
                    {
                        switch (protein[q].peptide[p].scan[s].included)
                        {

                            case 1:
                                {
                                    //scan is valid
                                    protein[q].peptide[p].validScans++;
                                    int row = protein[q].peptide[p].scan[s].row;
                                    int sc=0;
                                    if (!(int.TryParse(dv[row]["q_index"].ToString(),out sc)))
                                    {
                                        dv[row]["q_index"] = q; 
                                    }
                                    if (!(int.TryParse(dv[row]["p_index"].ToString(), out sc)))
                                    {
                                        dv[row]["p_index"] = p;
                                    } if (!(int.TryParse(dv[row]["s_index"].ToString(), out sc)))
                                    {
                                        dv[row]["s_index"] = s;
                                    }
                                    break;
                                }
                        }
                    }

                    //Print the peptide properties

                    if (protein[q].peptide[p].validScans == 0) protein[q].peptide[p].included = 0;


                    if (protein[q].peptide[p].included == 1)
                    {
                        //peptide is valid
                        protein[q].validPeptides++;
                    }

                    for (int s = protein[q].peptide[p].rowRanges.min; s <= protein[q].peptide[p].rowRanges.max; s++)
                    {

                        dv[s].Row["st_Meth"] = protein[q].peptide[p].methionines;

                        if (protein[q].peptide[p].cTerm)
                        {
                            dv[s].Row["st_Cterm"] = 1;
                        }
                        else
                        {
                            dv[s].Row["st_Cterm"] = 0;
                        }

                    }



                }

            }


            for (int q = 1; q <= protein.GetUpperBound(0); q++)
            {
                for (int p = 1; p <= protein[q].peptide.GetUpperBound(0); p++)
                {
                    if (protein[q].peptide[p].included == 1)
                    {
                        for (int s = 1; s <= protein[q].peptide[p].scan.GetUpperBound(0); s++)
                        {
                            if (protein[q].peptide[p].scan[s].included == 1)
                            {
                                Ns++;
                                protein[q].peptide[p].used++;
                            }
                        }
                        
                        if (protein[q].peptide[p].used > 0)
                        {
                            Np++;
                            protein[q].used++;
                        }                      

                    } 

                }
                if (protein[q].used > 0 && protein[q].included > 0)
                {
                    Nq++;
                }
            }

            #endregion

 
        }
        
        private static Q_protein[] getProteinTree(  DataView dv,
                                                    statOptionsStrt options
                                                    ,out int Ns, out int Np, out int Nq)
        {


            for (int i = 0; i < dv.Count; i++)
            {
                dv[i].Row["st_excluded"] = "excluded";
            }

            dv.RowFilter = options.filter;

            for (int i = 0; i < dv.Count; i++)
            {
                dv[i].Row["st_excluded"] = "";
            }

            dv.Sort = "FASTAshort, protLabel, eq_Sequence, peptLabel";

            bool catched = false;
            bool catchedFDRs = false;

            #           region Build Protein structure
            //Build the protein structure

            rowRangesStrt[] proteinRanges;
            int numProteins = countProteins(dv, out proteinRanges);

            if (numProteins == 0)
            {
                Ns = 0;
                Np = 0;
                Nq = 0;
                return null;
            }

            // protein contains the proteins' tree
            Q_protein[] protein = new Q_protein[numProteins + 1];


            //Build protein structure
            for (int i = 1; i <= numProteins; i++)
            {

                int peptidesProtein;

                rowRangesStrt[] peptideRange;
                peptidesProtein = countPeptides(dv, proteinRanges[i], out peptideRange);

                // protein[i] : the ith protein
                protein[i] = new Q_protein(peptidesProtein + 1);
                protein[i].rowRanges = proteinRanges[i];


                //Build peptide structure
                for (int j = 1; j <= peptidesProtein; j++)
                {

                    int numScans = peptideRange[j].max - peptideRange[j].min + 1;

                    Q_peptide peptide = new Q_peptide(numScans + 1);
                    peptide.rowRanges.min = peptideRange[j].min;
                    peptide.rowRanges.max = peptideRange[j].max;


                    //Build scan structure
                    for (int k = 0; k < numScans; k++)
                    {
                        Q_scan scan = new Q_scan();
                        scan.row = peptide.rowRanges.min + k;
                        peptide.insert(scan);

                    }

                    protein[i].insert(peptide);

                }

            }

            #endregion

            #region Fill the protein structure and look for partial digested peptide candidates

            //ArrayList containing all the sub-peptides produced by partial digested peptides
            ArrayList partialDigCandidates = new ArrayList();

            //fill the structure 

            //for each protein
            for (int i = 1; i <= protein.GetUpperBound(0); i++)
            {
                protein[i].included = 1;


                //for each peptide belonging to the protein
                for (int j = 1; j <= protein[i].peptide.GetUpperBound(0); j++)
                {
                    protein[i].peptide[j].included = 1;
                    int firstRow = protein[i].peptide[j].rowRanges.min;
                    string sSequence = dv[firstRow].Row["eq_Sequence"].ToString();
                    
                    protein[i].peptide[j].sequence = sSequence;


                    bool missDetected;
                    try
                    {
                        ArrayList tempList = detectMCleaveage(sSequence, out missDetected, 5);

                        for (int z = 0; z < tempList.Count; z++)
                        {
                            partialDigCandidates.Add(tempList[z]);
                        }

                        protein[i].peptide[j].partialDig = missDetected;


                        for (int t = protein[i].peptide[j].rowRanges.min; t <= protein[i].peptide[j].rowRanges.max; t++)
                        {
                            if (missDetected)
                            {
                                dv[t].Row["st_PartialDig"] = 1; //missed cleaveage detected
                            }
                            else { dv[t].Row["st_PartialDig"] = 0; }
                        }


                    }
                    catch
                    {
                    }


                    protein[i].peptide[j].methionines = detectMethionines(sSequence);
                    protein[i].peptide[j].cTerm = detectCTerminal(sSequence);


                    //for each scan belonging to the peptide
                    for (int k = 1; k <= protein[i].peptide[j].scan.GetUpperBound(0); k++)
                    {
                        protein[i].peptide[j].scan[k].included = 1;
                        int row = protein[i].peptide[j].scan[k].row;
                        protein[i].peptide[j].scan[k].Xs = double.Parse(dv[row].Row[options.colXs].ToString());
                        protein[i].peptide[j].scan[k].Vs = double.Parse(dv[row].Row[options.colVs].ToString());

                        int iZ;
                        bool bParseZ = int.TryParse(dv[row].Row["Charge"].ToString(), out iZ);
                        if (bParseZ)
                        {
                            protein[i].peptide[j].scan[k].z = iZ;
                        }
                        else
                        {
                            protein[i].peptide[j].scan[k].included = 0;
                            protein[i].peptide[j].scan[k].excludedReasons += " scan-NoCharge ";
                        }

                        protein[i].peptide[j].scan[k].quality = (double)dv[row].Row["numLabel1"];

                        try
                        {
                            protein[i].peptide[j].scan[k].FDR = (double)dv[row].Row["FDR"];
                        }
                        catch { }

                        try
                        {
                            if (!catchedFDRs)
                            {
                                protein[i].peptide[j].scan[k].FDRs = float.Parse(dv[row].Row["FDRs"].ToString());
                            }
                        }
                        catch { catchedFDRs = true; }

                        try
                        {
                            if (!catched)
                            {
                                protein[i].peptide[j].scan[k].f = (double)dv[row].Row["q_f"];

                                protein[i].peptide[j].scan[k].adSumSquares = (double)dv[row].Row["q_SQwindows"];
                                protein[i].peptide[j].scan[k].totSumSquares = (double)dv[row].Row["q_SQtotal"];
                                protein[i].peptide[j].scan[k].totSumSquares = (double)dv[row].Row["q_SQtotal"];
                                protein[i].peptide[j].scan[k].q_SQwindowLeft = (double)dv[row].Row["q_SQwindowLeft"];
                                protein[i].peptide[j].scan[k].q_SQPeptide = (double)dv[row].Row["q_SQPeptide"];
                                protein[i].peptide[j].scan[k].q_SQwindowRight = (double)dv[row].Row["q_SQwindowRight"];
                            }
                        }
                        catch
                        {
                            catched = true;
                        }


                    }

                }
            }
            #endregion

            #region Discard sub-products of partially digested peptides

            MCleavComparer digComparer = new MCleavComparer(SortDirection.Ascending);
            partialDigCandidates.Sort(digComparer);


            ArrayList partialPeptidesDetected=new ArrayList();

            //for each protein i
            for (int i = 1; i <= protein.GetUpperBound(0); i++)
            {
                //for each peptide j belonging to the protein i
                for (int j = 1; j <= protein[i].peptide.GetUpperBound(0); j++)
                {
                    bool isPartialPeptide = detectPartialPeptide(protein[i].peptide[j].sequence, partialDigCandidates);

                    if (isPartialPeptide)
                    {
                        for (int t = protein[i].peptide[j].rowRanges.min; t <= protein[i].peptide[j].rowRanges.max; t++)
                        {
                            dv[t].Row["st_PartialDig"] = 2; //partial digestion subpeptide
                        }
                        partialPeptidesDetected.Add(protein[i].peptide[j].sequence);
                    }


                    if (!protein[i].peptide[j].partialDig)
                    {
                        protein[i].peptide[j].subpepPartialDig = isPartialPeptide;
                    }

                }
            }


            //Detection of those missed cleavages with subpeptides already detected in the sample

            //for each protein i
            for (int i = 1; i <= protein.GetUpperBound(0); i++)
            {
                //for each peptide j belonging to the protein i
                for (int j = 1; j <= protein[i].peptide.GetUpperBound(0); j++)
                {
                    if (protein[i].peptide[j].partialDig)
                    {
                        if (isPartialPeptideDetected(protein[i].peptide[j].sequence, partialPeptidesDetected))
                        {
                            for (int t = protein[i].peptide[j].rowRanges.min; t <= protein[i].peptide[j].rowRanges.max; t++)
                            {
                                dv[t].Row["st_PartialDig"] = 3; //Missed cleavage with subpeptides detected
                            } 
                        }
                    }
                }
            }


            #endregion


            #region calc of Ns, Np, Nq

            Ns = 0;
            Np = 0;
            Nq = 0;

            for (int q = 1; q <= protein.GetUpperBound(0); q++)
            {
                for (int p = 1; p <= protein[q].peptide.GetUpperBound(0); p++)
                {
                    for (int s = 1; s <= protein[q].peptide[p].scan.GetUpperBound(0); s++)
                    {
                        switch (protein[q].peptide[p].scan[s].included)
                        {

                            case 1:
                                {
                                    //scan is valid
                                    protein[q].peptide[p].validScans++;
                                    break;
                                }
                        }
                    }

                    //Print the peptide properties

                    if (protein[q].peptide[p].validScans == 0) protein[q].peptide[p].included = 0;


                    if (protein[q].peptide[p].included == 1)
                    {
                        //peptide is valid
                        protein[q].validPeptides++;
                    }

                    for (int s = protein[q].peptide[p].rowRanges.min; s <= protein[q].peptide[p].rowRanges.max; s++)
                    {

                        dv[s].Row["st_Meth"] = protein[q].peptide[p].methionines;

                        if (protein[q].peptide[p].cTerm)
                        {
                            dv[s].Row["st_Cterm"] = 1;
                        }
                        else
                        {
                            dv[s].Row["st_Cterm"] = 0;
                        }

                    }



                }

            }


            for (int q = 1; q <= protein.GetUpperBound(0); q++)
            {
                for (int p = 1; p <= protein[q].peptide.GetUpperBound(0); p++)
                {
                    if (protein[q].peptide[p].included == 1)
                    {
                        for (int s = 1; s <= protein[q].peptide[p].scan.GetUpperBound(0); s++)
                        {
                            if (protein[q].peptide[p].scan[s].included == 1)
                            {
                                Ns++;
                                protein[q].peptide[p].used++;
                            }
                        }

                        if (protein[q].peptide[p].used > 0)
                        {
                            Np++;
                            protein[q].used++;
                        }

                    }
                }
                if (protein[q].used > 0 && protein[q].included>0)
                {
                    Nq++;
                }
            }

            //Advanced options: when "ignore scans" is selected, info about scans must be
            //obtained from the datagrid.
            if (options.ignScans)
            {
                Ns = 0;
                for (int q = 1; q <= protein.GetUpperBound(0); q++)
                {
                    for (int p = 1; p <= protein[q].peptide.GetUpperBound(0); p++)
                    {
                        int numOfscansPerPeptide = 0;
                        int currrow = protein[q].peptide[p].rowRanges.min;
                        bool b = int.TryParse(dv[currrow]["scan_per_peptide"].ToString(), out numOfscansPerPeptide);
                        if (b)
                        {
                            Ns += numOfscansPerPeptide;
                        }
                        double parseWp=0;                        
                        bool b1= double.TryParse(dv[currrow][options.colWp].ToString(),out parseWp);
                        if (b1)
                        {
                            protein[q].peptide[p].Wp = parseWp;
                        }

                        double parseXp = 0;                        
                        bool b2 = double.TryParse(dv[currrow]["Xp"].ToString(), out parseXp);
                        if (b2)
                        {
                            protein[q].peptide[p].Xp = parseXp;
                        }
                    }
                }
            }

            //Advanced options: when "ignore peptides" is selected, info about peptides must be
            //obtained from the datagrid.
            if (options.ignPeptides)
            {
                Np = 0;
                int numOfPepsPerProtein = 0;                   
                for (int q = 1; q <= protein.GetUpperBound(0); q++)
                {    
                    int currrow = protein[q].rowRanges.min;
                    bool b = int.TryParse(dv[currrow]["pep_per_protein"].ToString(), out numOfPepsPerProtein);
                    if (b)
                    {
                        Np += numOfPepsPerProtein;
                    }
                    double parseWq = 0;
                    bool b1 = double.TryParse(dv[currrow][options.colWq].ToString(), out parseWq);
                    if (b1)
                    {
                        protein[q].Wq = parseWq;
                    }

                    double parseXq = 0;
                    bool b2 = double.TryParse(dv[currrow]["Xq"].ToString(), out parseXq);
                    if (b2)
                    {
                        protein[q].Xq = parseXq;
                    }
                }
            }

            #endregion


            return protein;
        }
                 

        public static variancesStrt calVariances(DataView dv,
                                        statOptionsStrt options)
        {

            double sigma2S=0.01;
            double sigma2P=0.01;
            double sigma2Q=0.001;
            bool Fless1 = false;
            bool Fplus1 = false;
             
            bool sigma2S_Fcut = false;
            bool sigma2P_Fcut = false;
            bool sigma2Q_Fcut = false;
            double X = 0;

            // coeff = 1/DISTR.NORM.ESTAND.INV(3/4)
            double coeff = 1.4826022185056;

            variancesStrt varCalc = new variancesStrt();

            //Fit variables
            double divdelta = 50;
            int cicle = 0;
            double Fcurr = 1000;
            double Fbest = 10;

            

            #region initial changes in the data grid
            //if any protLabel or peptLabel is null, fill with 1.
            //double dNaN = double.NaN;

            for (int iRows = 0; iRows < dv.Count; iRows++)
            {

                Type ty = dv[iRows].Row["protLabel"].GetType();
                if (ty.FullName == "System.DBNull")
                {
                    dv[iRows].Row["protLabel"] = 1;
                }

                ty = dv[iRows].Row["peptLabel"].GetType();
                if (ty.FullName == "System.DBNull")
                {
                    dv[iRows].Row["peptLabel"] = 1;
                }


                //The redundances have been cutted by the SEQUEST, so we must cut all the references to compare them 
                //(we use FASTAshort field instead of FASTAProteinDescription)
                string pr1 = dv[iRows].Row["FASTAshort"].ToString();
                string pr2 = dv[iRows].Row["FASTAProteinDescription"].ToString();
                if (pr2 == "")
                {
                    dv[iRows].Row["FASTAProteinDescription"] = "Unknown protein (deployment error)";
                    pr2 = dv[iRows].Row["FASTAProteinDescription"].ToString();
                }
                if (pr2.Length >= 31)
                {
                    dv[iRows].Row["FASTAshort"] = dv[iRows].Row["FASTAProteinDescription"].ToString().Substring(0, 30);
                }
                else
                {
                    dv[iRows].Row["FASTAshort"] = dv[iRows].Row["FASTAProteinDescription"].ToString().Substring(0, pr2.Length - 1);
                }

            }
            # endregion

            //preserve the old filter
            string filterOld = dv.RowFilter;

            //obtain the protein tree
            int Ns = 0;
            int Np = 0;
            int Nq = 0;
            Q_protein[] protein = getProteinTree(dv, 
                                                options,
                                                out Ns, 
                                                out Np, 
                                                out Nq);

            varCalc.Ns = Ns;
            varCalc.Np = Np;
            varCalc.Nq = Nq;

            varCalc.k = options.k;

            #region calculate sigma2S (normalized median method)
            
            //Establish the Vs threshold in the protein tree & more than 1 scan for the statistics
            //calculate also the total number of scans used.
            varCalc.Ns = 0;

            if (!options.calSigmas)
            {
                varCalc.sigma2S = options.sigmas_default;
                sigma2S = options.sigmas_default;
                sigma2S_Fcut = true;
                varCalc.Ns = 0;
                varCalc.Fs = double.NaN;
            }

            if (options.calSigmas && protein != null)
            {
                for (int q = 1; q <= protein.GetUpperBound(0); q++)
                {
                    protein[q].included = 0;
                    for (int p = 1; p <= protein[q].peptide.GetUpperBound(0); p++)
                    {
                        protein[q].peptide[p].included = 0;
                        int Nsi = 0;
                        for (int s = 1; s <= protein[q].peptide[p].scan.GetUpperBound(0); s++)
                        {
                            if (protein[q].peptide[p].scan[s].Vs < options.vs_thres)
                            {
                                protein[q].peptide[p].scan[s].included = 0;
                            }
                            else
                            {
                                Nsi++;
                            }
                        }

                        if (Nsi > 1)
                        {
                            protein[q].peptide[p].included = 1;
                            protein[q].included = 1;
                            for (int s = 1; s <= protein[q].peptide[p].scan.GetUpperBound(0); s++)
                            {
                                if (protein[q].peptide[p].scan[s].included > 0)
                                {
                                    varCalc.Ns++;
                                }
                            }
                        }
                    }
                }



                double sigma2smin = options.s2smin;
                double sigma2smax = options.s2smax;
                double sigma2scurr = sigma2smin;
                double deltaSigma2s = options.s2sdelta;
                double sigma2Sbest = sigma2smin;

                for (int runs = 1; runs <= options.s2sCicles; runs++)
                {
                    while (sigma2scurr < sigma2smax)
                    {
                        protein = calAverages(options.k, sigma2scurr, sigma2P, sigma2Q, protein, options, out X);

                        ArrayList Med = new ArrayList();

                        //adding information for doing the median
                        for (int q = 1; q <= protein.GetUpperBound(0); q++)
                        {
                            if (protein[q].included > 0)
                            {
                                for (int p = 1; p <= protein[q].peptide.GetUpperBound(0); p++)
                                {
                                    if (protein[q].peptide[p].included > 0)
                                    {
                                        for (int s = 1; s <= protein[q].peptide[p].scan.GetUpperBound(0); s++)
                                        {
                                            if (protein[q].peptide[p].scan[s].included > 0)
                                            {
                                                double Xp = protein[q].peptide[p].Xp;
                                                double Xs = protein[q].peptide[p].scan[s].Xs;
                                                double Ws = protein[q].peptide[p].scan[s].Ws;
                                                double gs = (double)protein[q].peptide[p].validScans / ((double)protein[q].peptide[p].validScans - 1);

                                                double Fi = gs * Ws * (Xs - Xp) * (Xs - Xp);

                                                Med.Add(Fi);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        double med = Utilities.median(Med);
                        Fcurr = coeff * coeff * med;


                        //Check wether we have crossed the optimal value of the function
                        if (Fcurr < 1) Fless1 = true;
                        if (Fcurr > 1) Fplus1 = true;


                        if (Math.Abs(Fcurr - 1) < Math.Abs(Fbest - 1))
                        {
                            sigma2S = sigma2scurr;
                            sigma2Sbest = sigma2S;
                            Fbest = Fcurr;
                        }
                        sigma2scurr += deltaSigma2s;
                        cicle++;
                    }
                    sigma2smin = sigma2Sbest - deltaSigma2s;
                    if (sigma2smin < 0) sigma2smin = 0;
                    sigma2smax = sigma2Sbest + deltaSigma2s;
                    deltaSigma2s /= divdelta;
                    sigma2scurr = sigma2smin;
                }

                //Stablish the final value for sigma2S
                sigma2S = sigma2Sbest;
                varCalc.Fs = Fbest;
                if (Fless1 && Fplus1) sigma2S_Fcut = true;

            }

            #endregion


            #region calculate sigma2P (normalized median method)


            if (!options.calSigmap)
            {
                varCalc.sigma2P = options.sigmap_default;
                sigma2P = options.sigmap_default;
                sigma2P_Fcut = true;
                varCalc.Np = 0;
                varCalc.Fp = double.NaN;
            }

            if (options.calSigmap && protein != null)
            {

                Fless1 = false;
                Fplus1 = false;
                varCalc.Np = 0;

                //restore the tree
                for (int q = 1; q <= protein.GetUpperBound(0); q++)
                {
                    protein[q].included = 1;
                    for (int p = 1; p <= protein[q].peptide.GetUpperBound(0); p++)
                    {
                        protein[q].peptide[p].included = 1;
                        for (int s = 1; s <= protein[q].peptide[p].scan.GetUpperBound(0); s++)
                        {
                            protein[q].peptide[p].scan[s].included = 1;
                        }
                    }
                }


                //Select proteins with more than one peptide (and stablish Wp threshold)
                int otroCounter = 0;
                for (int q = 1; q <= protein.GetUpperBound(0); q++)
                {
                    int Npi = 0;
                    for (int p = 1; p <= protein[q].peptide.GetUpperBound(0); p++)
                    {
                        if (protein[q].peptide[p].included == 1)
                        {
                            if (protein[q].peptide[p].Wp < options.wp_thres)
                            {
                                protein[q].peptide[p].included = 0;
                            }
                            else
                            {
                                Npi++;
                                otroCounter++;
                            }
                        }
                    }

                    if (Npi < 2)
                    {
                        protein[q].included = 0;
                        for (int p = 1; p <= protein[q].peptide.GetUpperBound(0); p++)
                        {
                            protein[q].peptide[p].included = 0;

                        }
                    }

                }

                //Count the number of peptides used for calculating sigma2p
                int numOfProtsIncl = 0;
                for (int q = 1; q <= protein.GetUpperBound(0); q++)
                {
                    if (protein[q].included == 1)
                    {
                        numOfProtsIncl++;
                        for (int p = 1; p <= protein[q].peptide.GetUpperBound(0); p++)
                        {
                            if (protein[q].peptide[p].included == 1)
                            {
                                varCalc.Np++;
                            }
                        }
                    }
                }


                double sigma2pmin = options.s2pmin;
                double sigma2pmax = options.s2pmax;
                double sigma2pcurr = sigma2pmin;
                double deltaSigma2p = options.s2pdelta;
                divdelta = 50;
                cicle = 0;
                Fcurr = 1000;
                Fbest = 10;
                double sigma2Pbest = sigma2pmin;

                for (int runs = 1; runs <= options.s2pCicles; runs++)
                {
                    while (sigma2pcurr < sigma2pmax)
                    {
                        protein = calAverages(options.k, sigma2S, sigma2pcurr, sigma2Q, protein, options, out X);

                        ArrayList Med = new ArrayList();

                        //adding information for doing the median
                        for (int q = 1; q <= protein.GetUpperBound(0); q++)
                        {
                            if (protein[q].included > 0)
                            {
                                for (int p = 1; p <= protein[q].peptide.GetUpperBound(0); p++)
                                {
                                    if (protein[q].peptide[p].included > 0)
                                    {
                                        double Xp = protein[q].peptide[p].Xp;
                                        double Xq = protein[q].Xq;
                                        double Wp = protein[q].peptide[p].Wp;
                                        double gp = (double)protein[q].validPeptides / ((double)protein[q].validPeptides - 1);

                                        double Fi = gp * Wp * (Xp - Xq) * (Xp - Xq);

                                        Med.Add(Fi);
                                    }
                                }
                            }
                        }
                        double med = Utilities.median(Med);
                        Fcurr = coeff * coeff * med;


                        //Check wether we have crossed the optimal value of the function
                        if (Fcurr < 1) Fless1 = true;
                        if (Fcurr > 1) Fplus1 = true;


                        if (Math.Abs(Fcurr - 1) < Math.Abs(Fbest - 1))
                        {
                            sigma2P = sigma2pcurr;
                            sigma2Pbest = sigma2P;
                            Fbest = Fcurr;
                        }
                        sigma2pcurr += deltaSigma2p;
                        cicle++;
                    }
                    sigma2pmin = sigma2Pbest - deltaSigma2p;
                    if (sigma2pmin < 0) sigma2pmin = 0;
                    sigma2pmax = sigma2Pbest + deltaSigma2p;
                    deltaSigma2p /= divdelta;
                    sigma2pcurr = sigma2pmin;
                }

                //Stablish the final value for sigma2P
                sigma2P = sigma2Pbest;
                varCalc.Fp = Fbest;
                if (Fless1 && Fplus1) sigma2P_Fcut = true;

            }

            #endregion


            #region calculate sigma2Q (normalized median method)

            if (!options.calSigmaq)
            {
                varCalc.sigma2Q = options.sigmaq_default;
                sigma2Q = options.sigmaq_default;
                sigma2Q_Fcut = true;
                varCalc.Nq = 0;
                varCalc.Fq = double.NaN;
            }

            if (options.calSigmaq && protein != null)
            {
                Fless1 = false;
                Fplus1 = false;
                varCalc.Nq = 0;

                //restore the tree
                for (int q = 1; q <= protein.GetUpperBound(0); q++)
                {
                    protein[q].included = 1;
                    for (int p = 1; p <= protein[q].peptide.GetUpperBound(0); p++)
                    {
                        protein[q].peptide[p].included = 1;
                        for (int s = 1; s <= protein[q].peptide[p].scan.GetUpperBound(0); s++)
                        {
                            protein[q].peptide[p].scan[s].included = 1;
                        }
                    }
                }

                //Select proteins over Wq threshold
                for (int q = 1; q <= protein.GetUpperBound(0); q++)
                {
                    protein[q].included = 1;
                    if (protein[q].Wq < options.wq_thres)
                    {
                        protein[q].included = 0;
                    }
                    else { varCalc.Nq++; }
                }
                                

                double sigma2qmin = options.s2qmin;
                double sigma2qmax = options.s2qmax;
                double sigma2qcurr = sigma2qmin;
                double deltaSigma2q = options.s2qdelta;
                divdelta = 50;
                cicle = 0;
                Fcurr = 1000;
                Fbest = 10;
                double sigma2Qbest = sigma2qmin;

                for (int runs = 1; runs <= options.s2qCicles; runs++)
                {
                    while (sigma2qcurr < sigma2qmax)
                    {
                        protein = calAverages(options.k, sigma2S, sigma2P, sigma2qcurr, protein, options, out X);
                        X = calX(protein, 0);

                        ArrayList Med = new ArrayList();

                        //adding information for doing the median
                        for (int q = 1; q <= protein.GetUpperBound(0); q++)
                        {
                            if (protein[q].included > 0)
                            {
                                double Xq = protein[q].Xq;
                                double Wq = protein[q].Wq;

                                double Fi = Wq * (Xq - X) * (Xq - X);

                                Med.Add(Fi);
                            }
                        }

                        double med = Utilities.median(Med);
                        Fcurr = coeff * coeff * med;


                        //Check wether we have crossed the optimal value of the function
                        if (Fcurr < 1) Fless1 = true;
                        if (Fcurr > 1) Fplus1 = true;


                        if (Math.Abs(Fcurr - 1) < Math.Abs(Fbest - 1))
                        {
                            sigma2Q = sigma2qcurr;
                            sigma2Qbest = sigma2Q;
                            Fbest = Fcurr;
                        }
                        sigma2qcurr += deltaSigma2q;
                        cicle++;
                    }
                    sigma2qmin = sigma2Qbest - deltaSigma2q;
                    if (sigma2qmin < 0) sigma2qmin = 0;
                    sigma2qmax = sigma2Qbest + deltaSigma2q;
                    deltaSigma2q /= divdelta;
                    sigma2qcurr = sigma2qmin;
                }

                //Stablish the final value for sigma2Q
                sigma2Q = sigma2Qbest;
                varCalc.Fq = Fbest;
                if (Fless1 && Fplus1) sigma2Q_Fcut = true;

            }

            #endregion


            #region Output of the results
            varCalc.Fscut = sigma2S_Fcut;
            varCalc.Fpcut = sigma2P_Fcut;
            varCalc.Fqcut = sigma2Q_Fcut;

            varCalc.sigma2S = sigma2S;
            varCalc.sigma2P = sigma2P;
            varCalc.sigma2Q = sigma2Q;

            varCalc.X  = X;

            varCalc.vs_thres = options.vs_thres;
            varCalc.wp_thres = options.wp_thres;
            varCalc.wq_thres = options.wq_thres;

            varCalc.filter = options.filter;

            #endregion

            if (protein == null)
            {
                MessageBox.Show("Warning: statistics could not be performed,\nas all rows were excluded using the filter introduced");
            }

            return varCalc;
        }



        public static DataView calStatistics( DataView dv,
                                            DataView dvPrev,
                                            statOptionsStrt options,                                            
                                            QuiXoT.math.LNquantitate.quantitationStrategy qStrategy                                                                
                                          )
        {


            //for (int iRows = 0; iRows < dv.Count; iRows++)
            //{
            //    dv[iRows].Row["st_excluded"] = System.DBNull.Value.ToString();
            //}
            
            //get the constant values used            
            double ct_K=(double)dvPrev[0].Row["ct_k"];
            double sigma2S=(double)dvPrev[0].Row["ct_sigma2S"];
            double sigma2P=(double)dvPrev[0].Row["ct_sigma2P"];
            double sigma2Q=(double)dvPrev[0].Row["ct_sigma2Q"];;
            
  
            //obtain the protein tree
            int Ns = 0;
            int Np = 0;
            int Nq = 0;
            Q_protein[] protein = getProteinTree(dv,
                                                options,
                                                out Ns,
                                                out Np,
                                                out Nq);
                        
 
            double X;
  

            //calculate the averages
            protein = calAverages(  ct_K,
                                    sigma2S,
                                    sigma2P,
                                    sigma2Q,
                                    protein,
                                    options,
                                    out X);

            
            if (options.forceX)
            {
                X = options.forcedX;
            }            

            //calculate p-values and FDR for each level
            protein = calZiAndFDR(Ns, Np, Nq, protein, X);


            //recalculate super-mean, 
            //p-values and FDR for each level by stablishing an FDRq threshold
            double Xprev = X;
            int counter = 0;
            double diffX_Xprev = 1000;
            double fdrqth = 1e-3;
            double step = 4e-3;
            while(counter<100 && diffX_Xprev>0.01)
            {
                Xprev = X;

                if (options.forceX) 
                {
                    X = options.forcedX;
                }
                else
                {
                    X = calX(protein, fdrqth);
                }

                protein = calZiAndFDR(Ns, Np, Nq, protein, X);

                fdrqth += step;
                counter++;
                diffX_Xprev = Math.Abs(X - Xprev);
            }

            //For SILAC analysis, apply the SILAC correction of the Arg-->Pro conversion
            double Phi=0;
            if (options.silacCorrection)
            {
                protein = correctionSILAC(options, protein, out Phi);

                //If correction is applicable, then we recalculate averages and FDR
                if (Phi < 0)
                {
                    //calculate the averages
                    protein = calAverages(ct_K,
                                            sigma2S,
                                            sigma2P,
                                            sigma2Q,
                                            protein,
                                            options,                                            
                                            out X);

                    //calculate p-values and FDR for each level
                    protein = calZiAndFDR(Ns, Np, Nq, protein, X);                    
                }

            }


            //Write the data in the dataview
            dv = writeInDataView(dv, protein,options);

            //write the general details in the dataview 
            dvPrev[0].Row["X"] = X;
            dvPrev[0].Row["Ns"] = Ns;
            dvPrev[0].Row["Np"] = Np;
            dvPrev[0].Row["Nq"] = Nq;

            if (!options.silacCorrection) 
            {
                if (dvPrev.Table.Columns.Contains("Phi"))
                {
                    dvPrev[0].Row["Phi"] = System.DBNull.Value;
                }
                if (dvPrev.Table.Columns.Contains("FDRq_for_SILAC_correction"))
                {
                    dvPrev[0].Row["FDRq_for_SILAC_correction"] = System.DBNull.Value;
                }
            }

            if (options.silacCorrection)
            {
                if (dvPrev.Table.Columns.Contains("Phi"))
                {
                    dvPrev[0].Row["Phi"] = Phi;
                }
                if (dvPrev.Table.Columns.Contains("FDRq_for_SILAC_correction"))
                {
                    dvPrev[0].Row["FDRq_for_SILAC_correction"] = options.silacFDRq;
                }

                if(dv.Table.Columns.Contains("Xs_NoCorrP"))
                {
                    for (int q = 1; q <= protein.GetUpperBound(0); q++)
                    {
                        for (int p = 1; p <= protein[q].peptide.GetUpperBound(0); p++)
                        {                        

                                for (int s = 1; s <= protein[q].peptide[p].scan.GetUpperBound(0); s++)
                                {
                                    int row = protein[q].peptide[p].scan[s].row;
                                    dv[row].Row["Xs_NoCorrP"] = protein[q].peptide[p].scan[s].Xs_noCorrP;
                                }
                        }
                    }
                }

                if (dv.Table.Columns.Contains("Xp_NoCorrP"))
                {
                    for (int q = 1; q <= protein.GetUpperBound(0); q++)
                    {
                        for (int p = 1; p <= protein[q].peptide.GetUpperBound(0); p++)
                        {

                            for (int s = 1; s <= protein[q].peptide[p].scan.GetUpperBound(0); s++)
                            {
                                int row = protein[q].peptide[p].scan[s].row;
                                dv[row].Row["Xp_NoCorrP"] = protein[q].peptide[p].Xp_noCorrP;
                            }
                        }
                    }
                }


            }


           return dv;
        }

        private static Q_protein[] correctionSILAC(statOptionsStrt options, 
                                                        Q_protein[] _protein,
                                                        out double Phi)
        {
            Phi = 0; //Correction factor

            double sumWp_XqXp_rp = 0;
            double sumWp_rp2 = 0;
            for (int q = 1; q <= _protein.GetUpperBound(0); q++)
            {
                bool oneWithNoProls = false; //at least one of the peptides of the protein does not contain a proline, 
                //so that we can estimate the average Xq w/o prolines

                double currXq = 0;
                double currsum_wp = 0;
                double currsum_wpxp = 0;
                for (int p = 1; p <= _protein[q].peptide.GetUpperBound(0); p++)
                {
                    if (countProlines(_protein[q].peptide[p].sequence) == 0 && _protein[q].validPeptides > 0 && _protein[q].peptide[p].Wp > 0)
                    {
                        currsum_wp += _protein[q].peptide[p].Wp;
                        currsum_wpxp += _protein[q].peptide[p].Wp * _protein[q].peptide[p].Xp;
                        oneWithNoProls = true;
                    }
                    if (currsum_wp == 0)
                    {
                        oneWithNoProls = false;
                    }
                }

                currXq = currsum_wpxp / currsum_wp;

                //Evaluate only if the protein is quantitated by more than one peptides, and only if FDRq>FDRqThreshold
                double currFDRq = _protein[q].FDRq;
                if (_protein[q].validPeptides > 0 && currFDRq >= options.silacFDRq && oneWithNoProls)
                {
                    for (int p = 1; p <= _protein[q].peptide.GetUpperBound(0); p++)
                    {
                        int rqp = countProlines(_protein[q].peptide[p].sequence);
                        if (rqp > 0) //Phi must be only evaluated with those peptides containing prolines
                        {
                            sumWp_XqXp_rp += _protein[q].peptide[p].Wp * (currXq - _protein[q].peptide[p].Xp) * rqp;
                            sumWp_rp2 += _protein[q].peptide[p].Wp * rqp * rqp;
                        }
                    }
                }

            }

            Phi = sumWp_XqXp_rp / sumWp_rp2;

            //If Phi estimation is more than zero, the estimation is clearly erroneous --> No correction is made
            if (Phi > 0) 
            {
                Phi = 0;
                //return _protein;
            }

            //Correction to the scans of those peptides containing prolines
            for (int q = 1; q <= _protein.GetUpperBound(0); q++)
            {
                for (int p = 1; p <= _protein[q].peptide.GetUpperBound(0); p++)
                {
                    int nProls = countProlines(_protein[q].peptide[p].sequence);
                    
                    
                    //preserving xp before the correction
                    //It is not necessary to correct at peptide level at this moment, because we are going to recalculate all the data
                    //Correction must be done now only at scan level. It is only necessary to be corrected when the option "ignore scans"
                    //is activated.

                    _protein[q].peptide[p].Xp_noCorrP = _protein[q].peptide[p].Xp;

                    if (options.ignScans)
                    {
                        _protein[q].peptide[p].Xp += nProls * Phi;
                    }

                    for (int s = 1; s <= _protein[q].peptide[p].scan.GetUpperBound(0); s++)
                    {
                        //preserving xs before the correction
                        _protein[q].peptide[p].scan[s].Xs_noCorrP = _protein[q].peptide[p].scan[s].Xs;
                        if (nProls > 0 && _protein[q].validPeptides > 0 && _protein[q].peptide[p].Wp > 0)
                        {
                            _protein[q].peptide[p].scan[s].Xs += nProls*Phi;
                        }
                    } 
                } 
            }

            return _protein;
        }

        private static Q_protein[] calZiAndFDR(int Ns, 
                                                    int Np, 
                                                    int Nq, 
                                                    Q_protein[] _protein, 
                                                    double X)
        {
            for (int q = 1; q <= _protein.GetUpperBound(0); q++)
            {
                double Xq = _protein[q].Xq;
                double nq = _protein[q].validPeptides;

                for (int p = 1; p <= _protein[q].peptide.GetUpperBound(0); p++)
                {
                    double Xp = _protein[q].peptide[p].Xp;
                    int npq = _protein[q].peptide[p].validScans;
                    double Wp = _protein[q].peptide[p].Wp;
    
                    //Print the scan properties
                    for (int s = 1; s <= _protein[q].peptide[p].scan.GetUpperBound(0); s++)
                    {
                        int row = _protein[q].peptide[p].scan[s].row;

                        double Xs = _protein[q].peptide[p].scan[s].Xs;
                        double Ws = _protein[q].peptide[p].scan[s].Ws;

                        try
                        {
                            _protein[q].peptide[p].scan[s].Zs = (float)(Math.Sqrt(Ws * npq / (npq - 1)) * (Xs - Xp));
                        }
                        catch 
                        {
                            _protein[q].peptide[p].scan[s].Zs = float.NaN;
                        }
                    }

                    try
                    {
                        _protein[q].peptide[p].Zp = (float)(Math.Sqrt(Wp * nq / (nq - 1)) * (Xp - Xq));
                    }
                    catch 
                    {
                        _protein[q].peptide[p].Zp = float.NaN;
                    }
                }

                //Print the protein properties


                try
                {
                    _protein[q].Zq = (float)((_protein[q].Xq - X) * Math.Sqrt(_protein[q].Wq));
                }
                catch
                {
                    _protein[q].Zq = float.NaN;
                }

            }

            ArrayList ranking_s = new ArrayList();
            ArrayList ranking_p = new ArrayList();
            ArrayList ranking_q = new ArrayList();

            for (int q = 1; q <= _protein.GetUpperBound(0); q++)
            { 
                for (int p = 1; p <= _protein[q].peptide.GetUpperBound(0); p++)
                {
                    
                    //Check for valid scans (peptides with more than one valid scan) to calculate FDRs
                    if (_protein[q].peptide[p].included == 1 && _protein[q].peptide[p].validScans > 1)
                    {
                        for (int s = 1; s <= _protein[q].peptide[p].scan.GetUpperBound(0); s++)
                        {
                            if (_protein[q].peptide[p].scan[s].included == 1)
                            {
                                double pZs = pValue(_protein[q].peptide[p].scan[s].Zs, 0, 1);
                                ranking_s.Add(pZs);
                            }
                        }
                    }
                    
                    //Check for valid peptides (proteins with more than one valid peptide) to calculate FDRp
                    if (_protein[q].peptide[p].used > 0 && _protein[q].validPeptides>1)
                    {
                        double pZp = pValue(_protein[q].peptide[p].Zp, 0, 1);
                        ranking_p.Add(pZp);
                    }

                    
                }
                
                //Adding up proteins to calculate FDRq
                if (_protein[q].used > 0)
                {
                    double pZq = pValue(_protein[q].Zq, 0, 1);
                    ranking_q.Add(pZq);
                }
            }

            ranking_s.Sort();
            ranking_p.Sort();
            ranking_q.Sort();

            int NsFDR = ranking_s.Count;
            int NpFDR = ranking_p.Count;
            int NqFDR = ranking_q.Count;

            //ranking_s.Reverse(1, ranking_s.Count - 1);
            //ranking_p.Reverse(1, ranking_p.Count - 1);
            //ranking_q.Reverse(1, ranking_q.Count - 1);

            double[] rnk_s = new double[ranking_s.Count + 1];
            double[] rnk_p = new double[ranking_p.Count + 1];
            double[] rnk_q = new double[ranking_q.Count + 1];

            ranking_s.CopyTo(rnk_s, 1);
            ranking_p.CopyTo(rnk_p, 1);
            ranking_q.CopyTo(rnk_q, 1);


            for (int q = 1; q <= _protein.GetUpperBound(0); q++)
            {

                for (int p = 1; p <= _protein[q].peptide.GetUpperBound(0); p++)
                {
                    
                        for (int s = 1; s <= _protein[q].peptide[p].scan.GetUpperBound(0); s++)
                        {
                            if (_protein[q].peptide[p].validScans > 1)
                            {
                                int idx = _protein[q].peptide[p].scan[s].row;
                                double PZs = pValue(_protein[q].peptide[p].scan[s].Zs, 0, 1);
                                int rank = QuiXoT.math.Utilities.find(rnk_s, PZs);
                                float FDRs = (float)(NsFDR * PZs / (float)rank);
                                switch (_protein[q].peptide[p].scan[s].included)
                                {
                                    case 1:
                                        {
                                            _protein[q].peptide[p].scan[s].FDRs = FDRs;
                                            break;
                                        }
                                    default:
                                        {
                                            _protein[q].peptide[p].scan[s].FDRs = float.NaN;
                                            break;
                                        }
                                }
                            }
                            else 
                            {
                                _protein[q].peptide[p].scan[s].FDRs = 1;
                            }
                        }

                    
                    float FDRp = 1;
                    
                    if (_protein[q].validPeptides > 1)                    
                    {
                        double PZp = pValue(_protein[q].peptide[p].Zp, 0, 1);
                        FDRp = (float)(NpFDR * PZp / QuiXoT.math.Utilities.find(rnk_p, PZp));                            
                    }
                        
                    _protein[q].peptide[p].FDRp = FDRp;
                        
                        
                }

                double pZq = pValue(_protein[q].Zq,0,1);
                float FDRq = (float)(NqFDR * pZq / QuiXoT.math.Utilities.find(rnk_q, pZq));

                _protein[q].FDRq = FDRq;

            }

            return _protein;
        }



        private static DataView writeInDataView(    DataView _dv,
                                                    Q_protein[] _protein,
                                                    statOptionsStrt options)
        {
            for (int q = 1; q <= _protein.GetUpperBound(0); q++)
            {
                //Protein properties
                float Zq = _protein[q].Zq;
                double Xq = _protein[q].Xq;
                double Wq = _protein[q].Wq;
                int nq = _protein[q].validPeptides;
                double SD_Xq = _protein[q].SD_Xq;
                double Pq = _protein[q].Pq;
                float FDRq = _protein[q].FDRq;
                
                for (int p = 1; p <= _protein[q].peptide.GetUpperBound(0); p++)
                {
                    //Peptide properties
                    double Xp_noCorrP = _protein[q].peptide[p].Xp_noCorrP;
                    double Xp = _protein[q].peptide[p].Xp;
                    double Wp = _protein[q].peptide[p].Wp;
                    int npq = _protein[q].peptide[p].validScans;
                    double SD_Xp = _protein[q].peptide[p].SD_Xp;
                    // double Ppq = _protein[q].peptide[p].Ppq;
                    float FDRp = _protein[q].peptide[p].FDRp;
                    int methionines = _protein[q].peptide[p].methionines;
                    double Zp = _protein[q].peptide[p].Zp;
                    
                    //Print the scan properties
                    for (int s = 1; s <= _protein[q].peptide[p].scan.GetUpperBound(0); s++)
                    {
                        //Scan properties
                        double Xs_noCorrP = _protein[q].peptide[p].scan[s].Xs_noCorrP;
                        double Xs = _protein[q].peptide[p].scan[s].Xs;
                        double Ws = _protein[q].peptide[p].scan[s].Ws;
                        double totSumSquares = _protein[q].peptide[p].scan[s].totSumSquares;
                        double SD_Xs = _protein[q].peptide[p].scan[s].SD_Xs;
                        double q_SQwindowRight = _protein[q].peptide[p].scan[s].q_SQwindowRight;
                        double q_SQwindowLeft = _protein[q].peptide[p].scan[s].q_SQwindowLeft;
                        double q_SQtotal = _protein[q].peptide[p].scan[s].q_SQtotal;
                        double q_SQPeptide = _protein[q].peptide[p].scan[s].q_SQPeptide;
                        float Zs = _protein[q].peptide[p].scan[s].Zs;
                        float FDRs = _protein[q].peptide[p].scan[s].FDRs;
                        double Vs = _protein[q].peptide[p].scan[s].Vs;

                        int row = _protein[q].peptide[p].scan[s].row;
                        string tmpExclud = _dv[row].Row["st_excluded"].ToString();
                        tmpExclud += _protein[q].peptide[p].scan[s].excludedReasons;
                        _dv[row].Row["st_excluded"] = tmpExclud;
                        _dv[row].Row["s_index"] = s;
                        _dv[row].Row["p_index"] = p;
                        _dv[row].Row["q_index"] = q;
                        _dv[row].Row["Xs"] = Xs;
                        _dv[row].Row["SD_Xs"] = SD_Xs;
                        _dv[row].Row["Ws"] = Ws;
                        //_dv[row].Row["Psp"] = Math.Log10(Psp);
                        _dv[row].Row["Zs"] = Zs;  
                        _dv[row].Row["Vs"] = Vs;


                        switch (_protein[q].peptide[p].scan[s].included)
                        {
                            case 1:
                                {
                                    _dv[row].Row["FDRs"] = _protein[q].peptide[p].scan[s].FDRs;

                                    //SILAC corrections 
                                    if (options.silacCorrection)
                                    {
                                        try
                                        {
                                            _dv[s].Row["Xs_NoCorrP"] = Xs_noCorrP;
                                        }
                                        catch { }
                                    }

                                    break;
                                }
                            default:
                                {
                                    _dv[row].Row["FDRs"] = double.NaN;
                                    break;
                                }
                        }

                    }
               


                    for (int s = _protein[q].peptide[p].rowRanges.min; s <= _protein[q].peptide[p].rowRanges.max; s++)
                    {

                        _dv[s].Row["FDRp"] = FDRp;

                        switch (_protein[q].peptide[p].included)
                        {
                            case 1:
                                {

                                    _dv[s].Row["Xp"] = Xp;
                                    _dv[s].Row["SD_Xp"] = SD_Xp;
                                    _dv[s].Row["Wp"] = Wp;
                                    //_dv[s].Row["Ppq"] = Math.Log10(Ppq);
                                    _dv[s].Row["Zp"] = Zp;

                                    //SILAC corrections 
                                    if (options.silacCorrection)
                                    {
                                        try 
                                        {
                                            _dv[s].Row["Xp_NoCorrP"] = Xp_noCorrP;
                                        }
                                        catch { }
                                    }

                                    break;
                                }
                            default:
                                {
                                    _dv[s].Row["Xp"] = double.NaN;
                                    _dv[s].Row["SD_Xp"] = double.NaN;
                                    _dv[s].Row["Wp"] = double.NaN;
                                    _dv[s].Row["Zp"] = double.NaN;
                                    break;
                                }
                        }

                        if (!options.ignScans)
                        {
                            _dv[s].Row["scan_per_peptide"] =   npq;
                        }

                        _dv[s].Row["st_Meth"] = methionines;

                        string tmpExclud = _dv[s].Row["st_excluded"].ToString();
                        tmpExclud += _protein[q].peptide[p].excludedReasons;
                        _dv[s].Row["st_excluded"] = tmpExclud;

                    }
                }

                //Print the protein properties
                for (int k = _protein[q].rowRanges.min; k <= _protein[q].rowRanges.max; k++)
                {
                    if (!options.ignPeptides)
                    {
                        _dv[k].Row["pep_per_protein"] =  nq;
                    }

                    _dv[k].Row["Xq"] = Xq;
                    _dv[k].Row["Zq"] = Zq;
                    _dv[k].Row["SD_Xq"] = SD_Xq;
                    _dv[k].Row["Wq"] = Wq;
                    _dv[k].Row["FDRq"] = FDRq;

                }
            }

            return _dv;
        }

        private static Q_protein[] calAverages(double ct_K, 
                                                double sigma2S, 
                                                double sigma2P, 
                                                double sigma2Q, 
                                                Q_protein[] pr,
                                                statOptionsStrt options,                                                
                                                out double _X)
        {          
            
            
            //Experiment level
            double SumWq = 0;
            double SumWqXq = 0;

            //Protein level
            for (int q = 1; q <= pr.GetUpperBound(0); q++)
            {
                //peptide level

                pr[q].SumWp = 0;
                pr[q].SumWpXp = 0;

                for (int p = 1; p <= pr[q].peptide.GetUpperBound(0); p++)
                {
                    pr[q].peptide[p].validScans = 0;
                    pr[q].peptide[p].sumWs = 0;
                    pr[q].peptide[p].sumWsXs = 0;
                    //scan level
                    for (int s = 1; s <= pr[q].peptide[p].scan.GetUpperBound(0); s++)
                    {
                        if (pr[q].peptide[p].scan[s].included == 1)
                        {
                            //                1 
                            //Wqps = ------------------
                            //           k
                            //        ------ + sigma2S
                            //         Vqps
                            pr[q].peptide[p].scan[s].Ws = 1 / (sigma2S + (ct_K / pr[q].peptide[p].scan[s].Vs));


                            //  Sum(Wqps,s)                        
                            pr[q].peptide[p].sumWs += pr[q].peptide[p].scan[s].Ws;

                            // Sum(Wqps*Xqps,s)
                            pr[q].peptide[p].sumWsXs += pr[q].peptide[p].scan[s].Ws * pr[q].peptide[p].scan[s].Xs;

                            //           ____________________________________   
                            //           |  k
                            //SD_Xqps =  / --- + sigma2S + sigma2P + sigma2Q
                            //         _/  Vs 
                            pr[q].peptide[p].scan[s].SD_Xs = Math.Sqrt((ct_K / pr[q].peptide[p].scan[s].Vs) + sigma2S + sigma2P + sigma2Q);

                            pr[q].peptide[p].validScans ++;
                        }

                    }

                    //                  1 
                    //Wqp = ------------------------
                    //           1
                    //        ----------- + sigma2P
                    //        Sum(Wqps,s)
                    //Advanced options: when "ignore scans" is selected, Wp is already readen from the datagrid
                    if (!options.ignScans)
                    {
                        if (pr[q].peptide[p].sumWs > 0)
                        {
                            pr[q].peptide[p].Wp = 1 / (sigma2P + (1 / pr[q].peptide[p].sumWs));
                        }
                        else
                        {
                            pr[q].peptide[p].Wp = 0;
                        }
                    }
                    else 
                    {
                        //older peptide weights must be transformed according to new peptide variance (if new)
                        if (pr[q].peptide[p].Wp > 0)
                        {
                            double old_wp =pr[q].peptide[p].Wp;
                            double delta_s2P = sigma2P - options.ignScans_s2p;
                            double transf_wp= 1/(delta_s2P +1/old_wp);

                            pr[q].peptide[p].Wp = transf_wp;

                        }
                    }
                  
                    //         ________________________________   
                    //         |     1
                    //SD_Xqp = / --------- + sigma2P + sigma2Q
                    //       _/  Sum(Ws,s) 
                    //Advanced options: when "ignore scans" is selected, Wp is already readen from the datagrid
                    if (!options.ignScans)
                    {
                        pr[q].peptide[p].SD_Xp = Math.Sqrt(sigma2P + sigma2Q + 1 / pr[q].peptide[p].sumWs);
                    }
                    else 
                    {
                        pr[q].peptide[p].SD_Xp = Math.Sqrt(sigma2Q + 1 / pr[q].peptide[p].Wp);
                    }

                    //        Sum(Wqps*Xqps,s)
                    //Xqp = -------------------
                    //          Sum(Wqps,s)
                    //Advanced options: when "ignore scans" is selected, Wp is already readen from the datagrid
                    if (!options.ignScans)
                    {
                        if (pr[q].peptide[p].sumWs > 0)
                        {
                            pr[q].peptide[p].Xp = pr[q].peptide[p].sumWsXs / pr[q].peptide[p].sumWs;
                        }
                        else
                        {
                            pr[q].peptide[p].Xp = 0;
                        }
                    }

                    //Sum(Wqp,p)
                    if (pr[q].peptide[p].included == 1)
                    {
                        pr[q].SumWp += pr[q].peptide[p].Wp;

                        //Sum(Wqp*Xqp)
                        pr[q].SumWpXp += pr[q].peptide[p].Wp * pr[q].peptide[p].Xp;
                    }
                }

                //                1 
                //Wq = ------------------------
                //           1
                //        ---------- + sigma2Q
                //        Sum(Wqp,p)
                //Advanced options: when "ignore peptides" is selected, Wq is already readen from the datagrid
                if (!options.ignPeptides)
                {
                    if (pr[q].SumWp > 0)
                    {
                        pr[q].Wq = 1 / (sigma2Q + (1 / pr[q].SumWp));
                    }
                    else
                    {
                        pr[q].Wq = 0;
                    }
                }
                //         ______________________
                //         |     1
                //SD_Xq =  / ---------- + sigma2Q
                //       _/  Sum(Wqp,p) 
                //Advanced options: when "ignore peptides" is selected, Wq is already readen from the datagrid
                if (!options.ignPeptides)
                {
                    pr[q].SD_Xq = Math.Sqrt(sigma2Q + (1 / pr[q].SumWp));
                }
                else 
                {
                    pr[q].SD_Xq = Math.Sqrt(1 / pr[q].Wq);
                }

                //       Sum(Wqp*Xqp,p)
                //Xq = -------------------
                //       Sum(Wqp,p)
                //Advanced options: when "ignore peptides" is selected, Xq is already readen from the datagrid
                if (!options.ignPeptides)
                {
                    if (pr[q].SumWp > 0)
                    {
                        pr[q].Xq = pr[q].SumWpXp / pr[q].SumWp;
                    }
                    else
                    {
                        pr[q].Xq = 0;
                    }
                }               

                // Sum(Wq,q)
                SumWq += pr[q].Wq;

                // Sum(Wq*Xq,q)
                SumWqXq += pr[q].Wq * pr[q].Xq;

            }

            //      Sum(Wq*Xq,q)
            //X = ----------------
            //       Sum(Wq,q)
            _X = SumWqXq / SumWq;

            return pr;
        }



        private static double calX( Q_protein[] pr,
                                    double FDRqthreshold)
        {

            double X=0;
            //Experiment level
            double SumWq = 0;
            double SumWqXq = 0;

            //Protein level
            for (int q = 1; q <= pr.GetUpperBound(0); q++)
            {
                if (pr[q].included == 1)
                {
                    if (pr[q].FDRq >= FDRqthreshold)
                    {
                        // Sum(Wq,q)
                        SumWq += pr[q].Wq;

                        // Sum(Wq*Xq,q)
                        SumWqXq += pr[q].Wq * pr[q].Xq;


                    }
                }
            }

            //      Sum(Wq*Xq,q)
            //X = ----------------
            //       Sum(Wq,q)
            X = SumWqXq / SumWq;

            return X;
        }



        public static DataView choosePreferentRedundance(DataView dv, string sPref,bool onlyFilteredData)
        {
            string filter = dv.Sort;

            dv.Sort = "Index";

            switch (onlyFilteredData)
            {
                case true:
                    #region filtered
                    for (int i = 0; i < dv.Count; i++)
                    {

                        int iReds = 0;
                        try
                        {
                            iReds = int.Parse(dv[i].Row["Proteinswithpeptide"].ToString());
                        }
                        catch
                        {
                            // this catch is needed, because some schemas have no tag such as "Proteinswithpeptide"
                            iReds = 1;
                        }

                        if (!dv[i].Row["FASTAProteinDescription"].ToString().Contains(sPref) && iReds > 0)
                        {

                            System.Data.DataRow[] reddr = dv[i].Row.GetChildRows("peptide_match_Redundances");
                            System.Data.DataRow[] reddr2 = reddr[0].GetChildRows("Redundances_Red");

                            foreach (DataRow drInReds in reddr2)
                            {
                                if (drInReds["FASTAProteinDescription"].ToString().Contains(sPref))
                                {
                                    string oldFASTA = dv[i].Row["FASTAProteinDescription"].ToString();
                                    string newFASTA = drInReds["FASTAProteinDescription"].ToString();

                                    //swap
                                    drInReds["FASTAProteinDescription"] = oldFASTA;
                                    drInReds["FASTAIndex"] = 0;


                                    dv[i].Row["FASTAProteinDescription"] = newFASTA;
                                    if (newFASTA.Length >= 31)
                                    {
                                        dv[i].Row["FASTAshort"] = newFASTA.Substring(0, 30);
                                    }
                                    else
                                    {
                                        dv[i].Row["FASTAshort"] = newFASTA.Substring(0, newFASTA.Length - 1);
                                    }


                                    break;
                                }
                            }

                        }
                    }

                    #endregion
                break;
                case false:
                     #region not filtered
                foreach (DataRow dr in dv.Table.Rows)
                {
                    int iReds = 0;
                    try
                    {
                        iReds = int.Parse(dr["Proteinswithpeptide"].ToString());
                    }
                    catch
                    {
                        // this catch is needed, because some schemas have no tag such as "Proteinswithpeptide"
                        iReds = 1;
                    }

                    if (!dr["FASTAProteinDescription"].ToString().Contains(sPref) && iReds > 0)
                    {
                        try
                        {
                            System.Data.DataRow[] reddr = dr.GetChildRows("peptide_match_Redundances");
                            System.Data.DataRow[] reddr2 = reddr[0].GetChildRows("Redundances_Red");

                            foreach (DataRow drInReds in reddr2)
                            {
                                if (drInReds["FASTAProteinDescription"].ToString().Contains(sPref))
                                {
                                    string oldFASTA = dr["FASTAProteinDescription"].ToString();
                                    string newFASTA = drInReds["FASTAProteinDescription"].ToString();

                                    //swap
                                    drInReds["FASTAProteinDescription"] = oldFASTA;
                                    drInReds["FASTAIndex"] = 0;


                                    dr["FASTAProteinDescription"] = newFASTA;
                                    if (newFASTA.Length >= 31)
                                    {
                                        dr["FASTAshort"] = newFASTA.Substring(0, 30);
                                    }
                                    else
                                    {
                                        dr["FASTAshort"] = newFASTA.Substring(0, newFASTA.Length - 1);
                                    }


                                    break;
                                }
                            }
                        }
                        catch { }

                    }


                }
                #endregion
                break;
            }


            return dv;

        }

        public static DataView DeployRedundances(DataView dv)
        {

            string filter = dv.Sort;

            dv.Sort = "peptide_match_id";

            //Console.WriteLine(dv.AllowNew.ToString());
            int initialTotPepMatches = dv.Count;
            for (int iRows = 0; iRows < initialTotPepMatches; iRows++)
            {
                int iReds = 0;

                System.Data.DataRow[] dr = dv[iRows].Row.GetChildRows("peptide_match_Redundances");
                System.Data.DataRow[] dr2 = null;
                if (dr.Length > 0)
                {
                    dr2 = dr[0].GetChildRows("Redundances_Red");
                    iReds = dr2.Length;
                }

                dv[iRows].Row["Proteinswithpeptide"] = iReds;

                //try
                //{
                //    iReds = int.Parse(dv[iRows].Row["Proteinswithpeptide"].ToString());
                //}
                //catch
                //{
                //    // this catch is needed, because some schemas have no tag such as "Proteinswithpeptide"
                //    iReds = 1;
                //}

                if (iReds > 0 && !dv[iRows].Row["dp_deployment"].ToString().Contains("deploy"))
                {
                    if (dv.AllowNew.ToString() == bool.TrueString)
                    {

                        //dv[iRows].Row["Label5"] += "  deployed";
                        dv[iRows].Row["dp_deployment"] = "deployed";

                        for (int k = 1; k <= iReds; k++)
                        {
                            DataRowView drv = dv.AddNew();
                            
                            drv = dv[iRows];

                            for (int j = 0; j < drv.Row.ItemArray.GetUpperBound(0) - 2; j++)
                            {
                                dv[dv.Count - 1].Row[j] = drv.Row[j];
                            }

                            dv[dv.Count - 1].Row["FASTAProteinDescription"] = dr2[k - 1].ItemArray[1];
                            dv[dv.Count - 1].Row["dp_deployment"] = "product of a deployment";
                            
                            drv.EndEdit();
                        }                      

                        
                    }
                }
                    
            }

            dv.Sort = filter;
            MessageBox.Show("Redundances deployed:\n" +
                "Original number of peptide matches: " + initialTotPepMatches + "\n" +
                "Final number of peptide matches: " + dv.Count);

            return dv;
        }


        private static Array ReDimPreserve(Array input, int size) 
        {
            Array result=(Array)Activator.CreateInstance(input.GetType(), new object[] {size});
            Array.Copy(input, result, Math.Min(input.Length, result.Length));
            return result;
        }


        private static int countProteins(DataView dv,out rowRangesStrt[] proteinRange)
        {

            dv.Sort = "FASTAshort, protLabel, eq_Sequence, peptLabel";

            if (dv.Count == 0)
            {
                proteinRange = null;
                return 0;
            }

            int proteins = 1;
           
            for (int iRows = 0; iRows < dv.Count-1; iRows++)
            {

                string sProtein1 = dv[iRows].Row["FASTAShort"].ToString() + dv[iRows].Row["protLabel"].ToString();
                string sProtein2 = dv[iRows+1].Row["FASTAShort"].ToString() + dv[iRows+1].Row["protLabel"].ToString();

                if (sProtein1 != sProtein2)
                {
                    proteins++;
                }
                
            }
            //for the last scan in the list

            if (dv.Count >= 2)
            {
                string sProt1 = dv[dv.Count - 1].Row["FASTAShort"].ToString() + dv[dv.Count - 1].Row["protLabel"].ToString();
                string sProt2 = dv[dv.Count - 2].Row["FASTAShort"].ToString() + dv[dv.Count - 2].Row["protLabel"].ToString();
                if (sProt1 != sProt2)
                {
                    //proteins++;
                }
            }

            proteinRange = new rowRangesStrt[proteins+1];


            int iProt = 1;
            proteinRange[1].min = 0;
            for (int iRows = 0; iRows < dv.Count-1; iRows++)
            {

                string sProtein1 = dv[iRows].Row["FASTAShort"].ToString() + dv[iRows].Row["protLabel"].ToString();
                string sProtein2 = dv[iRows+1].Row["FASTAShort"].ToString() + dv[iRows+1].Row["protLabel"].ToString();

                if (sProtein1 != sProtein2)
                {
                    proteinRange[iProt].max = iRows;

                    if (iProt < proteins)
                    {
                        proteinRange[iProt + 1].min = iRows+1;
                    }
                    
                    iProt++;

                }
            }
            proteinRange[proteins].max = dv.Count-1;
            
            return proteins;
 
        }

        private static int countPeptides(DataView dv, rowRangesStrt range,out rowRangesStrt[] peptideRange)
        {
            int numPeptides=1;
            string sPeptide1;
            string sPeptide2;

            for (int i = range.min; i < range.max; i++)
            {
                sPeptide1 = dv[i].Row["eq_Sequence"].ToString() + dv[i].Row["peptLabel"].ToString();
                sPeptide2 = dv[i+1].Row["eq_Sequence"].ToString() + dv[i+1].Row["peptLabel"].ToString();

                if (sPeptide1 != sPeptide2)
                {
                    numPeptides++;
                }

            }
            //for the last scan in the list
            /*if (range.min < range.max)
            {
                sPeptide1 = dv[range.max].Row["eq_Sequence"].ToString() + dv[range.max].Row["peptLabel"].ToString();
                sPeptide2 = dv[range.max - 1].Row["eq_Sequence"].ToString() + dv[range.max - 1].Row["peptLabel"].ToString();
                if (sPeptide1 != sPeptide2)
                {
                    numPeptides++;
                }
            }
            */


            peptideRange = new rowRangesStrt[numPeptides + 1];

            int iPept = 1;
            peptideRange[1].min = range.min;
            for (int i = range.min; i < range.max; i++)
            {
                sPeptide1 = dv[i].Row["eq_Sequence"].ToString() + dv[i].Row["peptLabel"].ToString();
                sPeptide2 = dv[i + 1].Row["eq_Sequence"].ToString() + dv[i + 1].Row["peptLabel"].ToString();

                if (sPeptide1 != sPeptide2)
                {
                    peptideRange[iPept].max = i;

                    if (iPept < numPeptides)
                    {
                        peptideRange[iPept + 1].min = i + 1;
                    }

                    iPept++;
                }

            }
            //for the last scan of the peptide
            if (range.min < range.max)
            {
                sPeptide1 = dv[range.max].Row["eq_Sequence"].ToString() + dv[range.max].Row["peptLabel"].ToString();
                sPeptide2 = dv[range.max - 1].Row["eq_Sequence"].ToString() + dv[range.max - 1].Row["peptLabel"].ToString();
                if (sPeptide1 != sPeptide2)
                {
                    peptideRange[iPept].min = range.max;
                    peptideRange[iPept].max = range.max;
                    iPept++;
                }
            }
            else 
            {
                peptideRange[iPept].min = range.min;
                peptideRange[iPept].max = range.max;

            }
            peptideRange[peptideRange.GetUpperBound(0)].max = range.max;

            return numPeptides;
        }

        private static int detectMethionines(string sequence)
        {
            string sequenceProcessed = sequence;
            int detectionMeth = 0;
            char[] seps = new char[] { '.' };

            if (sequence.Contains("."))
            {
                string[] sSeqSplit = sequence.Split(seps);
                sequenceProcessed = sSeqSplit[1];
            }

            if (sequenceProcessed.Contains("M"))
            {
                detectionMeth = 1;

                char[] seps2 = new char[] { '*' };
                string[] sSeqOxid = sequenceProcessed.Split(seps2);

                if (sSeqOxid.Length > 1)
                {
                    detectionMeth = 2;
                }

            }


            return detectionMeth;

        }

        private static int countProlines(string sequence)
        {
            string currSequence = "";

            if (sequence.Contains("."))
            {
                char[] seps = new char[] { '.' };
                string[] sSeqSplit = sequence.Split(seps);
                currSequence = sSeqSplit[1];
            }
            else { currSequence = sequence; }


            int numProlines = 0;

            if (currSequence.Contains("P"))
            {
                while(currSequence.Contains("P"))
                {
                    numProlines++;
                    int idx = currSequence.IndexOf('P');
                    currSequence= currSequence.Remove(idx, 1);
                }
            }
            

            return numProlines;
        }

        private static bool detectCTerminal(string sequence)
        {
            string sequenceProcessed = sequence;

            if (sequence.Contains("."))
            {
                string[] sSeqSplit = sequence.Split('.');
                sequenceProcessed = sSeqSplit[1].ToString();
            }


            //check if last aa is a K
            if (sequenceProcessed.LastIndexOf('K') == sequenceProcessed.Length - 1)
                return false;
            //check if last aa is a R
            if (sequenceProcessed.LastIndexOf('R') == sequenceProcessed.Length - 1)
                return false;

            return true;

        }

        private static ArrayList detectMCleaveage(string sequence,
                                                    out bool missDetected,
                                                    short shortestSubproduct)
        {
            ArrayList tmpProducts = new ArrayList();
            string centralPart = "";
            sequence = sequence.ToUpper(); // just ni case
            bool[] isDotStyleSequence = new bool[2];
            isDotStyleSequence[0] = sequence.Substring(0, 2).Contains(".");
            isDotStyleSequence[1] = sequence.Substring(sequence.Length - 2, 2).Contains(".");
            string workingSequence = getWorkingSequence(sequence, isDotStyleSequence);

            ArrayList missedPos = new ArrayList();
            char[] cSeq = workingSequence.ToCharArray();

            missDetected = getTrypticCleavages(isDotStyleSequence, missedPos, cSeq);

            if (missDetected)
            {
                string provSeq = "";
                int firstPos = 0;
                int lastPos = 0;
                for (int i = 0; i < missedPos.Count; i++)
                {
                    if (i == 0) firstPos = 0;
                    else firstPos = (int)missedPos[i - 1] + 1;

                    lastPos = (int)missedPos[i];

                    if (isDotStyleSequence[0] || isDotStyleSequence[1])
                    {
                        if (firstPos > 0)
                        {
                            firstPos--;
                            // if is a modification symbol, then should go back two steps
                            if (cSeq[firstPos] < 65 || cSeq[firstPos] > 90) firstPos--;

                            if (firstPos < 0) firstPos = 0;
                        }
                    }

                    if (isDotStyleSequence[1] || isDotStyleSequence[0])
                    {
                        if (lastPos < cSeq.Length - 1)
                        {
                            lastPos++;
                            if (cSeq[lastPos] < 65 || cSeq[lastPos] > 90) lastPos++;
                            if (lastPos > cSeq.Length - 1) lastPos = cSeq.Length - 1;
                        }
                    }

                    provSeq = concatenateAminoacids(isDotStyleSequence, cSeq, firstPos, lastPos);

                    if (i == 0) provSeq = addDots(provSeq,
                        isDotStyleSequence[0],
                        (isDotStyleSequence[0] || isDotStyleSequence[1]),
                        ref centralPart);
                    else provSeq = addDots(provSeq,
                        (isDotStyleSequence[0] || isDotStyleSequence[1]),
                        (isDotStyleSequence[0] || isDotStyleSequence[1]),
                        ref centralPart);

                    if (centralPart.Length >= shortestSubproduct) tmpProducts.Add(new MCleavProduct(provSeq));

                    #region old buggy code
                    //firstPos = 0;
                    
                    //if (i != 0)
                    //{
                    //    firstPos = (int)missedPos[i] + 1;
                    //}

                    //lastPos = (int)missedPos[i];

                    ////if(cSeq(missedPos[i])

                    //string product = "";
                    //for (int k = firstPos; k <= lastPos + 1; k++)
                    //{
                    //    product += cSeq[k];
                    //}

                    //if (product.Length > 1)
                    //{
                    //    string Nterm = product.Substring(0, 1);
                    //    string Cterm = product.Substring(product.Length - 1, 1);
                    //    string pep = product.Substring(1, product.Length - 2);
                    //    product = Nterm + "." + pep + "." + Cterm;
                    //    tmpProducts.Add(new MCleavProduct(product));
                    //}

                    ////missedPos[i]
                    #endregion
                }
                
                //Remaining sub-peptide
                firstPos = lastPos;
                lastPos = cSeq.Length - 1;

                if ((isDotStyleSequence[0] || isDotStyleSequence[1]))
                {
                    if (firstPos > 0)
                    {
                        firstPos--;
                        // if is a modification symbol, then should go back two steps
                        if (cSeq[firstPos] < 65 || cSeq[firstPos] > 90) firstPos--;

                        if (firstPos < 0) firstPos = 0;
                    }
                }

                if (!(isDotStyleSequence[0] || isDotStyleSequence[1])) firstPos++;

                provSeq = concatenateAminoacids(isDotStyleSequence, cSeq, firstPos, lastPos);
                provSeq = addDots(provSeq,
                    (isDotStyleSequence[0] || isDotStyleSequence[1]),
                    isDotStyleSequence[1],
                    ref centralPart);

                if (centralPart.Length >= shortestSubproduct) tmpProducts.Add(new MCleavProduct(provSeq));

                #region old buggy code
                //if (lastPos > firstPos+4)
                //{
                //    string product = "";
                //    for (int k = firstPos; k <= lastPos + 1; k++)
                //    {
                //        product += cSeq[k];
                //    }
                //    string Nterm = product.Substring(0, 1);
                //    string Cterm = product.Substring(product.Length - 1, 1);
                //    string pep = product.Substring(1, product.Length - 2);
                //    product = Nterm + "." + pep + "." + Cterm;
                //    tmpProducts.Add(new MCleavProduct(product)); 
                //}
                #endregion
            }

            return tmpProducts;
        }

        private static bool getTrypticCleavages(bool[] isDotStyleSequence, ArrayList missedPos, char[] cSeq)
        {
            bool missDetected;
            int startCheck = 0;
            int endCheck = cSeq.Length - 1;

            if (isDotStyleSequence[0]) startCheck++;
            if (isDotStyleSequence[1]) endCheck--;

            missDetected = false;
            for (int i = startCheck; i < endCheck; i++)
            {
                int nextPosition = i + 1;
                if (cSeq[i + 1] > 90 || cSeq[i + 1] < 65)
                {
                    nextPosition++;
                    if (nextPosition > endCheck) break;
                }

                if ((cSeq[i] == 'K' || cSeq[i] == 'R')
                    && cSeq[nextPosition] != 'P')
                {
                    missDetected = true;
                    missedPos.Add(nextPosition - 1);
                }

            }
            return missDetected;
        }

        private static string getWorkingSequence(string sequence, bool[] isDotStyleSequence)
        {
            string workingSequence = "";

            if (isDotStyleSequence[0] || isDotStyleSequence[1])
            {
                char[] seps = new char[] { '.' };
                string[] sSeqNoDots = sequence.Split(seps, System.StringSplitOptions.None);
                string seqPlusCN = "";
                for (int i = 0; i <= sSeqNoDots.GetUpperBound(0); i++)
                {
                    seqPlusCN += sSeqNoDots[i];
                }

                workingSequence = seqPlusCN;
            }
            else
            {
                workingSequence = sequence;
            }
            return workingSequence;
        }

        private static string addDots(string provSeq, bool dotStart, bool dotEnd, ref string pep)
        {
            string Nterm;
            string Cterm;
            int pepStart = 0;
            int pepLength = provSeq.Length;

            if (dotStart)
            {
                Nterm = provSeq.Substring(0, 1);
                pepStart++;
                pepLength--;
            }
            else Nterm = "";

            if (dotEnd)
            {
                Cterm = provSeq.Substring(provSeq.Length - 1, 1);
                pepLength--;
            }
            else Cterm = "";

            pep = provSeq.Substring(pepStart, pepLength);

            if (dotStart) provSeq = string.Concat(Nterm, ".", pep);
            else provSeq = pep;

            if (dotEnd) provSeq += string.Concat(".", Cterm);

            return provSeq;
        }

        private static string concatenateAminoacids(bool[] isDotStyleSequence, char[] cSeq, int firstPos, int lastPos)
        {
            string provSeq = "";
            for (int j = firstPos; j <= lastPos; j++)
            {
                // next "if" is to avoid later R@LALALAK --> R.@LALALA.K
                // but leaving R.LALALA.K, which is what sequest leaves
                if (!(j == firstPos + 1 && (isDotStyleSequence[0] || isDotStyleSequence[1])
                    && (cSeq[j] < 65 || cSeq[j] > 90)))
                {
                    provSeq += cSeq[j];
                }
            }
            return provSeq;
        }

        private static bool detectPartialPeptide(string sequence, ArrayList subPeptides)
        {
            bool isPartialPeptide = false;

            for (int i = 0; i <= subPeptides.Count - 1; i++)
            {
                MCleavProduct subPep;
                subPep =(MCleavProduct)subPeptides[i];
                if (subPep.length > sequence.Length)
                {
                    break;
                }

                if (subPep.sequence == sequence)
                {
                    isPartialPeptide = true;
                    break;
                }

            }           

            return isPartialPeptide;            
        }

        private static bool isPartialPeptideDetected(string sequence, ArrayList partialPeptidesDetected)
        {

            bool detected = false;

            foreach (string subpeptide in partialPeptidesDetected)
            {
                string subpep = "";
                string[] subpeptide_sp = subpeptide.Split('.');
                foreach (string s in subpeptide_sp)
                {
                    if (s.Length > 2)
                    {
                        subpep = s;
                        break;
                    }
                }
                if (sequence.Contains(subpep))
                {
                    detected = true;
                }
            }

            return detected;
        }


        private static double pValue(double x, double mu, double sigma)
        {
            
            MathNet.Numerics.Distributions.NormalDistribution normDist = new MathNet.Numerics.Distributions.NormalDistribution(0, sigma);

            double p = 2 * (1 - normDist.CumulativeDistribution(Math.Abs(x - mu)));
            
            return p;
        }

    }
}
