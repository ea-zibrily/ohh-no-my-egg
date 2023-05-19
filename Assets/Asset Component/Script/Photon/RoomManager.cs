using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private bool allPlayersReady = false;

    private void Awake() 
    {
        //StopGame();
        
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Set a custom room property to keep track of player readiness
            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "AllPlayersReady", false } });
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
            Debug.Log("properties havent changed");

        if (PhotonNetwork.IsMasterClient && changedProps.ContainsKey("PlayerReady"))
        {
            // Check if all players are ready
            CheckAllPlayersReady();
            Debug.Log("properties changed");
        }
    }

    private void CheckAllPlayersReady()
    {
        allPlayersReady = true;

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            object playerReady;
            if (player.CustomProperties.TryGetValue("PlayerReady", out playerReady))
            {
                if (!(bool)playerReady)
                {
                    allPlayersReady = false;
                    break;
                }
            }
        }

        if (allPlayersReady)
        {
            // Start the game
            StartGame();
        }
    }

    private void StartGame()
    {
        // Start the game
        Time.timeScale = 1f;
        Debug.Log("All players are ready! Starting the game...");
    }

    private void StopGame()
    {
        // Start the game
        Time.timeScale = 0f;
        Debug.Log("All players are not ready! Stopping the game...");
    }
}
