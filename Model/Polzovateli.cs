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
    
    public partial class Polzovateli
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Polzovateli()
        {
            this.Zajavki = new HashSet<Zajavki>();
        }
    
        public int ID_Polzovatelia { get; set; }
        public string Familia { get; set; }
        public string Imya { get; set; }
        public string Otchestvo { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Parol { get; set; }
        public Nullable<int> ID_Organizacii { get; set; }
        public byte[] Photo { get; set; }
        public string Status { get; set; }
    
        public virtual Organizacii Organizacii { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zajavki> Zajavki { get; set; }
    }
}
