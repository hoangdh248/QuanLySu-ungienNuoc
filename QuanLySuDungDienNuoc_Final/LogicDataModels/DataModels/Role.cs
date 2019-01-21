namespace LogicDataModels.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Role()
        {
            Users_Roles = new HashSet<Users_Roles>();
        }

        public int ID { get; set; }

        [StringLength(150)]
        public string RoleName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? CreatedByID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedByID { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool? isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users_Roles> Users_Roles { get; set; }
    }
}
