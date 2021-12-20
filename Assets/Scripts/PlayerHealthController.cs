using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance; // to access the script from BulletController
    public int maxHealth, currentHealth;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth; // start from 100% health
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;

        // .text is the text box in that healthText obj
        UIController.instance.healthText.text = "HEALTH " + currentHealth + "/" + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamagePlayer(int damageAmount)
    {
        if (!GameManager.instance.levelEnding)
        {
            currentHealth -= damageAmount;
            AudioManager.instance.PlaySXF(6);
            UIController.instance.ShowDamage();
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
                currentHealth = 0;
                GameManager.instance.PlayerDied();
                //AudioManager.instance.StopBGM(); // stopping background music
                AudioManager.instance.PlaySXF(4); // player died
                AudioManager.instance.StopSFX(6); // if we are dead, stop damage sound, only keep death sound
            }

            UIController.instance.healthSlider.value = currentHealth; // updating the bar
            UIController.instance.healthText.text = "HEALTH " + currentHealth + "/" + maxHealth; // updating the text
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth) // we don't want our health to be more than max health
        {
            currentHealth = maxHealth;
        }

        UIController.instance.healthSlider.value = currentHealth; // updating the bar
        UIController.instance.healthText.text = "HEALTH " + currentHealth + "/" + maxHealth; // updating the text
    }
}
