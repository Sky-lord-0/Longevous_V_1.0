using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Porta : MonoBehaviour
{
    public string NOME_DA_CENA;
    public GameObject textoInteracao;

    [Header("Audio")]
    public AudioSource audioSource;
    public float tempoAntesTrocaCena = 1f;

    private bool jogadorPerto;
    private bool carregandoCena;

    void Update()
    {
        if (jogadorPerto && !carregandoCena && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(AbrirPortaETrocarCena());
        }
    }

    IEnumerator AbrirPortaETrocarCena()
    {
        carregandoCena = true;
        jogadorPerto = false;

        if (textoInteracao != null)
            textoInteracao.SetActive(false);

        if (audioSource != null)
            audioSource.Play();

        yield return new WaitForSeconds(tempoAntesTrocaCena);

        SceneManager.LoadScene(NOME_DA_CENA);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (carregandoCena) return;

        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;

            if (textoInteracao != null)
                textoInteracao.SetActive(true);

            Debug.Log("Jogador entrou na porta");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (carregandoCena) return;

        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;

            if (textoInteracao != null)
                textoInteracao.SetActive(false);
        }
    }
}