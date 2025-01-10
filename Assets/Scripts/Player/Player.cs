using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private static bool CanMove, CanAttack;
    private float Movespeed, RotateSpeed;
    private float RotY;
    private float MoveHorizontal, MoveVertical;
    public Transform GroundPosition;
    public LayerMask GroundLayer;
    public LayerMask EnemyLayer;
    private int DamageToEnemy = 50;
    public BoxCollider Swordcollider;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        CanMove = false;
        Movespeed = 30f;
        RotateSpeed = 2f;
        RotY = transform.eulerAngles.y;
    }
    private void Update()
    {

        InputCheck();
    }
    void FixedUpdate()
    {
        Move_Rotate();
        AnimatePlayer();
    }

    void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!Sound.instance.Run.isPlaying)
            {
                RunPlay();
            }
            MoveVertical = 1;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (Sound.instance.Run.isPlaying)
            {
                Sound.instance.Run.Stop();
            }
            MoveVertical = 0;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveHorizontal = 1;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            MoveHorizontal = 0;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveHorizontal = -1;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            MoveHorizontal = 0;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Move_Rotate()
    {
        // Calculate the movement vector based on the player's current rotation
        Vector3 movement = transform.forward * MoveVertical * Movespeed;

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        RotY += MoveHorizontal * RotateSpeed;
        transform.rotation = Quaternion.Euler(0f, RotY, 0f);
    }


    void AnimatePlayer()
    {
        if (MoveVertical != 0)
        {
            if (!CanMove)
            {
                CanMove = true;
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                {
                    anim.SetTrigger("Run");
                }
            }
        }
        if (MoveVertical == 0)
        {
            if (CanMove)
            {
                CanMove = false;
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                {
                    anim.SetTrigger("Stop");
                }
            }
        }
    }

    void Attack()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || !anim.GetCurrentAnimatorStateInfo(0).IsName("RunAttack"))
        {
            Sound.instance.Attack.Play();
            anim.SetTrigger("Attack");
        }
    }

    void Jump()
    {
        if (Physics.Raycast(GroundPosition.position, Vector3.down, .2f, GroundLayer))
        {
            anim.SetTrigger("Jump");
        }
    }

    public void JumpPlay()
    {
        Sound.instance.Jump.Play();
        Sound.instance.Run.Stop();
    }

    public void JumpStop()
    {
        Sound.instance.Jumped.Play();
    }

    public void RunPlay()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Sound.instance.Run.Play();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().ApplyDamage(DamageToEnemy);
        }
        if (other.tag == "coin")
        {
            other.gameObject.SetActive(false);
            GameManagerMaze.instance.CoinIncrement();
        }
        if(other.tag == "gold")
        {
            other.gameObject.SetActive(false);
            GameManagerMaze.instance.GameOver();
        }
    }

    public void ActivateDamagePoint()
    {
        Swordcollider.enabled = true;
    }

    public void DeactivateDamagePoint()
    {
        Swordcollider.enabled = false;
    }

    public void RunAttackInput()
    {
        anim.Play("Idle");
    }
}