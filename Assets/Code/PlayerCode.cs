using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCode : MonoBehaviour
{
    public Transform feetTrans;
    public LayerMask groundLayer;
    [SerializeField] public float speed = 10;
    [SerializeField] public float accel = 1;
    [SerializeField] public float runMult = 2;
    [SerializeField] public float slideForce = 500;
    [SerializeField] public int maxJumps = 1;
    [SerializeField] public int jumpForce = 3000;
    public int numJumps = 0;
    //[SerializeField] int coyoteTime = 8;
    public bool grounded = false;
    private float xSpeed = 0;
    private float ySpeed = 0;
    private Rigidbody2D _rigidbody;
    Animator _animator;
    SpriteRenderer _renderer;
    public Transform fireLoc;
    Vector2 spawnPoint;
    bool isDead = false;
    bool sliding = false;
    bool running = false;
    public AmmoCode ammoMechanic;
    public HealthCode hpMechanic;

    private void Awake()
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
        ySpeed = _rigidbody.velocity.y;
        _animator.SetFloat("xSpeed", Mathf.Abs(xSpeed));
        _animator.SetFloat("ySpeed", Mathf.Abs(ySpeed));
        _animator.SetBool("Slide", sliding);

        if (Input.GetButton("Run") && !sliding)
        {
            running = true;
        }
        else
        {
            running = false;
        }

        if (Input.GetButtonDown("Jump") && grounded && !sliding)
        {
            _animator.SetBool("Jump", true);
            _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x, jumpForce));
        }

        if (Input.GetButtonDown("Fire1"))
        {  
            _animator.SetTrigger("Shoot");
            Debug.Log("Shooting1");
            ammoMechanic.Fire(1);
            //hpMechanic.TakeDamage(1);
        }
        else { _animator.ResetTrigger("Shoot"); }

        if (Input.GetButtonDown("Slide") && grounded ) //&& Mathf.Abs(xSpeed) >= speed
        {
            if (!sliding) {
                runSlideAnim();
            }
            
        }
        
    }

    void FixedUpdate()
    {
        if (!sliding)
        {
            xSpeed = Input.GetAxisRaw("Horizontal") * speed;
            if (running) { xSpeed *= runMult; }
        }
        else { xSpeed = Mathf.Lerp(xSpeed, speed, 0.025f); }

        _rigidbody.velocity = new Vector2(xSpeed, ySpeed);

        if ((xSpeed < 0 && transform.localScale.x > 0) || (xSpeed > 0 && transform.localScale.x < 0))
        {
            transform.localScale *= new Vector2 (-1, 1);
        }

        _animator.ResetTrigger("Damaged");
        _animator.ResetTrigger("Dead");
    }

    public void runSlideAnim() {
        
        StartCoroutine(SlideEnd());

    }

    public void playDeathAnim() {
        _animator.SetTrigger("Dead");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*
        if (other.CompareTag("Checkpoint"))
        {

        }*/

        if (other.tag == "Enemy" && !sliding){
            hpMechanic.TakeDamage(1);
            _animator.SetTrigger("Damaged");
            Debug.Log("damage trigger");
        }
    }
    

    IEnumerator PlayerRespawn(Transform other)
    {
        isDead = true;
        yield return new WaitForSeconds(0.5f);
        isDead = false;
        spawnPoint = other.transform.position;
    }

    
    IEnumerator SlideEnd() {
        sliding = true;
        _animator.SetBool("Slide", sliding);
        
        Debug.Log(xSpeed);
        
        yield return new WaitForSeconds(0.75f);
        Debug.Log("Reached");
        sliding = false;
        _animator.SetBool("Slide", false);
        
    }
    

}