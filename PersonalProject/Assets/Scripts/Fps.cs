using UnityEngine;
using System.Collections;

public class Fps : MonoBehaviour
{
    private float count;

    private void Awake()
    {
        // Make the game run as fast as possible
        Application.targetFrameRate = -1;
        // Limit the framerate to 60
        Application.targetFrameRate = 150;

    }

    private IEnumerator Start()
    {
        GUI.depth = 2;
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    private void OnGUI()
    {
        GUI.Label(new Rect(5, 40, 100, 25), "FPS: " + Mathf.Round(count));
    }
}
