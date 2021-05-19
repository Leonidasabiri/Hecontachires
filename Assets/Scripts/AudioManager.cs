using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sounds;

    Player player;
    Satyr[] satyrs;

    [SerializeField] int _index;

    // Start is called before the first frame update
    void Start()
    {
        satyrs = FindObjectsOfType<Satyr>();
    }

    private void Update()
    {
       foreach(var satyr in satyrs)
        {
            if (satyr.isInFollowingState)
                sounds[0].Play();
        }
    }
}
