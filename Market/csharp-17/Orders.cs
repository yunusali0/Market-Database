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
    public partial class Orders : Form
    {
        ShoppingEntities db = new ShoppingEntities();
        Stock stock;
        decimal Total = 0;
        public Orders()
        {
            InitializeComponent();
            FillOrder();
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            lblProduct.Text = "";
            lblType.Text = "";
            stock = db.Stocks.FirstOrDefault(s => s.Barcode == txtBarcode.Text);
            if (stock != null)
            {
                lblProduct.Text = stock.Product.Name +" "+ Convert.ToDecimal(stock.Product.Price).ToString("#.##") ;
                lblType.Text = stock.Product.Type.Name;
                txtCount.Text = 1.ToString();
            }
        }

        private void btnCartAdd_Click(object sender, EventArgs e)
        {
            if (this.stock != null)
            {
                decimal count = 0;
                if(!decimal.TryParse(txtCount.Text,out count))
                {
                    MessageBox.Show("Duzgun say girin");
                    return;
                }

                if (dgvCart.Rows.Count > 0)
                {
                    dgvCart.Rows.RemoveAt(dgvCart.Rows.Count - 1);
                }
                
                decimal price = Convert.ToDecimal(this.stock.Product.Price);
                Total += price * count;
                dgvCart.Rows.Add(
                    this.stock.Id,
                    this.stock.Product.Name,
                    count,
                    price.ToString("#.##"),
                    (price * count).ToString("#.##")
               );

                dgvCart.Rows.Add(
                    "",
                    "",
                    "",
                    "",
                    Total.ToString("#.##")
                    );

                txtBarcode.Text = "";
                txtCount.Text = "";
                lblProduct.Text = "";
                lblType.Text = "";
            }
            else
            {
                MessageBox.Show("Duzgun Barcode yazin");
            }
        }

        private void btnOrderSubmit_Click(object sender, EventArgs e)
        {
            if (dgvCart.Rows.Count > 0)
            {
                DialogResult r = MessageBox.Show("Sifariş tamamlansın mi?", "Sifariş", MessageBoxButtons.OKCancel);
                if(r == DialogResult.OK)
                {
                    Order order = new Order
                    {
                        Date = DateTime.Now,
                        Total = this.Total
                    };
                    db.Orders.Add(order);
                    db.SaveChanges();

                    for (int i = 0; i < dgvCart.Rows.Count-1; i++)
                    {
                        OrderItem item = new OrderItem
                        {
                            OrderId = order.Id,
                            StockId = Convert.ToInt32(dgvCart.Rows[i].Cells[0].Value.ToString()),
                            Count = Convert.ToDecimal(dgvCart.Rows[i].Cells[2].Value.ToString()),
                            SellPrice = Convert.ToDecimal(dgvCart.Rows[i].Cells[3].Value.ToString()),
                        };

                        db.OrderItems.Add(item);
                    }
                    db.SaveChanges();
                    dgvCart.Rows.Clear();
                    Total = 0;
                    stock = null;
                }
            }
            else
            {
                MessageBox.Show("Sebetde hec bir mehsul yoxdur");
            }
           
        }

        private void dgvCart_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(dgvCart.Rows[e.RowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                DialogResult r = MessageBox.Show("Sifariş tamamlansın mi?", "Sifariş", MessageBoxButtons.OKCancel);
                if (r == DialogResult.OK)
                {
                    decimal deletedPrice = Convert.ToDecimal(dgvCart.Rows[e.RowIndex].Cells[4].Value.ToString());

                    dgvCart.Rows.RemoveAt(e.RowIndex);
                    if (dgvCart.Rows.Count > 1)
                    {
                        dgvCart.Rows[dgvCart.Rows.Count - 1].Cells[4].Value = (Total -= deletedPrice).ToString("#.##");
                    }
                    else
                    {
                        dgvCart.Rows.RemoveAt(0);
                        Total = 0;
                    }
                }
            }
        }

        private void FillOrder()
        {
            foreach (Order item in db.Orders.OrderByDescending(o=>o.Date).ToList())
            {
                DataGridViewButtonCell btn = new DataGridViewButtonCell();
                btn.Value = "Items";
                
                dgvOrders.Rows.Add(
                    item.Id,
                    item.Date.Value.ToString("dd.MM.yyyy"),
                    Convert.ToDecimal(item.Total).ToString("#.##"),
                    item.OrderItems.Count,
                    btn
                    );
            }
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                int orderId = Convert.ToInt32(dgvOrders.Rows[e.RowIndex].Cells[0].Value.ToString());
                Invoice invoice = new Invoice(orderId);
                invoice.ShowDialog();

            }
        }
    }
}
