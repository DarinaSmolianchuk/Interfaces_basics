//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lab5
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employees
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Id_Unit { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
    
        public virtual Units Units { get; set; }
    }
}
