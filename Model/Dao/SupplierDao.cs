using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SupplierDao
    {
        DatabaseSellEntities db = null;
        public SupplierDao()
        {
            db = new DatabaseSellEntities();
        }

        public IEnumerable<NhaCungCap> ListAllPaging(string search, int page, int pageSize)
        {
            IQueryable<NhaCungCap> model = db.NhaCungCap;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.TenNCC.Contains(search) || x.DiaChi.Contains(search) || x.SoDienThoai.Contains(search));
            }
            return model.OrderBy(x => x.MaNCC).ToPagedList(page, pageSize);
        }
    }
}
