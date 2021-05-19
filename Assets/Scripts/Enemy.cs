using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected CameraShake cameraShake;

    [SerializeField]protected int dir;

    [SerializeField]protected float speed;
    [SerializeField]protected float health;
    [SerializeField]protected float hitCost;
    [SerializeField]protected float sizeOfBoxChecker = 4;

    [SerializeField]protected ParticleSystem bloodEffect;
    [SerializeField]protected Animator              anim;

    public bool isInFollowingState;
    public bool isInAttackingState;
    public bool isFacingRight;
    public bool isTakingHit;

    [SerializeField]protected LayerMask relative;
    [SerializeField]protected Transform eye;

    private void Stop()
    {
        cameraShake = FindObjectOfType<CameraShake>();
    }

    protected void flip()
    {
        Vector3 scale =  transform.localScale;
        scale.x       = -transform.localScale.x;
        transform.localScale = scale;
    }

    protected virtual bool checkIfCloseToItsRelative(string relativeName)
    {
      RaycastHit2D rayhit = Physics2D.Raycast(eye.position, Vector2.right, .5f);
        if(rayhit.rigidbody != null)
         if (rayhit.rigidbody.name == relativeName)
            return true;
        return false;
    }
    protected bool checkIfContactingPlayerWhileAttacking()
    {
      RaycastHit2D rayhit = Physics2D.Raycast(eye.position, Vector2.right, .3f);
      if(rayhit.collider != null)     
       if (rayhit.collider.name == "Warrior")
             return true;        
      return false;
    }

    protected virtual void followingPlayer()
    {

    }
    
    protected virtual void hittingPlayer()
    {

    }
    
    protected virtual void takingHit()
    {

    }

}
