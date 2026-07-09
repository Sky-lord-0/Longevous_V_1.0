using System.Collections;
using UnityEngine;

public class HitstopManager : MonoBehaviour
{
    private Coroutine routine;

    public void DoHitstop(float duration)
    {
        // se já tem hitstop rodando, reinicia (pra não empilhar bugado)
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(HitstopRoutine(duration));
    }

    IEnumerator HitstopRoutine(float duration)
    {
        float original = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
        routine = null;
    }

    // segurança: se desativar/destruir por qualquer motivo, volta o tempo
    void OnDisable()
    {
        Time.timeScale = 1f;
    }

    void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}