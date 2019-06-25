using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class BaseDao
    {
        DatabaseSellEntities db = null;
        public BaseDao()
        {
            db = new DatabaseSellEntities();
        }

        public bool DeleteEmployees(string id)
        {
            try
            {
                var employees = db.NhanVien.Find(id);
                db.NhanVien.Remove(employees);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteProduct(long id)
        {
            try
            {
                var product = db.SanPham.Find(id);
                db.SanPham.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteWareHouse(string id)
        {
            try
            {
                var wareHouse = db.KhoHang.Find(id);
                db.KhoHang.Remove(wareHouse);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteSuppliere(string id)
        {
            try
            {
                var suppliere = db.NhaCungCap.Find(id);
                db.NhaCungCap.Remove(suppliere);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
