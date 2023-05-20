using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class GameTimer : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("Timer Component")]
    [Tooltip("Timer in seconds")]
    [SerializeField] private float currentTimer;
    
    [Tooltip("Boolean to start the timer")]
    [SerializeField] private bool isTimerStart;
    
    private bool isTimerEnd;
    public TextMeshProUGUI timerText;

    public float TimerValue { get; private set; }

    [SerializeField] private GameObject pauseUI;
    public Button startButton;

    private bool isPausedByMaster = false;

    private void Start()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
        startButton.onClick.AddListener(StartButtonClick); // Assign button click event
    }

    private void Update()
    {
        if (!isTimerStart || (PhotonNetwork.IsConnected && !PhotonNetwork.IsMasterClient))
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
    public void StartTimer()
    {
        if (!PhotonNetwork.IsMasterClient || isTimerStart) return;

        photonView.RPC("SyncTimerValue", RpcTarget.All, currentTimer);
        photonView.RPC("TogglePauseUI", RpcTarget.All, false);
        photonView.RPC("PauseGame", RpcTarget.Others, true);
    }

    [PunRPC]
    private void SyncTimerValue(float timerValue)
    {
        TimerValue = timerValue;
        Time.timeScale = 1f; // Set the Time.timeScale on all clients
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
        // Some logic to display the winning player or use a game manager
        Time.timeScale = 0f; // Set the Time.timeScale on all clients
        photonView.RPC("TogglePauseUI", RpcTarget.All, true);
    }

    private void StartButtonClick()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("StartTimer", RpcTarget.All);
        }
    }

    [PunRPC]
    private void TogglePauseUI(bool active)
    {
        pauseUI.SetActive(active);
    }

    [PunRPC]
    private void PauseGame(bool paused)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            if (paused)
            {
                isPausedByMaster = true;
                Time.timeScale = 0f;
            }
            else if (isPausedByMaster)
            {
                isPausedByMaster = false;
                Time.timeScale = 1f;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Time.timeScale);
            stream.SendNext(isTimerStart);
        }
        else if (stream.IsReading)
        {
            Time.timeScale = (float)stream.ReceiveNext();
            isTimerStart = (bool)stream.ReceiveNext();

            if (!PhotonNetwork.IsMasterClient && isPausedByMaster && Time.timeScale != 0f)
            {
                Time.timeScale = 0f;
            }
        }
    }
}


/*
using UnityEngine;
using TMPro;
using Photon.Pun;

public class GameTimer : MonoBehaviourPunCallbacks, IPunObservable 
{
    [Header("Timer Component")]
    [Tooltip("Timer dalam detik lur")]
    [SerializeField] private float currentTimer;
    
    [Tooltip("Boolean buat ngestart timer")]
    [SerializeField] private bool isTimerStart;
    
    private bool isTimerEnd;
    public TextMeshProUGUI timerText;

    public float TimerValue { get; private set; }

    [SerializeField] private GameObject PauseUI;

    private void Start()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (!isTimerStart || !PhotonNetwork.IsMasterClient)
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

    public void StartButtonClick()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("StartTimer", RpcTarget.All);
        }
    }

    [PunRPC]
    public void StartTimer()
    {
        if (!PhotonNetwork.IsMasterClient || isTimerStart) return;
        //if (isTimerStart) return;
        PauseUI.SetActive(false);
        isTimerStart = true;
        Time.timeScale = 1;
        photonView.RPC("SyncTimerValue", RpcTarget.All, currentTimer);
    }

    [PunRPC]
    private void SyncTimerValue(float timerValue)
    {
        TimerValue = timerValue;
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
        // Some logic to display the winning player or use a game manager
        Time.timeScale = 0;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Time.timeScale);
        }
        else if (stream.IsReading)
        {
            Time.timeScale = (float)stream.ReceiveNext();
        }
    }
}



/*
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

    public float TimerValue {get; private set;}


    public void StartTimer()
    {
        if(!PhotonNetwork.IsMasterClient) return;
        //TimerValue = duration;
        isTimerStart = true;
        photonView.RPC("TimerController", RpcTarget.All, currentTimer);
    }

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
*/