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
    }
}
