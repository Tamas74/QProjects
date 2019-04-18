/*
 * This a sample implementation of Image Viewer
 * It contains two class definations 
 *      1-PictureBoxEx
 *      2-ImageFile
 * 
 * PictureBoxEx: its acts like a picture box. its an implementation of Imageviewer control where its is
 * modified to only accept a file name and display the image much like a picture box but with the
 * enhanced capabilities
 * 
 * ImageFile: its a sample implemetation of the IZImage inorder to provide a source object
 * for the PictureBoxEx class,with an implementation provided that it accepts one file name
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace gloScanImaging
{
    /// <summary>
    /// this is a default implementation of the image viewer control
    /// that directly accepts the path of the image file
    /// and could be used for only one picture by passing the path of the image file
    /// </summary>
    public class PictureBoxEx:ImageControl
    {
        #region member fields

        string m_FileName;

        #endregion

        #region member properties

        public string FilePath
        {
            get { return m_FileName; }
            set 
            {
                m_FileName = value;
                if(!string.IsNullOrEmpty(m_FileName))
                this.SetImageSource(new ImageFile(m_FileName));
            }
        }

        #endregion

        #region Constructors

        public PictureBoxEx()
        {
            //disabel the move next and prev commands because they are not applicable in this case
            btnNext.Visible = false;
            btnPrev.Visible = false;
        }

        public PictureBoxEx(string FileName):this()
        {
            m_FileName = FileName;
            this.SetImageSource( new ImageFile(FileName));
        }

        #endregion
    }

    /// <summary>
    /// this a default implementation of the IZImage interface
    /// in order to use the image viewer to display a single file image
    /// </summary>
    public class ImageFile:IZImage
    {
        #region member fields

        //holds the file name of the image
        string m_FileName;
        //number of the image files that are found
        int m_ImageFilesCount;

        int m_CurrentIndex = 0;
        System.Drawing.Image m_Image = null;
        #endregion

        public int GetImagesCount()
        {
            if (m_Image == null) return 0;
            int nImageCount = 0;
            try
            {
                System.Guid imageType = m_Image.RawFormat.Guid;
                if ((imageType == System.Drawing.Imaging.ImageFormat.Gif.Guid) || (imageType == System.Drawing.Imaging.ImageFormat.Tiff.Guid))
                {
                    foreach (Guid guid in m_Image.FrameDimensionsList)
                    {
                        System.Drawing.Imaging.FrameDimension currentFrame = new System.Drawing.Imaging.FrameDimension(guid);
                        int noOfPages = 0;
                        try
                        {
                            noOfPages = m_Image.GetFrameCount(currentFrame);
                        }
                        catch
                        {
                        }
                        if (noOfPages == 0) noOfPages = 1;
                        nImageCount += noOfPages;
                    }
                    return nImageCount;
                }
                else
                {
                    return 1;
                }
            }
            catch
            {

            }
            return nImageCount;
        }

        public bool GoToImagesCount(int nGoToCount)
        {
            if (m_Image == null) return false;
            int nImageCount = 0;
            try
            {
                System.Guid imageType = m_Image.RawFormat.Guid;
                if ((imageType == System.Drawing.Imaging.ImageFormat.Gif.Guid) || (imageType == System.Drawing.Imaging.ImageFormat.Tiff.Guid))
                {
                    foreach (Guid guid in m_Image.FrameDimensionsList)
                    {
                        System.Drawing.Imaging.FrameDimension currentFrame = new System.Drawing.Imaging.FrameDimension(guid);
                        int noOfPages = 0;
                        try
                        {
                            noOfPages = m_Image.GetFrameCount(currentFrame);
                        }
                        catch
                        {
                        }
                        if (noOfPages == 0) noOfPages = 1;
                        if (((nImageCount + noOfPages) > nGoToCount) && (nImageCount <= nGoToCount))
                        {
                            int index = nGoToCount - nImageCount;
                            m_Image.SelectActiveFrame(currentFrame, index);
                            return true;
                        }
                        else
                        {
                            nImageCount += noOfPages;
                        }
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

            }
            return false;
        }
        #region Constructor

        public void DisposeMyLoadImg()
        {
            try
            {
                if (m_Image != null)
                {
                    m_Image.Dispose();
                    m_Image = null;
                }
            }
            catch
            {
            }
            try
            {
                if (ms != null)
                {
                    ms.Dispose();
                    ms = null;
                }
            }
            catch
            { }
        }

        private MemoryStream ms = null;
        public ImageFile(string str)
        {
            m_FileName = str;
            try
            {
                if (m_Image != null)
                {
                    m_Image.Dispose();
                    m_Image = null;
                }
            }
            catch
            {
            }
            try
            {
                if (ms != null)
                {
                    ms.Dispose();
                    ms = null;
                }
            }
            catch
            { }
            try
            {
                byte[] imgBytes = File.ReadAllBytes(m_FileName);
                ms = new MemoryStream(imgBytes);
                m_Image = System.Drawing.Image.FromStream(ms);
            }
            catch
            {
                try
                {
                    m_Image = System.Drawing.Image.FromFile(m_FileName);
                }
                catch
                {
                }
            }
            if (m_Image != null)
            {
                m_ImageFilesCount = GetImagesCount();
            }
            else
            {
                m_ImageFilesCount = 0;
            }
        }
        #endregion

        #region IZImage Members
        public int ImageCount
        {
            get 
            {
                return m_ImageFilesCount;
            }
        }
        public int CurrentIndex
        {
            get { return m_CurrentIndex; }
        }
        public System.Drawing.Image GetNextImage()
        {
            m_CurrentIndex++;
            if (m_CurrentIndex >= m_ImageFilesCount)
            {
                m_CurrentIndex = m_ImageFilesCount-1;
            }
            if (m_CurrentIndex < 0)
            {
                m_CurrentIndex = 0;
            }

            GoToImagesCount(m_CurrentIndex);
            return m_Image;
        }
        public System.Drawing.Image GetPreviousImage()
        {
            m_CurrentIndex--;
            if (m_CurrentIndex >= m_ImageFilesCount)
            {
                m_CurrentIndex = m_ImageFilesCount - 1;
            }
            if (m_CurrentIndex < 0)
            {
                m_CurrentIndex = 0;
            }
            GoToImagesCount(m_CurrentIndex);
            return m_Image;
        }
        #endregion
    }
}
