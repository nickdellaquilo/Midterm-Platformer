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
    [SerializeField] public int maxJumps = 1;
    [SerializeField] public int jumpForce = 2500;
    public int numJumps = 0;
    //[SerializeField] int coyoteTime = 8;
    public bool grounded = false;
    private float xSpeed = 0;
    private float ySpeed = 0;
    Rigidbody2D _rigidbody;
    Animator _animator;
    SpriteRenderer _renderer;
    public Transform fireLoc;
    Vector2 spawnPoint;
    bool isDead = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(feetTrans.position, 0.9f, groundLayer);
        _animator.SetBool("Grounded", grounded);
        //if (grounded) { numJumps = maxJumps; }
        xSpeed = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetButton("Run"))
        {
            xSpeed *= runMult;
            _animator.SetBool("Shoot", true);
        }
        else { _animator.SetBool("Shoot", false); }
        if (Input.GetButtonDown("Jump") && grounded)
        {
            _animator.SetBool("Jump", true);
            _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x, jumpForce));
        }
        if (Input.GetButtonDown("Fire1"))
        {  
            _animator.SetTrigger("Shoot");
        }
        if (grounded)
        {
            _animator.SetBool("Jump", false);
            
        }
    }

    void FixedUpdate()
    {
        ySpeed = _rigidbody.velocity.y;
        _rigidbody.velocity = new Vector2(xSpeed, ySpeed);

        if ((xSpeed < 0 && transform.localScale.x > 0) || (xSpeed > 0 && transform.localScale.x < 0))
        {
            transform.localScale *= new Vector2 (-1, 1);
        }

        _animator.SetFloat("xSpeed", Mathf.Abs(xSpeed));
        _animator.SetFloat("ySpeed", Mathf.Abs(ySpeed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {

        }
    }

    IEnumerator PlayerRespawn(Transform other)
    {
        isDead = true;
        yield return new WaitForSeconds(0.5f);
        isDead = false;
        spawnPoint = other.transform.position;
    }

}
