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
    using System.ComponentModel.DataAnnotations;
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.ChiTietDatHang = new HashSet<ChiTietDatHang>();
            this.ChiTietKho = new HashSet<ChiTietKho>();
            this.KhoHang = new HashSet<KhoHang>();
        }
    
        public long MaSP { get; set; }
        public string TenSP { get; set; }
        public string MetaTitle { get; set; }
        public int SoLuong { get; set; }
        public Nullable<decimal> DonGia { get; set; }
        public Nullable<decimal> GiaKhuyenMai { get; set; }
        public string HinhAnh { get; set; }
        public string MoTa { get; set; }
        public string ChiTiet { get; set; }
        public string ThoiHanBaoHanh { get; set; }
        public Nullable<System.DateTime> NgaySanXuat { get; set; }
        public Nullable<bool> TrangThai { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public string TaoBoi { get; set; }
        public Nullable<long> DanhMucSanPhamID { get; set; }
        public string MaNCC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDatHang> ChiTietDatHang { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietKho> ChiTietKho { get; set; }
        public virtual DanhMucSanPham DanhMucSanPham { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhoHang> KhoHang { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
