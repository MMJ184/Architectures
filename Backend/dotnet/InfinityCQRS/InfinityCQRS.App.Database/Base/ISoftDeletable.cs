namespace InfinityCQRS.Backend.Contracts
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }

        void Delete();
    }
}