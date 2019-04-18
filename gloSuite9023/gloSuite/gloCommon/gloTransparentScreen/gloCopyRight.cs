using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace gloTransparentScreen
{
    public static class clsgloCopyRightText
    {

        private static string CopyRightMain = null;
      
        public static string gloCopyRightMain
        {
            get
            {
                if (CopyRightMain == null)
                {
                    string filename = System.IO.Path.Combine(ExecutablePath, "CopyRightMain.Txt");
                    if (System.IO.File.Exists(filename))
                    {

                        CopyRightMain = System.IO.File.ReadAllText(filename);
                    }
                    if (CopyRightMain == null)
                    {
                        CopyRightMain = "CPT® copyright 2015 American Medical Association. All rights reserved.";
                    }
                }
                return CopyRightMain;
            }

        }

        private static string CopyRightSub = null;
        public static string gloCopyRightSub
        {
            get
            {
                if (CopyRightSub == null)
                {
                    string filename = System.IO.Path.Combine(ExecutablePath, "CopyRightSub.Txt");
                    if (System.IO.File.Exists(filename))
                    {
                        CopyRightSub = System.IO.File.ReadAllText(filename);
                    }
                    if (CopyRightSub == null)
                    {
                        CopyRightSub = "Fee schedules, relative value units, conversion factors and/or related components are not assigned by the AMA, and not part of CPT®, and the AMA is not recommending their use. The AMA does not directly or indirectly practice medicine or dispense medical services. The AMA assumes no liability for data contained or not contained herein. CPT® is a registered trademark of the American Medical Association. PDF technology in gloEMR is powered by PDFNet SDK © PDFTron™ Systems Inc., 2001-2012 and distributed by TRIARQ Health. under license. All rights reserved.";
                    }
                }
                return CopyRightSub;
            }
        }


        private static string PreRequisites = null;
        public static string gloPreRequisites
        {
            get
            {
                if (PreRequisites == null)
                {
                    string filename = System.IO.Path.Combine(ExecutablePath, "PreRequisites.Txt");
                    if (System.IO.File.Exists(filename))
                    {
                        PreRequisites = System.IO.File.ReadAllText(filename);
                    }
                    if (PreRequisites == null)
                    {
                        PreRequisites = "Dragon 10, Black Ice Printer, Alpha  II, MS Office 2007 SP2";
                    }
                }
                return PreRequisites;
            }
        }

        
        private static string executablePath = null;
        private static string ExecutablePath
        {

            get
            {
                if (executablePath == null)
                {
                    Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();

                    if (assembly != null)
                    {



                        executablePath = System.IO.Path.GetDirectoryName(assembly.Location);


                        if (executablePath == null)
                        {
                            executablePath = string.Empty;
                        }

                    }
                    else
                    {

                        executablePath = string.Empty;

                    }
                }
                return executablePath;

            }
        }
    }
}
