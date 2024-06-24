using System;
using System.Windows.Forms;
using System.Drawing;

namespace SanadDiP
{
    /// <CREDITS>
    /// Sanad Hajarat
    /// Data Science Intern
    /// ProgressSoft Corporation
    /// </CREDITS>
    
    public class Resize : Form
    {
        private Label label;
        private Label img1;
        private Label img2;
        private TextBox WidthBox;
        private TextBox HeightBox;
        private TextBox WidthBox2;
        private TextBox HeightBox2;
        private Label label2;
        private Button OK;
        
        private System.ComponentModel.Container components = null;

        public int nWidth1
        {
            get
            { return (Convert.ToInt32(WidthBox.Text)); }
            set { WidthBox.Text = value.ToString(); }
        }
        public int nHeight1
        {
            get { return (Convert.ToInt32(HeightBox.Text)); }
            set { HeightBox.Text = value.ToString(); }
        }
        
        public int nWidth2
        {
            get { return (Convert.ToInt32(WidthBox2.Text)); }
            set { WidthBox2.Text = value.ToString(); }
        }
        public int nHeight2
        {
            get { return (Convert.ToInt32(HeightBox2.Text)); }
            set { HeightBox2.Text = value.ToString(); }
        }        
        public Resize()
        {
            InitializeComponent();

            OK.DialogResult = DialogResult.OK;
        }
        
        
        private void InitializeComponent()
        {
            label = new Label();
            img1 = new Label();
            img2 = new Label();
            WidthBox = new TextBox();
            WidthBox2 = new TextBox();
            HeightBox = new TextBox();
            HeightBox2 = new TextBox();
            label2 = new Label();
            OK = new Button();
            SuspendLayout();
            
            label.Location = new Point(16, 62);
            label.Name = "label1";
            label.Size = new Size(40, 16);
            label.TabIndex = 0;
            label.Text = "Width";
            
            WidthBox.Location = new Point(64, 62);
            WidthBox.Name = "WidthBox";
            WidthBox.Size = new Size(56, 20);
            WidthBox.TabIndex = 1;
            WidthBox.Text = "";
            
            WidthBox2.Location = new Point(128, 62);
            WidthBox2.Name = "WidthBox";
            WidthBox2.Size = new Size(56, 20);
            WidthBox2.TabIndex = 4;
            WidthBox2.Text = "";
            
            HeightBox.Location = new Point(64, 94);
            HeightBox.Name = "HeightBox";
            HeightBox.Size = new Size(56, 20);
            HeightBox.TabIndex = 3;
            HeightBox.Text = "";
            
            HeightBox2.Location = new Point(128, 94);
            HeightBox2.Name = "HeightBox";
            HeightBox2.Size = new Size(56, 20);
            HeightBox2.TabIndex = 5;
            HeightBox2.Text = "";
            
            label2.Location = new Point(16, 94);
            label2.Name = "label2";
            label2.Size = new Size(40, 16);
            label2.TabIndex = 2;
            label2.Text = "Height";
            
            OK.Location = new Point(54, 128);
            OK.Name = "OK";
            OK.Size = new Size(100, 23);
            OK.TabIndex = 6;
            OK.Text = "OK";

            img1.Location = new Point(64, 20);
            img1.Name = "Img1";
            img1.Size = new Size(40, 16);
            img1.TabIndex = 7;
            img1.Text = "Image 1";

            img2.Location = new Point(128, 20);
            img2.Name = "Img2";
            img2.Size = new Size(40, 16);
            img2.TabIndex = 8;
            img2.Text = "Image 2";
            
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(200, 185);
            Controls.AddRange(new Control[] { OK, HeightBox, HeightBox2, label2, WidthBox, WidthBox2, label, img1, img2 });
            Name = "Resize";
            Text = "Resize";
            Load += Resize_Load;
            ResumeLayout(false);

        }

        private void Resize_Load(object sender, EventArgs e)
        {

        }
    }
}