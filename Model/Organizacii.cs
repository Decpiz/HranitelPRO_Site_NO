//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PR_21._106_HranitelPRO_Practic.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Organizacii
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Organizacii()
        {
            this.Polzovateli = new HashSet<Polzovateli>();
        }
    
        public int ID_Organizacii { get; set; }
        public string Nazvanie { get; set; }
        public string INN { get; set; }
        public string GenDir_FIO { get; set; }
        public string OGRN { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Polzovateli> Polzovateli { get; set; }
    }
}
