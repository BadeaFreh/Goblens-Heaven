using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f, gravityModifier, jumpPower, runSpeed = 12f;
    public CharacterController charCon;
    
    private Vector3 moveInput;
    public Transform camTrans;
    public float mouseSensitivity;
    public bool invertX;
    public bool invertY;
    private bool canJump, canDoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {

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
            canDoubleJump = true;
        }

        else if (canDoubleJump && Input.GetKeyDown(KeyCode.Space)) // hitting space for 2nd time in same frame
        {
            moveInput.y = jumpPower;
            canDoubleJump = false;
        }

        charCon.Move(moveInput * Time.deltaTime);

        // control camera rotation
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        // for testing in UNITY: change invertX and invertY
        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }

        if (invertY)
        {
            mouseInput.y = -mouseInput.y;
        }
        
        // transform object: whatever this object is attached to (player in this case), get the transform of it
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x
        , transform.rotation.eulerAngles.z); // changing the y axis (moving the mouse to left and right)

        transform.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f)); // changing the x axis

        anim.SetFloat("moveSpeed", moveInput.magnitude); // tells how face our object is moving
        anim.SetBool("onGround", canJump);
    }
}
