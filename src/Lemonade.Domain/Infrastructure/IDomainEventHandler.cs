﻿namespace Lemonade.Domain.Infrastructure
{
    public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        void Handle(TEvent @event);
    }
}