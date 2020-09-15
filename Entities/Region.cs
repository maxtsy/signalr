namespace Entities
{
    public class Region : ReferenceItem
    {
        public string AOGUID { get; set; }

        public bool IsMsk() => this.Id == 77;
        public bool IsNotMsk() => this.Id != 77;
    }
}