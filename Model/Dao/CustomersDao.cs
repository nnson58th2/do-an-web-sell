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

        public IEnumerable<KhachHang> ListAllPaging(string search, int page, int pageSize)
        {
            IQueryable<KhachHang> model = db.KhachHang;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.HoKH.Contains(search) || x.TenKH.Contains(search) || x.SDT.Contains(search) || x.DiaChi.Contains(search));
            }
            return model.OrderBy(x => x.TenKH).ToPagedList(page, pageSize);
        }
    }
}
