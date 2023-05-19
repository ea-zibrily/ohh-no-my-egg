using System;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks//MonoSingleton<PlayerManager>
{
    [Header("Egg Point Component")]
    private int currentEggPoint;
    public TextMeshProUGUI eggPointText;

    private void Start()
    {
        currentEggPoint = 0;
    }

    // private void Update()
    // {
    // }

    public void AddEggPoint()
    {
        currentEggPoint++;
        eggPointText.text = currentEggPoint.ToString();
    }
    
}