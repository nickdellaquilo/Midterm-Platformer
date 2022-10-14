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
    //private float ySpeed = 0;
    Rigidbody2D _rigidbody;
    SpriteRenderer player_SpriteRenderer;
    public Sprite spr_idle;
    public Sprite spr_walk;
    public Sprite spr_jump;
    public Sprite spr_damage;
    public Sprite spr_still;
    [SerializeField] Sprite curr_sprite;
    Animator _animator;
    public Transform fireLoc;
    Vector2 spawnPoint;
    bool isDead = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player_SpriteRenderer = GetComponent<SpriteRenderer>();
        player_SpriteRenderer.sprite = spr_still;
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(feetTrans.position, .3f, groundLayer);
        if (grounded) { numJumps = maxJumps; }
        //_animator.SetBool("Grounded", grounded);
        //_animator.SetFloat("Speed", Mathf.Abs(xSpeed));
        xSpeed = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetButton("Run")) { xSpeed *= runMult; }
        
        
        if (Input.GetButtonDown("Jump") && numJumps > 0)
        {
            _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x, jumpForce));
            numJumps--;
        }
        if (Input.GetButtonDown("Fire1"))
        {  
            _animator.SetTrigger("Shoot");
        }
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);

        if ((xSpeed < 0 && transform.localScale.x > 0) || (xSpeed > 0 && transform.localScale.x < 0)) { transform.localScale *= new Vector2 (-1, 1); }

        curr_sprite = player_SpriteRenderer.sprite; //doesn't actually change any sprites, just for debug purposes
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
