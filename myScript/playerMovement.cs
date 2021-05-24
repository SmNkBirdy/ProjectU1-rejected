using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Importing")]
    public LayerMask ground;
    public Transform groundCheck;
    public CharacterController controller;
    [Header("Variables")]
    [Range(1,10)]
    public float speed;
    [Range(1, 20)]
    public float strafeSpeed;
    [Range(0, 2)]
    public float strafeLength;
    public float strafeCD;
    [Header("Info")]
    public bool strafe = false;
    public bool strafeReload = false;

    Vector3 velocity;
    float strafeTime;
    float cdStart;
    float gravity = -9.81f;
    bool isGrounded;
    float x;
    float z;

    private void playerGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, ground);
        if (isGrounded && velocity.y< 0)
        {
              velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void playerMove()
    {
        Vector3 moveDirrection = new Vector3(x * speed * Time.deltaTime, 0, z * speed * Time.deltaTime);
        if (!strafe)
        {
            controller.Move(moveDirrection);
        }
    }

    private void playerStrafe()
    {
        if (strafeReload)
        {
            if (Time.time > cdStart + strafeCD)
            {
                strafeReload = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !strafe)
        {
            strafe = true;
            strafeTime = Time.time;
        }

        if (strafe)
        {
            if (strafeTime + strafeLength > Time.time)
            {
                controller.Move(new Vector3(x * strafeSpeed * Time.deltaTime, 0, z * strafeSpeed * Time.deltaTime));
            }
            else
            {
                strafe = false;
                cdStart = Time.time;
                strafeReload = true;
            }
        }
    }

    void Start()
    {
        // опустить игрока на землю
        /*
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, transform.up * -1, out hitInfo);
        controller.Move(new Vector3(0, hitInfo.distance * -1, 0));*/
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        playerGravity();
        playerMove();
        playerStrafe();
    }
}
