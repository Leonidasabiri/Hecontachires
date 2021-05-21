using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject satyr;

    [SerializeField]float x , minX, maxX;

    public void SpawnEnemy(int enemyNum)
    {
      x = Random.Range(minX, maxX);
      Vector2 pos = new Vector2(x, transform.position.y);
      while(enemyNum-- > 0)
        Instantiate(satyr, pos, Quaternion.identity);
    }
}
