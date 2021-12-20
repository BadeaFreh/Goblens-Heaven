using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    // public float moveSpeed;
    // public Rigidbody theRB; // this gets the rigidBody that is a component to this object (Enemy!)
    // public Transform target;
    private bool chasing;
    public float distanceToChase = 10f;
    public float distanceToLose = 15f; // the player has to move 15 units away
    public float distanceToStop = 2f; // prevent the enemy from pushing the player when he gets to him
    public float keepChasingTime = 5f;
    private float chaseCounter;
    public GameObject bullet;
    public Transform firePoint;
    public Animator anim;

    // timeToShoot is how much time enemy can shoot (consistently)
    // waitBetweenShots is how much time stops shooting in between
    public float fireRate, waitBetweenShots = 2f, timeToShoot = 1f;
    private float fireCount, shotWaitCounter, shootTimeCounter;

    // this will take the Player position, but with changing the y of it (prevent enemy flying)
    private Vector3 targetPoint;
    public NavMeshAgent agent; // this belongs to the AI library
    private Vector3 startPoint; // saves our initial position (to be able to go back to it)
    void Start()
    {
        startPoint = transform.position; // saving initial position of enemy

        //setting these 2 vars to their max vals (in the Update the start decreasing)
        shootTimeCounter = timeToShoot;
        shotWaitCounter = waitBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = PlayerController.instance.transform.position; // wherever the player is (the enemy target)
        // the target point we are going to look toward is just as the same point as where our enemy currently is (not in the air)
        targetPoint.y = transform.position.y; // what controls the up and down movements

        if (!chasing)
        {
            // Distance is Vector3 method that tells us how much between one object and another (takes Vector3 objs)
            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chasing = true;
                // when enemy starts chasing, reseting his shooting counters:
                shootTimeCounter = timeToShoot;
                shotWaitCounter = waitBetweenShots;
            }

            if (chaseCounter > 0) // we don't want him to go back to startPoint instantly
            {
                chaseCounter -= Time.deltaTime; // that's how we count down: decreasing by delta time
                if (chaseCounter <= 0)
                {
                    agent.destination = startPoint; // make him go back to his initial place
                }
            }

            if (agent.remainingDistance < .25f)
            {
                anim.SetBool("isMoving", false);
            }

            else
            {
                anim.SetBool("isMoving", true);
            }
        }
        else
        {  // LET ENEMY CHASE US

            // make the enemy LOOK AT the instance (there is only one) of the player

            // transform.LookAt(targetPoint); // THAT's what prevents enemy from flying chasing us

            // changing the enemy's own rigidBody velocity (speed with direction) to be: forward, by our speed
            // theRB.velocity = transform.forward * moveSpeed;

            // the next 2 statements prevent player from getting so close to us
            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
                agent.destination = targetPoint; // with this, no need to LookAt and updating velocity anymore!
            else // when he is 2 units close to us
                agent.destination = transform.position; // stop moving

            // if we are far enough from the enemy, make him stop chasing!
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToLose)
            {
                chasing = false;
                chaseCounter = keepChasingTime; // the time before going back to startPoint
            }

            if (shotWaitCounter > 0) // when enemy starts chasing, make him wait a little before starting to shoot
            {
                shotWaitCounter -= Time.deltaTime;

                // if it's time to shoot, now it's time to control how many bullets he can shoot at once
                if (shotWaitCounter <= 0)
                {
                    shootTimeCounter = timeToShoot; // init val for how much firing at once
                }

                anim.SetBool("isMoving", true);
            }

            else // when it's time for enemy to shoot on us
            {
                if (PlayerController.instance.gameObject.activeInHierarchy) // prevent player from shooting after we lose
                {
                    shootTimeCounter -= Time.deltaTime;

                    if (shootTimeCounter > 0)
                    {
                        fireCount -= Time.deltaTime;

                        if (fireCount <= 0)
                        {
                            fireCount = fireRate; // fireRate set in inspector to 0.3
                                                  // adding little bit in the y axis, so that enemy don't look at our feet
                            firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.2f, 0f)); // now enemy bullets go towards us (not always horizontally moving)

                            // check angle of the player
                            Vector3 targetDir = PlayerController.instance.transform.position - transform.position;
                            float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);
                            if (Mathf.Abs(angle) < 30f)
                            {
                                Instantiate(bullet, firePoint.position, firePoint.rotation);
                                anim.SetTrigger("fireShot"); // that's how we trigger the trigger variable
                            }
                            else
                            {
                                shotWaitCounter = waitBetweenShots;
                            }

                        }
                        agent.destination = transform.position;
                    }
                    else // shootTimeCounter is below 0
                    {
                        shotWaitCounter = waitBetweenShots;
                    }
                    anim.SetBool("isMoving", false);
                }
            }
        }
    }
}
