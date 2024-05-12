using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Options")]
    [SerializeField] protected float _visionDistance = 10f;
    [SerializeField] protected int _maxHealth = 100;
    [SerializeField] protected float _maxSpeed = 5;
    [SerializeField] protected float _speedIncrease = 2.5f;
    
    [SerializeField] protected Animator _animator;

    protected int _health;
    protected bool _isAlive = true;
    protected float _currentSpeed = 0f;

    protected StateMachine _stateMachine;
    protected List<StateSM> _states = new List<StateSM>();

    protected int Health => _health;

    public EventHandler<int> TakeDamage => OnTakeDmg;
    public EventHandler<int> TakeHeal => OnHeal;

    protected void Die()
    {
        print("Dead");
    }

    protected void OnHeal(object sender, int heal)
    {
        if (_health < _maxHealth)
            _health += heal;

        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    protected void OnTakeDmg(object sender, int damage)
    {
        if (sender is not Attacker)
            return;

        if (_health > damage)
            _health -= damage;
        else if (_isAlive)
        {
            _isAlive = false;
            _health = 0;
            Die();
        }
        else
            return;

        print($"Health: {_health} | Dmg: {damage}");
    }

    protected Vector3 FindWaypoint()
    {
        NavMeshTriangulation data = NavMesh.CalculateTriangulation();
        int index = UnityEngine.Random.Range(0, data.vertices.Length);
        return data.vertices[index];
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _visionDistance);
    }
}
