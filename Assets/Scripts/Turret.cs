using UnityEngine;

public class Turret : MonoBehaviour
{

    public GameObject bullet;
    public float rangeToTargetPlayer, timeBetweenShots = 0.5f;
    private float shotCounter; // time

    public Transform gun, firePoint;

    public float rotateSpeed = 5f; // rotation usually gets high values
    void Start()
    {
        shotCounter = timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        // is the player within range?
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToTargetPlayer)
        {
            // look at the player (but don't look at his feet)
            gun.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.2f, 0f));

            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
            }

        }
        // if we are not in range:
        else
        {
            shotCounter = timeBetweenShots;
            // used .Euler before,
            // here we use Lerp (make it move from one state to another over time)
            gun.rotation = Quaternion.Lerp(gun.rotation,
            Quaternion.Euler(0f, gun.rotation.eulerAngles.y + 10f, 0f), rotateSpeed * Time.deltaTime); // 3f is how fast rotating

        }
    }
}
