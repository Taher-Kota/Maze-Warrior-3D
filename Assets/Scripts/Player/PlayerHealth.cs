using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int Health;
    private Animator anim;
    private Player playerScript;
    public bool Dead= false;

    void Awake()
    {
        playerScript = GetComponent<Player>();
        anim = GetComponent<Animator>();
        Health = 450;
    }
    private void Start()
    {
        GameManagerMaze.instance.HealthDisplay(Health);
    }

    public void ApplyDamage(int damage)
    {
        Sound.instance.PlayerDamaged.Play();
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
        }
        GameManagerMaze.instance.HealthDisplay(Health);
        if (Health == 0)
        {
            Dead = true;
            playerScript.enabled = false;
            anim.Play("Dead");
            GameManagerMaze.instance.GameOver();
        }
    }
    
}
