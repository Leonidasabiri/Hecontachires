using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDur = 1f;
    public float shakeP = 1f;
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.attacking)
            shakeCamera(0.1f,ref shakeP);
    }

    void shakeCamera(float sDuration,ref float sPower)
    {
        if(sDuration > 0)
        {
         float y = Random.Range(-0.1f, 0.1f);
         transform.position += new Vector3(0,sPower * y,0);
        }
    }

}
