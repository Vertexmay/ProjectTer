using UnityEngine;

public class Enemy : MonoBehaviour, IHealth
{
    private int _maxHealth;
    private int _health;

    public int MaxHealth { get => _maxHealth; set { _maxHealth = value; } }
    public int Health { get => _health; set { _health += value; } }

    private void Awake()
    {
        MaxHealth = 100;
        Health = MaxHealth;
    }

    public void TakeDmg(int dmg)
    {
        _health -= dmg;
        print($"Dmg = {dmg} | Healt = {Health}");
    }
}
