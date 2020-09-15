using System;

namespace Entities
{
    public class Call
    {
        #region Properties
        public long Id { get; set; }
        public string LinkedId { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string CallerName { get; set; }
        public string CallerNumber { get; set; }
        public string ConnectedName { get; set; }
        public string ConnectedNumber { get; set; }
        public string Did { get; set; }
        public string UniqueId { get; set; }
        public string Dst { get; set; }
        public string Channel { get; set; }
        public string DstChannel { get; set; }
        public int? Duration { get; set; }
        public int? BillSec { get; set; }
        public int CdRok { get; set; }
        public string RecordingFile { get; set; }
        public long? Agree1 { get; set; }
        public long? Agree2 { get; set; }
        public long? Agree3 { get; set; }
        public long? RegionId { get; set; }
        public long? CompanyId { get; set; }
        public long? ReciptTypeId { get; set; }
        public string Note { get; set; }
        public string FirstNumber { get; set; }
        public DateTime? CallDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        #endregion
    }
}