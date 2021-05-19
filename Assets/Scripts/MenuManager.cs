using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Player player;
    public GameObject menu;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void Play()
    {
        player.canMove = true;
        menu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
