using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Animator")]
    [SerializeField] private Animator anim;


    private void awake(){
        initScale = enemy.localScale;
    }

    private void OnDisable(){
        anim.SetBool("moving", false);
    }

    private void Update(){
        MoveInDirection(1);
        if (movingLeft){
            if(enemy.position.x >= leftEdge.position.x){
                MoveInDirection(-1);
            } else {
                //change direction
                DirectionChange();
            }
        } else {
            if (enemy.position.x <= rightEdge.position.x){
                MoveInDirection(1);
            } else {
                //change direction
                DirectionChange();
            }
        }
    }

    private void DirectionChange(){
        //anim.SetBool("moving", false);
        movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction){
        //anim.SetBool("moving", false);

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) *_direction, initScale.y, initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y,
         enemy.position.z);
    }
}
