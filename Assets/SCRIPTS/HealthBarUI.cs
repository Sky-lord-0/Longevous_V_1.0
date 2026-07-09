using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image fillImage;

    public void UpdateHealth(float current, float max)
    {
        float value = current / max;
        fillImage.fillAmount = value;
    }
}