using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    [Header ("Moving Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Platform")]
    [SerializeField] private Transform platform;

    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle")]
    [SerializeField]private float idleDuration;
    private float idleTimer;

    private void Awake(){
        initScale = platform.localScale;
    }

    // Update is called once per frame
    private void Update()
    {
        if (movingLeft){
            if(platform.position.x >= leftEdge.position.x){
                MoveInDirection(-1);
            } else {
                //change direction
                DirectionChange();
            }
        } else {
            if (platform.position.x <= rightEdge.position.x){
                MoveInDirection(1);
            } else {
                //change direction
                DirectionChange();
            }
        }
        
    }

    private void DirectionChange(){
        idleTimer += Time.deltaTime;
        if(idleTimer > idleDuration){
            movingLeft = !movingLeft;
        }
    }

    private void MoveInDirection(int _direction){
        idleTimer = 0;
        platform.position = new Vector3(platform.position.x + Time.deltaTime * _direction * speed, platform.position.y,platform.position.z);
    }
}
