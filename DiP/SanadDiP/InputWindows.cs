using System;
using System.Drawing;
using System.Windows.Forms;

namespace SanadDiP
{
    /// <CREDITS>
    /// Sanad Hajarat
    /// Data Science Intern
    /// ProgressSoft Corporation
    /// </CREDITS>

    public class Input : Form
    {
        private Button OK;
        private TextBox Value;
        private Label Label;
        
        public int nValue
        {
            get
            {
                return (Convert.ToInt32(Value.Text, 10));
            }
            set { Value.Text = value.ToString(); }
        }

        public Input()
        {
            InitializeComponent();
            OK.DialogResult = DialogResult.OK;
        }

        private void InitializeComponent()
        {
            OK = new Button();
            Value = new TextBox();
            Label = new Label();
            SuspendLayout();

            OK.Location = new Point(16, 56);
            OK.Name = "OK";
            OK.TabIndex = 0;
            OK.Text = "OK";

            Value.Location = new Point(80, 16);
            Value.Name = "Value";
            Value.TabIndex = 2;
            Value.Text = "";

            Label.Location = new Point(16, 16);
            Label.Name = "Label";
            Label.Size = new Size(56, 23);
            Label.TabIndex = 3;
            Label.Text = "Enter a Value";

            AcceptButton = OK;
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(200, 85);
            Controls.AddRange(new Control[] { Label, Value, OK } );
            Name = "Input";
            Text = "Input";
            Load += Parameter_Load;
            ResumeLayout(false);

        }

        private void Parameter_Load(object sender, EventArgs e) { }
    } 
}