// In SettingsMenu.cs, add an exit button reference and a method to close the settings panel:

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button exitButton; // Exit button for closing the settings menu

    private AudioSource musicSource; // Reference for background music
    //private bool isMusicOn = true;

    private void Awake()
    {
        Debug.Log("founding music source");

        // Ensure the settings panel is disabled at start
        settingsPanel.SetActive(false);

        // Add listeners for the buttons
        restartButton.onClick.AddListener(RestartScene);
        quitButton.onClick.AddListener(QuitGame);
        musicButton.onClick.AddListener(ToggleMusic);
        exitButton.onClick.AddListener(CloseSettings); // Exit button calls CloseSettings
    }

    /// <summary>
    /// Called by the settings button. Toggles the settings panel.
    /// </summary>
    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    /// <summary>
    /// Closes the settings panel.
    /// </summary>
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    /// <summary>
    /// Restarts the current scene.
    /// </summary>
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Quits the game.
    /// Note: This only works in a built version, not in the Editor.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    /// <summary>
    /// Toggles the music on/off.
    /// </summary>
    public void ToggleMusic()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.ToggleMusic();
        }
        else
        {
            Debug.LogWarning("MusicManager instance was not found.");
        }
    }
}
