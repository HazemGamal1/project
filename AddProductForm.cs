﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shopManager
{
    public partial class AddProductForm : Form
    {
        InventoryForm inventoryForm = InventoryForm.Instance;

        public AddProductForm()
        {
            InitializeComponent();
        }

        private void AddProductForm_Load(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string squeezedFilePath = @"D:\Squeezefile.txt";
        private void addButton_Click(object sender, EventArgs e)
        {
            // Validate name
            string name = nameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a valid name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate id
            int id;
            if (!int.TryParse(idText.Text, out id) || id < 111 || id > 999)
            {
                MessageBox.Show("Please enter a valid ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate category
            string category = categoryComboBox.Text;
            if (category != "Cameras" && category != "Phones" && category != "Accessories")
            {
                MessageBox.Show("Please select a valid category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate quantity
            int quantity;
            if (!int.TryParse(quantityNumericUpDown.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity more than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate cost
            double cost;
            if (!double.TryParse(costTextBox.Text, out cost) || cost <= 0)
            {
                MessageBox.Show("Please enter a valid cost more than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate profit
            double profit;
            if (!double.TryParse(profitTextBox.Text, out profit) || profit < 0)
            {
                MessageBox.Show("Please enter a valid profit.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // All validations passed,
            //
            // add the product to the text file
            //

            string filePath = @"D:\products.txt";
            FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine($"{id} , {name} , {category} , {quantity} , {cost} , {profit}");

            
            sw.Close();
            fs.Close();
            // Clear the form
            ClearForm();

            // set focus to the first input field if needed
            nameTextBox.Focus();

            // Reload products into dataGridView
            inventoryForm.LoadProductsToDataGridView();
        }


        private void ClearForm()
        {
            nameTextBox.Text = "";
            idText.Text = "";
            categoryComboBox.SelectedIndex = -1; // Reset category selection
            quantityNumericUpDown.Value = 0;
            costTextBox.Text = "";
            profitTextBox.Text = "";
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
