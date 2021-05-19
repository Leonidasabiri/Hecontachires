using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DustFromAttackSpeed : MonoBehaviour
{
    Player player;
    ParticleSystem dust;

    [SerializeField] GameObject Dust;

    ParticleSystem.Particle[] particles;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        dust = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayEffect(player.attacking);
    }

    void PlayEffect(bool play)
    {
        dust.Play(play);
    }
}
