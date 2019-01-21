namespace LogicDataModels.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Price
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Price()
        {
            Customers = new HashSet<Customer>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string PriceName { get; set; }

        [Column(TypeName = "money")]
        public decimal? ElectricPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal? WaterPrice { get; set; }

        public int? CreatedByID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedByID { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool? isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
