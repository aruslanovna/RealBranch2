//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RealBranch.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Supply
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supply()
        {
            this.SupplyFlowers = new HashSet<SupplyFlower>();
        }
    
        public int Id { get; set; }
        public System.DateTime ClosedDate { get; set; }
        public int PlantationId { get; set; }
        public System.DateTime ScheduledDate { get; set; }
        public string Status { get; set; }
        public int WarehouseId { get; set; }
    
        public virtual Plantation Plantation { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplyFlower> SupplyFlowers { get; set; }
    }
}
