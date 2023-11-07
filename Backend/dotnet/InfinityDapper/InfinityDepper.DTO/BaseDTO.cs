namespace InfinityDapper.DTO
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public bool IsDisabled { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
