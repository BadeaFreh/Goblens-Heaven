using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private bool isCollected;
    public int healAmount;

    private void OnTriggerEnter(Collider other) // triggered when the player picks up health item
    {
        if (other.tag == "Player" && !isCollected)
        {
            PlayerHealthController.instance.HealPlayer(healAmount);
            Destroy(gameObject); // health item disappears

            AudioManager.instance.PlaySXF(3);
        }
    }
}
