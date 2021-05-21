using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] AudioSource punch;

    Vector2 initPos;

    public Animator anim;

    public LayerMask ground;
    public LayerMask enemy;

    public int dir;

    [SerializeField] float jumpForce         = 4;
    [SerializeField] float speedMax          = 4;
    [SerializeField] float attackForce       = 7;
    [SerializeField] float timeBetweenHits   = 2f,tmp;
    public float healthVal         = 10;      

    [Header("Check if grounded")]
    [SerializeField] Transform checkGround;

    [SerializeField] Transform checkEnemy;

    public float speed          = 10;
    public float gravityScale   = 2;

    Rigidbody2D rb;

    public bool canMove      = false;
    public bool faceRight    = true;
    public bool grounded     = true;

    [Header("Check inputs")]
    public bool jumping      = false;
    public bool attacking    = false;
    public bool run          = false;

    public bool Stillattacking            = false;

    // Start is called before the first frame update
    void Start()
    {
       tmp = timeBetweenHits;
       anim = GetComponent<Animator>();
       rb = GetComponent<Rigidbody2D>();
       initPos = transform.position;
    }

    Vector2 move(){
      float x = Input.GetAxisRaw("Horizontal") * speedMax * Time.fixedDeltaTime;
      return new Vector2(x, 0);
    }    

    void jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        if (rb.velocity.y < 0)
            rb.gravityScale = gravityScale;
        else if (rb.velocity.y > 0 && !jumping)
            rb.gravityScale = gravityScale;
    }

    void Inputs()
    {
        jumping    = Input.GetKeyDown(KeyCode.Space)    &&        (!attacking)                  ? true : false;
        attacking  = (Input.GetKeyDown(KeyCode.Mouse0)  && timeBetweenHits <= 0)||Stillattacking? true : false;
        run        = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)        ? true : false;
    }    

    void flipPlayer()
    {
        if (!attacking && faceRight && Input.GetAxisRaw("Horizontal") < 0)
        {
            flip();
            faceRight = false;
        }
        if(!attacking && !faceRight && Input.GetAxisRaw("Horizontal") > 0)
        {
            flip();
            faceRight = true;
        } 
    }

    void flip(){
        Vector3 scale = transform.localScale;
        scale.x = -transform.localScale.x;
        transform.localScale = scale;
    }

    void setAnimations(){ 
        anim.SetBool("Run"    ,run);
        anim.SetBool("Jumping",!grounded);
        anim.SetBool("Attack" ,attacking);
    }

    void Attack(){
        dir = faceRight ? 1 : -1;
        if (Input.GetKeyDown(KeyCode.Mouse0) && !attacking && timeBetweenHits <= 0)
        {
         punch.time = 0.1f;
         punch.Play();
         Stillattacking = true;
        // transform.position = Vector3.Lerp(transform.position, initPos + new Vector2(1.2f * dir,0), attackForce);
         //timeBetweenHits = tmp;
         //initPos = transform.position;
        }
        timeBetweenHits = timeBetweenHits > 0 ? timeBetweenHits -= Time.deltaTime/10 : 0;
    }

    private void Update()
    { 
        if (canMove)
        {
          Inputs();
          flipPlayer(); 
          grounded = checkIfGrounded();
          if(!attacking)   
          if(!detectIfCloseToEnemy())
           transform.Translate(move(),Space.World);
          if (attacking)
           {
             StartCoroutine(attackTime());   
           }
          setAnimations();
        } 
    }

    bool detectIfCloseToEnemy()
    {
        RaycastHit2D rayhit = Physics2D.Raycast(checkEnemy.position, Vector2.right, .2f);
        if (rayhit.rigidbody != null)
         if (rayhit.rigidbody.name == "Enemies")
            return true;     
        return false;
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
          if(grounded) 
           Attack();
          if (jumping && grounded)
             jump();

         if (rb.velocity.y < 0)
             rb.velocity += Physics2D.gravity * 1.5f * Time.deltaTime;
        }
    }

   public bool checkIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(checkGround.position, Vector2.down,.2f);
        if (hit.collider != null)
            return true;
        return false;
    }

    IEnumerator attackTime()
    {
        yield return new WaitForSeconds(0.7f);
        Stillattacking = false;
    }
}