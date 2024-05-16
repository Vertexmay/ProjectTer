using System;
using UnityEngine;

[Serializable]
public struct CharacterData
{
    [SerializeField] private int _level;
    [SerializeField] private int _maxHP;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _speedIncrase;

    public int Level => _level;
    public int MaxHP => _maxHP;
    public float MaxSpeed => _maxSpeed;
    public float SpeedIncrase => _speedIncrase;

    public CharacterData(int lvl, int maxHP, float maxSpeed, float speedIncrase)
    {
        _level = lvl;
        _maxHP = maxHP;
        _maxSpeed = maxSpeed;
        _speedIncrase = speedIncrase;

    }
}
