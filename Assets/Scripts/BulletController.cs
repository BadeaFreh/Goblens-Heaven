using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float moveSpeed, lifeTime; // lifeTime is in seconds
    public GameObject impactEffect;
    public Rigidbody theRB;
    public int damage = 1;

    // should it damage Enemy or should it damage Player? (setting this in unity inspector for each kind of bullet)
    // with this we prevent the enemy from getting destroyed by bullet of another enemy
    public bool damageEnemy, damagePlayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // bullet direction + speed
        theRB.velocity = transform.forward * moveSpeed; // unity automatically multiplies by deltatime
        lifeTime -= Time.deltaTime; // makes it decrease by seconds

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) // other is the collider we interacted with
    {

        
        if (other.gameObject.tag == "Enemy" && damageEnemy) // if the object is enemy, destroy it
        {
            // Destroy(other.gameObject);
            //now all objects have health (DamageEnemy destroys according to currentHealth)
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
        }

        if (other.gameObject.tag == "EnemyHead" && damageEnemy)
        {
            other.transform.parent.GetComponent<EnemyHealthController>().DamageEnemy(damage*2);
            Debug.Log("Head Shot");
        }


        if (other.gameObject.tag == "Player" && damagePlayer) // if we got hit by enemy bullet
        {
            Debug.Log("We got Hit at " + transform.position);
        }

        Destroy(gameObject);
        Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation);
    }

}
