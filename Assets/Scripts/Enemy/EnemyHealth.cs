using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int Health;
    private Animator anim;
    private Enemy EnemyScript;
    public bool Dead = false;

    private void Awake()
    {
        EnemyScript = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
        Health = 200;
    }


    void Update()
    {
        PlayerDead();
    }

    public void ApplyDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
        }
        if (Health == 0 && !Dead)
        {
            Sound.instance.EnemyDead.Play();
            Dead = true;
            EnemyScript.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            EnemyScript.enabled = false;
            anim.SetTrigger("Dead");
            Invoke("DeactivatePlayer", 3f);
        }
    }

    private void DeactivatePlayer()
    {
        gameObject.SetActive(false);
    }

    void PlayerDead()
    {
        if (GameObject.Find("Player").GetComponent<PlayerHealth>().Dead)
        {
            EnemyScript.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            anim.Play("Idle");
        }
    }
}
