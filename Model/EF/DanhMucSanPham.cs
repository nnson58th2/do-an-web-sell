﻿//------------------------------------------------------------------------------
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
    public partial class DanhMucSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhMucSanPham()
        {
            this.SanPham = new HashSet<SanPham>();
        }
    
        public long Id { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập tên danh mục")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập tiêu đề title")]
        public string MetaTitle { get; set; }

        public Nullable<long> ParentID { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập người tạo")]
        public string CreateBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPham { get; set; }
    }
}
