//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class DonDatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonDatHang()
        {
            this.ChiTietDatHang = new HashSet<ChiTietDatHang>();
        }
    
        public long MaDon { get; set; }
        public Nullable<System.DateTime> NgayDat { get; set; }
        public string TenNguoiDat { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public Nullable<int> TrangThai { get; set; }
        public string MaNV { get; set; }
        public string MaKH { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDatHang> ChiTietDatHang { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
