using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Nakama;

public class NakamaConnector : MonoBehaviour
{
    [SerializeField] private string matchId;
    [SerializeField] private int minPlayers;
    [SerializeField] private int maxPlayers;
    
    const string scheme = "http";
    const string host = "127.0.0.1";
    const int port = 7350;
    const string serverKey = "defaultkey";
    
    private IClient client;
    private ISession session;
    private ISocket socket;
    private string currentMatchmakingTicket;
    
    private async void Start()
    {
        client = new Client(scheme, host, port, serverKey, UnityWebRequestAdapter.Instance);

        var deviceId = SystemInfo.deviceUniqueIdentifier;
        session = await client.AuthenticateDeviceAsync(deviceId);
        socket = client.NewSocket();
        await socket.ConnectAsync(session, true);
        
        socket.ReceivedMatchmakerMatched += OnReceivedMatchmakerMatched;
        socket.ReceivedMatchState += OnReceivedMatchState;
        
        Debug.Log(session);
        Debug.Log(socket);
    }

    public async Task Connect()
    {
        client = new Client(scheme, host, port, serverKey, UnityWebRequestAdapter.Instance);

        var deviceId = SystemInfo.deviceUniqueIdentifier;
        session = await client.AuthenticateDeviceAsync(deviceId);
        socket = client.NewSocket();
        await socket.ConnectAsync(session, true);
        
        socket.ReceivedMatchmakerMatched += OnReceivedMatchmakerMatched;
        socket.ReceivedMatchState += OnReceivedMatchState;
        
        Debug.Log(session);
        Debug.Log(socket);
    }
    
    public async Task Disconnect()
    {
        socket.ReceivedMatchmakerMatched -= OnReceivedMatchmakerMatched;
        socket.ReceivedMatchState -= OnReceivedMatchState;
    }

    public async void FindMatch()
    {
        Debug.Log("Finding Match");
        
        // Add this client to the matchmaking pool and get a ticket.
        var matchmakerTicket = await socket.AddMatchmakerAsync("*", minPlayers, maxPlayers);
        currentMatchmakingTicket = matchmakerTicket.Ticket;
    }
    
    public async Task CancelMatchmaking() => await socket.RemoveMatchmakerAsync(currentMatchmakingTicket);

    public async void Ping()
    {
        Debug.Log("Check Player Ping");
        
        await socket.SendMatchStateAsync(matchId, 1, "", null);
    }
    
    private async void OnReceivedMatchmakerMatched(IMatchmakerMatched matched)
    {
        Debug.Log("Match Found");
        var match = await socket.JoinMatchAsync(matched);
        matchId = match.Id;
        
        Debug.Log($"Our Session Id: {match.Self.SessionId}");

        foreach (var user in match.Presences)
        {
            Debug.Log($"Connection User Session Id: {user.SessionId}");
        }
    }
    
    private async void OnReceivedMatchState(IMatchState matchState)
    {
        switch (matchState.OpCode)
        {
            case 1:
                Debug.Log("Ping Received");
                await socket.SendMatchStateAsync(matchId, 2, "", new[] {matchState.UserPresence});
                break;
            case 2:
                Debug.Log("Pong Received");
                await socket.SendMatchStateAsync(matchId, 1, "", new[] {matchState.UserPresence});
                break;
            default:
                Debug.Log("Unknown OpCode");
                break;
        }
    }
}
