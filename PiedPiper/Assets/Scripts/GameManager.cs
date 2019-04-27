using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;


    private float timer;
    private string timerString;
    private Text TimerUI;
    
    void Start()
    {
        // SINGLETON
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        //INIT
        timer = 0f;
        TimerUI = GameObject.Find("TimerUI").GetComponent<Text>();
    }


    void Update()
    {
        timer += Time.deltaTime;
        timerString = timer.ToString("F0");
        TimerUI.text = "Time Elapsed: " + timerString;
    }


}
