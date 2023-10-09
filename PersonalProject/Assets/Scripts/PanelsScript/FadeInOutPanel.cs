using UnityEngine;

public class FadeInOutPanel : MonoBehaviour
{
    private Camera mainCamera;            // Kameranýn referansý
    private CanvasGroup panelCanvasGroup; // Panelin CanvasGroup bileþeni
    public float targetDistance = 200f;   // Hedef mesafe
    public float transitionTime = 2f;     // Geçiþ süresi
    private float currentAlpha = 1f;      // Þu anki alfa deðeri
    private float targetAlpha = 1f;       // Hedef alfa deðeri

    private void Start()
    {
        mainCamera = Camera.main;  // Ana kamerayý al
        panelCanvasGroup = GetComponent<CanvasGroup>(); // Panelin CanvasGroup bileþenini al
    }

    private void Update()
    {

        float currentDistance = Vector3.Distance(mainCamera.transform.position, transform.position);

        if (currentDistance >= targetDistance)
        {
            targetAlpha = 0f; // Paneli kapatmak için hedef alfa deðerini 0 yap
        }
        else
        {
            targetAlpha = 1f; // Paneli açmak için hedef alfa deðerini 1 yap
        }

        // Belirli bir adým büyüklüðüyle alfa deðerini hedefe yaklaþtýr
        float step = Time.deltaTime / transitionTime; // Geçiþ adým büyüklüðü
        currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, step);
        panelCanvasGroup.alpha = currentAlpha;
    }
}
