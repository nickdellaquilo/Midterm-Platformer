using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_with : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.transform.SetParent(null);
        }
    }
}
