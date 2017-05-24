using System;
using System.Collections.Generic;
using System.Text;
using QuiXoT.math;

namespace QuiXoT.DA_Raw
{

    //OBSOLETE: the Xcalibur libraries are no more used in the QuiXoT: refer to RawToBinStack program

    /*
    public class DA_raw
    {



        public Comb.mzI[] extData;
        public string instrumentName;

        private XCALIBURFILESLib.XRaw _Raw = new XCALIBURFILESLib.XRaw();
        private XCALIBURFILESLib.XDetectorRead _Detector;
        private XCALIBURFILESLib.XSpectra _Spectra;
        
 

        /// <summary>
        /// Reads a given scan of a given raw, the structured mzI matrix extData
        /// contains the obtained data.
        /// </summary>
        /// <param name="filePath">(string) path+Rawfile</param>
        /// <param name="scannumber">(int) scan number</param>
        /// <returns>(boolean) read status</returns>
        public Boolean ReadScanRaw(string filePath, int scannumber)
        {
            //GC.KeepAlive(_Raw);

            if (filePath == null)
                return false;
            if (filePath.Length == 0)
                return false;

            try
            {
                // start Xcalibur objects
                //GC.AddMemoryPressure(300000000);

                //_Raw = new XCALIBURFILESLib.XRaw();

                int iGeneration = GC.GetGeneration(_Raw);
                int iMaxGeneration = GC.MaxGeneration;

                long iTotalMem = GC.GetTotalMemory(true);
                _Raw.Open(filePath);
                _Detector = (XCALIBURFILESLib.XDetectorRead)_Raw.get_Detector(XCALIBURFILESLib.XDetectorTypes.XMS_Device, 1);
                _Spectra = _Detector.get_Spectra(0) as XCALIBURFILESLib.XSpectra;

            }
            catch 
            {
                _Raw.Close();
                //MessageBox.Show("Could not open selected raw file: " + e.Message);
            }


            try
            {
                //Application.DoEvents();


                XCALIBURFILESLib.XSpectrumRead Xspec = _Spectra.Item((short)scannumber) as XCALIBURFILESLib.XSpectrumRead;

                
                //Get the instrument name
                XCALIBURFILESLib.XInstrument instrument = _Detector.Instrument as XCALIBURFILESLib.XInstrument;
                instrumentName = instrument.Name;
                instrument = null;

                // spectrum data
                double[,] data = Xspec.Data as double[,];

                extData = new Comb.mzI[data.GetUpperBound(1) + 1];

                for (int k = 1; k <= data.GetUpperBound(1); k++)
                {
                    extData[k].mz = data[0, k];
                    extData[k].I = data[1, k];
                }



                _Raw.Close();
                _Raw = null;
                _Detector = null;
                _Spectra = null;

                Xspec = null;
               
                GC.Collect();


                if (extData.Length == 0) // Blank spectrum
                {
                    extData = new Comb.mzI[1];
                    return false;
                }
                else
                {
                    return true;
                }



            }
            catch
            {
                _Raw.Close();
                _Raw = null;
                _Detector = null;
                _Spectra = null;
                
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.RemoveMemoryPressure(300000000);

                //MessageBox.Show("Could not open selected raw file: " + e.Message);
                //Application.DoEvents();
                return false;

            }




        }

        /// <summary>
        /// Reads a given set of selected scans of a given raw 
        /// </summary>
        /// <param name="filePath">(string) directory path of the raws</param>
        /// <param name="rawfile">(string) name of the raw to open</param>
        /// <param name="scannumber">(int[]) set of MSMS id scans to read</param>
        /// <param name="specType">type of spectrum to quantify (Full,MSMS,ZoomScan)</param>
        /// <param name="spectrumPosition">position of the spectrum to quantify(previous, same, next)</param>
        /// <returns>(Comb.mzI[][]) spectrum of each selected scan</returns>
        public Comb.mzI[][] ReadScanRaw(string filePath, string rawfile, int[] scannumber, string specType, string spectrumPosition)
        {

            int stepSearch;
   
            switch (spectrumPosition)
            {
                case "previous":
                     stepSearch = -1;
                     break;
                case "next":
                     stepSearch = 1;
                     break;
                default:
                     stepSearch = 0;
                     break;
            }
           

            if (filePath == null || filePath.Length == 0)
                return null;
            if (rawfile == null || rawfile.Length == 0)
                return null;

            Comb.mzI[][] scansRaw = new Comb.mzI[scannumber.GetUpperBound(0) + 1][];


            try
            {
                // start Xcalibur objects
                //GC.AddMemoryPressure(300000000);
                //_Raw = new XCALIBURFILESLib.XRaw();

                int iGeneration = GC.GetGeneration(_Raw);
                int iMaxGeneration = GC.MaxGeneration;

                long iTotalMem = GC.GetTotalMemory(true);
                string rawFilePath = filePath.ToString().Trim() + "\\" + rawfile.ToString().Trim();

                _Raw.Open(rawFilePath);
                _Detector = (XCALIBURFILESLib.XDetectorRead)_Raw.get_Detector(XCALIBURFILESLib.XDetectorTypes.XMS_Device, 1);
                
                _Spectra = _Detector.get_Spectra(0) as XCALIBURFILESLib.XSpectra;

                
            }
            catch
            {
                _Raw.Close();
                //MessageBox.Show("Could not open selected raw file: " + e.Message);
                return null;
            }

            XCALIBURFILESLib.XFilters _filter = _Detector.Filters as XCALIBURFILESLib.XFilters;
  

            for (int i = 0; i <= scannumber.GetUpperBound(0); i++)
            {
               
                bool spectrumFound = false;
               

                try
                {
                    short tentativeSpectrum = (short)(scannumber[i] + stepSearch);
                    short spectrumToQuantitate = 0;
 
                    if (spectrumPosition == "same")
                    {
                        spectrumFound = true;
                        spectrumToQuantitate = tentativeSpectrum;
                    }

                    while (!spectrumFound)
                    {
                        try
                        {
                            XCALIBURFILESLib.XFilter filt = (XCALIBURFILESLib.XFilter)_filter.ScanNumber(tentativeSpectrum);  //(short)scannumber[i]
                            string ff = filt.Text;

                            switch (specType)
                            {
                                case "Full":
                                    if (ff.Contains("Full") && !ff.Contains("ms2"))
                                    {
                                        spectrumFound = true;
                                        spectrumToQuantitate = tentativeSpectrum;
                                    }
                                    break;
                                case "MSMS":
                                    if (ff.Contains("ms2"))
                                    {
                                        spectrumFound = true;
                                        spectrumToQuantitate = tentativeSpectrum;
                                    }
                                    break;
                                case "ZoomScan":
                                    if (ff.Contains("Z") && !ff.Contains("Full"))
                                    {
                                        spectrumFound = true;
                                        spectrumToQuantitate = tentativeSpectrum;
                                    } 
                                    break;
                            }

                        }
                        catch
                        {
                        }

                        tentativeSpectrum = (short)(tentativeSpectrum + stepSearch);
                    }


                    XCALIBURFILESLib.XSpectrumRead Xspec = _Spectra.Item(spectrumToQuantitate) as XCALIBURFILESLib.XSpectrumRead;

                   
   
     

                    #region not-useful
                    //try
                    //{

                    //    XCALIBURFILESLib.XParentScans XparentScans = Xspec.ParentScans as XCALIBURFILESLib.XParentScans;
                    //    short prScansCount = XparentScans.Count;
                    //    short numOfSpectra = _Spectra.Count;
                    //    string[] fll = new string[_filter.Count];
                    //    for (int k=0; k<_filter.Count;k++)
                    //    {
                    //        XCALIBURFILESLib.XFilter fil= (XCALIBURFILESLib.XFilter)_filter.Item(1);
                    //        fll[k] = fil.Text;
                    //        //fil.Validate
                    //    }
                       
                    //}
                    //catch { }
                    

                    //Get the instrument name
                    //XCALIBURFILESLib.XInstrument instrument = _Detector.Instrument as XCALIBURFILESLib.XInstrument;
                    //instrumentName = instrument.Name;
                    //instrument = null;
                    // spectrum data
#endregion

                    double[,] data = Xspec.Data as double[,];

 
                    extData = new Comb.mzI[data.GetUpperBound(1) + 1];

                    for (int k = 0; k <= data.GetUpperBound(1); k++)
                    {
                        extData[k].mz = data[0, k];
                        extData[k].I = data[1, k];
                    }

                    try
                    {

                        int bound = extData.GetUpperBound(0);
                        scansRaw[i] = new Comb.mzI[extData.GetUpperBound(0)];

                        scansRaw[i] = (Comb.mzI[])extData.Clone();


                    }
                    catch 
                    {
                        //blank spectrum
                        extData = new Comb.mzI[1];
                        scansRaw[i] = (Comb.mzI[])extData.Clone();
                        //return null;
              
                    }




                }
                catch
                {
                    _Raw.Close();
                    
                    _Detector = null;
                    _Spectra = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    //MessageBox.Show("Could not open selected raw file: " + e.Message);
                    //Application.DoEvents();
                    return null;

                }



            }

            _Raw.Close();

            GC.Collect();
            return scansRaw;

        }


    }

    */
}
