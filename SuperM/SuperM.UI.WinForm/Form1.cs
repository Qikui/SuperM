namespace SuperM.UI.WinForm
{
    using SumperM.Business.Services;
    using SuperM.Data.Entities;
    using System;
    using System.Linq;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        private ProductService productService;

        private InventoryService inventoryService;

        public Form1()
        {
            InitializeComponent();
            productService = new ProductService();
            inventoryService = new InventoryService();
        }

        private Product product { get; set; }

        private Inventory inventory { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.RefreshAllBindings();
        }

        private void RefreshDataBinding()
        {
            dataGridView1.DataSource = productService.AllProducts().ToList();
            dataGridView1.Refresh();
        }

        private void RefreshProductDataBinding()
        {
            dataGridView2.DataSource = productService.AllProducts().ToList();
            dataGridView2.Refresh();
        }

        private void RefreshInventoryDataBinding()
        {
            dataGridView3.DataSource = inventoryService.AllInventories();
            dataGridView3.Refresh();
        }

        private void RefreshAllBindings()
        {
            this.RefreshDataBinding();
            this.RefreshInventoryDataBinding();
            this.RefreshProductDataBinding();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var name = (string)textBox1.Text;
            if (name == "")
            {
                MessageBox.Show("Name is a must field!");
                ClearInput();
                return;
            }
            decimal price = 0.0m;
            if (textBox2.Text == "" || decimal.TryParse(textBox2.Text, out price))
            {
                if (price <= 0.0m)
                {
                    MessageBox.Show("invalid price amount!");
                    ClearInput();
                    return;
                }

            }

            var description = (string)textBox3.Text;
            var category = (string)textBox4.Text;
            bool isSafe = (bool)checkBox1.Checked;

            Product product = new Product()
            {
                Name = name,
                Price = price,
                Description = description,
                Category = category,
                IsSafe = isSafe
            };
            if (this.product == null)
            {
                productService.Add(product);
                this.RefreshDataBinding();
                MessageBox.Show("Your new product has been added!");
            }
            else { MessageBox.Show("Error", "You can't add modified product! Please redo it"); this.product = null; }
            ClearInput();
        }

        private void ClearInput()
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
            this.textBox4.Clear();
            checkBox1.Checked = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            product = (Product)dataGridView1.CurrentRow.DataBoundItem;
            ClearInput();
            textBox1.Text = product.Name;
            textBox2.Text = product.Price.ToString();
            textBox3.Text = product.Description;
            textBox4.Text = product.Category;
            if (product.IsSafe == true)
            {
                checkBox1.Checked = true;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (this.product != null)
            {
                this.product.Name = (string)textBox1.Text;
                this.product.Price = decimal.Parse(textBox2.Text);
                this.product.Description = (string)textBox3.Text;
                this.product.Category = (string)textBox4.Text;
                this.product.IsSafe = (bool)checkBox1.Checked;

                productService.Edit(product);
                this.RefreshDataBinding();
                MessageBox.Show("Your modification has been successfully saved!");
            }
            else
            {
                MessageBox.Show("You can't save a new product! Please use ADD button");
            }
            this.product = null;
            ClearInput();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearInput();
            this.product = null;
        }

        private void ClearInventoryControlForm()
        {
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            product = (Product)dataGridView2.CurrentRow.DataBoundItem;
            inventory = inventoryService.GetInventory(product);
            if (inventory != null)
            {
                textBox5.Text = inventory.InventoryId.ToString();
                textBox6.Text = product.Name;
                textBox7.Text = inventory.Qty.ToString();
                textBox8.Text = inventory.Description;
            }
            else
            {
                ClearInventoryControlForm();
                textBox5.Text = "TBD";
                textBox6.Text = product.Name;
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            inventory = (Inventory)dataGridView3.CurrentRow.DataBoundItem;
            product = inventoryService.GetProduct(inventory.InventoryId);

            textBox5.Text = inventory.InventoryId.ToString();
            textBox6.Text = product.Name;
            textBox7.Text = inventory.Qty.ToString();
            textBox8.Text = inventory.Description;
        }

        private void button4_Click(object sender, EventArgs e) //clear form
        {
            ClearInventoryControlForm();
            this.product = null;
            this.inventory = null;
        }

        private void button2_Click(object sender, EventArgs e) //add
        {
            if (this.product == null)
            {
                MessageBox.Show("Select a product first");
            }
            else
            {
                Inventory newInventory = new Inventory()
                {
                    Qty = int.Parse(textBox7.Text),
                    Description = textBox8.Text,
                    ProductId = this.product.ProductId,
                    //Product = productService.GetById(this.product.ProductId)
                };
                inventoryService.Add(newInventory);
                RefreshInventoryDataBinding();
                RefreshProductDataBinding();
            }
            ClearInventoryControlForm();
            this.product = null;
            this.inventory = null;
        }

        private void button3_Click(object sender, EventArgs e)//edit
        {
            if (this.product != null && this.inventory != null)
            {
                Inventory newInventory = new Inventory()
                {
                    InventoryId = int.Parse(textBox5.Text),
                    Qty = int.Parse(textBox7.Text),
                    Description = textBox8.Text,
                    ProductId = this.product.ProductId,
                    Product = productService.GetById(this.product.ProductId)
                };
                inventoryService.Edit(newInventory);
                RefreshInventoryDataBinding();
            }
            else
            {
                MessageBox.Show("Use ADD new inventory for adding NEW inventory");
            }
        }

        private void button6_Click(object sender, EventArgs e) //delete
        {
            if (this.inventory != null)
            {
                inventoryService.Delete(this.inventory);
                RefreshInventoryDataBinding();
            }
            ClearInventoryControlForm();
            this.product = null;
            this.inventory = null;
        }
    }
}
