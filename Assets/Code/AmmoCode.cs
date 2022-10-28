using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmmoCode : MonoBehaviour
{
    [SerializeField] public GameObject[] ammo;
    private int ammoMax;
    private int bullets;
    private Animator anim;

    private void Start()
    {
        bullets = ammo.Length;
        ammoMax = ammo.Length;
    }

    public void reload(int ammoRestored) {
        
        if (bullets < 5) {
            ammo[bullets].gameObject.SetActive(true);
            bullets += ammoRestored;
            
        }
        
    }

    public void Fire(int ammoConsumed)
    {
        Debug.Log("AmmoEaten");
        if (ammoConsumed > 0)
        {
            bullets -= ammoConsumed;
            ammo[bullets].gameObject.SetActive(false);
        }
    }
}
