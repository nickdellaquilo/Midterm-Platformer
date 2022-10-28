using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorCode : MonoBehaviour
{
    public string levelName = "Scene2";
    
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
                
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
