using System;
using System.Web;

namespace QLTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ShiftsModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShiftsModel()
        {

        }


        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(50)]
        public string TenDoan { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(1000)]
        public string DSnguoidi { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public int GiaTour { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public double Total { get; set; }


    }
}
