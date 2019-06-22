using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class EmployeesDao
    {
        DatabaseSellEntities db = null;
        public EmployeesDao()
        {
            db = new DatabaseSellEntities();
        }

        public IEnumerable<NhanVien> ListAllPaging(string search, int page, int pageSize)
        {
            IQueryable<NhanVien> model = db.NhanVien;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.MaNV.Contains(search) || x.TenNV.Contains(search) || x.SDT.Contains(search));
            }
            return model.OrderBy(x => x.MaNV).ToPagedList(page, pageSize);
        }
    }
}
