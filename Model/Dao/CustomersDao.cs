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

        // GET: Mã khách tự động
        string getKeyCustomer()
        {
            var maMax = db.KhachHang.ToList().Select(n => n.MaKH).Max();
            int maKH = int.Parse(maMax.Substring(2)) + 1;
            string KH = String.Concat("000", maKH.ToString());
            return "KH" + KH.Substring(maKH.ToString().Length - 1);
        }

        public string Insert(KhachHang entity)
        {
            entity.MaKH = getKeyCustomer();
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
