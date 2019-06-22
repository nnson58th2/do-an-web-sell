using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDetailDao
    {
        DatabaseSellEntities db = null;
        public OrderDetailDao()
        {
            db = new DatabaseSellEntities();
        }

        public bool Insert(ChiTietDatHang orderDetail)
        {
            try
            {
                db.ChiTietDatHang.Add(orderDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }           
        }

        public List<ChiTietDatHang> ListOrder(long id)
        {
            return db.ChiTietDatHang.Where(x => x.MaDon == id).ToList();
        }
    }
}
