namespace QLSDDienNuoc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Consume
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Consume()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int ID { get; set; }

        public int? CustomerID { get; set; }

        public int? NewElectricIndex { get; set; }

        public int? OldElectricIndex { get; set; }

        public int? ElectricConsume { get; set; }

        public int? NewWaterIndex { get; set; }

        public int? OldWaterIndex { get; set; }

        public int? WaterConsume { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Time { get; set; }

        public bool? isPay { get; set; }

        public int? CreatedByID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedByID { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool? isDelete { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
