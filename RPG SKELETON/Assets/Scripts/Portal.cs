// Portal.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // You can set the scene name or index in the Inspector.
    [SerializeField] private string sceneToLoad = "Scene2";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision triggered");  
        // Check if the colliding object is the player
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
            Debug.Log("Player entered the portal!");
        }
    }
}
