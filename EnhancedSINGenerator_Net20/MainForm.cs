using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EnhancedSINGenerator
{
    public partial class MainForm : Form
    {
        #region A few constants        
        const int NUM_DIGIT_IN_SIN = 9;

        int[] digits = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {                       
            if (FirstDigitCheckBox.Checked)
            {
                // specify the first digit in SIN.
                char firstDigit = Char.Parse(DigitsComboBox.SelectedValue.ToString());               
                ResultTextbox.AppendText(Luhn.GenerateLuhnNumber(NUM_DIGIT_IN_SIN, firstDigit));
                ResultTextbox.AppendText(System.Environment.NewLine);               
            }
            else
            {
                ResultTextbox.AppendText(Luhn.GenerateLuhnNumber(NUM_DIGIT_IN_SIN));
                ResultTextbox.AppendText(System.Environment.NewLine);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ResultTextbox.Clear();
        }

        private void FirstDigitCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DigitsComboBox.Enabled = FirstDigitCheckBox.Checked;
        }
     
        private void MainForm_Load(object sender, EventArgs e)
        {
            DigitsComboBox.DataSource = digits;            
        }

        private void CopyAllButton_Click(object sender, EventArgs e)
        {
            if(ResultTextbox.Text.ToString().Length > 0)            
                Clipboard.SetText(ResultTextbox.Text);
        }
        
        #region Helper Methods
        /*
        private bool IsNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }
         **/
        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();            
            af.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
