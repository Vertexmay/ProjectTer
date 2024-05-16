using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObject/CreateWeapon")]
public class WeaponeSO : ScriptableObject 
{
    [SerializeField] private string _name;
    [SerializeField] private Mesh _weaponMesh;
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _cost;
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private float _range;

    public string Name => _name;
    public Sprite Icon => _icon;
    public float Cost => _cost;
    public int Damage => _damage;
    public float Cooldown => _cooldown;
    public WeaponType WeaponType => _weaponType;
    public float Range => _range;
    public Mesh WeaponMesh => _weaponMesh;
}

public enum WeaponType
{
    melee,
    range
}
