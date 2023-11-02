using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutPanel : MonoBehaviour
{
    //private Camera mainCamera;            // Kameranýn referansý
    //private CanvasGroup panelCanvasGroup; // Panelin CanvasGroup bileþeni
    //public float targetDistance = 200f;   // Hedef mesafe
    //public float transitionTime = 2f;     // Geçiþ süresi
    //private float currentAlpha = 1f;      // Þu anki alfa deðeri
    //private float targetAlpha = 1f;       // Hedef alfa deðeri

    //private void Start()
    //{
    //    mainCamera = Camera.main;  // Ana kamerayý al
    //    panelCanvasGroup = GetComponent<CanvasGroup>(); // Panelin CanvasGroup bileþenini al
    //}

    //private void Update()
    //{

    //    float currentDistance = Vector3.Distance(mainCamera.transform.position, transform.position);

    //    if (currentDistance >= targetDistance)
    //    {
    //        targetAlpha = 0f; // Paneli kapatmak için hedef alfa deðerini 0 yap
    //    }
    //    else
    //    {
    //        targetAlpha = 1f; // Paneli açmak için hedef alfa deðerini 1 yap
    //    }

    //    // Belirli bir adým büyüklüðüyle alfa deðerini hedefe yaklaþtýr
    //    float step = Time.deltaTime / transitionTime; // Geçiþ adým büyüklüðü
    //    currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, step);
    //    panelCanvasGroup.alpha = currentAlpha;
    //}

    private CanvasGroup panelCanvasGroup;
    public float fadeDuration = 0.5f;


    private void Awake()
    {
        panelCanvasGroup = GetComponentInParent<CanvasGroup>();
    }

    private void OnEnable()
    {
        StartFadeIn();
    }

    // Bu fonksiyon, paneli belirli bir süre içinde fade-out yapar.
    public void StartFadeOut()
    {
        StartCoroutine(FadeTo(0,1, fadeDuration));
    }

    // Bu fonksiyon, paneli belirli bir süre içinde fade-in yapar.
    public void StartFadeIn()
    {
        StartCoroutine(FadeTo(1,0, fadeDuration));
    }

    private IEnumerator FadeTo(float targetAlpha,float startAlpha, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            panelCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        // Alpha deðerini kesinlikle hedefe ayarlayýn.
        panelCanvasGroup.alpha = targetAlpha;
    }

}
