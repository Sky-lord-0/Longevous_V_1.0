using UnityEngine;
using UnityEngine.SceneManagement;

public class RunManager : MonoBehaviour
{
    public static RunManager Instance;

    public Transform spawnPoint;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetRun()
    {
        SceneManager.sceneLoaded += OnSceneLoaded_Reset;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded_Reset(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded_Reset;

        GameObject sp = GameObject.Find("SpawnPoint");
        if (sp != null)
            spawnPoint = sp.transform;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p == null)
            return;

        if (spawnPoint != null)
            p.transform.position = spawnPoint.position;

        PlayerHealth ph = p.GetComponent<PlayerHealth>();
        if (ph != null)
            ph.currentHP = ph.maxHP;
    }
}