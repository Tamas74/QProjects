namespace gloSnoMed
{
    partial class Frm1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm1));
            this.imgList1 = new System.Windows.Forms.ImageList(this.components);
            this.backgroundSearch = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // imgList1
            // 
            this.imgList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList1.ImageStream")));
            this.imgList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList1.Images.SetKeyName(0, "Bullet06.ico");
            this.imgList1.Images.SetKeyName(1, "Small Arrow.ico");
            this.imgList1.Images.SetKeyName(2, "Defination.ico");
            this.imgList1.Images.SetKeyName(3, "Description.ico");
            this.imgList1.Images.SetKeyName(4, "ICD 09.ico");
            this.imgList1.Images.SetKeyName(5, "ICD 10.ico");
            this.imgList1.Images.SetKeyName(6, "Procedure.ico");
            this.imgList1.Images.SetKeyName(7, "Qualifier.ico");
            this.imgList1.Images.SetKeyName(8, "Zip code.ico");
            this.imgList1.Images.SetKeyName(9, "ICD10GalleryGreen.png");
            // 
            // Frm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 251);
            this.Name = "Frm1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ImageList imgList1;
        private System.ComponentModel.BackgroundWorker backgroundSearch;


    }
}