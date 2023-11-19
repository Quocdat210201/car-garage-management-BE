namespace Gara.Domain
{
    public abstract class EntityBase<TId>
    {
        public bool? IsDeleted { get; set; }

        public bool? IsActived { get; set; }

        public DateTime? CreatedOn { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }

        public Guid? DeletedBy { get; set; }

        public DateTime? DeletedOn { get; set; }
    }

    public abstract class EntityBaseWithId<TId> : EntityBase<TId>
    {
        public TId Id { get; set; }

        public EntityBaseWithId() { }

        public EntityBaseWithId(TId id)
        {
            Id = id;
        }
    }

    public abstract class EntityBaseWithId : EntityBaseWithId<Guid>
    {
        public EntityBaseWithId() : base() { }

        public EntityBaseWithId(Guid id) : base(id) { }
    }
}
