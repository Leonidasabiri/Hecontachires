using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Player))]
public class Satyr : Enemy 
{
    Rigidbody2D rb;
    Player player;

    [SerializeField] AudioSource runSound;
    [SerializeField] AudioSource punchedHit;

    [SerializeField] float   timeBeforeGettingUp = 2;
    [SerializeField] float   impulseForce        = 5;
    [SerializeField] float   stepsOfAttack       = 1;
    [SerializeField] float   headHitForce        = 10;

    [SerializeField] string  selectedHitString;

    [SerializeField] int randomHitType; 

    protected override void followingPlayer()
    {
        Transform target = GameObject.Find("Warrior").transform;

        if (Vector2.Distance(transform.position, target.position) > 3 && !this.isTakingHit)
        {
          if (!checkIfCloseToItsRelative("Satyr(Clone)"))
             transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x,0), 40 * this.speed/10 * Time.fixedDeltaTime);                          
          if(transform.position.x - target.position.x < 0 && !this.isFacingRight)
          {
            this.isFacingRight = true;
            this.flip();
          }else if(transform.position.x - target.position.x > 0 && this.isFacingRight)
          {
            this.isFacingRight = false;
            this.flip();  
          }

          this.isInAttackingState = false;
          this.isInFollowingState = true;
        }
        else
        {
          transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, 0), 40 * stepsOfAttack / 10 * Time.fixedDeltaTime);         
          this.isInFollowingState = false;
          this.isInAttackingState = true;
          StartCoroutine(returnToFollow());
        }
    }

    protected override void takingHit()
    {
      rb.AddForce(Vector2.right * impulseForce * Time.fixedDeltaTime * this.player.dir, ForceMode2D.Impulse);
        punchedHit.time = 0.02f;
        punchedHit.Play();
    }
    
    void isHitting()
    {
        rb.AddForce(Vector2.right * headHitForce * Time.fixedDeltaTime * -this.player.dir, ForceMode2D.Impulse);
        StartCoroutine(returnToFollow());
    }

    // Start is called before the first frame update
    void Start()
    {
        this.bloodEffect.Stop(true);
        player = FindObjectOfType<Player>();
        this.anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void AnimationsSetting()
    {
        this.anim.SetBool("Running",this.isInFollowingState);
        if (this.isTakingHit)
         this.anim.SetTrigger("Hitted");
        this.anim.SetBool("Attack", this.isInAttackingState);
    }

    void SettingHitType()
    {
        selectedHitString  = randomHitType == 1 ? "Attack" : "AttackWithHead";
    }

    // Update is called once per frame
    void Update()
    {
        if (this.health <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<KillsCounter>().kills++;
        }
        this.dir = this.isFacingRight ? 1 : -1;
        SettingHitType();
        randomHitType = Random.Range(1, 5);
        this.bloodEffect.Stop(this.isTakingHit);

        followingPlayer();
        AnimationsSetting();        
    }

    private void FixedUpdate()
    {
        if (this.checkIfContactingPlayerWhileAttacking() && player.attacking)
        {
            takingHit();
            this.health -= 0.03f;
            this.isTakingHit = true;
            this.isInFollowingState = false;
            StartCoroutine(getUp());
        }
    } 

    public void ShootRayToDetectPlayer()
    {
        RaycastHit2D rayhit = Physics2D.Raycast(eye.position, Vector2.right, .2f);
        if (rayhit.rigidbody == null)
            return;
        if (rayhit.rigidbody.name == "Warrior")
        {
            rayhit.rigidbody.AddForce(Vector2.right * 3 * this.dir, ForceMode2D.Impulse);
            if (this.isInAttackingState)
            {
                player.anim.SetTrigger("Hitted");
            }
        }
    }

    IEnumerator returnToFollow()
    {
        yield return new WaitForSeconds(timeBeforeGettingUp);
        this.isInAttackingState = false;
    }

    IEnumerator getUp()
    {
        yield return new WaitForSeconds(timeBeforeGettingUp);
        this.isTakingHit = false;     
    }
}
