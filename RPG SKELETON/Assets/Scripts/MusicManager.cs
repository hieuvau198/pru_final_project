// MusicManager.cs
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip backgroundMusic;

    private void Awake()
    {
        // Singleton pattern to ensure only one MusicManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // This makes the MusicManager persist between scenes
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        if (audioSource != null && backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    /// <summary>
    /// Toggles the background music on or off.
    /// </summary>
    public void ToggleMusic()
    {
        if (audioSource == null)
            return;

        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            Debug.Log("Music paused");
        }
        else
        {
            audioSource.Play();
            Debug.Log("Music playing");
        }
    }
}
