using core.events;

namespace core.Domain
{
    /// <summary>
    ///  the AggregateRoot class provides a framework for managing events and changes in a domain-driven design using event sourcing. 
    ///  It allows you to raise and apply events, manage uncommitted changes, and handle the replaying of events. This kind of 
    ///  architecture is often used to ensure a complete and accurate history of changes to a system's stat
    /// </summary>
    public abstract class AggregateRoot
    {
        /// <summary>
        /// This protected field _id represents the unique identifier for the aggregate root
        /// </summary>
        protected Guid _id;

        /// <summary>
        /// This private field _changes is a list that holds all the uncommitted changes (events) made to the aggregate root. 
        /// Events are objects that represent changes to the state of the aggregate.
        /// </summary>
        private readonly List<BaseEvent> _changes = new();


        /// <summary>
        /// This public property allows you to access the _id field from outside the class
        /// </summary>
        public Guid Id 
        {
            get { return _id; }
        }

        /// <summary>
        /// This public property represents the version of the aggregate root. It's initially set to -1.
        /// </summary>
        public int Version { get; set; } = -1;

        /// <summary>
        /// This method returns an enumerable of uncommitted changes (events) made to the aggregate
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BaseEvent> GetUncommittedChanges() 
        {
            return _changes;
        }

        /// <summary>
        /// This method clears the list of uncommitted changes, indicating that they have been successfully saved or applied.
        /// </summary>
        public void MarkChangesAsCommitted() 
        {
            _changes.Clear();
        }

        /// <summary>
        ///  This private method is responsible for applying a change (event) to the aggregate. 
        ///  It uses reflection to find and invoke the corresponding Apply method on the aggregate
        /// </summary>
        /// <param name="event"></param>
        /// <param name="isNew"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private void ApplyChange(BaseEvent @event, bool isNew) 
        {
            var method = this.GetType().GetMethod("Apply", new Type[] {@event.GetType()});

            if(method == null) 
            {
                throw new ArgumentNullException(nameof(method), $"The Apply method was not found in the aggregate for {@event.GetType().Name}!");
            }

            method.Invoke(this, new object[] { @event});

            if (isNew) 
            {
                _changes.Add(@event);
            }
        }

        /// <summary>
        /// This protected method allows you to raise an event by applying the change and adding it to the list of uncommitted changes
        /// </summary>
        /// <param name="event"></param>
        protected void RaiseEvent(BaseEvent @event) 
        {
            ApplyChange(@event, true);
        }

        /// <summary>
        /// This method allows you to replay events on the aggregate, applying each event's change without adding it as 
        /// an uncommitted change
        /// </summary>
        /// <param name="events"></param>
        public void ReplayEvents(IEnumerable<BaseEvent> events) 
        {
            foreach(var @event in events) 
            {
                ApplyChange(@event, false);
            }
        }
    }
}
