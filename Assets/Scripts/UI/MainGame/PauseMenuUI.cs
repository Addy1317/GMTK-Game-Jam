#region Summary
///<Summary>
///PauseMenu Script for Handling PauseUI
///</Summary>
#endregion
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameJam
{
    public class PauseMenuUI : MonoBehaviour
    {
        [Header("PauseMenu Buttons")]
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _homeButton;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(ResumeButton);
            _restartButton.onClick.AddListener(RestartButton);
            _homeButton.onClick.AddListener(HomeButton);
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(ResumeButton);
            _restartButton.onClick.RemoveListener(RestartButton);
            _homeButton.onClick.RemoveListener(HomeButton);
        }

        #region Buttons Methods

        private void ResumeButton()
        {
            _pausePanel.SetActive(false);
        }

        private void RestartButton()
        {
            SceneManager.LoadScene("MainGame");
        }

        private void HomeButton()
        {
            SceneManager.LoadScene("MainLobby");
        }
        #endregion
    }
}
