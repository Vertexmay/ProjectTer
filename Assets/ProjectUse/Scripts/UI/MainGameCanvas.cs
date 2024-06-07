using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _inventory;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _inventory.SetActive(!_inventory.activeSelf);
        }
    }
}
