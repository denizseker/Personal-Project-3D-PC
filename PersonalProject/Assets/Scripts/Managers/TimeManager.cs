using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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



    [HideInInspector] public Seasons currentSeason;
    [HideInInspector] public float pastTime;
    [HideInInspector] public float HourInRealTimeSecond = 4;
    [HideInInspector] public int InGameDay = 1;
    [HideInInspector] public int InGameMonth = 1;
    [HideInInspector] public int InGameYear = 1152;

    public int InGameHour = 0;
    public bool isGameStopped;
    public GameObject sun;
    public TMP_Text DateText;

    private void Awake()
    {
        Instance = this;
        Instance.currentSeason = Seasons.Winter;
        AdjustSunRotationAtStart(InGameHour);
        
    }

    private void Start()
    {
        UIManager.Instance.UpdateDateText();
    }

    public void CheckGameSpeed()
    {
        UIManager.Instance.UpdateTimeScaleText();
        if (Time.timeScale == 0)
        {
            Instance.isGameStopped = true;
        }
        else
        {
            Instance.isGameStopped = false;
        }
    }


    public void AdjustSunRotationAtStart(int _timeOfDay)
    {
        //Instance.sun.transform.Rotate(-120f, 0, 0, Space.Self);
        Instance.sun.transform.Rotate( _timeOfDay * 15f, 0, 0, Space.Self);
    }

    private void Update()
    {
        Instance.pastTime += Time.deltaTime;
        Instance.sun.transform.Rotate(3.75f * Time.deltaTime, 0, 0, Space.Self);

        if(Instance.pastTime >= 4)
        {
            InGameHour += 1;
            Instance.pastTime = 0;
            UIManager.Instance.UpdateDateText();
        }
        if (InGameHour == 24)
        {
            InGameHour = 0;
            InGameDay += 1;
            UIManager.Instance.UpdateDateText();
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
        }

        if (Instance.InGameMonth == 5)
        {
            Instance.InGameYear += 1;
            Instance.InGameMonth = 1;
        }
    }
}
