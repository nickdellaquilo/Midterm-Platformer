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
    Animator anim;
    public PlayerCode pcscript;

    private void Start()
    {
        life = hearts.Length;
    }

    private void Update() {
        if (life == 0)
        {
            pcscript.gameOver();
        }
    }

    public void TakeDamage(int damage) {
        if (life > 0)
        {
            life--;
            hearts[life].gameObject.SetActive(false);
        }
        if (life == 0)
            {
                pcscript.playDeathAnim();
                pcscript.gameOver();
            }
    }

    public void RestoreHealth(int damage) {
        if (life < 5)
        {
            hearts[life].gameObject.SetActive(true);
            life += damage;
            
        }
    }
}
