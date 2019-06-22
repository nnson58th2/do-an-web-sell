using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.Dao
{
    public class ProductCategoryDao
    {
        DatabaseSellEntities db = null;
        public ProductCategoryDao()
        {
            db = new DatabaseSellEntities();
        }

        public List<DanhMucSanPham> ListAll()
        {
            return db.DanhMucSanPham.ToList();
        }

        public DanhMucSanPham ViewDetail(long id)
        {
            return db.DanhMucSanPham.Find(id);
        }

        public IEnumerable<DanhMucSanPham> ListAllPaging(string search, int page, int pageSize)
        {
            IQueryable<DanhMucSanPham> model = db.DanhMucSanPham;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.Contains(search));
            }
            return model.OrderBy(x => x.Name).ToPagedList(page, pageSize);
        }
    }
}
