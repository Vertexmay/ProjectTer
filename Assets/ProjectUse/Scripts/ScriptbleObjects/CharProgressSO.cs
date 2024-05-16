using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character type", menuName = "ScriptableObject/CreateCharectertype")]
public class CharProgressSO : ScriptableObject
{
    [SerializeField] private List<CharacterData> _charProgress;
    public List<CharacterData> CharProgress => _charProgress;
}
