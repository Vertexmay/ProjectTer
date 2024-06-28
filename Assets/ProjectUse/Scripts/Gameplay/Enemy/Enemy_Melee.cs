using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Attacker))]
public class Enemy_Melee : Enemy, IMoveble
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private CharProgressSO _charProgSO;
    private Attacker _attacker;


    public float Speed => _currentSpeed;
    public float MaxSpeed => _charData.MaxSpeed;
    public float SpeedIncrase => _charData.SpeedIncrase;

    private void Start()
    {
        _attacker = GetComponent<Attacker>();

        _stateMachine = new StateMachine();
        _stateMachine.AddState(new IdleState(_stateMachine, _animator));
        _stateMachine.AddState(new MoveState(_stateMachine, _animator, this));
        _stateMachine.AddState(new PersecutionState(_stateMachine, _animator, _attacker, GameManager.PlayerTransform, _agent));
        _stateMachine.AddState(new AttackState(_stateMachine, _animator, GameManager.PlayerTransform.GetComponent<PlayerCont>(), GameManager.PlayerTransform, _attacker));
        _stateMachine.SetState<IdleState>();

        _health = _charData.MaxHP;
        _isAlive = _health > 0 ? true : false;
    }

    private void Update()
    {
        if (_isAlive) 
        {
            if (!_agent.hasPath)
            {
                _stateMachine.SetState<IdleState>();
                _agent.SetDestination(FindWayPoint());
            }
            else if (_agent.hasPath && _stateMachine.CurrentState is IdleState)
                _stateMachine.SetState<MoveState>();

            if (_stateMachine.CurrentState is not PersecutionState && _stateMachine.CurrentState is not AttackState)
            {
                float distance = Vector3.Distance(GameManager.PlayerPos, transform.position);
                if (distance <= _visionDistance && distance > _attacker.AttackRadius)
                    _stateMachine.SetState<PersecutionState>();
                else if (distance <= _attacker.AttackRadius)
                    _stateMachine.SetState<AttackState>();
            }

            _agent.speed = _animator.GetFloat("Speed");

            _stateMachine.Update();


            Vector3 FindWayPoint()
            {
                NavMeshTriangulation data = NavMesh.CalculateTriangulation();
                int index = Random.Range(0, data.vertices.Length);
                return data.vertices[index];
            }

            Health = _health;
        }
    }
}
