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

    SpriteRenderer player_SpriteRenderer;
    public Sprite spr_idle;
    public Sprite spr_walk;
    public Sprite spr_jump;
    public Sprite spr_damage;
    public Sprite spr_still;

    [SerializeField] Sprite curr_sprite;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player_SpriteRenderer = GetComponent<SpriteRenderer>();
        player_SpriteRenderer.sprite = spr_still;
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

        if      (xSpeed <  0 && grounded) { player_SpriteRenderer.flipX  = true;  }
        else if (xSpeed >  0 && grounded) { player_SpriteRenderer.flipX  = false; }
        else if (xSpeed == 0 && grounded) { player_SpriteRenderer.sprite = spr_idle; }


        curr_sprite = player_SpriteRenderer.sprite;
    }
}
