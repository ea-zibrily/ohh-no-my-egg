using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameTimer : MonoBehaviour
{
    [Header("Timer Component")]
    
    [Tooltip("Timer dalam detik lur")]
    [SerializeField] private float currentTimer;
    
    [Tooltip("Boolean buat ngestart timer")]
    [SerializeField] private bool isTimerStart;
    
    private bool isTimerEnd;
    public TextMeshProUGUI timerText;
    
    private void Update()
    {
        TimerController();
        
        if (isTimerEnd)
        {
            // some logic buat nampilin player mana yg menang
            // ato bisa pake game manager
            // bebas wes lur
            
            Time.timeScale = 0;
        }
    }

    private void TimerController()
    {
        if (!isTimerStart)
        {
            return;
        }
        
        isTimerEnd = false;
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
            UpdateTimerUI(currentTimer);
        }
        else
        {
            Debug.Log("Time is up!");
            currentTimer = 0;
            isTimerStart = false;
            isTimerEnd = true;
        }
    }

    private void UpdateTimerUI(float timer)
    {
        timer += 1;
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
