using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CustomersDao
    {
        DatabaseSellEntities db = null;
        public CustomersDao()
        {
            db = new DatabaseSellEntities();
        }

        public string Insert(KhachHang entity)
        {
            db.KhachHang.Add(entity);
            db.SaveChanges();
            return entity.MaKH;
        }

        public IEnumerable<KhachHang> ListAllPaging(string search, int page, int pageSize)
        {
            IQueryable<KhachHang> model = db.KhachHang;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.HoTenKH.Contains(search) || x.SDT.Contains(search) || x.DiaChi.Contains(search));
            }
            return model.OrderBy(x => x.HoTenKH).ToPagedList(page, pageSize);
        }
    }
}
