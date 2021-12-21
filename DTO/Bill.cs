using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManagement.DTO
{
    public class Bill
    {
        private int iD;
        private DateTime? dateCheckIn;
        private DateTime? dateCheckOut;
        private int status;
        public int ID { get => iD; set => iD = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Status { get => status; set => status = value; }
        public int Discount { get => discount; set => discount = value; }

        public Bill(int id, DateTime? dateChechin, DateTime? dateCheckout, int status, int discount = 0)
        {
            this.ID = id;
            this.DateCheckIn = dateChechin;
            this.DateCheckOut = dateCheckout;
            this.Status = status;
            this.Discount = discount;
        }
        public Bill(DataRow row)
        {
            this.ID = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["DateCheckIn"];
            this.DateCheckOut = (DateTime?)row["DateCheckOut"];
            this.Status = (int)row["status"];
                if (row["discount"].ToString() != "")
                this.Discount = (int)row["discount"];
        }
        private int discount;

    }
}
