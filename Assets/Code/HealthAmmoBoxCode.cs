using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthAmmoBoxCode : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
private void OnCollisionEnter2D(Collision2D other)
{
    if (other.gameObject.CompareTag("Player"))
    {
        Destroy(gameObject, 1f);
    }
}
}
