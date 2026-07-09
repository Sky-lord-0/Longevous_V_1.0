using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform spawnPoint;

    void Start()
    {
        player.transform.position = spawnPoint.position;
    }
}