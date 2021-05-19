using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject satyr;

    public int numberTracker = 0;

    public void SpawnEnemy(int enemyNum)
    {
      while(numberTracker++ < enemyNum)
       Instantiate(satyr, transform.position, Quaternion.identity);        
    }
}
