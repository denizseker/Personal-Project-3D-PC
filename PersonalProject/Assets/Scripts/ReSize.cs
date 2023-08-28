using UnityEngine;

public class ReSize : MonoBehaviour
{
    private Camera mainCamera; // Ana kamera referansý
    [SerializeField] private float minScale = 1f; // Minimum ölçek deðeri
    [SerializeField] private float maxScale = 3f; // Maksimum ölçek deðeri
    public float baseDistance = 30f; // Panelin baz boyutuna karþýlýk gelen uzaklýk

    private Transform panelTransform;

    private void Start()
    {
        mainCamera = Camera.main;
        // Scriptin eklendiði GameObject'in Transform bileþenini al
        panelTransform = transform;
    }

    private void Update()
    {
        // Panel ile kamera arasýndaki mevcut uzaklýðý hesapla
        float currentDistance = Vector3.Distance(panelTransform.position, mainCamera.transform.position);

        // Normalized bir deðer elde et
        float normalizedDistance = Mathf.Clamp01((currentDistance - baseDistance) / (maxScale * baseDistance - baseDistance));

        // Ölçeði hesapla
        float newScale = Mathf.Lerp(minScale, maxScale, normalizedDistance);

        // Ölçeði güncelle
        panelTransform.localScale = Vector3.one * newScale;
    }
}






