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

        public Form1()
        {
            InitializeComponent();
            productService = new ProductService();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'products._Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.products._Products);

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

            productService.Add(product);
            ClearInput();

        }

        private void ClearInput()
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
            this.textBox4.Clear();
        }
    }
}
