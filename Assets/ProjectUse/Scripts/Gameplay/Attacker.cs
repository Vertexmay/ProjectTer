using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private bool CanAttack => _attackTime <= 0;

    [SerializeField] private Animator _animator;
    [SerializeField] private WeaponeSO _weapon;
    [SerializeField] MeshFilter _weaponMeshFilter;
    [SerializeField] private LayerMask _damageMask;

    [SerializeField] private float _attackCooldown => _weapon.Cooldown;
    private int _damage => _weapon.Damage;

    public float AttackRadius => _weapon.Range;

    Collider[] _hits = new Collider[3];
    private float _attackTime;

    private void Start()
    {
        ResetAttackTimer();

        _weaponMeshFilter.mesh = _weapon.WeaponMesh;
    }

    private void FixedUpdate()
    {
        if (!CanAttack)
        {
            _attackTime -= Time.deltaTime;
        }
    }

    public void AttackEnemy()
    {
        MeleeAttackEnemy();
    }
    public void Attack()
    {
        MeleeAttack();
    }

    private void MeleeAttackEnemy()
    {
        if (!CanAttack) { return; }

        _animator.SetInteger("AttackVariant", 0);
        _animator.SetTrigger("Attack");
        ResetAttackTimer();
        AttackNear();
    }

    public void MeleeAttack()
    {
        if (!CanAttack) { return; }

        if (Input.GetMouseButton(0) && CanAttack)
        {
            //var index = Random.Range(0, 2);
            _animator.SetInteger("AttackVariant", 0);
            _animator.SetTrigger("Attack");
            ResetAttackTimer();
            AttackNearPlayer();
        }
    }

    private void AttackNear()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, AttackRadius, _hits, _damageMask);

        for (int i = 0; i < count; i++)
        {
            if (_hits[i].TryGetComponent<IHealth>(out var enemy))
            {
                enemy.TakeDamage?.Invoke(this, _damage);
            }
        }
    }

    private void AttackNearPlayer()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, AttackRadius, _hits, _damageMask);

        for (int i = 0; i < count; i++)
        {
            if (_hits[i].TryGetComponent<IHealth>(out var player))
            {
                player.TakeDamage?.Invoke(this, _damage);
            }
        }
    }

    private void ResetAttackTimer() => _attackTime = _attackCooldown;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
        
    }

}
