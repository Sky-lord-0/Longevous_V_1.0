using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public RectTransform creditsText;
    public float speed = 50f;

    public GameObject finishText;

    private bool finished = false;

    void Update()
    {
        if (!finished)
        {
            creditsText.anchoredPosition += Vector2.up * speed * Time.deltaTime;

            if (creditsText.anchoredPosition.y > 1800f)
            {
                finished = true;

                if (finishText != null)
                    finishText.SetActive(true);

                Invoke(nameof(ReturnToMenu), 5f);
            }
        }
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene("MAIN_MENU");
    }
}