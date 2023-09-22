using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeButtonHandler : MonoBehaviour
{
    //Taking all buttonimage for changing their color
    public List<Image> buttonImageList = new List<Image>();

    //If buttons pressed
    public void ButtonPressed(int _scale)
    {
        //checking is scale same with button value
        if(Time.time == _scale)
        {
            return;
        }
        else
        {
            //checking all buttons and changing matched button color
            for (int i = 0; i < buttonImageList.Count; i++)
            {
                if(i == _scale)
                {
                    Time.timeScale = _scale;
                    buttonImageList[_scale].color = Color.yellow;
                }
                //if button value not matching with color, setting default.
                else
                {
                    buttonImageList[i].color = Color.white;
                }
            }
        }
        
    }
}
