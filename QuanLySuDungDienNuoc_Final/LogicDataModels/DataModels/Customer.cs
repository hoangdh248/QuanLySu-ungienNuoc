namespace LogicDataModels.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Consumes = new HashSet<Consume>();
        }

        public int ID { get; set; }

        public int? UserID { get; set; }

        [StringLength(50)]
        public string PassportID { get; set; }

        public int? PriceID { get; set; }

        public int? CreatedByID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedByID { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool? isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Consume> Consumes { get; set; }

        public virtual Price Price { get; set; }

        public virtual User User { get; set; }
    }
}
