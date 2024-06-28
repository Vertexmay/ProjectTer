using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private PlayerCont _player;
    [SerializeField] private GameObject _Settings_UI;
    [SerializeField] private GameObject _Main_UI;
    [SerializeField] private GameObject _start_UI;

    public void Boosty_URL()
    {
        Application.OpenURL("https://boosty.to/vertexfar");
    }

    public void Settings()
    {
        _Main_UI.SetActive(!_Main_UI.activeSelf);
        _Settings_UI.SetActive(!_Settings_UI.activeSelf);
        print("Settings");
    }

    public void BacktoMain()
    {
        _Main_UI.SetActive(!_Main_UI.activeSelf);
        _start_UI.SetActive(!_start_UI.activeSelf);
    }

    public void StartGame()
    {
        _Main_UI.SetActive(!_Main_UI.activeSelf);
        _start_UI.SetActive(!_start_UI.activeSelf);
        //SceneManager.LoadScene(1);
    }

    public void StartGameIn(int count)
    {
        if (count == 0)
        {
            _player.Class = 1;
            SceneManager.LoadScene(1);
        }
        if (count == 1)
        {
            _player.Class = 2;
            SceneManager.LoadScene(1);
        }
        if (count == 2)
        {
            _player.Class = 3;
            SceneManager.LoadScene(1);
        }
        else
        {
            Exit();
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
