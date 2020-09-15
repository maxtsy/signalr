
using System;

namespace Entities
{
    public class ReferenceItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string GroupCode { get; set; }
        public bool? IsEnabledForRegistrator { get; set; }
        public bool? IsEnabledForOperator { get; set; }
    }
}