using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [Header("Importing")]
    public CharacterController controller;
    public LayerMask ground;
    public Transform groundCheck;
    [Header("Variables")]
    [Range(1, 10)]
    public float speed;
    public float sideTime;
    [Header("Info")]
    public float groundFails = 0;

    GameObject player;
    float sideTimeStart;
    float sideState;

    float gravity = -9.81f;
    bool isGrounded;
    Vector3 velocity;

    public bool checkGround(Vector3 position)
    {
        RaycastHit hit;
        Physics.Raycast(position, Vector3.down, out hit, 5f);
        return hit.collider != null;
    }

    private void enemyGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, ground);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    //функции передвижения

    public bool moveRight()
    {
        Vector3 direction = transform.right * speed * Time.deltaTime;
        if (checkGround(transform.position + direction))
        {
            controller.Move(direction);
        }
        return checkGround(transform.position + direction);
    }

    public bool moveLeft()
    {
        Vector3 direction = transform.right * -1 * speed * Time.deltaTime;
        if (checkGround(transform.position + direction))
        {
            controller.Move(direction);
        }
        return checkGround(transform.position + direction);
    }

    public bool moveForward()
    {
        Vector3 direction = transform.forward * speed * Time.deltaTime;
        if (checkGround(transform.position + direction))
        {
            controller.Move(direction);
        }
        return checkGround(transform.position + direction);
    }

    public bool moveBack()
    {
        Vector3 direction = transform.forward * -1 * speed * Time.deltaTime;
        if (checkGround(transform.position + direction))
        {
            controller.Move(direction);
        }
        return checkGround(transform.position + direction);
    }


    //Возвращение на арену (защита от out of bounce)
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        //сокращение дистанции
        if (checkGround(transform.position))
        {
            if (playerDistance > 6f)
            {
                moveForward();
            }
            else if (playerDistance < 5.5f)
            {
                moveBack();
            }
        }
        if(sideState == 0)
        {
            if(!moveLeft())
            {
                sideState = 1;
                sideTimeStart = Time.time;
            }    
        }
        else
        {
            if (!moveRight())
            {
                sideState = 0;
                sideTimeStart = Time.time;
            }
        }

        if (sideTimeStart+sideTime < Time.time)
        {
            sideState = Random.Range(0, 2);
            sideTimeStart = Time.time;
        }

        transform.LookAt(player.transform);
        enemyGravity();
    }
}