using SumperM.Business.Services;
using SuperM.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperM.UI.WinForm
{
    public partial class Form1 : Form
    {
        private ProductService productService;
        private Product product { get; set; }

        public Form1()
        {
            InitializeComponent();
            productService = new ProductService();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'products._Products' table. You can move, or remove it, as needed.
            this.RefreshDataBinding();
        }

        private void RefreshDataBinding()
        {
            dataGridView1.DataSource = productService.AllProducts().ToList();
            dataGridView1.Refresh();
        }


        private void AddButton_Click(object sender, EventArgs e)
        {
            var name = (string)textBox1.Text;
            var price = decimal.Parse(textBox2.Text);
            var description = (string)textBox3.Text;
            var category = (string)textBox4.Text;

            Product product = new Product()
            {
                Name = name,
                Price = price,
                Description = description,
                Category = category
            };
            int? id = this.product.ProductId;
            if (id == null) { productService.Add(product); }
            else { MessageBox.Show("Error","You can't add modified product!"); }
            ClearInput();
            
        }

        private void ClearInput()
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
            this.textBox4.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            product = (Product)dataGridView1.CurrentRow.DataBoundItem;
            ClearInput();
            textBox1.Text = product.Name;
            textBox2.Text = product.Price.ToString();
            textBox3.Text = product.Description;
            textBox4.Text = product.Category;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            this.product.Name = (string)textBox1.Text;
            this.product.Price = decimal.Parse(textBox2.Text);
            this.product.Description = (string)textBox3.Text;
            this.product.Category = (string)textBox4.Text;



            if(this.product.ProductId > 0)
            {
                productService.Edit(product);
            }
            this.product = null;
            ClearInput();
        }
    }
}
