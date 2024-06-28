using UnityEngine.UI;
using UnityEngine;

public class HUD_UI : MonoBehaviour
{
    [SerializeField] private Image _slider;
    [SerializeField] private PlayerCont player;
    void Update()
    {
        float fill = player.Health / player.MaxHP;
        _slider.fillAmount = fill;
    }
}
