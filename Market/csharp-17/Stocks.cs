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
    public partial class Stocks : Form
    {
        ShoppingEntities db = new ShoppingEntities();
        private Stock selected = new Stock();
        int clickRow = 0;
        public Stocks()
        {
            InitializeComponent();
            FillStocks();
            FillProduct();
        }

        private void FillStocks()
        {
            dgvStocks.Rows.Clear();
            var list = db.Stocks.Select(s => new
            {
                s.Id,
                s.Count,
                Selled = s.OrderItems,
                Product = s.Product.Name,
                s.Barcode,
                EnterDate = s.EnterDate.Value,
                ExpireDate = s.ExpireDate.Value
            }).ToList();

            foreach (var item in list)
            {
                dgvStocks.Rows.Add(
                    item.Id,
                    (item.Count%1>0?item.Count:(int)item.Count),
                    (item.Selled != null ? item.Selled.Sum(s => s.Count) : 0),
                    item.Product,
                    item.Barcode,
                    item.EnterDate.ToString("dd.MM.yyyy"),
                    item.ExpireDate.ToString("dd.MM.yyyy")
                    );
            }

            dgvStocks.Columns[0].Visible = false;
        }

        private void FillProduct()
        {
            foreach (Model.Product item in db.Products.ToList())
            {
                cmbProuct.Items.Add(item.Id + "-" + item.Name);
                cmbSearchProduct.Items.Add(item.Id + "-" + item.Name);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            lblError1.Text = "";
            string product = cmbProuct.Text;
            string barcode = txtBarcode.Text;
            string count = txtCount.Text;

            if (product == string.Empty || barcode == string.Empty || count == string.Empty)
            {
                lblError1.Text = "* olan yerleri doldurun";
                return;
            }

            if(!decimal.TryParse(count,out decimal Count))
            {
                lblError1.Text = "Sayı düzgün yazın";
                return;
            }
            int ExpireDate = 0;
            if (txtExpireDate.Text != string.Empty)
            {
                if(!int.TryParse(txtExpireDate.Text,out ExpireDate))
                {
                    lblError1.Text = "Mehsul bitme vaxtini duzgun daxil edin";
                    return;
                }
            }

            Stock finded = db.Stocks.FirstOrDefault(s => s.Barcode == barcode);
            if (finded != null)
            {
                finded.Count += Count;
                finded.ExpireDate = DateTime.Now.AddDays(ExpireDate);
                db.SaveChanges();

                DataGridViewRow row = dgvStocks.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Cells[0].Value.ToString() == finded.Id.ToString());
                dgvStocks.Rows[row.Index].Cells[1].Value = finded.Count.ToString();
                dgvStocks.Rows[row.Index].Cells[6].Value = finded.ExpireDate.Value.ToString("dd.MM.yyyy");
                Reset();
            }
            else
            {
                Stock stock = new Stock
                {
                    ProductId = Convert.ToInt32(product.Split('-')[0]),
                    Barcode = barcode,
                    Count = Count,
                    EnterDate = DateTime.Now,
                    ExpireDate = DateTime.Now.AddDays(ExpireDate),
                };
                db.Stocks.Add(stock);
                db.SaveChanges();

                dgvStocks.Rows.Add(
                    stock.Id,
                    stock.Count,
                    (stock.OrderItems != null ? stock.OrderItems.Sum(o => o.Count) : 0),
                    stock.Product.Name,
                    stock.Barcode,
                    stock.EnterDate.Value.ToString("dd.MM.yyyy"),
                    stock.ExpireDate.Value.ToString("dd.MM.yyyy"));
                Reset();
            }
        }

       private void Reset()
        {
            txtBarcode.Text = "";
            txtCount.Text = "";
            cmbProuct.Text = "";
            txtExpireDate.Text = "";

            btnAdd.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
        }

        private void dgvStocks_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tbcTab.SelectedTab = tabAdd;
            int id = Convert.ToInt32(dgvStocks.Rows[e.RowIndex].Cells[0].Value.ToString());
            this.selected = db.Stocks.Find(id);
            this.clickRow = e.RowIndex;

            txtBarcode.Text = this.selected.Barcode;
            txtCount.Text = (this.selected.Count%1>0?this.selected.Count:(int)this.selected.Count).ToString();
            if (this.txtExpireDate != null)
            {
                txtExpireDate.Text = Math.Ceiling(this.selected.ExpireDate.Value.Subtract(DateTime.Now).TotalDays).ToString();
            }
            cmbProuct.Text = this.selected.ProductId + "-" + this.selected.Product.Name;

            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.selected.OrderItems.Count==0)
            {
                DialogResult r = MessageBox.Show("Eminsiniz mi ?", "Silme", MessageBoxButtons.OKCancel);
                if (r == DialogResult.OK)
                {
                    db.Stocks.Remove(this.selected);
                    db.SaveChanges();
                    dgvStocks.Rows.RemoveAt(this.clickRow);
                    Reset();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            lblError1.Text = "";
            string product = cmbProuct.Text;
            string barcode = txtBarcode.Text;
            string count = txtCount.Text;

            if (product == string.Empty || barcode == string.Empty || count == string.Empty)
            {
                lblError1.Text = "* olan yerleri doldurun";
                return;
            }

            if (!decimal.TryParse(count, out decimal Count))
            {
                lblError1.Text = "Sayı düzgün yazın";
                return;
            }
            int ExpireDate = 0;
            if (txtExpireDate.Text != string.Empty)
            {
                if (!int.TryParse(txtExpireDate.Text, out ExpireDate))
                {
                    lblError1.Text = "Mehsul bitme vaxtini duzgun daxil edin";
                    return;
                }
            }

            if(db.Stocks.FirstOrDefault(s => s.Barcode == barcode && s.Id != this.selected.Id) != null)
            {
                lblError1.Text = "Bu barcode baska mehsulda istifa olunub";
                return;
            }

            this.selected.ProductId = Convert.ToInt32(product.Split('-')[0]);
            this.selected.Barcode = barcode;
            this.selected.Count = Count;
            this.selected.ExpireDate = DateTime.Now.AddDays(ExpireDate);
            db.SaveChanges();

            dgvStocks.Rows[this.clickRow].Cells[1].Value = this.selected.Count.ToString();
            dgvStocks.Rows[this.clickRow].Cells[3].Value = this.selected.Product.Name;
            dgvStocks.Rows[this.clickRow].Cells[4].Value = this.selected.Barcode;
            dgvStocks.Rows[this.clickRow].Cells[6].Value = this.selected.ExpireDate.Value.ToString("dd.MM.yyyy");

            Reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowAll();
            var others = dgvStocks.Rows.Cast<DataGridViewRow>();

            if (cmbSearchProduct.Text != string.Empty)
            {
                string product = cmbSearchProduct.Text.Split('-')[1];
                others = others.Where(r => r.Cells[3].Value.ToString() != product);
            }

            if (txtSearchBarcode.Text != string.Empty)
            {
                string barcode = txtSearchBarcode.Text;
                others = others.Where(r => !(r.Cells[4].Value.ToString().Contains(barcode)));
            }

            if (dtpSearchEnterDate.Value.ToString("dd.MM.yyyy")!="29.01.1755")
            {
                DateTime enterdate = dtpSearchEnterDate.Value;
                others = others.Where(r => r.Cells[5].Value.ToString() != enterdate.ToString("dd.MM.yyyy"));
            }

            if (txtSearchExpireDate.Text !=string.Empty)
            {
                DateTime searchday = DateTime.Now;
                int day = 0;
                if(!int.TryParse(txtSearchExpireDate.Text,out day))
                {
                    MessageBox.Show("dergah esebidi");
                    return;
                }
                searchday = searchday.AddDays(day);

                others = others.Where(r => r.Cells[6].Value.ToString() != searchday.ToString("dd.MM.yyyy"));

            }

            foreach (DataGridViewRow item in others.ToList())
            {
                dgvStocks.Rows[item.Index].Visible = false;
            }
        }
        private void ShowAll()
        {
            foreach (DataGridViewRow item in dgvStocks.Rows)
            {
                dgvStocks.Rows[item.Index].Visible = true ;
            }
        }

        private void tabAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
