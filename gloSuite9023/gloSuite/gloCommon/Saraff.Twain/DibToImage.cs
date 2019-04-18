/* ���� ���� �������� ������ ���������� Saraff.Twain.NET
 * � SARAFF SOFTWARE (����������� ������), 2011.
 * Saraff.Twain.NET - ��������� ���������: �� ������ ������������������ �� �/���
 * �������� �� �� �������� ������� ����������� ������������ �������� GNU � ��� ����,
 * � ����� ��� ���� ������������ ������ ���������� ������������ �����������;
 * ���� ������ 3 ��������, ���� (�� ������ ������) ����� ����� �������
 * ������.
 * Saraff.Twain.NET ���������������� � �������, ��� ��� ����� ��������,
 * �� ���� ������ ��������; ���� ��� ������� �������� ��������� ����
 * ��� ����������� ��� ������������ �����. ��������� ��. � ������� �����������
 * ������������ �������� GNU.
 * �� ������ ���� �������� ����� ������� ����������� ������������ �������� GNU
 * ������ � ���� ����������. ���� ��� �� ���, ��.
 * <http://www.gnu.org/licenses/>.)
 * 
 * This file is part of Saraff.Twain.NET.
 * � SARAFF SOFTWARE (Kirnazhytski Andrei), 2011.
 * Saraff.Twain.NET is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * Saraff.Twain.NET is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 * You should have received a copy of the GNU Lesser General Public License
 * along with Saraff.Twain.NET. If not, see <http://www.gnu.org/licenses/>.
 * 
 * PLEASE SEND EMAIL TO:  twain@saraff.ru.
 */
using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Collections.Generic;


namespace Saraff.Twain
{

    internal sealed class DibToImage
    {
        private const int BufferSize = 256 * 1024; //256K

        public static Stream WithStream(IntPtr dibPtr, IStreamProvider provider)
        {
            Stream _stream = provider != null ? provider.GetStream() : new MemoryStream();
            BinaryWriter _writer = new BinaryWriter(_stream);

            BITMAPINFOHEADER _bmi = (BITMAPINFOHEADER)Marshal.PtrToStructure(dibPtr, typeof(BITMAPINFOHEADER));

            int _extra = 0;
            if (_bmi.biCompression == 0)
            {
                int _bytesPerRow = ((_bmi.biWidth * _bmi.biBitCount) >> 3);
                _extra = Math.Max(_bmi.biHeight * (_bytesPerRow + ((_bytesPerRow & 0x3) != 0 ? 4 - _bytesPerRow & 0x3 : 0)) - _bmi.biSizeImage, 0);
            }

            int _dibSize = _bmi.biSize + _bmi.biSizeImage + _extra + (_bmi.ClrUsed << 2);

            #region BITMAPFILEHEADER

            _writer.Write((ushort)0x4d42);
            _writer.Write(14 + _dibSize);
            _writer.Write(0);
            _writer.Write(14 + _bmi.biSize + (_bmi.ClrUsed << 2));

            #endregion

            #region BITMAPINFO and pixel data

            byte[] _buffer = new byte[DibToImage.BufferSize];
            for (int _offset = 0, _len = 0; _offset < _dibSize; _offset += _len)
            {
                _len = Math.Min(DibToImage.BufferSize, _dibSize - _offset);
                Marshal.Copy((IntPtr)(dibPtr.ToInt64() + _offset), _buffer, 0, _len);
                _writer.Write(_buffer, 0, _len);
            }

            #endregion
            if (_writer != null)
            {
                //_writer.Close();
                //_writer.Dispose();
                _writer = null;
            }
            _buffer = null;

            return _stream;
        }

        public static Stream WithStream(IntPtr dibPtr)
        {
            return DibToImage.WithStream(dibPtr, null);
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        private class BITMAPINFOHEADER
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;

            public int ClrUsed
            {
                get
                {
                    return this.IsRequiredCreateColorTable ? 1 << this.biBitCount : this.biClrUsed;
                }
            }

            public bool IsRequiredCreateColorTable
            {
                get
                {
                    return this.biClrUsed == 0 && this.biBitCount <= 8;
                }
            }
        }
    }

    /// <summary>
    /// Provides instances of the <see cref="System.IO.Stream"/> for data writing.
    /// </summary>
    public interface IStreamProvider
    {

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <returns>The stream.</returns>
        Stream GetStream();
    }
}
