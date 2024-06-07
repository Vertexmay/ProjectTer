using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Rendering.Universal;

public class PlayerCont : MonoBehaviour, IHealth, IMoveble
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Animator _animator;
    [SerializeField] private Attacker _attacker;

    [Header("Player Options")]
    [SerializeField] private float _lootingRadius = 1;
    [SerializeField] private int _level;
    [SerializeField] private CharProgressSO _charProgressSO;
    private Inventory _inventory = new Inventory();
    private Collider[] _loot = new Collider[5];
    private CharacterData _charData => _charProgressSO.CharProgress[_level];
    [SerializeField] private float _minSpeed = 1f;
    
    private float _currentSpeed = 2.5f;
    private int _health;
    private bool _isAlive = true;
    private Vector3 _input;
    private Camera _camera;
    public List<LootSO> Loot => _inventory.Backpack;

    public int Health => _health;

    public EventHandler<int> TakeDamage => OnTakeDmg;
    public EventHandler<int> TakeHeal => OnHeal;

    public float Speed => _currentSpeed;

    public float MaxSpeed => _charData.MaxSpeed;

    public float SpeedIncrase => _charData.SpeedIncrase;

    private void Start()
    {
        _camera = Camera.main;
        _health = _charData.MaxHP;
    }

    private void Update()
    {
        if (_isAlive)
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            _input = new Vector3(horizontal, 0, vertical);

            Vector3 movementVector = _camera.transform.TransformDirection(_input);
            movementVector.y = 0;
            movementVector.Normalize();

            transform.forward = movementVector;

            if (_input.x != 0 || _input.z != 0)
            {
                if (_currentSpeed < MaxSpeed)
                {
                    _currentSpeed += SpeedIncrase * Time.deltaTime;
                }
            }
            else
            {
                _currentSpeed = _minSpeed;
            }

            _characterController.Move(movementVector * (_currentSpeed * Time.deltaTime));
            _animator.SetFloat("Speed", _characterController.velocity.magnitude);
        }
    }

    private void LateUpdate()
    {
        if (_isAlive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _attacker.Attack();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpLoot();
            }
        }
    }
    private void PickUpLoot()
    {
        print("Try pickup");
        int count = Physics.OverlapSphereNonAlloc(transform.position, _lootingRadius, _loot, 1);

        for (int i = 0; i < count; i++)
        {
            if (_loot[i].TryGetComponent<Loot>(out var loot))
            {
                print("Add loot");
                _inventory.AddLoot(loot.LootData);
                loot.OnPickUp();
            }
        }
    }

    private void OnHeal(object sender, int heal)
    {
        if (_health < _charData.MaxHP)
            _health += heal;

        if (_health > _charData.MaxHP)
            _health = _charData.MaxHP;
    }

    private void OnTakeDmg(object sender, int damage)
    {
        if (sender is not Attacker)
            return;

        if (_health > damage)
        {
            _health -= damage;            
            _animator.Play("GetHit"); 
        }
        else if (_isAlive)
        {
            _isAlive = false;
            _health = 0;
            Die();
        }

        _particleSystem.Play();
    }

    private void Die()
    {
        print("You Dead");
        _animator.Play("Die");
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _lootingRadius);
    }
}
