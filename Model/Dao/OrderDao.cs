using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.Dao
{
    public class OrderDao
    {
        DatabaseSellEntities db = null;
        public OrderDao()
        {
            db = new DatabaseSellEntities();
        }
        public long Insert(DonDatHang order)
        {
            db.DonDatHang.Add(order);
            db.SaveChanges();
            return order.MaDon;
        }

        public bool Delete(int id)
        {
            try
            {
                var order = db.DonDatHang.Find(id);
                db.DonDatHang.Remove(order);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        public IEnumerable<DonDatHang> ListAllPaging(string search, int page, int pageSize)
        {
            IQueryable<DonDatHang> model = db.DonDatHang;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.TenNguoiDat.Contains(search) || x.SoDienThoai.Contains(search));
            }
            return model.OrderBy(x => x.MaDon).ToPagedList(page, pageSize);
        }

        public DonDatHang ViewDetail(long id)
        {
            return db.DonDatHang.Find(id);
        }
    }
}
