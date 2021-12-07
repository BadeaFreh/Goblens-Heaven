using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int currentHealth = 5; // why int? we want to be able to say that enemy takes only exactly 5 shots

    void Start()
    {

    }

    void Update()
    {

    }

    public void DamageEnemy(int damageAmount)
    {

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
