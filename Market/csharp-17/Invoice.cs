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
    public partial class Invoice : Form
    {
        ShoppingEntities db = new ShoppingEntities();
        public int OrderId { get; set; }

        public Invoice(int id)
        {
            this.OrderId = id;
            InitializeComponent();
            FillItems();
        }

        private void FillItems()
        {
            foreach (OrderItem item in db.OrderItems.Where(oi => oi.OrderId == this.OrderId).ToList())
            {
                decimal count = Convert.ToDecimal(item.Count);
                decimal price = Convert.ToDecimal(item.SellPrice);
                dgvItems.Rows.Add(
                    item.Stock.Product.Name,
                    price.ToString("#.##"),
                    count.ToString("#.##"),
                    (price * count).ToString("#.##")
                    );
            }
        }
    }
}
