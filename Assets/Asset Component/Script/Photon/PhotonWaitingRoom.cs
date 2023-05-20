using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class PhotonWaitingRoom : MonoBehaviourPunCallbacks 
{
    private PhotonView myPhotonView;
    
    [SerializeField] private string MainGameLevelName;
    [SerializeField] private int MenuOrLoadingSceneIndex;

    private int playerCount;
    private int roomSize;
    [SerializeField] private int minPlayersToStart;

    [SerializeField] private TextMeshProUGUI playerCountDisplay;
    [SerializeField] private TextMeshProUGUI timerToStartDisplay;

    //bool for if timer can count down
    private bool readyToCountDown;
    private bool readyToStart;
    private bool startingGame;

    //countdown timer variables
    private float timerToStartGame;
    private float notFullGameTimer;
    private float fullGameTimer;

    //countdown timer reset variables
    [SerializeField] private float maxWaitTime;
    [SerializeField] private float maxFullGameWaitTime;

    private void Start() 
    {
        myPhotonView = GetComponent<PhotonView>();
        fullGameTimer = maxFullGameWaitTime;
        notFullGameTimer = maxWaitTime;
        timerToStartGame = maxWaitTime;

        PlayerCountUpdate();
    }

    private void PlayerCountUpdate()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        playerCountDisplay.text = playerCount + " : " + roomSize;

        if(playerCount == roomSize)
        {
            readyToStart = true;
        }
        else if(playerCount >= minPlayersToStart)
        {
            readyToCountDown = true;
        }
        else
        {
             readyToCountDown = false;
             readyToStart = false;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountUpdate();

        if(PhotonNetwork.IsMasterClient)
        {
            myPhotonView.RPC("RPC_SendTimer", RpcTarget.Others, timerToStartGame);
        }

    }

    [PunRPC]
    private void RPC_SendTimer(float timeIn)
    {
        timerToStartGame = timeIn;
        notFullGameTimer = timeIn;

        if(timeIn < fullGameTimer)
        {
            fullGameTimer = timeIn;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayerCountUpdate();
    }

    private void Update()
    {
        WAitingForMorePlayers();
    }

    private void WAitingForMorePlayers()
    {
        if(playerCount <= 1)
        {
            ResetTimer();
        }

        if(readyToStart)
        {
            fullGameTimer -= Time.deltaTime;
            timerToStartGame = fullGameTimer;
        }
        else if(readyToCountDown)
        {
            notFullGameTimer -= Time.deltaTime;
            timerToStartGame = notFullGameTimer;
        }

        string tempTimer = string.Format("{0:00}", timerToStartGame);
        timerToStartDisplay.text = tempTimer;

        if(timerToStartGame <= 0f)
        {
            if(startingGame) return;

            StartGame();
        }
    }

    private void ResetTimer()
    {
        timerToStartGame = maxWaitTime;
        notFullGameTimer = maxWaitTime;
        fullGameTimer = maxFullGameWaitTime;
    }

    public void StartGame()
    {
        startingGame = true;
        //if(!PhotonNetwork.IsMasterClient) return;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(MainGameLevelName);
    }

    public void DelayCancel()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(MenuOrLoadingSceneIndex);
    }
}