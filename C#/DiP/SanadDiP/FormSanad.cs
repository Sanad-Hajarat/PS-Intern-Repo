using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SanadDiP
{
    /// <CREDITS>
    /// Sanad Hajarat
    /// Data Science Intern
    /// ProgressSoft Corporation
    /// </CREDITS>

    public class GUI : Form
    {
        private Bitmap img1;
        private Bitmap img2;
        private Bitmap m_Undo;
        private Bitmap m_Undo2;
        private MainMenu mainMenu;
        private MenuItem menuFile;
        private MenuItem menuThresh;
        private MenuItem FileLoad1;
        private MenuItem FileLoad2;
        private MenuItem FileSave1;
        private MenuItem FileSave2;
        private MenuItem FileSaveConcatH;
        private MenuItem FileSaveConcatV;
        private MenuItem FileExit;
        private MenuItem threshMean;
        private MenuItem threshStatic;
        private MenuItem inverseThreshMean;
        private MenuItem inverseThreshStatic;
        private MenuItem grayScale;
        private MenuItem Undo;
        private MenuItem Clear;
        private MenuItem ResizeWin;
        
        private Container components = null;

        public GUI() // Initializes all menuitems and mainmenu & bitmap
        {
            InitializeComponent();

            img1 = new Bitmap(2, 2);  // [ [(0, 0), (0, 1)],                                                                    //
            img2 = new Bitmap(2, 2);  // [(1, 0), (1, 1)] ] randomly initialized, this variable is where our pictures are stored  //
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

        #region
        
        // GUI Initialization
        private void InitializeComponent()
        {
            mainMenu = new MainMenu();
            menuFile = new MenuItem();
            FileLoad1 = new MenuItem();
            FileLoad2 = new MenuItem();
            FileSave1 = new MenuItem();
            FileSave2 = new MenuItem();
            FileSaveConcatH = new MenuItem();
            FileSaveConcatV = new MenuItem();
            FileExit = new MenuItem();
            menuThresh = new MenuItem();
            threshMean = new MenuItem();
            threshStatic = new MenuItem();
            inverseThreshMean = new MenuItem();
            inverseThreshStatic = new MenuItem();
            grayScale = new MenuItem();
            Undo = new MenuItem();
            Clear = new MenuItem();
            ResizeWin = new MenuItem();

            mainMenu.MenuItems.AddRange(new[] { menuFile, menuThresh, Undo, grayScale, Clear, ResizeWin });

            menuFile.Index = 0;
            menuFile.MenuItems.AddRange(new[] { FileLoad1, FileLoad2, FileSave1, FileSave2, FileSaveConcatH, FileSaveConcatV, FileExit});
            menuFile.Text = "File";

            FileLoad1.Index = 0;
            FileLoad1.Shortcut = Shortcut.CtrlL;
            FileLoad1.Text = "Load Image 1";
            FileLoad1.Click += File1_Load;
            FileLoad2.Index = 1;
            FileLoad2.Shortcut = Shortcut.CtrlShiftL;
            FileLoad2.Text = "Load Image 2";
            FileLoad2.Click += File2_Load; 
            FileSave1.Index = 2;
            FileSave1.Shortcut = Shortcut.CtrlS;
            FileSave1.Text = "Save Image 1";
            FileSave1.Click += File_Save1;
            FileSave2.Index = 3;
            FileSave2.Shortcut = Shortcut.CtrlShiftS;
            FileSave2.Text = "Save Image 2";
            FileSave2.Click += File_Save2;
            FileSaveConcatH.Index = 4;
            FileSaveConcatH.Text = "Save Horizontally Concatenated Image";
            FileSaveConcatH.Click += File_Save_HConcat;
            FileSaveConcatV.Index = 5;
            FileSaveConcatV.Text = "Save Vertically Concatenated Image";
            FileSaveConcatV.Click += File_Save_VConcat; 
            FileExit.Index = 6;
            FileExit.Shortcut = Shortcut.CtrlE;
            FileExit.Text = "Exit";
            FileExit.Click += File_Exit;

            Undo.Index = 1;
            Undo.Shortcut = Shortcut.CtrlU;
            Undo.Text = "Undo";
            Undo.Click += UndoEdit;

            grayScale.Index = 2;
            grayScale.Shortcut = Shortcut.CtrlShiftG;
            grayScale.Text = "Convert to 8-Bit GrayScale";
            grayScale.Click += GrayConvert;

            menuThresh.Index = 3;
            menuThresh.MenuItems.AddRange(new[] { threshMean, threshStatic, inverseThreshMean, inverseThreshStatic });
            menuThresh.Text = "Thresholding";

            threshMean.Index = 0;
            threshMean.Shortcut = Shortcut.CtrlT;
            threshMean.Text = "Mean Thresholding";
            threshMean.Click += Mean_Thresh;
            threshStatic.Index = 1;
            threshStatic.Shortcut = Shortcut.CtrlShiftT;
            threshStatic.Text = "Static Thresholding";
            threshStatic.Click += Static_Thresh;
            inverseThreshMean.Index = 2;
            inverseThreshMean.Shortcut = Shortcut.CtrlI;
            inverseThreshMean.Text = "Inverse Mean Thresholding";
            inverseThreshMean.Click += Inverse_Mean_Thresh;
            inverseThreshStatic.Index = 3;
            inverseThreshStatic.Shortcut = Shortcut.CtrlShiftI;
            inverseThreshStatic.Text = "Inverse Static Thresholding";
            inverseThreshStatic.Click += Inverse_Static_Thresh;

            Clear.Index = 4;
            Clear.Shortcut = Shortcut.CtrlC;
            Clear.Text = "Clear Canvas";
            Clear.Click += ClearCanvas;

            ResizeWin.Index = 5;
            ResizeWin.Shortcut = Shortcut.CtrlR;
            ResizeWin.Text = "Resize";
            ResizeWin.Click += ResizeImage;
            
            AutoScaleBaseSize = new Size(5, 13);
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(500, 50);
            Menu = mainMenu;
            Name = "FormSanad";
            Text = "Graphic Filters For Slightly Smarter Dummies";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Load += GUI_Load;
        }
        #endregion
        
        static void Main()
        {
            Application.Run(new GUI());
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int w1 = img1.Width;
            int h1 = img1.Height;
            int w2 = img2.Width;
            int h2 = img2.Height;
            g.DrawImage(img1, new Rectangle(AutoScrollPosition.X, AutoScrollPosition.Y, w1, h1));
            g.DrawImage(img2, new Rectangle(w1, AutoScrollPosition.Y, w2, h2));
        }
        private void GUI_Load(object sender, EventArgs e) {  }

        private void File1_Load(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); //  Dialog box allows to select file  // 

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp;*.jpg"; // File type Options //
            openFileDialog.FilterIndex = 3; //  The default displayed option is of index 3 (All valid files (*.bmp/*.jpg))  //
            openFileDialog.RestoreDirectory = false; //  Saves last opened file instead of restoring every single time  //

            if (DialogResult.OK == openFileDialog.ShowDialog()) //   Checks if OK button is pressed   //
            {
                img1 = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false); // Saves new image into m_Bitmap                //
                AutoScroll = true;                                             // (false signifies no color embedded to photo) //
                AutoScrollMinSize = new Size(img1.Width, img1.Height);
                Invalidate(); // Invalidate triggers a paint event //
            }
            ClientSize = new Size(Width = img1.Width + img2.Width, Height = (img1.Height > img2.Height) ? img1.Height : img2.Height);
        }
        private void File2_Load(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); //  Dialog box allows to select file  // 

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp;*.jpg"; // File type Options //
            openFileDialog.FilterIndex = 3; //  The default displayed option is of index 3 (All valid files (*.bmp/*.jpg))  //
            openFileDialog.RestoreDirectory = false; //  Saves last opened file instead of restoring every single time  //

            if (DialogResult.OK == openFileDialog.ShowDialog()) //   Checks if OK button is pressed   //
            {
                img2 = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false); // Saves new image into m_Bitmap                //
                AutoScroll = true;                                             // (false signifies no color embedded to photo) //
                AutoScrollMinSize = new Size(img1.Width, img1.Height);
                Invalidate(); // Invalidate triggers a paint event //
            }
            ClientSize = new Size(Width = img1.Width + img2.Width, Height = (img1.Height > img2.Height) ? img1.Height : img2.Height);
        }
        private void File_Save1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = false;

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                img1.Save(saveFileDialog.FileName);
            }
        }
        private void File_Save2(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = false;

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                img2.Save(saveFileDialog.FileName);
            }
        }
        private void File_Save_HConcat(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = false;

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                BitmapFilter.ConcatImages(img1, img2, true).Save(saveFileDialog.FileName);
            }
        }
        private void File_Save_VConcat(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = false;

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                BitmapFilter.ConcatImages(img1, img2, false).Save(saveFileDialog.FileName);
            }
        }
        private void File_Exit(object sender, EventArgs e)
        {
            Close();
        }

        private void Mean_Thresh(object sender, EventArgs e)
        {
            m_Undo = (Bitmap)img1.Clone();
            m_Undo2 = (Bitmap)img2.Clone();
            if (BitmapFilter.GrayScale(img1) && BitmapFilter.GrayScale(img2))
                Invalidate();
            if (BitmapFilter.ApplyMeanThresh(img1, false) && BitmapFilter.ApplyMeanThresh(img2, false))
                Invalidate();
        }
        private void Inverse_Mean_Thresh(object sender, EventArgs e)
        {
            m_Undo = (Bitmap)img1.Clone();
            m_Undo2 = (Bitmap)img2.Clone();
            if (BitmapFilter.GrayScale(img1) && BitmapFilter.GrayScale(img2))
                Invalidate();
            if (BitmapFilter.ApplyMeanThresh(img1, true) && BitmapFilter.ApplyMeanThresh(img2, true))
                Invalidate();
        }
        private void Static_Thresh(object sender, EventArgs e)
        {
            Input val = new Input();
            val.nValue = 0;

            if (DialogResult.OK == val.ShowDialog())
            {
                m_Undo = (Bitmap)img1.Clone();
                m_Undo2 = (Bitmap)img2.Clone();
                if (BitmapFilter.GrayScale(img1) && BitmapFilter.GrayScale(img2))
                    Invalidate();
                if (BitmapFilter.ApplyStaticThresh(img1, val.nValue, false) && BitmapFilter.ApplyStaticThresh(img2, val.nValue, false))
                    Invalidate();
            }
        }
        private void Inverse_Static_Thresh(object sender, EventArgs e)
        {
            Input val = new Input();
            val.Text = "Input Threshold";
            val.nValue = 0;

            if (DialogResult.OK == val.ShowDialog())
            {
                m_Undo = (Bitmap)img1.Clone();
                m_Undo2 = (Bitmap)img2.Clone(); 
                img1 = (Bitmap)(BitmapFilter.GrayConvert8(img1)).Clone();
                img2 = (Bitmap)(BitmapFilter.GrayConvert8(img2)).Clone();
                if (BitmapFilter.ApplyStaticThresh(img1, val.nValue, true) && BitmapFilter.ApplyStaticThresh(img2, val.nValue, true))
                    Invalidate();
            }
        }

        private void UndoEdit(object sender, EventArgs e)
        {
            Bitmap temp = (Bitmap)img1.Clone();
            Bitmap temp2 = (Bitmap)img2.Clone();
            img1 = (Bitmap)m_Undo.Clone();
            img2 = (Bitmap)m_Undo2.Clone();
            m_Undo = (Bitmap)temp.Clone();
            m_Undo2 = (Bitmap)temp2.Clone();
            Invalidate();
            ClientSize = new Size(Width = img1.Width + img2.Width, Height = (img1.Height > img2.Height) ? img1.Height : img2.Height);
        }

        private void GrayConvert(object sender, EventArgs e)
        {
            m_Undo = (Bitmap)img1.Clone();
            m_Undo2 = (Bitmap)img2.Clone();
            img1 = (Bitmap)(BitmapFilter.GrayConvert8(img1)).Clone();
            img2 = (Bitmap)(BitmapFilter.GrayConvert8(img2)).Clone();
            Invalidate();
        }

        private void ClearCanvas(object sender, EventArgs e)
        {
            img1 = new Bitmap(2, 2);
            img2 = new Bitmap(2, 2);
            Invalidate();
            ClientSize = new Size(500, 50);
        }

        private void ResizeImage(object sender, EventArgs e)
        {
            Resize val = new Resize();
            val.nWidth1 = img1.Width;
            val.nHeight1 = img1.Height;
            val.nWidth2 = img2.Width;
            val.nHeight2 = img2.Height;
            if (DialogResult.OK == val.ShowDialog())
            {
                m_Undo = (Bitmap)img1.Clone();
                m_Undo2 = (Bitmap)img2.Clone();

                img1 = (Bitmap)BitmapFilter.Resize(img1, val.nWidth1, val.nHeight1).Clone();
                img2 = (Bitmap)BitmapFilter.Resize(img2, val.nWidth2, val.nHeight2).Clone();
                Invalidate();
            }
            ClientSize = new Size(Width = img1.Width + img2.Width, Height = (img1.Height > img2.Height) ? img1.Height : img2.Height);
        }

    }

}
