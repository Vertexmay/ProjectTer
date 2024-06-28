using System;

public interface IHealth
{
    public float Health { get; }

    public EventHandler<int> TakeDamage { get; }
    public EventHandler<int> TakeHeal { get; }
}
