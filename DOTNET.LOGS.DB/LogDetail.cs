//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DOTNET.LOGS.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class LogDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LogDetail()
        {
            this.MILogs = new HashSet<MILog>();
        }
    
        public int LogId { get; set; }
        public string StartDateTime { get; set; }
        public Nullable<System.DateTime> EndDateTime { get; set; }
        public Nullable<int> ActionSequenceNumber { get; set; }
        public string Level { get; set; }
        public string Exception { get; set; }
        public string Message { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Url { get; set; }
        public string UrlReferrer { get; set; }
        public string Thread { get; set; }
        public string FormValues { get; set; }
        public int LogDetailId { get; set; }
        public string ServerName { get; set; }
        public string AreCookiesDisabled { get; set; }
        public string IsScriptDisabled { get; set; }
        public string Browser { get; set; }
        public Nullable<bool> InPageAction { get; set; }
    
        public virtual LogMain LogMain { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MILog> MILogs { get; set; }
    }
}
