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
        bullets += ammoRestored;
        if (bullets > ammoMax) {
            bullets = ammoMax;
        }
        ammo[bullets].gameObject.SetActive(true);
    }

    public void Fire(int ammoConsumed)
    {
        if (bullets >= ammoConsumed)
        {
            bullets -= ammoConsumed;
            ammo[bullets].gameObject.SetActive(false);
        }
    }
}
