  /*
  * http://www.gnupdf.org/Introduction_to_PDF
  * http://theinterw3bs.com/wiki/index.php?title=Analyzing_PDFs
   * Author S. Lakshmanaraj
   * 
  */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;


    namespace gloPDFWriter
    {


 
        /// <summary>
        /// Utilization for creating the pdf from the raw text, files and byte[].
        /// </summary>
        public class gloPdfWriter
        {
            //Initiation
            public float pageWidth = 842.0f;
            public float pageDepth = 1190.0f;
            public float margin = 30.0f;
            //Bug #92179: Patient form Enhancements :- Whole Question label is not reflecting In PDF Doc scan Docs
            public float orgRightMargin = 0.0f;
            public float rightMargin = 0.0f;
            public float rightMarginBold = 100.0f;
            public float leadSize = 13.0f;
            public float fontSize = 12.0f;
            public float fontSize15 = 13.0f;
            public float fontSize18 = 18.0f;
            public bool wordWrap = true;
            public bool addURIAction = true;

            
            public float textFontMultiplier = 1.0f;
            public float textKern = 0.0f;
                        
            public string textWindowsFontName = "Courier";
            public string textPdfFontName = "Courier";

            public string urlWindowsFontName = "Courier";
            public string urlPdfFontName = "Courier";
            public float urlFontMultiplier = 1.0f;
            public float urlKern = 0.0f;
            
            public string[] URLStarters = { "HTTP:", "HTTPS:", "SFTP:", "FTP:", "WWW.", "MAILTO:" };
            public string SpaceChars = ": ()\t\\/<>{}#";

            public string URLDisplayStarter = "@?@?@?";
            public string URLDisplayEnder = "?@??@?";
            public string URLLinkEnder = "?@?@?@";

            private float yPos; //y-position

            private float[] textWidth = new float[256];
            private float[] urlWidth = new float[256];
            private float[] boldfontWidth = new float[256];

            //Patient Portal
            string placeholderforBoldFont = "<!@#$%^Font-Bold!@#$%^>";
            public Boolean IsHeaderinLargeFont = false;

            //No of pages
            private int numPages = 0;
            private string strKidsPageId = "";
            //Next Object number
            private int objectID = 1;

            //Start of Page Object
            private int pageTreeID = 0;
            //  private int textfont_id = 0;
            //  private int urlfont_id = 0;

            //crossreference
            private string xRefString = "";
            private string originalLink = "";

            //private string AnnotsString = "";
            //private string AnnotObj = "";
            private List<string> arrayURLObjects = new List<string> { };
            //stream
            private int mStreamID = 0;
            private int mStreamLenID = 0;
            private long mStreamStart = 0;

            //files
            private string outputStreamPath = @"c:\temp\txtPdf.pdf";
            private FileStream outFileStream;

            //private static Regex regExHttpLinks = new Regex(@"(?<=\()\b(https?://|www\.)[-A-Za-z0-9+&@#/%?=~_()|!:,.;]*[-A-Za-z0-9+&@#/%=~_()|](?=\))|(?<=(?<wrap>[=~|_#]))\b(https?://|www\.)[-A-Za-z0-9+&@#/%?=~_()|!:,.;]*[-A-Za-z0-9+&@#/%=~_()|](?=\k<wrap>)|\b(https?://|www\.)[-A-Za-z0-9+&@#/%?=~_()|!:,.;]*[-A-Za-z0-9+&@#/%=~_()|]", RegexOptions.Compiled | RegexOptions.IgnoreCase);


            public gloPdfWriter(string path, float width, float depth, float margin, float lead, float rightmargin=0)
            {
                this.outputStreamPath = path;
                this.pageWidth = width;
                this.pageDepth = depth;
                this.margin = margin;
                this.leadSize = lead;
                //Bug #92179: Patient form Enhancements :- Whole Question label is not reflecting In PDF Doc scan Docs
                this.orgRightMargin = rightmargin;

                this.numPages = 0;
                this.objectID = 1;
            }
            /// <summary>
            /// Open File
            /// </summary>
           
            private StreamReader Open(string filePath)
            {
                StreamReader sr = null;
                try
                {
                    sr = new StreamReader(filePath);
                }
                catch (IOException ioe)
                {
                    System.Windows.Forms.MessageBox.Show(ioe.ToString());
                    return null;
                }
                return sr;

            }
            /// <summary>
            /// Open Bytes
            /// </summary>

            private StreamReader Open(byte[] fileBytes)
            {
                StreamReader sr = null;
                try
                {
                    sr = new StreamReader(new MemoryStream(fileBytes), Encoding.Default);
                }
                catch (IOException ioe)
                {
                    System.Windows.Forms.MessageBox.Show(ioe.ToString());
                    return null;
                }
                return sr;

            }
            private void Close(StreamReader sr)
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                    sr = null;
                }
                return; 

            }
            /// <summary>
            /// Write StreamReader  
            /// </summary>
            public int Write(StreamReader sr)
            {
                try
                {
                    outFileStream = new FileStream(outputStreamPath, FileMode.Create, FileAccess.Write);
                }
                catch (IOException ioewrite)
                {
                    System.Windows.Forms.MessageBox.Show(ioewrite.ToString());
                    return -1;
                }
                int catalog_id;


                string PDFHeader = "%PDF-1.7\r\n%%Creator: gloStream Inc\r\n%%Created on "+System.DateTime.UtcNow.ToString()+"\r\n";
                FileStreamWrite(PDFHeader);

                InitializeFontVectors();
                int textwidth_id = objectID++;
                StartObject(textwidth_id);
                FileStreamWrite("[");
                for (int charI = 0; charI < 256; charI++)
                {
                    float myWidth = textWidth[charI] * 1000.0f / fontSize;
                    FileStreamWrite(" " + myWidth.ToString());

                }
                FileStreamWrite(" ] endobj\r\n");
                //Increment font_id
                int textfont_id = objectID++;
                StartObject(textfont_id);

                string strTextFont = string.Format("<</Type/Font/Subtype/Type1/BaseFont/{0}/FirstChar 0/LastChar 255/Widths {1} 0 R/Encoding/WinAnsiEncoding>>\r\nendobj\r\n", textPdfFontName, textwidth_id);
                FileStreamWrite(strTextFont);

                int urlwidth_id = objectID++;
                StartObject(urlwidth_id);
                FileStreamWrite("[");
                for (int charI = 0; charI < 256; charI++)
                {
                    float myWidth = urlWidth[charI] * 1000.0f / fontSize;
                    FileStreamWrite(" " + myWidth.ToString());

                }
                FileStreamWrite(" ] endobj\r\n");

                int urlfont_id = objectID++;
                StartObject(urlfont_id);

                string strURLFont = string.Format("<</Type/Font/Subtype/Type1/BaseFont/{0}/FirstChar 0/LastChar 255/Widths {1} 0 R/Encoding/WinAnsiEncoding>>\r\nendobj\r\n", urlPdfFontName, urlwidth_id);
                FileStreamWrite(strURLFont);


                int boldfontwidth_id = objectID++;
                StartObject(boldfontwidth_id);
                FileStreamWrite("[");
                for (int charI = 0; charI < 256; charI++)
                {
                    float myWidth = boldfontWidth[charI] * 1000.0f / fontSize;
                    FileStreamWrite(" " + myWidth.ToString());

                }
                FileStreamWrite(" ] endobj\r\n");

                int BoldFont_id = objectID++;
                StartObject(BoldFont_id);

                string strBoldFont = string.Format("<</Type/Font/Subtype/Type1/BaseFont/{0}-Bold/FirstChar 0/LastChar 255/Widths {1} 0 R/Encoding/WinAnsiEncoding>>\r\nendobj\r\n", urlPdfFontName, urlwidth_id);
                FileStreamWrite(strBoldFont);


                //Get the Parent Root Tree of pageTreeID
                pageTreeID = objectID++;


                //StreamReader sr = null;
                //try
                //{
                //    sr = new StreamReader(filePath);
                //}
                //catch (IOException ioe)
                //{
                //    System.Windows.Forms.MessageBox.Show(ioe.ToString());
                //    return 0;
                //}

                //Invoke the ConvertText function

                ConvertText(sr);


                //Invoke the StartObject function
                StartObject(pageTreeID);

                //Write out page details
                string strPagesMediaBox = string.Format("<</Type/Pages/MediaBox[0 0 {0} {1}]/Count {2}/Resources<</Font <</F0 {3} 0 R/F1 {4} 0 R/F2 {5} 0 R>> >>\r\n", this.pageWidth, this.pageDepth, this.numPages, textfont_id, urlfont_id, BoldFont_id);
                FileStreamWrite(strPagesMediaBox);






                string strKids = string.Format("/Kids[\r\n{0}]\r\n", strKidsPageId);
                FileStreamWrite(strKids);

                //int nextid = 0;
                //for (int x = 1; x <= this.numPages; x++)
                //{
                //    nextid = x * 3 + pageTreeID;
                //    string strPage_id = string.Format("{0} 0 R\r\n", nextid);

                //    FileStreamWrite(strPage_id);
                //}


                ////String strKidsClose
                //string strKidsClose = ;
                //FileStreamWrite(strKidsClose);






                //String endobj
                string strEndobj = ">>\r\nendobj\r\n";
                FileStreamWrite(strEndobj);


                //Catalog ID
                catalog_id = objectID++;

                //Invoke the StartObject function
                StartObject(catalog_id);

                //String Catalog
                string strCatalog = string.Format("<</Type/Catalog/Pages {0} 0 R>>\r\nendobj\r\n", pageTreeID);
                FileStreamWrite(strCatalog);

                long start_xref = outFileStream.Length;
                //String xref
                string strXref = "xref\r\n";
                FileStreamWrite(strXref);




                //String ObjectID
                string strObjectID = string.Format("0 {0}\r\n", objectID);
                FileStreamWrite(strObjectID);

                //String ObjectID
                string str65535 = "0000000000 65535 f\r\n";
                FileStreamWrite(str65535);
                //PageID alone written at End before one which has to be shifted to Top)
                string shiftPageID = xRefString.Substring(0, pageTreeID * 10 - 10) + xRefString.Substring(xRefString.Length - 20, 10) + xRefString.Substring(pageTreeID * 10 - 10, xRefString.Length - pageTreeID * 10 - 10) + xRefString.Substring(xRefString.Length - 10, 10);
                for (int i = 0; i < objectID - 1; i++)
                {
                    string str010ld = string.Format("{0} 00000 n\r\n", shiftPageID.Substring(i * 10, 10));
                    FileStreamWrite(str010ld);
                }

                string strTrailer = string.Format("trailer\r\n<<\r\n/Size {0}\r\n/Root {1} 0 R\r\n>>\r\n", objectID, catalog_id);
                FileStreamWrite(strTrailer);

                //xref
                string strEOF = string.Format("startxref\r\n{0}\r\n%%EOF", start_xref);
                FileStreamWrite(strEOF);
               
                outFileStream.Close();
                outFileStream.Dispose();

                return 0;

            }
            /// <summary>
            /// Write Bytes
            /// </summary>
            public int Write(byte[] fileBytes)
            {
                StreamReader sr = Open(fileBytes);
                if (sr == null)  return -2;
                return Write(sr);
                
            }
            /// <summary>
            /// Write File
            /// </summary>
            public int Write(string filePath)
            {
                StreamReader sr = Open(filePath);
                if (sr == null) return -2;
                return Write(sr);
            }

            /// <summary>
            /// FileStreamWrite
            /// </summary>
            /// <param name="anyString">string</param>
            private void FileStreamWrite(string anyString)
            {
                Byte[] buffer = null;
                buffer = ASCIIEncoding.ASCII.GetBytes(anyString);
                outFileStream.Write(buffer, 0, buffer.Length);
            }

            public void InitializeFontVectors()
            {
                System.Drawing.Font myURLFont = new System.Drawing.Font(urlWindowsFontName, fontSize);
                for (int charI = 0; charI < 256; charI++)
                {
                    string myChar = ((char)charI).ToString();
                    urlWidth[charI] = (float)TextRenderer.MeasureText(myChar, myURLFont).Width * 0.4f * urlFontMultiplier + (urlKern * fontSize);
                }
                myURLFont.Dispose();
                System.Drawing.Font myTextFont = new System.Drawing.Font(textWindowsFontName, fontSize);
                for (int charI = 0; charI < 256; charI++)
                {
                    string myChar = ((char)charI).ToString();
                    textWidth[charI] = (float)TextRenderer.MeasureText(myChar, myTextFont).Width * 0.4f * textFontMultiplier + (textKern * fontSize);
                }
                myTextFont.Dispose();
                System.Drawing.Font myTextpdfFont = new System.Drawing.Font(textPdfFontName , fontSize);
                for (int charI = 0; charI < 256; charI++)
                {
                    string myChar = ((char)charI).ToString();
                    boldfontWidth[charI] = (float)TextRenderer.MeasureText(myChar, myTextpdfFont).Width * 0.4f * textFontMultiplier + (textKern * fontSize);
                }
                myTextpdfFont.Dispose();
            }
            /// <summary>
            /// ConvertText
            /// </summary>

            private void ConvertText(StreamReader sr)
            {
                string strLine = string.Empty;
              
                float maxDepth = margin;
                //Start Page
                Int16 fontNo = 0;
                float fontsize = 0;
                if (IsHeaderinLargeFont)
                {
                    fontNo = 2;
                    fontsize = fontSize18;
                    StartPage(fontNo, fontsize);
                    fontNo = 0;
                    fontsize = fontSize;
                }
                else
                {
                    fontNo = 0;
                    fontsize = fontSize;
                    StartPage(fontNo, fontsize);
                }
                
                //System.Windows.Forms.MessageBox.Show("Name: " + myTextFont.Name + " orginal Name: " + myTextFont.OriginalFontName);
                try
                {
                    while (sr.Peek() >= 0)
                    {
                        //Get one string at a time from the input text file
                        strLine = sr.ReadLine();
                        if (strLine.Contains(placeholderforBoldFont))
                        {
                            fontNo = 2;
                            fontsize = fontSize15;
                            strLine = strLine.Replace(placeholderforBoldFont, "");
                            GoToSameLine(fontNo, fontsize);
                            //Bug #92179: Patient form Enhancements :- Whole Question label is not reflecting In PDF Doc scan Docs
                            rightMargin = rightMarginBold;
                        }
                        else
                        {
                            if (fontNo == 2)
                            {
                                fontNo = 0;
                                fontsize = fontSize;
                                GoToSameLine(fontNo, fontsize);
                                //Bug #92179: Patient form Enhancements :- Whole Question label is not reflecting In PDF Doc scan Docs
                                rightMargin = orgRightMargin;
                            }
                        }
                        string upperLine = strLine.ToUpper();
                        //If yPos <= this.margin?
                        if (yPos <= maxDepth)
                        {
                            //Invoke EndPage and StartPage functions
                            EndPage();
                            StartPage(fontNo, fontsize);
                           
                        }
                        if (strLine == "" || strLine == null)
                        {
                            FileStreamWrite(@"T* ");
                        }
                        else
                        {

                            float myPos = 0;
                            bool breakfromURL = false;
                            bool firstLineURLWritten = false;
                            bool dontWriteURLinNextLine = false;
                            bool doWriteURLinNextPage = true;
                            int linkPresent = -1;
                            bool displayStarter = false;
                            int myDisplayEnder = -1;

                            while (true)
                            {
                                if (breakfromURL)
                                {
                                    linkPresent = 0;
                                }
                                else
                                {
                                    int dlinkPresent = FindURLDisplayStarter(upperLine);
                                    linkPresent = FindHyperLinkAvailable(upperLine);
                                    if (dlinkPresent != -1)
                                    {
                                            if (dlinkPresent < linkPresent)
                                            {
                                                linkPresent = dlinkPresent;
                                                displayStarter = true;
                                            }
                                       
                                    }
                                    
                                }
                                if (linkPresent != -1) //Hyperlink available
                                {
                                    int thisBreak = linkPresent;
                                    if (linkPresent > 0)
                                    {
                                        string beforeLink = strLine.Substring(0, linkPresent);
                                        if (wordWrap)
                                        {
                                            thisBreak = FindWordBreakPoint(myPos, beforeLink, textWidth, SpaceChars);
                                            beforeLink = beforeLink.Substring(0, thisBreak);
                                        }
                                        if (thisBreak != 0)
                                        {
                                            // myPos += FileStreamWriteOneByOne(outFileStream, beforeLink, myTextFont);
                                            FileStreamWrite(@"(");

                                            float myValue = FileStreamWriteWithMeasurement(beforeLink, textWidth);
                                            FileStreamWrite(@") Tj ");
                                            //double myValue = System.Windows.Forms.TextRenderer.MeasureText(beforeLink, myTextFont).Width * 1.05;

                                            myPos += Convert.ToInt32(myValue);
                                        }
                                        if (thisBreak < linkPresent)
                                        {
                                            // FileStreamWrite(outFileStream, @" T* ");
                                            GoToNextLine(fontNo,fontsize);
                                          
                                            yPos = yPos - leadSize;
                                            if (yPos <= maxDepth)
                                            {
                                                //Invoke EndPage and StartPage functions
                                                EndPage();
                                                StartPage(fontNo, fontsize);
                                              
                                            }
                                            myPos = 0;
                                        }


                                    }
                                    if (thisBreak == linkPresent)
                                    {
                                        strLine = strLine.Substring(linkPresent);
                                        upperLine = upperLine.Substring(linkPresent);
                                        if ((breakfromURL && firstLineURLWritten))
                                        {
                                            if (displayStarter)
                                            {
                                                linkPresent = myDisplayEnder;
                                            }
                                            else
                                            {
                                                linkPresent = strLine.IndexOf(" ", 1);
                                            }
                                        }
                                        else
                                        {
                                            if (displayStarter)
                                            {
                                                linkPresent = strLine.IndexOf(URLLinkEnder);
                                                if (linkPresent == -1)
                                                {
                                                    linkPresent = strLine.IndexOf(" ");
                                                }
                                                else
                                                {
                                                    linkPresent = linkPresent + URLLinkEnder.Length;
                                                }
                                            }
                                            else
                                            {
                                                linkPresent = strLine.IndexOf(" ");
                                            }
                                        }
                                        if (linkPresent == -1)
                                        {
                                            linkPresent = strLine.Length;
                                        }
                                        string myLink = strLine.Substring(0, linkPresent);
                                        thisBreak = linkPresent;
                                        if (wordWrap)
                                        {
                                            if ((breakfromURL && firstLineURLWritten))
                                            {
                                                if (displayStarter)
                                                {
                                                    thisBreak = FindWordBreakPoint(myPos, myLink.Substring(1), urlWidth, SpaceChars, addURIAction) + 1;

                                                }
                                                else
                                                {

                                                    thisBreak = FindWordBreakPoint(myPos, myLink.Substring(1), urlWidth, " ", addURIAction) + 1;
                                                }

                                            }
                                            else
                                            {
                                                if (displayStarter)
                                                {
                                                    thisBreak = FindWordBreakPoint(myPos, ExtractDisplayFromURL(myLink), urlWidth, SpaceChars);
                                                }
                                                else
                                                {
                                                    thisBreak = FindWordBreakPoint(myPos, myLink, urlWidth, " ");
                                                }
                                            }
                                            if (dontWriteURLinNextLine)
                                            {
                                                myLink = myLink.Substring(0, thisBreak);
                                            }
                                        }
                                        if (thisBreak != 0)
                                        {
                                            //int endPos = FileStreamWriteOneByOne(outFileStream, myLink, myURLFont);
                                            if (firstLineURLWritten == false)
                                            {
                                                if (displayStarter)
                                                {
                                                    originalLink = ConvertToPDFFormat(ExtractDisplayLinkFromURL(myLink));
                                                    strLine = RemoveHyperLink(strLine);
                                                    upperLine = strLine.ToUpper();
                                                    myLink = ExtractDisplayFromURL(myLink);
                                                    linkPresent = myLink.Length;
                                                    myDisplayEnder = linkPresent;
                                                }
                                                else
                                                {
                                                    originalLink = ConvertToPDFFormat(ExtractLinkFromURL(myLink));
                                                }

                                            }
                                            if ((addURIAction) && (wordWrap))
                                            {

                                                myLink = myLink.Substring(0, thisBreak);
                                                myDisplayEnder = myDisplayEnder - thisBreak;
                                            }
                                            StartURLObject(myPos);
                                            FileStreamWrite(@"(");



                                            float mydoubleValue = FileStreamWriteWithMeasurement(myLink, urlWidth);
                                            FileStreamWrite(@" ) Tj ");

                                            //         double mydoubleValue = System.Windows.Forms.TextRenderer.MeasureText(myLink, myURLFont).Width * 1.05;
                                            float endPos = Convert.ToInt32(mydoubleValue);
                                            EndURLObject(myPos, endPos);
                                            myPos += endPos;
                                            firstLineURLWritten = true;
                                        }
                                        if (thisBreak < linkPresent)
                                        {
                                            GoToNextLine(fontNo,fontsize);
                                           
                                            yPos = yPos - leadSize;
                                            if (yPos <= maxDepth)
                                            {
                                                //Invoke EndPage and StartPage functions
                                                if (doWriteURLinNextPage)
                                                {
                                                    EndPage();
                                                    StartPage(fontNo, fontsize);
                                                  
                                                }
                                            }
                                            myPos = 0;
                                            breakfromURL = true;
                                        }
                                        else
                                        {
                                            breakfromURL = false;
                                            firstLineURLWritten = false;
                                            displayStarter = false;
                                        }

                                    }
                                    strLine = strLine.Substring(thisBreak);
                                    upperLine = upperLine.Substring(thisBreak);
                                    if (breakfromURL && firstLineURLWritten)
                                    {
                                        strLine = " " + strLine;
                                        upperLine = " " + upperLine;
                                    }
                                }
                                else
                                {
                                    if ((strLine != "") && (strLine != null))
                                    {

                                        int thisBreak = strLine.Length;
                                        string wordBreak = strLine;
                                        if (wordWrap)
                                        {
                                            thisBreak = FindWordBreakPoint(myPos, strLine, textWidth, SpaceChars);
                                            wordBreak = strLine.Substring(0, thisBreak);
                                        }

                                        if (thisBreak != 0)
                                        {
                                            // myPos += FileStreamWriteOneByOne(outFileStream, beforeLink, myTextFont);
                                            FileStreamWrite(@"(");

                                            float myValue = FileStreamWriteWithMeasurement(wordBreak, textWidth);
                                            FileStreamWrite(@") Tj ");
                                            //double myValue = System.Windows.Forms.TextRenderer.MeasureText(beforeLink, myTextFont).Width * 1.05;

                                            myPos += Convert.ToInt32(myValue);
                                        }
                                        if (thisBreak < strLine.Length)
                                        {
                                            GoToNextLine(fontNo,fontsize);
                                          
                                            yPos = yPos - leadSize;
                                            if (yPos <= maxDepth)
                                            {
                                                //Invoke EndPage and StartPage functions

                                                EndPage();
                                                StartPage(fontNo, fontsize);
                                             
                                            }
                                            myPos = 0;
                                        }


                                        if (thisBreak == strLine.Length)
                                        {
                                            //double myValue = System.Windows.Forms.TextRenderer.MeasureText(strLine, myTextFont).Width * 1.05;
                                            GoToNextLine(fontNo,fontsize);

                                          
                                            break;
                                        }
                                        else
                                        {
                                            strLine = strLine.Substring(thisBreak);
                                            upperLine = upperLine.Substring(thisBreak);
                                        }

                                    }
                                    else
                                    {
                                        GoToNextLine(fontNo,fontsize);
                                     
                                        break;
                                    }
                                }
                            }//while true loop



                        }
                        //Set yPos
                        yPos -= leadSize;
                    }
                    //Close file
                    sr.Close();
                    sr.Dispose();
                    sr = null;

                    //End Page
                    EndPage();

                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                }

            }

            /// <summary>
            /// StartPage
            /// </summary>
            private void StartPage(int fontno, float font_size)
            {
                mStreamID = objectID++;
                mStreamLenID = objectID++;

                StartObject(mStreamID);
                arrayURLObjects.Clear();
                //String Length
                string strLength = string.Format("<</Length {0} 0 R>>\r\n", mStreamLenID);
                FileStreamWrite(strLength);

                //String Stream
                string strStream = "stream\r\n";
                FileStreamWrite(strStream);

                mStreamStart = outFileStream.Length;
                string fontsize = font_size.ToString("G");
                if (fontno > 0)
                    fontsize = font_size.ToString("G");
                //String BT
                string strBT = string.Format("BT\r\n/F" + fontno + " {0} Tf 0 0 0 rg\r\n", fontsize);
                FileStreamWrite(strBT);

                //Calculate yPos
                yPos = this.pageDepth - this.margin;

                //String Td
                string strTd = string.Format("{0} {1} Td\r\n", this.margin.ToString("G"), yPos.ToString("G"));
                FileStreamWrite(strTd);

                //String TL
                string strTL = string.Format("{0} TL\r\n", leadSize.ToString("G"));
                FileStreamWrite(strTL);
            }
            /// <summary>
            /// StartObject
            /// </summary>
            /// <param name="id">int</param>
            private void StartObject(int id)
            {
                if (id == int.MaxValue)
                {
                    MessageBox.Show("Can not Handle the Request due to size limit in Number of Objects");
                    return;
                }

                long pXrefs = outFileStream.Length;
                if (pXrefs >= 10000000000)
                {
                    MessageBox.Show("Can not Handle the Request due to size limit in Filesize");
                    return;
                }
                xRefString = xRefString + pXrefs.ToString("0000000000");
                //String objId
                string strobjId = string.Format("{0} 0 obj\r\n", id);
                FileStreamWrite(strobjId);



            }

            /// <summary>
            /// EndPage
            /// </summary>

            private void EndPage()
            {
                int page_id = 0;
                long stream_len = 0;




                //String ET
                string strET = " T*\r\nET\r\n";
                FileStreamWrite(strET);

                //Calculate stream length
                stream_len = outFileStream.Length - mStreamStart;

                //String Endstream
                string strEndstream = "endstream\r\nendobj\r\n";
                FileStreamWrite(strEndstream);

                //Invoke the "StartObject" function
                StartObject(mStreamLenID);

                string strEndobj = string.Format("{0}\r\nendobj\r\n", stream_len);
                FileStreamWrite(strEndobj);
                string annotString = "";
                if (arrayURLObjects.Count > 0)
                {
                    foreach (string urlObject in arrayURLObjects)
                    {
                        int annot_id = objectID++;
                        StartObject(annot_id);
                        FileStreamWrite(urlObject);
                        annotString = annotString + String.Format("{0} 0 R\r\n", annot_id);
                    }
                }
                //Invoke the "StartObject"
                page_id = objectID++;
                StartObject(page_id);

                ////String Parent
                //string strParent = string.Format("<</Type/Page/Parent {0} 0 R /Resources << /Font << /F0 {1} 0 R /F1 {2} 0 R >> >> /Contents {3} 0 R>>\r\nendobj\r\n", pageTreeID, textfont_id, urlfont_id, mStreamID);
                //FileStreamWrite(strParent);

                //String Parent
                if (arrayURLObjects.Count == 0)
                {
                    string strParent = string.Format("<</Type/Page/Parent {0} 0 R/Contents {1} 0 R>>\r\nendobj\r\n", pageTreeID, mStreamID);
                    FileStreamWrite(strParent);
                }
                else
                {
                    string strParent = string.Format("<</Type/Page/Parent {0} 0 R/Contents {1} 0 R/Annots[\r\n{2}]>>\r\nendobj\r\n", pageTreeID, mStreamID, annotString);
                    FileStreamWrite(strParent);

                }

                //Invoke the StorePage function
                StoreAndIncrementPage(page_id);
            }



            /// <summary>
            /// StoreAndIncrementPage
            /// </summary>
            /// <param name="id">int</param>
            public void StoreAndIncrementPage(int id)
            {
                strKidsPageId = strKidsPageId + string.Format("{0} 0 R\r\n", id);
                this.numPages++;

            }

            /// <summary>
            /// FindHyperLInkAvaialble
            /// </summary>
            /// <param name="strLine">string</param>

            int FindHyperLinkAvailable(string strLine)
            {
                int linkPresent = -1;
                int arrayDone = 0;
                int lastLink = linkPresent;
                string myString = strLine;
                int endPos = myString.Length;
                while (arrayDone < URLStarters.Length)
                {
                    bool foundALink = false;
                    for (int i = arrayDone; i < URLStarters.Length; i++)
                    {
                        int startPos = 0;

                        while (startPos < endPos)
                        {
                            linkPresent = myString.IndexOf(URLStarters[i], startPos);

                            if (linkPresent != -1)
                            {
                                if (linkPresent > 0)
                                {
                                    string previousChar = myString.Substring(linkPresent - 1, 1);
                                    int mySpace = SpaceChars.IndexOf(previousChar);
                                    if (mySpace != -1)
                                    {
                                        lastLink = linkPresent;
                                        endPos = linkPresent;
                                        arrayDone = i + 1;
                                        myString = myString.Substring(0, linkPresent);
                                        foundALink = true;
                                        break;
                                    }
                                    else
                                    {
                                        startPos = linkPresent + URLStarters[i].Length;
                                    }
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                            else
                            {
                                break;
                            }
                        } //While StartPos < endPos
                        if (foundALink)
                        {
                            break;
                        }
                    } //For Loop
                    if (!foundALink)
                    {
                        break;
                    }
                } //While Loop
                return lastLink;
            }
            /// <summary>
            /// FindURLDisplayStarter
            /// </summary>
            /// <param name="strLine">string</param>

            int FindURLDisplayStarter(string strLine)
            {
                return strLine.IndexOf(URLDisplayStarter);
            }

            /// <summary>
            /// FindWordBreakPoint
            /// </summary>

            private int FindWordBreakPoint(float startWidth, string anyString, float[] WidthMeasurement, string mySpaceChars, bool reduceSpace = false)
            {
                Byte[] buffer = null;
                buffer = ASCIIEncoding.ASCII.GetBytes(anyString);
                float myWidth = startWidth + margin;
                int lastSpace = 0;
                int lastLength = buffer.Length;
                if (reduceSpace)
                {
                    myWidth += WidthMeasurement[32];
                }

                for (int i = 0; i < lastLength; i++)
                {
                    int myNumber = buffer[i];
                    string myChar = ((char)myNumber).ToString();

                    int mySpace = mySpaceChars.IndexOf(myChar);
                    if (mySpace != -1)
                    {
                        lastSpace = i;
                    }
                    //if ((myChar == " ")) // || (myChar == "\\") || (myChar == "/") )
                    //{
                    //    lastSpace = i;
                    //}
                    myWidth += WidthMeasurement[myNumber];
                    //Bug #92179: Patient form Enhancements :- Whole Question label is not reflecting In PDF Doc scan Docs
                    if (myWidth > (pageWidth - rightMargin))
                    {
                        if ((lastSpace == 0) && (startWidth == 0))
                        {
                            return i;

                        }
                        else
                        {
                            return lastSpace;
                        }
                    }
                }
                return lastLength;
            }

            /// <summary>
            /// StartURLObject
            /// </summary>

            private void StartURLObject(float myPos)
            {
                string fontChange = string.Format(" T*\r\nET\r\nBT\r\n/F1 {0} Tf 0 0 1 rg\r\n", this.fontSize.ToString("G"));
                FileStreamWrite(fontChange);
                float xStartPos = this.margin + myPos;
                //  yPos -= leadSize;
                string strTd = string.Format("{0} {1} Td\r\n", xStartPos.ToString("G"), yPos.ToString("G"));
                FileStreamWrite(strTd);

                //String TL
                string strTL = string.Format("{0} TL\r\n", leadSize.ToString("G"));

                FileStreamWrite(strTL);
            }
            /// <summary>
            /// EndURLObject
            /// </summary>

            private void EndURLObject(float startPos, float endPos)
            {


                FileStreamWrite(" T*\r\n\r\nET\r\n");
                //yPos -= leadSize;


                float zPos = yPos - fontSize / 3;
                float rPos = yPos + fontSize * 2 / 3;
                float xStartPos = this.margin + startPos;
                float xEndPos = xStartPos + endPos;
                float rEndPos = xEndPos;
                if (rEndPos > pageWidth)
                {
                    rEndPos = pageWidth;
                }
                string lineDraw = string.Format("{0} {1} m {2} {3} {4} {5} re f\r\n", xStartPos.ToString("G"), zPos.ToString("G"), xStartPos.ToString("G"), zPos.ToString("G"), endPos.ToString("G"), 1);
                FileStreamWrite(lineDraw);
                string fontChange = string.Format("BT\r\n/F0 {0} Tf 0 0 0 rg\r\n", this.fontSize.ToString("G"));
                FileStreamWrite(fontChange);


                string strTd = string.Format("{0} {1} Td\r\n", xEndPos.ToString("G"), yPos.ToString("G"));
                FileStreamWrite(strTd);

                //String TL
                string strTL = string.Format("{0} TL\r\n", leadSize.ToString("G"));

                FileStreamWrite(strTL);
                if (addURIAction)
                {
                    string urlAnnotation = string.Format("<</Type/Annot/Subtype/Link/Rect[{0} {1} {2} {3}]/Border[0 0 0]/A<</Type/Action/S/URI/URI ({4}) >> >>\r\nendobj\r\n", xStartPos.ToString("G"), zPos.ToString("G"), rEndPos.ToString("G"), rPos.ToString("G"), originalLink);
                    arrayURLObjects.Add(urlAnnotation);
                }
            }
            /// <summary>
            /// GoToNextLine
            /// </summary>

            private void GoToNextLine(int fontno,float font_size)
            {

                FileStreamWrite(" T*\r\nET\r\n");
                //yPos -= leadSize;
                string fontsize = font_size.ToString("G");
                if (fontno > 0)
                    fontsize = font_size.ToString("G");
                string fontChange = string.Format("BT\r\n/F" + fontno + " {0} Tf 0 0 0 rg\r\n", fontsize);
                FileStreamWrite(fontChange);
                float zPos = yPos - leadSize;

                string strTd = string.Format("{0} {1} Td\r\n", this.margin.ToString("G"), zPos.ToString("G"));
                FileStreamWrite(strTd);

                //String TL
                string strTL = string.Format("{0} TL\r\n", leadSize.ToString("G"));

                FileStreamWrite(strTL);

            }
            private void GoToSameLine(int fontno, float font_size)
            {

                FileStreamWrite(" T*\r\nET\r\n");
                //yPos -= leadSize;
                string fontsize = font_size.ToString("G");
                if (fontno > 0)
                    fontsize = font_size.ToString("G");
                string fontChange = string.Format("BT\r\n/F" + fontno + " {0} Tf 0 0 0 rg\r\n", fontsize);
                FileStreamWrite(fontChange);
                float zPos = yPos;

                string strTd = string.Format("{0} {1} Td\r\n", this.margin.ToString("G"), zPos.ToString("G"));
                FileStreamWrite(strTd);

                //String TL
                string strTL = string.Format("{0} TL\r\n", leadSize.ToString("G"));

                FileStreamWrite(strTL);

            }
            /// <summary>
            /// ExrtractLinkFromURL
            /// </summary>
            private string ExtractLinkFromURL(string anyString)
            {

                if (!anyString.Contains("://"))
                {
                    if (!anyString.ToUpper().Contains("MAILTO:"))
                    {
                        return "http://" + anyString;
                    }
                    else
                    {
                        return anyString;
                    }
                }
                else
                {
                    return anyString;
                }
            }
            /// <summary>
            /// ExtractDisplayFromURL
            /// </summary>
            private string ExtractDisplayFromURL(string anyString)
            {
                string myString = anyString;
                int myUrlStarter = myString.IndexOf(URLDisplayEnder);
                if (myUrlStarter == -1)
                {
                    myUrlStarter = myString.IndexOf(" ");
                    if (myUrlStarter == -1)
                    {
                        myUrlStarter = myString.Length;
                    }

                }

                return (myString.Substring(URLDisplayStarter.Length, myUrlStarter - URLDisplayStarter.Length));

            }
            /// <summary>
            /// ExtractDisplayLinkFromURL
            /// </summary>
            private string ExtractDisplayLinkFromURL(string anyString)
            {
                string myString = anyString;
                int myUrlStarter = myString.IndexOf(URLDisplayEnder);
                if (myUrlStarter != -1)
                {
                    myString = myString.Substring(myUrlStarter + URLDisplayEnder.Length);
                }
                int myUrlEnder = myString.IndexOf(URLLinkEnder);
                if (myUrlEnder == -1)
                {
                    myUrlEnder = myString.IndexOf(" ");
                    if (myUrlEnder == -1)
                    {
                        myUrlEnder = myString.Length;
                    }
                }
                myString = myString.Substring(0, myUrlEnder);
                return ExtractLinkFromURL(myString);
            }
            /// <summary>
            /// RemoveHyperLink
            /// </summary>
            private string RemoveHyperLink(string anyString)
            {
                string myString = anyString;
                int myUrlStarter = myString.IndexOf(URLDisplayEnder);
                if (myUrlStarter == -1)
                {
                    return myString;
                }
                else
                {

                    int myUrlEnder = myString.IndexOf(URLLinkEnder);
                    if (myUrlEnder == -1)
                    {
                        myUrlEnder = myString.IndexOf(" ");
                        if (myUrlEnder == -1)
                        {
                            myUrlEnder = myString.Length;
                            return (myString.Substring(URLDisplayStarter.Length, myUrlStarter - URLDisplayStarter.Length));
                        }
                        else
                        {
                            return (myString.Substring(URLDisplayStarter.Length, myUrlStarter - URLDisplayStarter.Length) + myString.Substring(myUrlEnder + 1));
                        }
                    }
                    else
                    {
                        return (myString.Substring(URLDisplayStarter.Length, myUrlStarter - URLDisplayStarter.Length) + myString.Substring(myUrlEnder + URLLinkEnder.Length));
                    }
                }
            }


            /// <summary>
            /// FileStreamWriteWithMeasurement
            /// </summary>

            private string ConvertToPDFFormat(string anyString)
            {
                Byte[] buffer = null;
                buffer = ASCIIEncoding.ASCII.GetBytes(anyString);
                string thisString = "";
                for (int i = 0; i < buffer.Length; i++)
                {
                    int myNumber = buffer[i];
                    string myChar = ((char)myNumber).ToString();
                    if ((myNumber < 32) || (myNumber > 127))
                    {
                        myChar = "";
                        //Convert to Octal
                        for (int j = 0; j < 2; j++)
                        {
                            int reminder = myNumber % 8;
                            myChar = reminder.ToString() + myChar;
                            myNumber = myNumber / 8;
                        }
                        myChar = "\\" + myChar;
                    }
                    else
                    {
                        if (myChar == "(" || myChar == ")" || myChar == "\\")
                        {
                            //Append slash to inbuilt litersls
                            myChar = "\\" + myChar;
                        }
                    }
                    thisString = thisString + myChar;
                }
                return thisString;
            }

            /// <summary>
            /// FileStreamWriteWithMeasurement
            /// </summary>

            private float FileStreamWriteWithMeasurement(string anyString, float[] WidthMeasurement)
            {
                Byte[] buffer = null;
                buffer = ASCIIEncoding.ASCII.GetBytes(anyString);
                float myWidth = 0;
                for (int i = 0; i < buffer.Length; i++)
                {
                    int myNumber = buffer[i];
                    myWidth += WidthMeasurement[myNumber];
                    string myChar = ((char)myNumber).ToString();
                    if ((myNumber < 32) || (myNumber > 127))
                    {
                        myChar = "";
                        //Convert to Octal
                        for (int j = 0; j <= 2; j++)
                        {
                            int reminder = myNumber % 8;
                            myChar = reminder.ToString() + myChar;
                            myNumber = myNumber / 8;
                        }
                        myChar = "\\" + myChar;
                    }
                    else
                    {
                        if (myChar == "(" || myChar == ")" || myChar == "\\")
                        {
                            //Append slash to inbuilt litersls
                            myChar = "\\" + myChar;
                        }
                    }
                    FileStreamWrite(myChar);

                }
                return myWidth;
            }
        }//class PdfWriter


    }
