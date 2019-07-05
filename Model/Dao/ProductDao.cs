using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using Model.ViewModel;
using PagedList;

namespace Model.Dao
{
    public class ProductDao
    {
        DatabaseSellEntities db = null;
        public ProductDao()
        {
            db = new DatabaseSellEntities();
        }

        //Thêm sản phẩm vào database
        public long Insert(SanPham entity)
        {
            db.SanPham.Add(entity);
            db.SaveChanges();
            return entity.MaSP;
        }

        public IEnumerable<SanPham> ListAllPaging(string search, int page, int pageSize)
        {
            IQueryable<SanPham> model = db.SanPham;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.TenSP.Contains(search));
            }
            return model.OrderBy(x => x.TenSP).ToPagedList(page, pageSize);
        }

        public IEnumerable<SanPham> ListAllProduct(int page, int pageSize)
        {
            IQueryable<SanPham> model = db.SanPham;
            return model.OrderBy(x => x.TenSP).ToPagedList(page, pageSize);
        }

        public List<SanPham> ListNewProduct(int top)
        {
            return db.SanPham.OrderByDescending(x => x.NgayTao).Take(top).ToList();
        }

        public List<string> ListName(string keyword)
        {
            return db.SanPham.Where(x => x.TenSP.Contains(keyword)).Select(x => x.TenSP).ToList();
        }

        /// <summary>
        /// Get list product by category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>

        public List<SanPham> ListRelatedProducts(long productId, int top)
        {
            var product = db.SanPham.Find(productId);
            return db.SanPham.Where(x => x.MaSP != productId && x.DanhMucSanPhamID == product.DanhMucSanPhamID).Take(top).ToList();
        }

        public IEnumerable<SanPham> ListByCategoryId(long categoryID, int page, int pageSize)
        {
            IQueryable<SanPham> model = db.SanPham;
            return model.Where(x => x.DanhMucSanPhamID == categoryID).OrderBy(x => x.NgayTao).ToPagedList(page, pageSize);
        }

        public IEnumerable<SanPham> Search(string search, int page, int pageSize)
        {
            IQueryable<SanPham> model = db.SanPham;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.TenSP.Contains(search));
            }
            return model.OrderBy(x => x.TenSP).ToPagedList(page, pageSize);
        }

        public SanPham ViewDetail(long id)
        {
            return db.SanPham.Find(id);
        }
    }
}
