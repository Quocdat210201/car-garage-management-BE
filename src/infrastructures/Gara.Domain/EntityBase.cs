namespace Gara.Domain
{
    public abstract class EntityBase<TId>
    {
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
