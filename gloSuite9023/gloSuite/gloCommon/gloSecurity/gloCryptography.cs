using System;
using System.Diagnostics;
using System.Text;
using System.Globalization;
using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Resources;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;
using System.Data;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace gloSecurity
{
    public static class gloEncryption
    {
        #region " Code for encrypted exe "

        private static string _applicationPath = Application.StartupPath;
        //Assembly.GetExecutingAssembly().Location;
    //    private static string _outputFileName = "";
        private static string _algorithmName = "TripleDES";
        private static string _algorithmId = "2";
        private const bool EncryptWithSelfExtractingExe = false;

        private static string Encrypt(string SourceFileName, string EncryptionKey)
        {
            System.Windows.Forms.OpenFileDialog dlgEncryptFile = new OpenFileDialog();
            string _outputEncryptedZipFileName = "";
            string _exportedFilePath = "";

            try
            {
                _exportedFilePath = SourceFileName;

                if (ValidateKey(EncryptionKey) == true)
                {

                    byte[] Key = null;
                    byte[] IV = null;
                    string algid = "0";
                    PasswordDeriveBytes pdb = new PasswordDeriveBytes(EncryptionKey, UnicodeEncoding.Unicode.GetBytes("☻æ▼☺ÆΦ²‼⌡µ≤▐≈¼╓▐┼╔╩╦Ç→£╪↨☼¥¢τ≡ß§▲φ⌐ε£⌠°ⁿ◄Ω╪Ö↔║√┐Íù¼º╣▓í¢+÷ё2ыΣ⌂τ♫"), "SHA512", 100);
                    algid = _algorithmId;
                    Key = pdb.GetBytes(24);
                    IV = pdb.GetBytes(8);
                   
                    dlgEncryptFile.FileName = SourceFileName;
                    Stream inStream = dlgEncryptFile.OpenFile();

                    if (inStream.Length > 0)
                    {
                        SymmetricAlgorithm symalg = SymmetricAlgorithm.Create(_algorithmName);
                        ICryptoTransform encryptor = symalg.CreateEncryptor(Key, IV);

                        CryptoStream cryptStream = new CryptoStream(inStream, encryptor, CryptoStreamMode.Read);
                        
                        byte[] encrData = new byte[inStream.Length + encryptor.OutputBlockSize];
                        int readBlockSize = encryptor.OutputBlockSize * 1000;
                        int totalEncrBytes = 0;
                        for (int BytesRead = 0; totalEncrBytes < encrData.Length; totalEncrBytes += BytesRead)
                        {
                            if (totalEncrBytes + readBlockSize > encrData.Length)
                                readBlockSize = encrData.Length - totalEncrBytes;
                            BytesRead = cryptStream.Read(encrData, totalEncrBytes, readBlockSize);
                            if (BytesRead == 0)
                                break;
                        }

                        encryptor.Dispose();
                        cryptStream.Clear();
                        cryptStream.Close();
                        symalg.Clear();

                        int pos = dlgEncryptFile.FileName.LastIndexOf('\\');
                        if (pos != -1)
                        {
                            pos += 1; //for \
                            string fileTitle = dlgEncryptFile.FileName.Substring(pos, dlgEncryptFile.FileName.Length - pos);
                            ResourceWriter resWriter = new ResourceWriter(_applicationPath + "\\encrypted.resources");
                            resWriter.AddResource("1", algid);
                            resWriter.AddResource("2", fileTitle);
                            resWriter.AddResource("3", totalEncrBytes);
                            resWriter.AddResource("4", encrData);
                            resWriter.AddResource("5", Key);
                            encrData = null;
                            inStream.Close();
                            resWriter.Generate();
                            resWriter.Dispose();

                            //SourceFileName = BuildDecryptorAssembly(GetDestinationFileName(SourceFileName,".exe"));

                            SourceFileName = BuildDecryptorAssembly_New(GetDestinationFileName(SourceFileName, ".exe"));


                        }
                        else
                        {
                            inStream.Close();
                        }
                        Array.Clear(Key, 0, Key.Length);
                        Array.Clear(IV, 0, IV.Length);
                        FileInfo ofileInfo = new FileInfo(SourceFileName);
                        _outputEncryptedZipFileName = SourceFileName.Replace(ofileInfo.Extension, ".zip");
                        

                        gloZip.ZipMyFile(SourceFileName, _outputEncryptedZipFileName);

                        try
                        { 
                            ofileInfo.Delete();
                            File.Delete(_exportedFilePath);
                        }
                        catch (Exception) { }

                        ofileInfo = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (File.Exists(_applicationPath + "\\encrypted.resources"))
                    File.Delete(_applicationPath + "\\encrypted.resources");
                if (dlgEncryptFile != null) { dlgEncryptFile.Dispose(); }

            }

            return _outputEncryptedZipFileName;
        }

        private static string BuildDecryptorAssembly(string OutputFileName)
        {

            CSharpCodeProvider csprov = new CSharpCodeProvider();
            ICodeCompiler compiler = csprov.CreateCompiler();
            CompilerParameters compilerparams = new CompilerParameters(new string[] { "System.dll", "System.Windows.Forms.dll", "System.Drawing.dll" }, OutputFileName, false);
            compilerparams.GenerateExecutable = true;
            compilerparams.CompilerOptions = "/target:winexe /resource:\"" + _applicationPath + "\\encrypted.resources\",encrypted";

            #region Embeded Source
            CompilerResults cr = compiler.CompileAssemblyFromSource(compilerparams,
@"using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Resources;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Security.Cryptography;

namespace dotnetdecrypt
{ 
public class FormDecrypt: Form
{ 
	private System.Windows.Forms.Button btnCancel;
	private System.Windows.Forms.Button btnOK;
	private System.Windows.Forms.Label lblPwd;
	private System.Windows.Forms.TextBox txtPassword;
	private string applicationPath;
	public FormDecrypt()
	{
			this.Load += new System.EventHandler(this.FormDecryptor_Load);
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblPwd = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(176, 56);
" +
                "			this.btnCancel.Name = \"btnCancel\";" +
                "this.btnCancel.TabIndex = 2;" +
                "this.btnCancel.Text = \"&Cancel\";" +
                @"			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(64, 56);
" +
                "			this.btnOK.Name = \"btnOK\";" +
                "this.btnOK.TabIndex = 1;" +
                "this.btnOK.Text = \"&OK\";" +
                @"			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lblPwd
			// 
			this.lblPwd.Location = new System.Drawing.Point(16, 16);
" +
                "this.lblPwd.Name = \"lblPwd\";" +
                "this.lblPwd.TabIndex = 3;" +
                "this.lblPwd.Text = \"&Password:\";" +
                @"			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(136, 16);
" +
                "this.txtPassword.Name = \"txtPassword\";" +
                @"			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(136, 22);
			this.txtPassword.TabIndex = 0;
			this.txtPassword.MaxLength = 256;
" +
                "this.txtPassword.Text = \"\";" +
                @"			// 
			// FormDecrypt
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(292, 93);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.lblPwd);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
" +
                "this.Name = \"FormDecrypt\";" +
                "this.Text = \"gloEMR Decrypt File\";" +
                @"			this.ResumeLayout(false);
	}
	[STAThread]
	static void Main() 
	{
		Application.Run(new FormDecrypt());
	}

	private void btnOK_Click(object sender, System.EventArgs e)
	{
		Cursor curCursor = Cursor.Current;
		Cursor.Current = Cursors.WaitCursor;
		try
		{" +
"			Stream resStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(\"encrypted\");" +
"           if (txtPassword.Text.Trim() != \"\" && txtPassword.Text.Length < 8) " +
"           {MessageBox.Show(\"Decryption key must be minimum 8 characters.\", \"gloEMR\", MessageBoxButtons.OK, MessageBoxIcon.Information);" +
"            return;" +
"            }" +
"			string algid = \"\", fileTitle=\"\";" +
@"			int totalEncrBytes = 0;
			byte[] encrData = null;
			ResourceReader resRdr = new ResourceReader(resStream);
			foreach(DictionaryEntry entry in resRdr)
			{
				string resID = (string)entry.Key;
				switch(resID)
				{" +
                "case \"1\":" +
                "	algid = (string)entry.Value;" +
                "	break;" +
                "case \"2\":" +
                "	fileTitle = (string)entry.Value;" +
                "	break;  " +
                "case \"3\":" +
                "   totalEncrBytes = (int)entry.Value;" +
                "	break;" +
                "case \"4\":" +
                @"	encrData = (byte[]) entry.Value;
					break;
				}
			}" +
"			PasswordDeriveBytes pdb = new PasswordDeriveBytes(txtPassword.Text, UnicodeEncoding.Unicode.GetBytes(\"☻æ▼☺ÆΦ²‼⌡µ≤▐≈¼╓▐┼╔╩╦Ç→£╪↨☼¥¢τ≡ß§▲φ⌐ε£⌠°ⁿ◄Ω╪Ö↔║√┐Íù¼º╣▓í¢+÷ё2ыΣ⌂τ♫\"), \"SHA512\", 100);" +
"			txtPassword.Text = \"*************************************\";" +
"			txtPassword.Text = \"\";" +
@"			byte[] Key=null;
			byte[] IV =null;" +
"			if(algid == \"1\")" +
"			{" +
"				algid = \"Rijndael\";" +
@"				Key = pdb.GetBytes(32);
				IV = pdb.GetBytes(16);
			}" +
"			else if(algid == \"2\")" +
"			{" +
"				algid = \"TripleDES\";" +
@"				Key = pdb.GetBytes(24);
				IV = pdb.GetBytes(8);
			}
			SymmetricAlgorithm symalg = SymmetricAlgorithm.Create(algid);
			ICryptoTransform decryptor = symalg.CreateDecryptor(Key, IV);
			Array.Clear(Key, 0, Key.Length);
			Array.Clear(IV, 0, IV.Length);
			string filePath = applicationPath;
			filePath += '\\';
			Stream outStream = new FileStream(filePath + fileTitle, FileMode.CreateNew, FileAccess.Write);
			CryptoStream cryptStream = new CryptoStream(outStream, decryptor, CryptoStreamMode.Write);
			cryptStream.Write(encrData, 0, totalEncrBytes);
			cryptStream.FlushFinalBlock();
			cryptStream.Clear();
			cryptStream.Close();
			outStream.Close();
			decryptor.Dispose();
			symalg.Clear();" +
            "MessageBox.Show(\"Operation Successful\", \"Status\", MessageBoxButtons.OK, MessageBoxIcon.Information);" +
@"		}
		finally
		{
			Cursor.Current = curCursor;
		}
		this.Close();
	}
	private void btnCancel_Click(object sender, System.EventArgs e)
	{
		this.Close();
	}
	private void FormDecryptor_Load(object sender, System.EventArgs e)
	{
		applicationPath = Assembly.GetExecutingAssembly().Location;
		int pos = applicationPath.LastIndexOf('\\');
		if(pos != -1)
		{
			applicationPath = applicationPath.Remove(pos, applicationPath.Length - pos);
		}
		else
		{" +
            "throw new ApplicationException(\"Failed to get current Application's Path\");" +
@"		}
	}
}
}");
            #endregion

            CompilerErrorCollection errs = cr.Errors;
            if (!errs.HasErrors)
            {

                //MessageBox.Show("Operation Successful");
                return OutputFileName;
            }
            else
            {
                foreach (CompilerError err in errs)
                    MessageBox.Show(err.ToString());

                return "";
            }
        }

        private static string BuildDecryptorAssembly_New(string OutputFileName)
        {
            
            CSharpCodeProvider csprov = new CSharpCodeProvider();
            ICodeCompiler compiler = csprov.CreateCompiler();
            CompilerParameters compilerparams = new CompilerParameters(new string[] { "System.dll", "System.Windows.Forms.dll", "System.Drawing.dll" }, OutputFileName, false);
            compilerparams.GenerateExecutable = true;
            compilerparams.CompilerOptions = "/target:winexe /resource:\"" + _applicationPath + "\\encrypted.resources\",encrypted";

            #region Embeded Source
            CompilerResults cr = compiler.CompileAssemblyFromSource(compilerparams,
@"using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Resources;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Security.Cryptography;

namespace dotnetdecrypt
{ 
public class FormDecrypt: Form
{ 
	
	private string applicationPath;
    private  System.Windows.Forms.Panel Panel2;
    private  System.Windows.Forms.Label Label5;
    private  System.Windows.Forms.TextBox txtPassword;
    private  System.Windows.Forms.Label Label4;
    private  System.Windows.Forms.Label Label3;
    private  System.Windows.Forms.Label Label2;
    private  System.Windows.Forms.Label Label1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;

	public FormDecrypt()
	{
			this.Load += new System.EventHandler(this.FormDecryptor_Load);
            
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.btnCancel);
            this.Panel2.Controls.Add(this.btnOK);
            this.Panel2.Controls.Add(this.Label5);
            this.Panel2.Controls.Add(this.txtPassword);
            this.Panel2.Controls.Add(this.Label4);
            this.Panel2.Controls.Add(this.Label3);
            this.Panel2.Controls.Add(this.Label2);
            this.Panel2.Controls.Add(this.Label1);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel2.Location = new System.Drawing.Point(0, 0);
"+
            "this.Panel2.Name = \"Panel2\"; "+
            "this.Panel2.Padding = new System.Windows.Forms.Padding(3);"+
            "this.Panel2.Size = new System.Drawing.Size(375, 86);"+
            "this.Panel2.TabIndex = 0;"+
            
            "this.Label5.AutoSize = true;"+
            "this.Label5.Font = new System.Drawing.Font(\"Tahoma\", 9F, System.Drawing.FontStyle.Bold);"+
            "this.Label5.Location = new System.Drawing.Point(16, 21);"+
            "this.Label5.Name = \"Label5\";"+
            "this.Label5.Size = new System.Drawing.Size(145, 14);"+
            "this.Label5.TabIndex = 5;"+
            "this.Label5.Text = \"Enter Decryption Key :\";"+
            " " +
            "this.txtPassword.Font = new System.Drawing.Font(\"Tahoma\", 9F, System.Drawing.FontStyle.Bold);" +
            "this.txtPassword.Location = new System.Drawing.Point(162, 18);" +
            "this.txtPassword.MaxLength = 256;" +
            "this.txtPassword.Name = \"txtPassword\";"+
            "this.txtPassword.PasswordChar = '*';"+
            "this.txtPassword.Size = new System.Drawing.Size(197, 22);"+
            "this.txtPassword.TabIndex = 0;"+
            " "+
            "this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));"+
            "this.Label4.Dock = System.Windows.Forms.DockStyle.Right;"+
            "this.Label4.Location = new System.Drawing.Point(371, 4);"+
            "this.Label4.Name = \"Label4\";"+
            "this.Label4.Size = new System.Drawing.Size(1, 78);"+
            "this.Label4.TabIndex = 3;"+
            " "+
            "this.Label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));"+
            "this.Label3.Dock = System.Windows.Forms.DockStyle.Left;"+
            "this.Label3.Location = new System.Drawing.Point(3, 4);"+
            "this.Label3.Name = \"Label3\";"+
            "this.Label3.Size = new System.Drawing.Size(1, 78);"+
            "this.Label3.TabIndex = 2;"+
            " "+
            "this.Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));"+
            "this.Label2.Dock = System.Windows.Forms.DockStyle.Bottom;"+
            "this.Label2.Location = new System.Drawing.Point(3, 82);"+
            "this.Label2.Name = \"Label2\";"+
            "this.Label2.Size = new System.Drawing.Size(369, 1);"+
            "this.Label2.TabIndex = 1;"+
            " "+
            "this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));"+
            "this.Label1.Dock = System.Windows.Forms.DockStyle.Top;"+
            "this.Label1.Location = new System.Drawing.Point(3, 3);"+
            "this.Label1.Name = \"Label1\";"+
            "this.Label1.Size = new System.Drawing.Size(369, 1);"+
            "this.Label1.TabIndex = 0;"+
            " "+
            "this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;"+
            "this.btnOK.Font = new System.Drawing.Font(\"Tahoma\", 9F, System.Drawing.FontStyle.Bold);"+
            "this.btnOK.Location = new System.Drawing.Point(162, 49);"+
            "this.btnOK.Name = \"btnOK\";"+
            "this.btnOK.Size = new System.Drawing.Size(96, 23);"+
            "this.btnOK.TabIndex = 1;"+
            "this.btnOK.Text = \"OK\";"+
            "this.btnOK.UseVisualStyleBackColor = true;"+
            @"this.btnOK.Click += new System.EventHandler(this.btnOK_Click);"+
            " "+
            "this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;"+
            "this.btnCancel.Font = new System.Drawing.Font(\"Tahoma\", 9F, System.Drawing.FontStyle.Bold);"+
            "this.btnCancel.Location = new System.Drawing.Point(264, 49);"+
            "this.btnCancel.Name = \"btnCancel\";"+
            "this.btnCancel.Size = new System.Drawing.Size(95, 23);"+
            "this.btnCancel.TabIndex = 2;"+
            "this.btnCancel.Text = \"Cancel\";"+
            "this.btnCancel.UseVisualStyleBackColor = true;"+
            @"this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);"+
            " "+
            "this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);"+
            "this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;"+
            "this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));"+
            "this.ClientSize = new System.Drawing.Size(375, 86);"+
            "this.Controls.Add(this.Panel2);"+
            "this.Font = new System.Drawing.Font(\"Tahoma\", 9F);"+
            "this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));"+
            "this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;"+
            "this.Name = \"FormDecrypt\";" +
            "this.ShowInTaskbar = false;"+
            "this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;"+
            "this.Text = \"   gloEMR Decrypt File\";"+
            "this.Panel2.ResumeLayout(false);"+
            "this.Panel2.PerformLayout();"+
            @"this.ResumeLayout(false);
	}
	[STAThread]
	static void Main() 
	{
		Application.Run(new FormDecrypt());
	}
    
    public bool Compare(byte[] b1, byte[] b2) {
        if ( b1.Length != b2.Length )
        return false;
        for ( int i = 0; i < b1.Length; i++ )
        if ( b1[i] != b2[i] )
        return false;
        return true;
    }

	private void btnOK_Click(object sender, System.EventArgs e)
	{
		Cursor curCursor = Cursor.Current;
		Cursor.Current = Cursors.WaitCursor;
        string _outputFilePath = String.Empty;
        Stream outStream = null;
		CryptoStream cryptStream = null;
		try
		{" +
"			Stream resStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(\"encrypted\");" +
"           if (txtPassword.Text.Trim() != \"\" && txtPassword.Text.Length < 8) " +
"           {MessageBox.Show(\"Decryption key must be minimum 8 characters.\", \"gloEMR\", MessageBoxButtons.OK, MessageBoxIcon.Information);" +
"            return;" +
"            }" +
"			string algid = \"\", fileTitle=\"\";" +
@"			int totalEncrBytes = 0;
            byte[] _encryptionKey = null;
			byte[] encrData = null;
			ResourceReader resRdr = new ResourceReader(resStream);
			foreach(DictionaryEntry entry in resRdr)
			{
				string resID = (string)entry.Key;
				switch(resID)
				{" +
                "case \"1\":" +
                "	algid = (string)entry.Value;" +
                "	break;" +
                "case \"2\":" +
                "	fileTitle = (string)entry.Value;" +
                "	break;  " +
                "case \"3\":" +
                "   totalEncrBytes = (int)entry.Value;" +
                "	break;" +
                "case \"5\":" +
                "   _encryptionKey = (byte[])entry.Value;" +
                "	break;" +
                "case \"4\":" +
                @"	encrData = (byte[]) entry.Value;
					break;
				}
			}" +
"			PasswordDeriveBytes pdb = new PasswordDeriveBytes(txtPassword.Text, UnicodeEncoding.Unicode.GetBytes(\"☻æ▼☺ÆΦ²‼⌡µ≤▐≈¼╓▐┼╔╩╦Ç→£╪↨☼¥¢τ≡ß§▲φ⌐ε£⌠°ⁿ◄Ω╪Ö↔║√┐Íù¼º╣▓í¢+÷ё2ыΣ⌂τ♫\"), \"SHA512\", 100);" +
"			txtPassword.Text = \"*************************************\";" +
"			txtPassword.Text = \"\";" +
@"			byte[] Key=null;
			byte[] IV =null;" +
"			if(algid == \"1\")" +
"			{" +
"				algid = \"Rijndael\";" +
@"				Key = pdb.GetBytes(32);
				IV = pdb.GetBytes(16);
			}" +
"			else if(algid == \"2\")" +
"			{" +
"				algid = \"TripleDES\";" +
@"				Key = pdb.GetBytes(24);
				IV = pdb.GetBytes(8);
			}
            if(!Compare(Key,_encryptionKey))
            {
               " +
            "MessageBox.Show(\"Invalid decryption key, file decryption failed.\", \"gloEMR\", MessageBoxButtons.OK, MessageBoxIcon.Information);" +
            "return; "+
@"		}
			SymmetricAlgorithm symalg = SymmetricAlgorithm.Create(algid);
			ICryptoTransform decryptor = symalg.CreateDecryptor(Key, IV);
			Array.Clear(Key, 0, Key.Length);
			Array.Clear(IV, 0, IV.Length);
			string filePath = applicationPath;
			filePath += '\\';
            _outputFilePath = filePath + fileTitle;
			outStream = new FileStream(filePath + fileTitle, FileMode.Create, FileAccess.Write);
			cryptStream = new CryptoStream(outStream, decryptor, CryptoStreamMode.Write);
			cryptStream.Write(encrData, 0, totalEncrBytes);
			cryptStream.FlushFinalBlock();
			cryptStream.Clear();
			cryptStream.Close();
			outStream.Close();
			decryptor.Dispose();
			symalg.Clear();" +
            "MessageBox.Show(\"File decrypted successfully.\", \"gloEMR\", MessageBoxButtons.OK, MessageBoxIcon.Information);" +
@"		}
        catch(Exception){ " +
"MessageBox.Show(\"Invalid decryption key, file decryption failed.\", \"gloEMR\", MessageBoxButtons.OK, MessageBoxIcon.Information);" +
" outStream.Close(); " +
@" try { FileInfo ofileInfo = new FileInfo(_outputFilePath); if(ofileInfo.Exists) {ofileInfo.Delete();} }catch(Exception){}" +
@"} 
		finally
		{
			Cursor.Current = curCursor;
		}
		this.Close();
	}
	private void btnCancel_Click(object sender, System.EventArgs e)
	{
		this.Close();
	}
	private void FormDecryptor_Load(object sender, System.EventArgs e)
	{
		applicationPath = Assembly.GetExecutingAssembly().Location;
		int pos = applicationPath.LastIndexOf('\\');
		if(pos != -1)
		{
			applicationPath = applicationPath.Remove(pos, applicationPath.Length - pos);
		}
		else
		{" +
            "throw new ApplicationException(\"Failed to get current Application's Path\");" +
@"		}
	}
}
}");
            #endregion

            CompilerErrorCollection errs = cr.Errors;
            if (!errs.HasErrors)
            {
                //MessageBox.Show("Operation Successful");
                return OutputFileName;
            }
            else
            {
                foreach (CompilerError err in errs)
                    MessageBox.Show(err.ToString());

                return "";
            }
            
            
        }

        private static string GetDestinationFileName(string SourceFileName,string Extension)
        {
            string _outputFileName = "";
            string _filePath = "";
            string _newfileSuffix = "";
            string _fileExt = "";

            try
            {
                FileInfo ofileInfo = new FileInfo(SourceFileName);
                _filePath = ofileInfo.DirectoryName;
                _fileExt = ofileInfo.Extension;
                _newfileSuffix = DateTime.Now.ToString().Replace('/', ' ').Replace(':', ' ');
                //commeneted as per discussion-in secure message after attaching file it was giving error of max length limit upto 35 char.
                _outputFileName = SourceFileName.Replace(_fileExt,  Extension);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            return _outputFileName;
        }

        public static bool ValidateKey(string EncryptionKey)
        {
            if (EncryptionKey != null && EncryptionKey.Trim() != "" && EncryptionKey.Length < 8)
            {
                MessageBox.Show("Encryption key must be minimum 8 characters.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion " Code for encrypted exe "

        #region " Code for encrypt file only "

        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);

        private static string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

            // Use the Automatically generated key for Encryption. 
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }

        private static string EncryptFile(string SourceFileName, string sKey)
        {
            string sOutputFilename = "";
            string _ext = "";

            try
            {
                FileInfo ofileInfo = new FileInfo(SourceFileName);
                _ext = ofileInfo.Extension;
                ofileInfo = null;
                sOutputFilename = GetDestinationFileName(SourceFileName, _ext);

                FileStream fsInput = new FileStream(SourceFileName,
                   FileMode.Open,
                   FileAccess.Read);

                FileStream fsEncrypted = new FileStream(sOutputFilename,
                   FileMode.Create,
                   FileAccess.Write);
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = DES.CreateEncryptor();
                CryptoStream cryptostream = new CryptoStream(fsEncrypted,
                   desencrypt,
                   CryptoStreamMode.Write);

                byte[] bytearrayinput = new byte[fsInput.Length];
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
                cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptostream.Close();
                fsInput.Close();
                fsEncrypted.Close();

            }
            catch (Exception ex)
            {
                sOutputFilename = "";
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }

            return sOutputFilename;
        }

        private static void DecryptFile(string SourceFileName, string sKey)
        {

            string sOutputFilename = "";
            sOutputFilename = GetDestinationFileName(SourceFileName,".zip");

            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.
            FileStream fsread = new FileStream(SourceFileName,
               FileMode.Open,
               FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //DES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread,
               desdecrypt,
               CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            fsDecrypted.Flush();
            fsDecrypted.Close();
        }

        #endregion " Code for encrypt file only "

        public static string PerformFileEncryption(string SourceFileName, string EncryptionKey)
        {
            string _returnValue = "";

            if (EncryptWithSelfExtractingExe == true)
            {
                //Call method if file need to be self extracting encrypted exe which is zipped
                _returnValue = Encrypt(SourceFileName, EncryptionKey);
            }
            else
            {
                //Call method if file need to be encrypted only which is zipped
                EncryptionKey = GenerateKey();
                _returnValue = EncryptFile(SourceFileName, EncryptionKey); 
            }

            return _returnValue;
        }

        public static string PerformFileEncryption(string SourceFileName, string EncryptionKey,bool IsEncryptedExe)
        {
            string _returnValue = "";

            if (IsEncryptedExe == true)
            {
                //Call method if file need to be self extracting encrypted exe which is zipped
                _returnValue = Encrypt(SourceFileName, EncryptionKey);
            }
            else
            {
                //Call method if file need to be encrypted only which is zipped
                EncryptionKey = GenerateKey();
                _returnValue = EncryptFile(SourceFileName, EncryptionKey);
            }
            return _returnValue;
        }

        
    }

}