using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthAmmoBoxCode : MonoBehaviour
{
    public AmmoCode ammoMech;
    public HealthCode hpMech;
    [SerializeField] private Rigidbody2D rb;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hpMech.RestoreHealth(1);
            ammoMech.reload(1);
            Destroy(gameObject, 0f);
        }
    }
}
