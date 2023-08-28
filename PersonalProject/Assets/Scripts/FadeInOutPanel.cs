using UnityEngine;

public class FadeInOutPanel : MonoBehaviour
{
    private Camera mainCamera;            // Kameran�n referans�
    private CanvasGroup panelCanvasGroup; // Panelin CanvasGroup bile�eni
    public float targetDistance = 200f;   // Hedef mesafe
    public float transitionTime = 2f;     // Ge�i� s�resi
    private float currentAlpha = 1f;      // �u anki alfa de�eri
    private float targetAlpha = 1f;       // Hedef alfa de�eri

    private void Start()
    {
        mainCamera = Camera.main;  // Ana kameray� al
        panelCanvasGroup = GetComponent<CanvasGroup>(); // Panelin CanvasGroup bile�enini al
    }

    private void Update()
    {

        float currentDistance = Vector3.Distance(mainCamera.transform.position, transform.position);

        if (currentDistance >= targetDistance)
        {
            targetAlpha = 0f; // Paneli kapatmak i�in hedef alfa de�erini 0 yap
        }
        else
        {
            targetAlpha = 1f; // Paneli a�mak i�in hedef alfa de�erini 1 yap
        }

        // Belirli bir ad�m b�y�kl���yle alfa de�erini hedefe yakla�t�r
        float step = Time.deltaTime / transitionTime; // Ge�i� ad�m b�y�kl���
        currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, step);
        panelCanvasGroup.alpha = currentAlpha;
    }
}
