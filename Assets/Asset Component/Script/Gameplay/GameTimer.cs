using UnityEngine;
using TMPro;
using Photon.Pun;

public class GameTimer : MonoBehaviourPunCallbacks
{
    [Header("Timer Component")]
    
    [Tooltip("Timer dalam detik lur")]
    [SerializeField] private float currentTimer;
    
    [Tooltip("Boolean buat ngestart timer")]
    [SerializeField] private bool isTimerStart;
    
    private bool isTimerEnd;
    public TextMeshProUGUI timerText;
    
    public static GameTimer Instance { get; private set; }

    public float TimerValue {get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start() 
    {
        StartTimer();    
    }

    public void StartTimer()
    {
        //TimerValue = duration;
        isTimerStart = true;
        photonView.RPC("TimerController", RpcTarget.All, currentTimer);
    }

    //[PunRPC]
    private void Update()
    {
        //photonView.RPC("TimerController", RpcTarget.All, currentTimer);
        //TimerController();
        if (!isTimerStart)
        {
            return;
        }
        
        isTimerEnd = false;
        if (TimerValue > 0)
        {
            TimerValue -= Time.deltaTime;
            UpdateTimerUI(TimerValue);
        }
        else
        {
            Debug.Log("Time is up!");
            TimerValue = 0;
            isTimerStart = false;
            isTimerEnd = true;
        }

        if (isTimerEnd)
        {
            photonView.RPC("OnTimerFinished", RpcTarget.All);
        }
    }

    [PunRPC]
    private void TimerController(float currentTimer)
    {
        TimerValue = currentTimer;
        
    }

 
    private void UpdateTimerUI(float timer)
    {
        timer += 1;
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    [PunRPC]
    private void OnTimerFinished()
    {
        // some logic buat nampilin player mana yg menang
        // ato bisa pake game manager
        // bebas wes lur
        Time.timeScale = 0;
    }
}
