using UnityEngine;

public class GameManager : MonoBehaviour
{
    MenuManager  menuManager;
    EnemySpawner enemySpawner;

    public int enemyCount = 2 , timeOfSpawn = 1;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        menuManager = FindObjectOfType<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (menuManager.player.canMove)
          if(timeOfSpawn > 0){
            enemySpawner.SpawnEnemy(enemyCount);
            timeOfSpawn--;
          }
    }
}
