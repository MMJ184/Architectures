namespace InfinityCQRS.Backend.Contracts
{
    public class SoftDeleteBase : GuidModelBase, ISoftDeletable
    {
        // Soft Delete Property
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Soft Deletes Entity
        /// </summary>
        public void Delete() => OnDelete();

        /// <summary>
        /// Bring Back Entity to Life
        /// </summary>
        public void BringBack() => OnBringBack();

        /// <summary>
        /// Logic for deleting entity (Ex: Cascade deletion)
        /// </summary>
        protected virtual void OnDelete()
            => IsDeleted = true;


        /// <summary>
        /// Bring Back Entity Logic
        /// </summary>
        protected virtual void OnBringBack()
            => IsDeleted = false;
    }
}
