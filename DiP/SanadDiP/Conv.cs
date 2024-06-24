using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SanadDiP
{
    /// <CREDITS>
    /// Sanad Hajarat
    /// Data Science Intern
    /// ProgressSoft Corporation
    /// </CREDITS>
    
    public class ConvMatrix
    {
        public int TopLeft = 0, TopMid = 0, TopRight = 0;
        public int MidLeft = 0, Pixel = 1, MidRight = 0;
        public int BottomLeft = 0, BottomMid = 0, BottomRight = 0;
        public int Factor = 1;
        public int Offset = 0;
        public void SetAll(int nVal)
        {
            TopLeft = TopMid = TopRight = MidLeft = Pixel = MidRight = BottomLeft = BottomMid = BottomRight = nVal;
        }
    }
    public class Convolution : Form
    {
        private Button OK;
        private ConvMatrix matrix = new ConvMatrix();
        private TextBox TL;
        private TextBox TR;
        private TextBox TM;
        private TextBox Pixel;
        private TextBox MR;
        private TextBox ML;
        private TextBox BM;
        private TextBox BR;
        private TextBox BL;
        
        private Container components = null;

        public Convolution()
        {
            InitializeComponent();

            OK.DialogResult = DialogResult.OK;
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        public ConvMatrix Matrix
        {
            get
            {
                ConvMatrix mat = new ConvMatrix();
                mat.TopLeft = Convert.ToInt32(TL.Text);
                mat.TopMid = Convert.ToInt32(TM.Text);
                mat.TopRight = Convert.ToInt32(TR.Text);
                mat.MidLeft = Convert.ToInt32(ML.Text);
                mat.MidRight = Convert.ToInt32(MR.Text);
                mat.BottomLeft = Convert.ToInt32(BL.Text);
                mat.BottomMid = Convert.ToInt32(BM.Text);
                mat.BottomRight = Convert.ToInt32(BR.Text);
                mat.Pixel = Convert.ToInt32(Pixel.Text);
                return mat;
            }
        }
        
        private void InitializeComponent()
        {
            TL = new TextBox();
            TR = new TextBox();
            TM = new TextBox();
            Pixel = new TextBox();
            MR = new TextBox();
            ML = new TextBox();
            BM = new TextBox();
            BR = new TextBox();
            BL = new TextBox();
            OK = new Button();
            SuspendLayout();
            
            TL.Location = new System.Drawing.Point(24, 24);
            TL.Name = "TL";
            TL.Size = new System.Drawing.Size(24, 20);
            TL.TabIndex = 0;
            TL.Text = "1";
            
            TR.Location = new System.Drawing.Point(104, 24);
            TR.Name = "TR";
            TR.Size = new System.Drawing.Size(24, 20);
            TR.TabIndex = 1;
            TR.Text = "1";
            
            TM.Location = new System.Drawing.Point(64, 24);
            TM.Name = "TM";
            TM.Size = new System.Drawing.Size(24, 20);
            TM.TabIndex = 2;
            TM.Text = "1";
            
            Pixel.Location = new System.Drawing.Point(64, 56);
            Pixel.Name = "Pixel";
            Pixel.Size = new System.Drawing.Size(24, 20);
            Pixel.TabIndex = 5;
            Pixel.Text = "1";
            
            MR.Location = new System.Drawing.Point(104, 56);
            MR.Name = "MR";
            MR.Size = new System.Drawing.Size(24, 20);
            MR.TabIndex = 4;
            MR.Text = "1";
            
            ML.Location = new System.Drawing.Point(24, 56);
            ML.Name = "ML";
            ML.Size = new System.Drawing.Size(24, 20);
            ML.TabIndex = 3;
            ML.Text = "1";
            
            BM.Location = new System.Drawing.Point(64, 88);
            BM.Name = "BM";
            BM.Size = new System.Drawing.Size(24, 20);
            BM.TabIndex = 8;
            BM.Text = "1";
            
            BR.Location = new System.Drawing.Point(104, 88);
            BR.Name = "BR";
            BR.Size = new System.Drawing.Size(24, 20);
            BR.TabIndex = 7;
            BR.Text = "1";
            
            BL.Location = new System.Drawing.Point(24, 88);
            BL.Name = "BL";
            BL.Size = new System.Drawing.Size(24, 20);
            BL.TabIndex = 6;
            BL.Text = "1";
            
            OK.Location = new System.Drawing.Point(38, 144);
            OK.Name = "OK";
            OK.TabIndex = 13;
            OK.Text = "Apply";
            
            AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            ClientSize = new System.Drawing.Size(160, 181);
            Controls.AddRange(new Control[] { OK, BM, BR, BL, Pixel, MR, ML, TM, TR, TL});
            Name = "Morphology";
            Text = "Morphology";
            CenterToParent();
            ResumeLayout(false);

        }

    }
}
