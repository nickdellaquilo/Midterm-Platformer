using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCode : MonoBehaviour
{
    public Transform feetTrans;
    public LayerMask groundLayer;

    [SerializeField] public int speed = 5;
    [SerializeField] public int accel = 1;
    [SerializeField] public int runMult = 2;

    public int numJumps = 0;
    [SerializeField] public int maxJumps = 1;
    [SerializeField] public int jumpForce = 500;
    //[SerializeField] int coyoteTime = 8;
    
    public bool grounded = false;

    private float xSpeed = 0;
    //private float ySpeed = 0;

    Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        xSpeed = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetButton("Run")) { xSpeed *= runMult; }

        grounded = Physics2D.OverlapCircle(feetTrans.position, .3f, groundLayer);
        if (grounded) { numJumps = maxJumps; }

        if (Input.GetButtonDown("Jump") && numJumps > 0)
        {
            _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x, jumpForce));
            numJumps--;
        }
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        
    }
}
