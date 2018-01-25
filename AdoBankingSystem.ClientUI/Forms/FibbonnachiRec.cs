using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoBankingSystem.ClientUI.Forms
{
    public partial class FibbonnachiRec : Form
    {
        public FibbonnachiRec()
        {
            InitializeComponent();
        }


        public int CalculateFibRec(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            else if (n == 1)
            {
                return 1;
            }
            else return CalculateFibRec(n - 1) + CalculateFibRec(n - 2);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            
            int calculateTill = Int32.Parse(textBox1.Text);

            Task<int> task = new Task<int>(() => CalculateFibRec(calculateTill));
            textBox1.Text = "35";
            task.Start();

            int result = await task;
            textBox1.Text = result.ToString();
        }
    }
}
