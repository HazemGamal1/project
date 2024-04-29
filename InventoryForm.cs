using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Xml.Linq;



namespace shopManager
{
    public partial class InventoryForm : Form
    {
        private static InventoryForm instance;

        public static InventoryForm Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new InventoryForm();
                }
                return instance;
            }
        }
        string filePath = @"D:\products.txt";
        string squeezedFilePath = @"D:\Squeezefile.txt";

        FileStream fs;
        StreamReader sr;
        public static StackDataList dataList;
        private InventoryForm()
        {
            InitializeComponent();
            LoadProductsToDataGridView();
        }

        private void addProdButton_Click(object sender, EventArgs e)
        {
            AddProductForm addProduct = new AddProductForm();
            addProduct.ShowDialog();
        }
        public void updateSqueezed()
        {
            fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            sr = new StreamReader(fs);
            string record;
            string[] fields;
            //int steps = 0;

            FileStream SQfile = new FileStream(squeezedFilePath, FileMode.Create, FileAccess.Write);
            StreamWriter SQR = new StreamWriter(SQfile);
            while ((record = sr.ReadLine()) != null)
            {
                if (record[0] != '*')
                {
                    SQR.WriteLine(record);
                    SQR.Flush();
                }

            }
            SQR.Close();
            SQfile.Close();
            sr.Close();
            fs.Close();
        }
        public void LoadProductsToDataGridView()
        {
            // Clear existing data
            dataGridView.Rows.Clear();

            // Get all products from the stack
            string updateRec;
            FileStream database = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            StreamReader dbSR = new StreamReader(database);
            FileStream SQfile = new FileStream(squeezedFilePath, FileMode.Truncate, FileAccess.Write);
            StreamWriter SQR = new StreamWriter(SQfile);

            while ((updateRec = dbSR.ReadLine()) != null)
            {
                if (updateRec[0] != '*')
                {
                    SQR.WriteLine(updateRec);
                    SQR.Flush();
                }

            }

            SQR.Close();
            SQfile.Close();
            fs = new FileStream(squeezedFilePath, FileMode.Open, FileAccess.Read);
            sr = new StreamReader(fs);
            string record;
            string[] fields;

            while ((record = sr.ReadLine()) != null)
            {
                fields = record.Split(",");
                dataGridView.Rows.Add(fields[1], fields[0], fields[2], fields[3], fields[4]);
            }
            sr.Close();
            fs.Close();
            database.Close();
            dbSR.Close();

            //// Ensure that the entire application is closed when MyForm is closed
            this.FormClosed += (sender, e) => Application.Exit();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs dataGrid)
        {
            if (dataGrid.RowIndex >= 0)
            {
                int columnIndex = dataGrid.ColumnIndex;
                int rowIndex = dataGrid.RowIndex;

                // Check if the "Sell" button is clicked
                if (columnIndex == dataGridView.Columns["Column6"].Index)
                {
                    string prodId = dataGridView.Rows[rowIndex].Cells[1].Value.ToString();
                    int id = int.Parse(prodId);
                    SellProductForm sellProduct = new SellProductForm(id, 'S');
                    sellProduct.ShowDialog();

                }
                // Check if the "Delete" button is clicked
                 if (columnIndex == dataGridView.Columns["Column7"].Index)
                {
                    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                    StreamReader sr = new StreamReader(fs);
                    StreamWriter sw = new StreamWriter(fs);
                    fs.Seek(0, SeekOrigin.Begin);
                    fs.Flush();
                    sw.Flush();

                    string record;
                    string[] fields;
                    int steps = 0;

                    while ((record = sr.ReadLine()) != null)
                    {
                        fields = record.Split(",");
                        int id = int.Parse(dataGridView.Rows[rowIndex].Cells[1].Value.ToString());
                        try
                        {
                            if (!fields[0].Contains("*"))
                            {
                                if (id == int.Parse(fields[0]))
                                {
                                    fs.Seek(steps, SeekOrigin.Begin);
                                    sw.Write("*");
                                    sw.Flush();
                                    MessageBox.Show("Deleted");
                                    
                                }
                            }

                        }catch(IOException error){
                            Console.WriteLine(error.Message);
                        }
                        steps += record.Length + 2;
                    }


                    fs.Seek(0 , SeekOrigin.Begin);
                    fs.Close();
                    //update squeezed
                    //FileStream SQfile = new FileStream(squeezedFilePath, FileMode.Create, FileAccess.Write);
                    //StreamWriter SQR = new StreamWriter(SQfile);
                    //while ((record = sr.ReadLine()) != null)
                    //{
                    //    if (record[0] != '*')
                    //    {
                    //        SQR.WriteLine(record);
                    //        SQR.Flush();
                    //    }

                    //}
                    //SQR.Close();
                    //SQfile.Close();

                    //MessageBox.Show("Squeezeddd");
                    //sr.Close();
                    LoadProductsToDataGridView();

                }
            }
        }

        private void UpdateDataGridViewWithProduct(Product product)
        {
            // Clear existing data
            dataGridView.Rows.Clear();

            // Update UI with the retrieved product information
            dataGridView.Rows.Add(product.Name, product.ID, product.Category, product.Quantity, product.TotalPrice() + "$", "Edit", "Delete");
        }

        private void cancelSearchButton_Click(object sender, EventArgs e)
        {
            searchTextBox.Text = "";
            LoadProductsToDataGridView();
        }

        private void home2Button_Click(object sender, EventArgs e)
        {
            HomepageForm home = new HomepageForm();
            this.Hide();
            home.Show();
        }

        private void searchIcon_Click_1(object sender, EventArgs e)
        {
            fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            sr = new StreamReader(fs);
            string record;
            string[] fields;

            if (searchTextBox.Text != "")
            {
                int proID = int.Parse(searchTextBox.Text);
                while ((record = sr.ReadLine()) != null)
                {
                    fields = record.Split(",");
                    if (!fields[0].Contains("*"))
                    {
                        if (proID == int.Parse(fields[0]))
                        {
                            dataGridView.Rows.Clear();
                            dataGridView.Rows.Add(fields[1], fields[0], fields[2], fields[3], fields[4]);
                            MessageBox.Show("Found");
                            sr.Close();
                            fs.Close();
                            return;
                        }
                    }
                }
                MessageBox.Show("Not Found");
                LoadProductsToDataGridView();
            }
            else
            {
                dataGridView.Rows.Clear();
                while ((record = sr.ReadLine()) != null)
                {
                    fields = record.Split(",");
                    dataGridView.Rows.Add(fields[1], fields[0], fields[2], fields[3], fields[4]);
                }
            }          
            
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            dataGridView.CellContentClick += dataGridView_CellContentClick;

            // Load existing products into dataGridView
            LoadProductsToDataGridView();
        }
    }
}