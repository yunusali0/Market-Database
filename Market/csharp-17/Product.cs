using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using csharp_17.Model;

namespace csharp_17
{
    public partial class Product : Form
    {
        ShoppingEntities db = new ShoppingEntities();
        Model.Product selected = new Model.Product();
        public Product()
        {
            InitializeComponent();
            FillProducts();
            FillCategories();
            FillTypes();
        }
        private void FillProducts()
        {
            dgvProducts.DataSource = db.Products.Select(p => new
            {
               p.Id,
               p.Name,
               p.Price,
               Category = p.Category.Name,
               Type = p.Type.Name
            }).ToList();

            dgvProducts.Columns[0].Visible = false;
        }

        private void FillCategories()
        {
            foreach (Model.Category item in db.Categories.ToList())
            {
                cmbCategory.Items.Add(item.Id + "-" + item.Name);
            }
        }
        private void FillTypes()
        {
            foreach (Model.Type item in db.Types.ToList())
            {
                cmbTypes.Items.Add(item.Id + "-" + item.Name);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            string name = txtName.Text;
            string price = txtPrice.Text;
            string category = cmbCategory.Text;
            string type = cmbTypes.Text;

            if (name == string.Empty || price == string.Empty || category == string.Empty || type == string.Empty)
            {
                lblError.Text = "Bosluq buraxmayin";
                return;
            }

            if(!decimal.TryParse(price,out decimal Price))
            {
                lblError.Text = "Qiymeti duzgun yazin";
                return;
            }

            Model.Product prt = new Model.Product
            {
                Name = name,
                Price = Price,
                CategoryId = Convert.ToInt32(category.Split('-')[0]),
                TypeId = Convert.ToInt32(type.Split('-')[0]),
            };
            db.Products.Add(prt);
            db.SaveChanges();
            Reset();
        }

        private void Reset()
        {
            FillProducts();
            txtName.Text = "";
            txtPrice.Text = "";
            cmbCategory.Text = "";
            cmbTypes.Text = "";
            btnAdd.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = Convert.ToInt32(dgvProducts.Rows[e.RowIndex].Cells[0].Value.ToString());
            selected = db.Products.Find(id);
            txtName.Text = selected.Name;
            txtPrice.Text = selected.Price.ToString();
            cmbCategory.Text = selected.Category.Id + "-" + selected.Category.Name;
            cmbTypes.Text = selected.Type.Id + "-" + selected.Type.Name;
            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            string name = txtName.Text;
            string price = txtPrice.Text;
            string category = cmbCategory.Text;
            string type = cmbTypes.Text;

            if (name == string.Empty || price == string.Empty || category == string.Empty || type == string.Empty)
            {
                lblError.Text = "Bosluq buraxmayin";
                return;
            }

            if (!decimal.TryParse(price, out decimal Price))
            {
                lblError.Text = "Qiymeti duzgun yazin";
                return;
            }

            selected.Name = name;
            selected.Price = Price;
            selected.CategoryId = Convert.ToInt32(category.Split('-')[0]);
            selected.TypeId = Convert.ToInt32(type.Split('-')[0]);
            db.SaveChanges();
            Reset();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selected.Stocks.Count == 0)
            {
                DialogResult r = MessageBox.Show("Eminsiniz mi?", "Silme", MessageBoxButtons.OKCancel);
                if (r == DialogResult.OK)
                {
                    db.Products.Remove(selected);
                    db.SaveChanges();
                    Reset();
                }
            }
            else
            {
                MessageBox.Show("Bu mehsulu sile bilmezsiniz");
            }
        }
    }
}
