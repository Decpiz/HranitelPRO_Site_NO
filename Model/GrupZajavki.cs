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
    
    public partial class GrupZajavki
    {
        public int ID_Zajavki { get; set; }
        public int ID_Posetitelia { get; set; }
        public string A1 { get; set; }
    
        public virtual Posetiteli Posetiteli { get; set; }
        public virtual Zajavki Zajavki { get; set; }
    }
}
