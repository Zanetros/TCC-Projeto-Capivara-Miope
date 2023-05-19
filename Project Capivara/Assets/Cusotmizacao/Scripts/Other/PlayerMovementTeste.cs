using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementTeste : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    private Rigidbody2D myRigidbody;
    private Vector3 playerMovement;
    public Vector2 lastMotionVector;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        playerMovement = Vector3.zero;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        playerMovement = new Vector3(
            horizontal,
            vertical
            );

        if (horizontal != 0 || vertical != 0)
        {
            lastMotionVector = new Vector2(
                horizontal,
                vertical
                ).normalized;

            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical);
        }

        UpdateAnimationAndMove();
    }

    private void UpdateAnimationAndMove()
    {
        if (playerMovement != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", playerMovement.x);
            animator.SetFloat("moveY", playerMovement.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    private void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + playerMovement * speed * Time.deltaTime);
    }
}
