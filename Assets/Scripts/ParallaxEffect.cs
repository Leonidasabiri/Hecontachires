using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    Camera cam;

    public float followSpeed = 3;
    public float parrlaxSpeed = 13;
    public List<SpriteRenderer> backgrounds;

    Vector3 lastPos;

    private void Start()
    {
      cam = Camera.main;        
      Vector3 lastPos = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = cam.transform.position;
        float moveCostX = currentPos == lastPos ? 0 : (currentPos.x-lastPos.x);
        float moveCostY = currentPos == lastPos ? 0 : (currentPos.y-lastPos.y);

        if(moveCostX > 0 || moveCostX < 0)
            lastPos = currentPos;
        if(moveCostY > 0 || moveCostY < 0)
            lastPos = currentPos;   

        foreach(SpriteRenderer back in backgrounds)
        {
            back.transform.position = new Vector3(back.transform.position.x - (moveCostX / (back.transform.position.z-1)
                * followSpeed) * Time.fixedDeltaTime,
                back.transform.position.y - (moveCostY / (back.transform.position.z - 1) * followSpeed) * Time.fixedDeltaTime,
                back.transform.position.z);
        }
    }
}
