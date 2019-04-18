using System;
using System.Collections.Generic;
using System.Text;

namespace gloCardScanning
{

    public class ClsgloScanConstants
    {
        /* Setup the license value. */

        public const string LICENSE_VALUE = "FGT97FWF8UXK7L34";//"W39FANLJ71N25WJ8" ;// "KVLLFE57M7DCDVBW";
        //Error types
        public const int LICENSE_VALID = 1;
        public const int LICENSE_EXPIRED = -20;
        public const int LICENSE_INVALID = -21;
        public const int LICENSE_DOES_NOT_MATCH_LIBRARY = -22;

        public ClsgloScanConstants()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

    public class gloCSlibconst
    {
        //Resolution types
        public const int RES_200 = 200;
        public const int RES_300 = 300;
        public const int RES_400 = 400;
        public const int RES_600 = 600;

        //Scanner types
        public const int CSSN_NONE = 0;
        public const int CSSN_600 = 1;
        public const int CSSN_800 = 2;
        public const int CSSN_800N = 3;
        public const int CSSN_1000 = 4;
        public const int CSSN_2000 = 5;
        public const int CSSN_2000N = 6;
        public const int CSSN_800E = 7;
        public const int CSSN_800EN = 8;
        public const int CSSN_3000 = 9;
        public const int CSSN_4000 = 10;
        public const int CSSN_800G = 11;
        public const int CSSN_5000 = 12;
        public const int CSSN_IDR = 13;   //snapshell
        public const int CSSN_800DX = 14;
        public const int CSSN_800DXN = 15;
        public const int CSSN_FDA = 16;   //snapshell
        public const int CSSN_TWN = 18;   //snapshell
        public const int LAST_SCANNER = CSSN_TWN;

        //Scanner color scheme types
        public const int GRAY_COLOR = 1;
        public const int BW = 2;
        public const int TRUECOLOR = 4;

        //Scanner return values
        public const int SLIB_FALSE = 0;
        public const int SLIB_TRUE = 1;

        //Scanner general error types
        public const int SLIB_ERR_NONE = 1;
        public const int SLIB_ERR_INVALID_SCANNER = -1;

        //Scanning failure definition
        public const int SLIB_ERR_SCANNER_GENERAL_FAIL = -2;
        public const int SLIB_ERR_CANCELED_BY_USER = -3;
        public const int SLIB_ERR_SCANNER_NOT_FOUND = -4;
        public const int SLIB_ERR_HARDWARE_ERROR = -5;
        public const int SLIB_ERR_PAPER_FED_ERROR = -6;
        public const int SLIB_ERR_SCANABORT = -7;
        public const int SLIB_ERR_NO_PAPER = -8;
        public const int SLIB_ERR_PAPER_JAM = -9;
        public const int SLIB_ERR_FILE_IO_ERROR = -10;
        public const int SLIB_ERR_PRINTER_PORT_USED = -11;
        public const int SLIB_ERR_OUT_OF_MEMORY = -12;

        public const int SLIB_ERR_BAD_WIDTH_PARAM = -2;
        public const int SLIB_ERR_BAD_HEIGHT_PARAM = -3;

        public const int SLIB_ERR_BAD_PARAM = -2;

        public const int SLIB_LIBRARY_ALREADY_INITIALIZED = -13;
        public const int SLIB_ERR_DRIVER_NOT_FOUND = -14;
        public const int GENERAL_ERR_PLUG_NOT_FOUND = -200;

        //Button definition for ScanShell1000
        public const int TOP_BUTTON = 1;
        public const int MIDDLE_BUTTON = 3;
        public const int BOTTOM_BUTTON = 2;

        public gloCSlibconst()
        {
            //
            // TODO: Add const intructor logic here
            //
        }
    }

    public class gloCImageConsts
    {
        public const int IMG_ERR_SUCCESS = 0;
        public const int IMG_ERR_FILE_OPEN = -100;
        public const int IMG_ERR_BAD_ANGLE_0 = -101;
        public const int IMG_ERR_BAD_ANGLE_1 = -102;
        public const int IMG_ERR_BAD_DESTINATION = -103;
        public const int IMG_ERR_FILE_SAVE_TO_FILE = -104;
        public const int IMG_ERR_FILE_SAVE_TO_CLIPBOARD = -105;
        public const int IMG_ERR_FILE_OPEN_FIRST = -106;
        public const int IMG_ERR_FILE_OPEN_SECOND = -107;
        public const int IMG_ERR_COMB_TYPE = -108;

        public const int IMG_ERR_BAD_COLOR = -130;
        public const int IMG_ERR_BAD_DPI = -131;
        public const int INVALID_INTERNAL_IMAGE = -132;


        //image saving target definition
        public const int SAVE_TO_FILE = 0;
        public const int SAVE_TO_CLIPBOARD = 1;

        //image rotation angle definitions
        public const int ANGLE_0 = 0;
        public const int ANGLE_90 = 1;
        public const int ANGLE_180 = 2;
        public const int ANGLE_270 = 3;

        //image combination options
        public const int IMAGE_COMB_VERTICAL = 0;
        public const int IMAGE_COMB_HORIZONTAL = 1;

        //image color conversion
        public const int IMAGE_SAME_COLOR = 0;
        public const int IMAGE_BW = 2;
        public const int IMAGE_GRAY_256 = 1;
        public const int IMAGE_COLOR_256 = 3;
        public const int IMAGE_COLOR_TRUE = 4;

        public gloCImageConsts()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

}
