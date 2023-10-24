using UnityEngine;

public class PlayerControllerOld : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform cameraTransform;
    public Animator animator;
    public float stepHeight = 3.0f;
    public float stepSmoothness = 0.3f;

    private bool isJumping = false;
    private bool isClimbing = false;
    private float stepOffset = 0.0f;
    private RaycastHit hitInfo;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isRun", false);
        animator.SetBool("isJump", false);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = cameraTransform.forward * verticalInput + cameraTransform.right * horizontalInput;
        direction.y = 0.0f;
        direction = direction.normalized;

        // Check if the player is currently climbing stairs
        if (isClimbing)
        {
            
            // Move the player up the stairs with a smooth step motion
            float stepSpeed = (stepHeight+hitInfo.distance - stepOffset) / stepSmoothness;
            transform.position += direction * stepSpeed * Time.deltaTime;
            Debug.Log("||1||");
            Debug.Log(stepSpeed);
            Debug.Log(transform.position);
            Debug.Log("||1||");

            transform.position += Vector3.up * stepSpeed * Time.deltaTime;

            Debug.Log("||2||");
            Debug.Log(stepSpeed);
            Debug.Log(transform.position);
            Debug.Log("||2||");
            // Check if the player has reached the top of the stairs
            if (transform.position.y >= hitInfo.point.y + hitInfo.normal.y * stepHeight)
            {
                // Stop climbing and resume normal movement
                isClimbing = false;
                
            }

            return;
        }


        transform.position += direction * speed * Time.deltaTime;

        // Make the character face the movement direction
        if (direction != Vector3.zero)
        {
            transform.LookAt(transform.position + direction);
        }

        // Set the isRun parameter of the animator based on the player's movement
        if (horizontalInput != 0 || verticalInput != 0)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }

        // Set the isJump parameter of the animator when spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            animator.SetBool("isJump", true);
            isJumping = true;
        }

        // Set the isJump parameter of the animator to false when the jump animation finishes
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Jump") && stateInfo.normalizedTime >= 1.0f)
        {
            animator.SetBool("isJump", false);
            isJumping = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stairs"))
        {
         
            isClimbing = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stairs"))
        {

            isClimbing = false;
        }
    }
}
