using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public bool shouldMove, shouldRotate;
    public float moveSpeed, rotateSpeed; // rotationSpeed should be relatively high in general (45 is good)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            // change the position of this object (one direction)
            transform.position += new Vector3(moveSpeed, 0f, 0f) * Time.deltaTime;
        }

        if (shouldRotate) // with rotations we usually use Quaternion, and to make it Vector3, we use .Euler
        {
            // you need euler angels after .rotation
            // current rotation + force we want (defining it inside Vector3)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, rotateSpeed * Time.deltaTime, 0f));
        }
    }
}
