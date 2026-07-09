using UnityEngine;

public class ENEMY_SPAWNER : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    bool spawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (spawned) return;

        if (other.CompareTag("Player"))
        {
            spawned = true;

            foreach (Transform point in spawnPoints)
            {
                Instantiate(enemyPrefab,
                            point.position,
                            Quaternion.identity);
            }
        }
    }
}