using UnityEngine;

public class Attacker : MonoBehaviour
{
    private bool CanAttack => _attackTime <= 0;

    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _damageMask;

    [SerializeField] private float _attackCooldown;
    [SerializeField] private int _damage;
    [SerializeField] private float _radius;

    Collider[] _hits = new Collider[3];
    private float _attackTime;

    public float AttackRadius => _radius;

    private void Start() => ResetAttackTimer();

    private void FixedUpdate()
    {
        if (!CanAttack)
        {
            _attackTime -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        MeleeAttack();
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
            AttackNear();
        }
    }

    private void AttackNear()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _hits, _damageMask);

        for (int i = 0; i < count; i++)
        {
            if (_hits[i].TryGetComponent<IHealth>(out var enemy))
            {
                enemy.TakeDamage?.Invoke(this, _damage);
            }
        }
    }

    private void ResetAttackTimer() => _attackTime = _attackCooldown;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
        
    }

}
