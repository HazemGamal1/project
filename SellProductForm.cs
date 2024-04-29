using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shopManager
{
    public partial class SellProductForm : Form
    {
        InventoryForm inventoryForm = InventoryForm.Instance;
        StockForm stockForm = StockForm.Instance;

        private int iD;
        public SellProductForm(int id, char flag)
        {
            InitializeComponent();
            if (flag == 'S')
                iD = id;
            else
            {
                sellIdLapel.Visible = true;
                sellIdTextBox.Visible = true;
            }
        }
       

        private void sellButton_Click(object sender, EventArgs e)
        {
            
           // this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sellIdTextBox_TextChanged_1(object sender, EventArgs e)
        {
            iD = int.Parse(sellIdTextBox.Text);
        }

        private void quantityNumericUpDown_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void SellProductForm_Load(object sender, EventArgs e)
        {

        }
    }
}
