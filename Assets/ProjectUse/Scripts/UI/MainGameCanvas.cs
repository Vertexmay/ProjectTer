using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _weaponPanel;
    private bool WeaponUI = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _inventory.SetActive(!_inventory.activeSelf);
        }
    }

    public void RefPanelW()
    {
        if (WeaponUI == false)
        {
            _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
            _weaponPanel.SetActive(!_weaponPanel.activeSelf);
            WeaponUI = !WeaponUI;
        }
    }
    public void RefPanelI()
    {
        if (WeaponUI == true)
        {
            _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
            _weaponPanel.SetActive(!_weaponPanel.activeSelf);
            WeaponUI = !WeaponUI;
        }
    }
}
