using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

    public AudioSource Coin,Run,Jump,Jumped,Attack,PlayerDamaged,EnemyDead;
    public static Sound instance;

    private void Awake()
    {
        instance = this;
    }

    public void CoinSound()
    {
        Coin.Play();
    }
    public void RunSound()
    {
        Run.Play();
    }
}
