using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnLootPoints = new List<Transform>();
    [SerializeField] private int _spawnLootCount = 0;
    [SerializeField] private GameObject _lootPrefab;
    [SerializeField] private List<LootSO> _lootVariants = new List<LootSO>();

    private static PlayerCont _player;
    private static Transform _playerTransform;

    public static Vector3 PlayerPos => _playerTransform.position;
    public static Transform PlayerTransform => _playerTransform;
    public static PlayerCont Player => _player;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCont>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        for (int i = 0; i < _spawnLootCount; i++)
        {
            if (i > _spawnLootPoints.Count)
                break;

            int index = Random.Range(0, _lootVariants.Count);

            Loot loot = Instantiate(_lootPrefab, _spawnLootPoints[i].position, Quaternion.identity).GetComponent<Loot>();
            loot.Init(_lootVariants[index]);
        }
    }
}