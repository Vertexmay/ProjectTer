using UnityEngine;
using System;

public class PlayerCont : MonoBehaviour, IHealth
{

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private Attacker _attacker;

    [Header("Player Options")]
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float _maxSpeed = 6f;
    [SerializeField] private float _minSpeed = 1f;
    [SerializeField] private float _speedIncrease = 2f;

    private float _currentSpeed = 1f;
    private int _health;
    private bool _isAlive = true;
    private Vector3 _input;
    private Camera _camera;

    public int Health => _health;

    public EventHandler<int> TakeDamage => OnTakeDmg;
    public EventHandler<int> TakeHeal => OnHeal;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
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
            if (_currentSpeed < _maxSpeed)
            {
                _currentSpeed += _speedIncrease * Time.deltaTime;
            }
        }
        else
        {
            _currentSpeed = _minSpeed;
        }

        _characterController.Move(movementVector * (_currentSpeed * Time.deltaTime));
        _animator.SetFloat("Speed", _characterController.velocity.magnitude);
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
            _attacker.Attack();
    }

    private void Die()
    {
        print("Dead");
    }

    private void OnHeal(object sender, int heal)
    {
        if (_health < _maxHealth)
            _health += heal;

        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    private void OnTakeDmg(object sender, int damage)
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
    }

}
