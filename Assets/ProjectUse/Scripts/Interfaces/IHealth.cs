using System;

public interface IHealth
{
    public int Health { get; }

    public EventHandler<int> TakeDamage { get; }
    public EventHandler<int> TakeHeal { get; }
}
