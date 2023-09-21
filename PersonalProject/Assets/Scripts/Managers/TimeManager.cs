using System.Collections;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public enum Seasons
    {
        Winter,
        Spring,
        Summer,
        Autumn
    }

    public static TimeManager Instance;

    public Seasons currentSeason;
    public float pastTime;
    public float DayInRealTimeSecond = 96;
    public int InGameDay = 1;
    public int InGameMonth = 1;
    public int InGameYear = 1152;
    public GameObject sun;
    public TMP_Text DateText;

    private void Awake()
    {
        Instance = this;
        Instance.currentSeason = Seasons.Winter;
    }

    public void UpdateUI()
    {
        Instance.DateText.text = currentSeason + "  " + InGameDay + ",  " + InGameYear;
    }

    public void SetTimeScale(int _scale)
    {
        Time.timeScale = _scale;
    }


    private void Update()
    {
        Instance.pastTime += Time.deltaTime;
        Instance.sun.transform.Rotate(3.75f * Time.deltaTime, 0, 0, Space.Self);


        if (Instance.pastTime >= 96)
        {
            Instance.InGameDay += 1;
            Instance.pastTime = 0;
            Instance.UpdateUI();
        }

        if (Instance.InGameDay == 31)
        {
            Instance.InGameMonth += 1;
            Instance.InGameDay = 1;
            //Seasons
            switch (Instance.InGameMonth)
            {
                case 1:
                    Instance.currentSeason = Seasons.Winter;
                    break;
                case 2:
                    Instance.currentSeason = Seasons.Spring;
                    break;
                case 3:
                    Instance.currentSeason = Seasons.Summer;
                    break;
                case 4:
                    Instance.currentSeason = Seasons.Autumn;
                    break;
                default:
                    print("Incorrect season");
                    break;
                    
            }
            Instance.UpdateUI();
        }

        if (Instance.InGameMonth == 5)
        {
            Instance.InGameYear += 1;
            Instance.InGameMonth = 1;
            Instance.UpdateUI();
        }

       
    }
}
