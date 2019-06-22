using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class CategoryDao
    {
        DatabaseSellEntities db = null;
        public CategoryDao()
        {
            db = new DatabaseSellEntities();
        }    

        public List<SanPham> ListAll()
        {
            return db.SanPham.Where(x => x.TrangThai == true).ToList();
        }

        public DanhMucSanPham ViewDetail(long id)
        {
            return db.DanhMucSanPham.Find(id);
        }
    }
}
