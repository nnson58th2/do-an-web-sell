using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class WarehouseDao
    {
        DatabaseSellEntities db = null;
        public WarehouseDao()
        {
            db = new DatabaseSellEntities();
        }

        public IEnumerable<KhoHang> ListAllPaging(string search, int page, int pageSize)
        {
            IQueryable<KhoHang> model = db.KhoHang;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.TenKho.Contains(search));
            }
            return model.OrderBy(x => x.MaKho).ToPagedList(page, pageSize);
        }

        public List<ChiTietKho> ListOrder(string id)
        {
            return db.ChiTietKho.Where(x => x.MaKho == id).ToList();
        }
    }
}
