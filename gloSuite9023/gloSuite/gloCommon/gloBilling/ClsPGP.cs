using System;
using System.Collections.Generic;
using System.Text;
using SBPGPKeys;
using SBStringList;
using SBPGPMD;
using SBPGPStreams;
using SBPGP;
using SBPGPEntities;
using System.IO;
using SBUtils;
using System.Windows.Forms;

namespace gloBilling
{
    public class gloPMPGPEncryption
    {

        #region " Public & Private Variables "

        private SBPGP.TElPGPWriter pgpWriter;
        private SBPGPKeys.TElPGPKeyring keyring;
        private SBPGPKeys.TElPGPKeyring pubKeyring;
        private SBPGPKeys.TElPGPKeyring secKeyring;

        #endregion " Public & Private Variables "

        #region "Constructor & Distructor"


        public gloPMPGPEncryption()
        {

        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~gloPMPGPEncryption()
        {
            Dispose(false);
        }

        #endregion

        public bool EncryptFile(string SourceFile, out string DestFile)
        {
            bool _isFileEncrypted = false;
            DestFile = "";
            System.IO.FileStream inF, outF;
            System.IO.FileInfo info;


            if (File.Exists(SourceFile) == false)
            { return _isFileEncrypted; }

            SetParameters();
            gloPMPGPLicense.Intialize();

            //Select & load public & secret keys in keyring
            if (File.Exists(gloPMPGPParameters.gateway_PGP_PublicKeyFile_Path) && File.Exists(gloPMPGPParameters.glo_PGP_SecretKeyFile_Path))
            {
                try
                {
                    keyring = new TElPGPKeyring();
                    keyring.Load(gloPMPGPParameters.gateway_PGP_PublicKeyFile_Path, gloPMPGPParameters.glo_PGP_SecretKeyFile_Path, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load keyring: " + ex.Message, "Keyring error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return _isFileEncrypted;
                }
            }

            pgpWriter = new TElPGPWriter();
            pgpWriter.Armor = true;
            pgpWriter.ArmorHeaders.Clear();
            pgpWriter.ArmorHeaders.Add("Version: EldoS OpenPGPBlackbox (.NET edition)");
            pgpWriter.ArmorBoundary = "PGP MESSAGE";
            pgpWriter.Compress = gloPMPGPParameters.PGP_Writer_FCompress; //cbCompress.Checked;

            pgpWriter.EncryptingKeys = new TElPGPKeyring();
            pgpWriter.EncryptingKeys.AddPublicKey(((SBPGPKeys.TElPGPPublicKey)keyring.get_PublicKeys(1)));   // pubKeyring;

            pgpWriter.SigningKeys = new TElPGPKeyring();
            pgpWriter.SigningKeys.AddSecretKey(((SBPGPKeys.TElPGPSecretKey)keyring.get_SecretKeys(0)));  //secKeyring;

            pgpWriter.EncryptionType = gloPMPGPParameters.PGP_Writer_FEncryptionType; //  SBPGP.TSBPGPEncryptionType.etBoth;
            info = new System.IO.FileInfo(SourceFile);
            pgpWriter.Filename = info.Name;
            pgpWriter.InputIsText = gloPMPGPParameters.PGP_Writer_FInputIsText;  //cbTextInput.Checked;
            pgpWriter.Passphrases.Clear();
            pgpWriter.Passphrases.Add(gloPMPGPParameters.PGP_Writer_FOnKeyPassphrase);  //(tbPassphrase.Text);
            pgpWriter.Protection = gloPMPGPParameters.PGP_Writer_FProtection;  //SBPGPConstants.TSBPGPProtectionType.ptLow;
            pgpWriter.SignBufferingMethod = gloPMPGPParameters.PGP_Writer_FSignBufferingMethod; //SBPGP.TSBPGPSignBufferingMethod.sbmRewind;
            pgpWriter.SymmetricKeyAlgorithm = gloPMPGPParameters.PGP_Writer_FSymmetricKeyAlgorithm_SBPGPConstants; // SBPGPConstants.Unit.SB_PGP_ALGORITHM_SK_CAST5;
            pgpWriter.HashAlgorithm = gloPMPGPParameters.PGP_Writer_FHashAlgorithm_SBPGPConstants;  // SBPGPConstants.Unit.SB_PGP_ALGORITHM_MD_MD5;
            pgpWriter.Timestamp = DateTime.Now;
            pgpWriter.UseNewFeatures = gloPMPGPParameters.PGP_Writer_FUseNewFeatures;
            pgpWriter.UseOldPackets = false;

            inF = new System.IO.FileStream(SourceFile, FileMode.Open);
            try
            {
                DestFile = SourceFile + ".pgp";
                outF = new System.IO.FileStream(DestFile, FileMode.Create);
                try
                {
                    if (gloPMPGPParameters.PGP_Writer_Operation_Encrypt &&
                        gloPMPGPParameters.PGP_Writer_Operation_Sign &&
                        gloPMPGPParameters.PGP_Writer_FInputIsText)//if ((!cbEncrypt.Checked) && (cbSign.Checked) && (cbTextInput.Checked))
                    {
                        pgpWriter.ClearTextSign(inF, outF, 0);
                    }
                    else if (gloPMPGPParameters.PGP_Writer_Operation_Encrypt &&
                        gloPMPGPParameters.PGP_Writer_Operation_Sign)//else if ((cbEncrypt.Checked) && (cbSign.Checked))
                    {
                        pgpWriter.EncryptAndSign(inF, outF, 0);
                    }
                    else if (gloPMPGPParameters.PGP_Writer_Operation_Encrypt) //else if ((cbEncrypt.Checked) && (!cbSign.Checked))
                    {
                        pgpWriter.Encrypt(inF, outF, 0);
                    }
                    else
                    {
                        pgpWriter.Sign(inF, outF, false, 0);
                    }
                }
                finally
                {
                    outF.Close();
                }
            }
            finally
            {
                inF.Close();
                _isFileEncrypted = true;
            }
            return _isFileEncrypted;
        }

        private bool SetParameters()
        {
            bool _IsParametersSet = false;
            try
            {
                gloPMPGPParameters.glo_PGP_PublicKeyFile_Path = Application.StartupPath + "\\glostream inc pub.asc";
                gloPMPGPParameters.glo_PGP_SecretKeyFile_Path = Application.StartupPath + "\\glostream inc sec.asc";
                gloPMPGPParameters.gateway_PGP_PublicKeyFile_Path = Application.StartupPath + "\\GatewayEDI.asc";
                gloPMPGPParameters.PGP_Writer_FArmor = true;
                gloPMPGPParameters.PGP_Writer_FArmorBoundary = "PGP MESSAGE";
                gloPMPGPParameters.PGP_Writer_FArmorHeaders = new TElStringList();
                gloPMPGPParameters.PGP_Writer_FArmorHeaders.Add("Version: EldoS OpenPGPBlackbox (.NET edition)");
                gloPMPGPParameters.PGP_Writer_FBytesLeft = 0;
                gloPMPGPParameters.PGP_Writer_FCompress = true;
                gloPMPGPParameters.PGP_Writer_Operation_Encrypt = true;
                gloPMPGPParameters.PGP_Writer_Operation_Sign = false;
                gloPMPGPParameters.PGP_Writer_FCompressionAlgorithm = 0;
                gloPMPGPParameters.PGP_Writer_FDestroyTempStream = false;
                gloPMPGPParameters.PGP_Writer_FEncryptingKeys = null;
                gloPMPGPParameters.PGP_Writer_FEncryptionType = TSBPGPEncryptionType.etPublicKey;
                gloPMPGPParameters.PGP_Writer_FFinished = false;
                gloPMPGPParameters.PGP_Writer_FHashAlgorithm_SBPGPConstants = SBPGPConstants.Unit.SB_PGP_ALGORITHM_MD_SHA1;
                gloPMPGPParameters.PGP_Writer_FHashingPool = null;
                gloPMPGPParameters.PGP_Writer_FInputIsText = false;
                gloPMPGPParameters.PGP_Writer_FInputStream = null;
                //gloPMPGPParameters.PGP_Writer_FOnKeyPassphrase = null;
                //gloPMPGPParameters.PGP_Writer_FOnProgress = null;
                //gloPMPGPParameters.PGP_Writer_FOnTemporaryStream = null;
                gloPMPGPParameters.PGP_Writer_FOutputStream = null;
                gloPMPGPParameters.PGP_Writer_FPassphrases = null;
                gloPMPGPParameters.PGP_Writer_FProtection = SBPGPConstants.TSBPGPProtectionType.ptHigh;
                gloPMPGPParameters.PGP_Writer_FSignatures = null;
                gloPMPGPParameters.PGP_Writer_FSignBufferingMethod = TSBPGPSignBufferingMethod.sbmTemporaryStream;
                gloPMPGPParameters.PGP_Writer_FSigningKeys = null;
                gloPMPGPParameters.PGP_Writer_FStreams = null;
                gloPMPGPParameters.PGP_Writer_FSymmetricKeyAlgorithm_SBPGPConstants = SBPGPConstants.Unit.SB_PGP_ALGORITHM_SK_AES256;
                gloPMPGPParameters.PGP_Writer_FTempStream = null;
                gloPMPGPParameters.PGP_Writer_FTextCompatibilityMode = false;
                gloPMPGPParameters.PGP_Writer_FTimestamp = DateTime.Now;
                gloPMPGPParameters.PGP_Writer_FUseNewFeatures = false;
            }
            catch (Exception ex)
            {

            }
            return _IsParametersSet;
        }
    }

    public class gloPMPGPDecryption
    {
        #region "Constructor & Distructor"


        public gloPMPGPDecryption()
        {

        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~gloPMPGPDecryption()
        {
            Dispose(false);
        }

        #endregion
    }

    public static class gloPMPGPParameters
    {

        public static string glo_PGP_PublicKeyFile_Path = "";
        public static string glo_PGP_SecretKeyFile_Path = "";

        public static string gateway_PGP_PublicKeyFile_Path = "";
        //public static string gateway_PGP_SecretKeyFile_Path = "";

        public static bool PGP_Writer_FArmor = false;
        public static string PGP_Writer_FArmorBoundary = "";
        public static TElStringList PGP_Writer_FArmorHeaders = null;
        public static long PGP_Writer_FBytesLeft = 0;
        public static bool PGP_Writer_FCompress = false;
        public static bool PGP_Writer_Operation_Encrypt = false;
        public static bool PGP_Writer_Operation_Sign = false;
        public static int PGP_Writer_FCompressionAlgorithm = 0;
        public static bool PGP_Writer_FDestroyTempStream = false;
        public static TElPGPKeyring PGP_Writer_FEncryptingKeys = null;
        public static SBPGP.TSBPGPEncryptionType PGP_Writer_FEncryptionType = TSBPGPEncryptionType.etPublicKey;
        //public static string PGP_Writer_FFilename = "";
        public static bool PGP_Writer_FFinished = false;
        public static int PGP_Writer_FHashAlgorithm_SBPGPConstants = SBPGPConstants.Unit.SB_PGP_ALGORITHM_MD_SHA1;
        public static TElPGPHashingPool PGP_Writer_FHashingPool = null;
        public static bool PGP_Writer_FInputIsText = false;
        public static Stream PGP_Writer_FInputStream = null;
        public static TSBPGPKeyPassphraseEvent PGP_Writer_FOnKeyPassphrase = null;
        public static TSBPGPProgressEvent PGP_Writer_FOnProgress = null;
        public static TSBPGPTemporaryStreamEvent PGP_Writer_FOnTemporaryStream = null;
        public static Stream PGP_Writer_FOutputStream = null;
        public static TElStringList PGP_Writer_FPassphrases = null;
        public static SBPGPConstants.TSBPGPProtectionType PGP_Writer_FProtection = SBPGPConstants.TSBPGPProtectionType.ptHigh;
        public static TSBDisposableObjectList PGP_Writer_FSignatures = null;
        public static TSBPGPSignBufferingMethod PGP_Writer_FSignBufferingMethod;
        public static TElPGPKeyring PGP_Writer_FSigningKeys;
        public static TSBDisposableObjectList PGP_Writer_FStreams = null;
        //public static int PGP_Writer_FSymmetricKeyAlgorithm = 0;
        public static int PGP_Writer_FSymmetricKeyAlgorithm_SBPGPConstants = SBPGPConstants.Unit.SB_PGP_ALGORITHM_SK_AES256;
        public static Stream PGP_Writer_FTempStream = null;
        public static bool PGP_Writer_FTextCompatibilityMode = false;
        public static DateTime PGP_Writer_FTimestamp = DateTime.Now;
        public static bool PGP_Writer_FUseNewFeatures = false;

    }

    public static class gloPMPGPLicense
    {
        private static string _License = "0645C4826BDD43A40ECE12F6A3C466742F6927E98B7286DDA4816770868118A626B317E25287FDF99EC0736064C8A98197F80591522A0BB5F064F1BF04E1404D74D5B9597C258E71F3682858B39CA09679DEBEB9C617A4CAC176906421D9DD68A09B8F7D2A09A081ADA46FD2FAD2EF16E051FB3051B6D1940AE3F4F034A2E2A81B282449FFF2304C5BFB8531496531E4D6A969F39E7023FC68B940E29DBC5BC81E93E1511FD394E87030EE3732E75CFA37BA69707D499093AF50937DA3CEE4A9ECF9970602C1B5D785892A8BAAA71F79740E2DEA9D1A3B88765C78394029A07E834629E4322B34908B4B4EFBC131824A698F71BD521413F5394C09FDDC13589D";

        public static bool Intialize()
        {
            bool _isInitialized = true;

            try
            {
                SBUtils.Unit.SetLicenseKey(SBUtils.Unit.BytesOfString(_License));
            }
            catch (Exception ex)
            {
                _isInitialized = false;
            }
            return _isInitialized;

        }
    }

    class ClsPGP
    {
    }
}
