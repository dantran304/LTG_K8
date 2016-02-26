using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

    public float moveSpeed;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigid;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigid = GetComponent<Rigidbody>();
    }


    // dung cho physic
    void FixedUpdate()
    {
        float mx = Input.GetAxisRaw("Horizontal");
        float mz = Input.GetAxisRaw("Vertical");
        Move(mx, mz);
        Turning();
    }

    // ham di chuyen
    void Move(float x, float z) 
    {
        // set movement vector
        movement.Set(x, 0f, z);

        //deltatime = time between update call
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        playerRigid.MovePosition(transform.position + movement);
    }

    // quay player = chuot
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            playerRigid.MoveRotation(newRotation);
        }
    }

    void Update()
    {
        AnimationControl();
        RotatePlayer();

        if (Input.GetButton("Fire1"))
        {
            Attack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetTrigger("guard");
        }
    }
    void AnimationControl()
    {

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (Input.GetKey(KeyCode.A)) transform.rotation = Quaternion.Euler(0, 135, 0);
            if (Input.GetKey(KeyCode.D)) transform.rotation = Quaternion.Euler(0, -135, 0);
        }

        if (Input.GetKey(KeyCode.A))
            transform.rotation = Quaternion.Euler(0, 90, 0);

        if (Input.GetKey(KeyCode.D))
            transform.rotation = Quaternion.Euler(0, -90, 0);

        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (Input.GetKey(KeyCode.A)) transform.rotation = Quaternion.Euler(0, 45, 0);
            if (Input.GetKey(KeyCode.D)) transform.rotation = Quaternion.Euler(0, -45, 0);
        }
    } 

    void Attack()
    {
        anim.SetTrigger("attack1");
    }

}
