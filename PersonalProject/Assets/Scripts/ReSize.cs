using UnityEngine;

public class ReSize : MonoBehaviour
{
    private Camera mainCamera; // Ana kamera referans�
    [SerializeField] private float minScale = 1f; // Minimum �l�ek de�eri
    [SerializeField] private float maxScale = 3f; // Maksimum �l�ek de�eri
    public float baseDistance = 30f; // Panelin baz boyutuna kar��l�k gelen uzakl�k

    private Transform panelTransform;

    private void Start()
    {
        mainCamera = Camera.main;
        // Scriptin eklendi�i GameObject'in Transform bile�enini al
        panelTransform = transform;
    }

    private void Update()
    {
        // Panel ile kamera aras�ndaki mevcut uzakl��� hesapla
        float currentDistance = Vector3.Distance(panelTransform.position, mainCamera.transform.position);

        // Normalized bir de�er elde et
        float normalizedDistance = Mathf.Clamp01((currentDistance - baseDistance) / (maxScale * baseDistance - baseDistance));

        // �l�e�i hesapla
        float newScale = Mathf.Lerp(minScale, maxScale, normalizedDistance);

        // �l�e�i g�ncelle
        panelTransform.localScale = Vector3.one * newScale;
    }
}






