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
    public partial class Category : Form
    {
        ShoppingEntities db = new ShoppingEntities();
        Model.Category selected = new Model.Category();
        public Category()
        {
            InitializeComponent();
            FillCategories();
        }

        private void FillCategories()
        {
            dgvCategories.DataSource = db.Categories.Select(c => new
            {
                id = c.Id,
                Name = c.Name,
                Product = c.Products.Count
            }).ToList();

            dgvCategories.Columns[0].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            if (name == string.Empty)
            {
                lblError.Text = "Bosluq buraxmayin";
                return;
            }

            Model.Category cat = new Model.Category
            {
                Name = name
            };
            db.Categories.Add(cat);
            db.SaveChanges();
            FillCategories();
            txtName.Text = "";
        }

        private void dgvCategories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = Convert.ToInt32(dgvCategories.Rows[e.RowIndex].Cells[0].Value.ToString());
            selected = db.Categories.Find(id);
            txtName.Text = selected.Name;
            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
        }

        private void Reset()
        {
            FillCategories();
            txtName.Text = "";
            btnAdd.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            if (name == string.Empty)
            {
                lblError.Text = "Bosluq buraxmayin";
                return;
            }

            selected.Name = name;
            db.SaveChanges();

            Reset();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selected.Products.Count == 0)
            {
               DialogResult r=  MessageBox.Show("Eminsiniz mi?", "Silme", MessageBoxButtons.OKCancel);
               if(r == DialogResult.OK)
                {
                    db.Categories.Remove(selected);
                    db.SaveChanges();
                    Reset();
                }
            }
            else
            {
                MessageBox.Show("Bu kategoriyani sile bilmezsiniz");
            }
        }
    }
}
