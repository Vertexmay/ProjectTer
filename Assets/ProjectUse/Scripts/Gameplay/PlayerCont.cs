using UnityEngine;
using System;

public class PlayerCont : MonoBehaviour, IHealth, IMoveble
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private Attacker _attacker;

    [Header("Player Options")]
    [SerializeField] private int _level;
    [SerializeField] private CharProgressSO _charProgressSO;
    private CharacterData _charData => _charProgressSO.CharProgress[_level];
    [SerializeField] private float _minSpeed = 1f;
    
    private float _currentSpeed = 2.5f;
    private int _health;
    private bool _isAlive = true;
    private Vector3 _input;
    private Camera _camera;

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
    }

    private void Die()
    {
        print("You Dead");
        _animator.Play("Die");
    }
}
