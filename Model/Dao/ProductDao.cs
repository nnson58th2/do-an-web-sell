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
        public List<SanPham> ListByCategoryId(long categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.SanPham.Where(x => x.DanhMucSanPhamID == categoryID).Count();
            var model =  db.SanPham.Where(x => x.DanhMucSanPhamID == categoryID).OrderByDescending(x => x.NgayTao).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return model;
        }

        public List<SanPham> ListRelatedProducts(long productId)
        {
            var product = db.SanPham.Find(productId);
            return db.SanPham.Where(x => x.MaSP != productId && x.DanhMucSanPhamID == product.DanhMucSanPhamID).ToList();
        }

        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 4)
        {
            totalRecord = db.SanPham.Where(x => x.TenSP == keyword).Count();
            var model = (from x in db.SanPham
                         join y in db.DanhMucSanPham
                         on x.DanhMucSanPhamID equals y.Id
                         where x.TenSP.Contains(keyword)
                         select new
                         {
                             CateMetaTitle = y.MetaTitle,
                             CateName = y.Name,
                             CreatedDate = x.NgayTao,
                             ID = x.MaSP,
                             Images = x.HinhAnh,
                             Name = x.TenSP,
                             MetaTitle = x.MetaTitle,
                             Price = x.DonGia,
                             PromotionPrice = x.GiaKhuyenMai
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price,
                             PromotionPrice = x.PromotionPrice
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }

        public SanPham ViewDetail(long id)
        {
            return db.SanPham.Find(id);
        }
    }
}
