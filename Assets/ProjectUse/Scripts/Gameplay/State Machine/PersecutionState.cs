using UnityEngine;
using UnityEngine.AI;

public class PersecutionState : StateSM
{
    private float _maxSpeed = 5f;
    private float _speedIncrease = 2.5f;
    private Transform _target;
    private NavMeshAgent _agent;
    private Attacker _attacker;
    private float _speed;
    //
    private float _persDistance = 15f;
    private bool PersZone => Vector3.Distance(_target.position, _agent.nextPosition) < _persDistance;
    //
    private float _persTime = 1f;
    private float _timer = 0f;

    public PersecutionState(StateMachine machine, Animator animator, Attacker attacker, Transform target, NavMeshAgent agent) : base(machine, animator)
    {
        _attacker = attacker;
        _target = target;
        _agent = agent;
        _speed = _animator.GetFloat("Speed");
    }

    public override void Enter()
    {
        _agent.stoppingDistance = _attacker.AttackRadius / 2;
        Debug.Log("[ENTER] Persucution State");
    }

    public override void Exit()
    {
        _agent.stoppingDistance = 0;
        Debug.Log("[Exit] Persucution State");
    }

    public override void Update()
    {
        Debug.Log("[Update] Persucution State");

        _agent.SetDestination(_target.position);

        if (_speed < _maxSpeed)
        {
            _speed += _speedIncrease * Time.deltaTime;
        }

        //Проверяем что противник остановился и дистанция подходит для атаки
        if (Vector3.Distance(_target.position, _agent.nextPosition) < _attacker.AttackRadius)
        {
            _speed = 0;
            _machine.SetState<AttackState>();
        }

        //Проверка вышел ли игрок за зону преследования и в случае истечения таймера перейти в состояние отдыха
        if (!PersZone && _timer >= _persTime)
            _machine.SetState<IdleState>();
        else if (!PersZone)
            _timer += Time.deltaTime;
        else
            _timer = 0;

        _animator.SetFloat("Speed", _speed);
    }
}
