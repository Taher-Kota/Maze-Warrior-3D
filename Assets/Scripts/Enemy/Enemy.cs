using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private Transform player;
    private float Enemy_Attack_Dist = 6f, Enemy_Chasing_Dist = 70f;
    private float Enemy_Speed;
    private int EnemyHitDamage = 5;
    public GameObject DamagePoint;
    public LayerMask PlayerLayer;
    private int PlayerHitTime = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        Enemy_Speed = .5f;
    }

    void Update()
    {
        EnemyAI();
    }

    void EnemyAI()
    {
        Vector3 Direction = player.transform.position-transform.position;
        // float Distance = Direction.sqrMagnitude;
        float Distance = Vector3.Distance(player.transform.position, transform.position);
        if (Distance > Enemy_Attack_Dist && Distance < Enemy_Chasing_Dist)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                anim.SetTrigger("Stop");
            }

            anim.SetTrigger("Chase");
            transform.LookAt(player.transform.position);
            rb.velocity = new Vector3(Direction.x*Enemy_Speed,rb.velocity.y,Direction.z*Enemy_Speed);

        }
        else if(Distance < Enemy_Attack_Dist)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Chase"))
            {
                anim.SetTrigger("Stop");
            }
            AttackPlayer();
            transform.LookAt(player.transform.position);
            anim.SetTrigger("Attack");
        }
        else
        {
            rb.velocity = Vector3.zero;
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || anim.GetCurrentAnimatorStateInfo(0).IsName("Chase"))
            {
                anim.SetTrigger("Stop");
            }
        }

    }

    void AttackPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(DamagePoint.transform.position, .1f, PlayerLayer);
        if (hits.Length > 0)
        {
            PlayerHitTime++;
            if (PlayerHitTime == 1)
            {
                hits[0].gameObject.GetComponent<PlayerHealth>().ApplyDamage(EnemyHitDamage);
                PlayerHitTime--;
            }
        }
    }

    public void ActivateDamagePoint()
    {
        DamagePoint.SetActive(true);
    }

    public void DeactivateDamagePoint()
    {
        DamagePoint.SetActive(false);
    }
}