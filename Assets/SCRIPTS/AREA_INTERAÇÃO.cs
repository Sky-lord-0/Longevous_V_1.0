using UnityEngine;

public class InteracaoArea : MonoBehaviour
{
    public GameObject textoUI;

    private void Start()
    {
        if (textoUI != null)
            textoUI.SetActive(false);
        else
            Debug.LogError("textoUI não foi atribuído no Inspector!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && textoUI != null)
        {
            textoUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && textoUI != null)
        {
            textoUI.SetActive(false);
        }
    }
}