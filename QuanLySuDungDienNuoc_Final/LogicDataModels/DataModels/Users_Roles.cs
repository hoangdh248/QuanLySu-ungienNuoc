namespace LogicDataModels.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users_Roles
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleID { get; set; }

        public bool? isView { get; set; }

        public bool? isAdd { get; set; }

        public bool? isRemove { get; set; }

        public bool? isEdit { get; set; }

        public int? CreatedByID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedByID { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool? isDelete { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
