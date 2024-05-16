using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected Animator _animator;

    [Header("Enemy Options")]
    [SerializeField] protected float _visionDistance = 10f;
    [SerializeField] protected int _level = 1;
    [SerializeField] protected CharProgressSO _charProgressSO;
    
    
    protected CharacterData _charData 
    {
        get
        {
            int level = _level -1;

            if (_level > _charProgressSO.CharProgress.Count)
                return _charProgressSO.CharProgress[_charProgressSO.CharProgress.Count];
            else if (_level < 1)
                return _charProgressSO.CharProgress[0];

            return _charProgressSO.CharProgress[level];
        }
    }

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
        if (_health < _charData.MaxHP)
            _health += heal;

        if (_health > _charData.MaxHP)
            _health = _charData.MaxHP;
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
