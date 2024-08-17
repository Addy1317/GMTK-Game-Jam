#region Summary
///<Summary>
///MainLobby Script for Handling UI
///</Summary>
#endregion

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GameJam
{
    public class MainLobbyUI : MonoBehaviour
    {
        [Header("MainLobbyButtons")]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitbutton;

        private void OnEnable() 
        {
            _playButton.onClick.AddListener(PlayButton);
            _settingsButton.onClick.AddListener(SettingsButton);
            _quitbutton.onClick.AddListener(QuitButton);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(PlayButton);
            _settingsButton.onClick.RemoveListener(SettingsButton);
            _quitbutton.onClick.RemoveListener(QuitButton);
        }

        #region Buttons Methods
        private void PlayButton()
        {
            SceneManager.LoadScene("MainGame");
        }

        private void SettingsButton()
        {
            Debug.Log("SettingsButton Presse");
        }

        private void QuitButton()
        {
            Application.Quit();
            Debug.Log("QuitButton Pressed " + "Application Quitting");
        }

        #endregion

        #region Panels Activation/Deactivation Methods

        #endregion
    }
}