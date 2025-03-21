using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private SpriteRenderer sr;



    [SerializeField] private ItemData itemData;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        
        sr.sprite = itemData.Icon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Debug.Log("Picked up item " + itemData.itemName);
            CharacterStats stats = collision.GetComponent<CharacterStats>();

            if (itemData.itemName == "Heart")
            {
                if (stats != null)
                    stats.Heal(10);
            }
            else if (itemData.itemName == "Sword")
            {
                if (stats != null)
                {
                    // Increase strength by 5 (or any value you choose)
                    stats.strength.AddModifier(5);

                    // Optionally, show a notification that strength has increased
                    if (NotificationManager.Instance != null)
                    {
                        NotificationManager.Instance.ShowNotification("Strength +5", 2f);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}
