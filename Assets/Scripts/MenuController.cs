using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    void Start()
    {
        _playButton.onClick.AddListener(PlayButton_OnClick);
        _settingsButton.onClick.AddListener(SettingsButton_OnClick);
        _exitButton.onClick.AddListener(ExitButton_OnClick);
    }

    private void PlayButton_OnClick()
    {
        SceneManager.LoadScene("PrototypeScene");
    }
    
    private void SettingsButton_OnClick()
    {
        // Open settings page
    }
    
    private void ExitButton_OnClick()
    {
        // show window "Do you want to leave?"

        Application.Quit();
    }
}