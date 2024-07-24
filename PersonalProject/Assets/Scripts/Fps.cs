using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
public class Fps : MonoBehaviour
{
    [SerializeField] private TMP_Text _fpsText;
    [SerializeField] private float _hudRefreshRate = 1f;

    private float _timer;
    private void Awake()
    {
        //Target FPS
        Application.targetFrameRate = -1;

    }
    private void Update()
    {
        if (Time.unscaledTime > _timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            _fpsText.text = "FPS: " + fps;
            _timer = Time.unscaledTime + _hudRefreshRate;
        }
    }
}