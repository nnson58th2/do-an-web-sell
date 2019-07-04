using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.Dao
{
    public class UserDao
    {
        DatabaseSellEntities db = null;
        public UserDao()
        {
            db = new DatabaseSellEntities();
        }
        public long Insert(QuanTri entity)
        {
            db.QuanTri.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public bool Update(QuanTri entity)
        {
            try
            {
                var user = db.QuanTri.Find(entity.Id);
                user.UserName = entity.UserName;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.HoTen = entity.HoTen;
                user.DienThoai = entity.DienThoai;
                user.Email = entity.Email;
                user.DiaChi = entity.DiaChi;
                user.Quyen = entity.Quyen;
                user.TrangThai = entity.TrangThai;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }

        public IEnumerable<QuanTri> ListAllPaging(string search, int page, int pageSize)
        {
            IQueryable<QuanTri> model = db.QuanTri;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.UserName.Contains(search) || x.HoTen.Contains(search));
            }
            return model.OrderBy(x => x.Id).ToPagedList(page, pageSize);
        }

        public QuanTri GetById(string userName)
        {
            return db.QuanTri.SingleOrDefault(x => x.UserName == userName);
        }

        public QuanTri ViewDetail(int id)
        {
            return db.QuanTri.Find(id);
        }

        public int LoginAdmin(string userName, string passWord, bool power)
        {
            var result = db.QuanTri.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.TrangThai == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Quyen == false)
                        return -3;
                    else
                    {
                        if (result.Password == passWord && result.Quyen == true)
                            return 1;
                        else
                            return -2;
                    }
                }
            }
        }

        public int LoginUser(string userName, string passWord)
        {
            var result = db.QuanTri.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.TrangThai == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else
                        return -2;
                }
            }
        }

        public bool Delete(long id)
        {
            try
            {
                var user = db.QuanTri.Find(id);
                db.QuanTri.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckUserName(string userName)
        {
            return db.QuanTri.Count(x => x.UserName == userName) > 0;
        }

        public bool CheckEmail(string email)
        {
            return db.QuanTri.Count(x => x.Email == email) > 0;
        }

        public bool ChangeStatus (long id)
        {
            var user = db.QuanTri.Find(id);
            user.TrangThai = !user.TrangThai;
            db.SaveChanges();
            return user.TrangThai;
        }
    }
}
