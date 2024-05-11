using System.Diagnostics;

public interface IHealth
{
    public int MaxHealth
    {
        get; set;
    }

    public int Health 
    {
        get; set;
    }

    public void TakeDmg(int dmg) 
    { 
        if (Health > 0)
        {
            Health -= dmg;
        }
        if (Health <= 0)
        {
            Die();
        }
    
    }

    public void Heal(int heal)
    {
        if (Health < MaxHealth)
        {
            Health += heal;
        }
        if (Health >= MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    public static void Die()
    {
        Debug.Print("Char Dead X_X");
    }
}
