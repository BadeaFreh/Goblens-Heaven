using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // static vars can't be shown in UNITY
    public static PlayerController instance; // unique version of the script within the game. This is instance for this script!

    public float moveSpeed = 8f, gravityModifier, jumpPower, runSpeed = 12f;
    public CharacterController charCon;
    private Vector3 moveInput;
    public Transform camTrans;
    public float mouseSensitivity;

    private bool canJump, canDoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public Animator anim;

    // public GameObject bullet; // from prefabs
    public Transform firePoint; // creating a "spawner" for bullets on our gun
    public Gun activeGun;

    // Start is called before the first frame update

    // Awake happens right before Start()
    void Awake()
    { // happens straight away in unity, for example when we deactivate or reactivate an object
        instance = this;
    }

    void Start()
    {
        UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        // Why is the player moving too fast?
        // it moves 8 places every frame => in 1 sec it is 60 * 8 units!
        // the good thing about Time.DeltaTime is no matter what frame rates you have, you'll still do the same movement

        // moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // A D Buttons
        // moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime; // S W Buttons (See Input Manager in Project Settings)

        // store y velocity
        if (!UIController.instance.pauseScreen.activeInHierarchy && !GameManager.instance.levelEnding)
        {
            float yStore = moveInput.y;

            Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
            Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

            moveInput = (vertMove + horiMove);
            moveInput.Normalize();

            if (Input.GetKey(KeyCode.LeftShift)) // using left shift to speed up
            {
                moveInput *= runSpeed;
            }
            else
            {
                moveInput *= moveSpeed;
            }

            moveInput.y = yStore;
            moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime; // physics gravity = 9.81 (9.81*2)

            if (charCon.isGrounded) // charCon is the player
            {
                moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
            }

            // Jumping
            // draws imaginary sphere and checks if any object is in that sphere
            // 0.25 is radius for the sphere
            canJump = Physics.OverlapSphere(groundCheckPoint.position, 0.25f, whatIsGround).Length > 0;

            if (Input.GetKeyDown(KeyCode.Space) && canJump) // we did first jump
            {
                moveInput.y = jumpPower;
                // canDoubleJump = true;
                AudioManager.instance.PlaySXF(5);
            }

            else if (canDoubleJump && Input.GetKeyDown(KeyCode.Space)) // hitting space for 2nd time in same frame
            {
                moveInput.y = jumpPower;
                canDoubleJump = false;
                AudioManager.instance.PlaySXF(5);
            }

            charCon.Move(moveInput * Time.deltaTime);

            // control camera rotation
            Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

            // transform object: whatever this object is attached to (player in this case), get the transform of it
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x
            , transform.rotation.eulerAngles.z); // changing the y axis (moving the mouse to left and right)

            transform.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f)); // changing the x axis

            // Handle shooting
            if (Input.GetMouseButtonDown(0)) // left click
            {
                RaycastHit hit; //stores information about whether a raycast is hit

                // make the ray travel for 50 units
                if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 50f)) // true if it hits something
                {
                    if (Vector3.Distance(camTrans.position, hit.point) > 2f)
                    {
                        // rotate out firepoint to face wherever the object
                        firePoint.LookAt(hit.point); // .point tells us where did the raycast hit against object
                    }
                }

                else
                {
                    // telling the firepoint not to look straigt a head
                    firePoint.LookAt(camTrans.position + (camTrans.forward * 30f));

                }

                FireShot();
                //Instantiate(bullet, firePoint.position, firePoint.rotation); // that's how we generate the bullet prefab, using Instantiate
            }

            anim.SetFloat("moveSpeed", moveInput.magnitude); // tells how face our object is moving
            anim.SetBool("onGround", canJump);
        }
    }

    public void FireShot()
    {
        if (activeGun.currentAmmo > 0)
        {
            activeGun.currentAmmo--;
            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation); // that's how we generate the bullet prefab, using Instantiate
            UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmo;
        }
        else
        {
            Debug.Log("You are out of ammo");
        }
    }

}
