using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthCode : MonoBehaviour
{
    [SerializeField] public GameObject[] hearts;
    private int life;
    private bool dead;
    private Animator anim;

    private void Start()
    {
        life = hearts.Length;
    }

    void Update()
    {
        if (dead)
        {
            //implement death sequence
            
        }
    }

    public void TakeDamage(int damage)
    {
        if (life >= 1)
        {
            life -= damage;
            hearts[life].gameObject.SetActive(false);
            if (life < 1)
            {
                isDead = true;
            }
            
        }
    }
}
