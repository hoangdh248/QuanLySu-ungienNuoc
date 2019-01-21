namespace QLSDDienNuoc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Invoice
    {
        public int ID { get; set; }

        public int? ConsumeID { get; set; }

        public int? PriceID { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalMoney { get; set; }

        public int? CreatedByID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedByID { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool? isDelete { get; set; }

        public virtual Consume Consume { get; set; }

        public virtual Price Price { get; set; }
    }
}
