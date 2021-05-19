using UnityEngine;
using TMPro;

public class KillsCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    public int kills;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Kills: " + kills; 
    }
}
