// NotificationManager.cs
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }
    [SerializeField] public TMP_Text notificationText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void ShowNotification(string message, float duration = 2f)
    {
        notificationText.text = message;
        notificationText.gameObject.SetActive(true);
        StartCoroutine(HideNotificationAfterDuration(duration));
    }

    private IEnumerator HideNotificationAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        notificationText.gameObject.SetActive(false);
    }
}
