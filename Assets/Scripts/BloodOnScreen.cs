using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodOnScreen : MonoBehaviour
{
    [SerializeField] List<Image> screenSplashes;

    Color color;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        color = Color.white;
        color.a = Mathf.Lerp(color.a, 0, 0.5f);
        foreach (var splash in screenSplashes)
        {            
            splash.color = color;
        }
    }
}
