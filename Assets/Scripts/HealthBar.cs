using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider healthBar;

    [SerializeField] float healthVal;
    [SerializeField] float healthCurrentVal;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Slider>();
        healthVal = healthBar.maxValue;
        healthCurrentVal = healthVal;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = healthCurrentVal;
    }
}
