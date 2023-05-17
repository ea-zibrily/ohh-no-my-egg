using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonSpawnPlayer : MonoBehaviourPunCallbacks
{
    [SerializeField]private GameObject firstPlayerObject;
    [SerializeField]private GameObject secondPlayerObject;
    
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Start is called before the first frame update
    private void Start()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            PhotonNetwork.Instantiate(firstPlayerObject.name, randomPosition, Quaternion.identity);
        }
        else if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            PhotonNetwork.Instantiate(secondPlayerObject.name, randomPosition, Quaternion.identity);
        }
    }
}
