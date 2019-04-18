using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

/*
 * This is a sample implementation of the IZImage interface
 * 
 * The main purpose of the Directory Images is to list out all the image files found in the
 * designated directory, this class can have other purposes and definations included.
 * 
 * what I wish to demonistrate here is by implementing IZImage interface you can preveiw the images
 * one by one using the viewer control
 * 
 * By Samqty
 */

namespace gloScanImaging
{
    public class DirectoryImages:IZImage
    {
        #region member fields

        //path of the directory
        string m_DirectoryName;
        //number of the image files that are found
        int m_ImageFilesCount;

        int m_CurrentIndex=-1;
        string[] ImageFiles;

        #endregion

        #region Constructor

        public DirectoryImages(string str)
        {
            m_DirectoryName = str;
            ImageFiles = Directory.GetFiles(str, "*.jpg");
            m_ImageFilesCount = ImageFiles.Length;
        }

        #endregion

        #region IZImage Members

        public int ImageCount
        {
            get { return m_ImageFilesCount; }
        }

        public int CurrentIndex
        {
            get { return m_CurrentIndex; }
        }

        public System.Drawing.Image GetNextImage()
        {
            m_CurrentIndex++;
            if (m_CurrentIndex >= m_ImageFilesCount)
                throw new Exception("No More Images");
            return System.Drawing.Image.FromFile(ImageFiles[m_CurrentIndex]);
        }

        public System.Drawing.Image GetPreviousImage()
        {
            m_CurrentIndex--;
            if (m_CurrentIndex <= 0)
                throw new Exception("No More Images");
            return System.Drawing.Image.FromFile(ImageFiles[m_CurrentIndex]);
        }

        #endregion
    }
}
