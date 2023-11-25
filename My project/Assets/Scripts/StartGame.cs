using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class StartGame : MonoBehaviour
{

    public float timeLeft = 3.0f;
    public float wave = 3.0f;
    public TMP_Text startText; // used for showing countdown from 3, 2, 1 


    private void countdown()
    {
        wave -= Time.deltaTime;
        startText.text = (wave).ToString("0");
        if (wave < 0)
        {
            timeLeft -=Time.deltaTime;
            startText.text = (timeLeft).ToString("0");
            if (timeLeft < 0) 
            {
                timeLeft = timeLeft+2;
                countdown();
                
                
           
            }
        }

        {
            //Do something useful or Load a new game scene depending on your use-case
        }
    }
} 
