using System;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [Header("Egg Point Component")]
    private int currentEggPoint;
    public TextMeshProUGUI eggPointText;
    
    private void Start()
    {
        currentEggPoint = 0;
    }

    private void Update()
    {
        eggPointText.text = currentEggPoint.ToString();
    }

    public void AddEggPoint()
    {
        currentEggPoint++;
    }
    
}