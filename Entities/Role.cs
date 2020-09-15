using System.Collections.Generic;

namespace Entities
{
    public class Role
    {
        private static long defaultWeight { get; } = 1000;        

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
    }
}