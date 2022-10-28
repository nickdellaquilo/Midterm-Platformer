using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorCode : MonoBehaviour
{
    public string levelName = "Scene2";
    
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("hit");
        if(other.CompareTag("Player")) {
                Debug.Log("Made It!");
                SceneManager.LoadScene(levelName);
            }
            
        }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
