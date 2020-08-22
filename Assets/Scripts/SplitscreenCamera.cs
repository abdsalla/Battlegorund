using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitscreenCamera : MonoBehaviour
{
    [SerializeField] Camera playerOneCam;
    [SerializeField] Camera playerTwoCam;
    [SerializeField] TankData playerData;
    private GameManager instance;

    void Start()
    {
        instance = GameManager.Instance;
        playerData = GetComponentInParent<TankData>();

        if (playerData.playerNum == 1) playerOneCam = playerData.GetComponentInChildren<Camera>();

        if (playerData.playerNum == 2) playerTwoCam = playerData.GetComponentInChildren<Camera>();

        if (instance.numOfPlayers > 1)
        {
            if (playerData.playerNum == 1) { playerOneCam.rect = new Rect(0f, .5f, 1f, .5f); }
            else if (playerData.playerNum == 2) { playerTwoCam.rect = new Rect(0f, 0f, 1f, .5f); }
        }
    }
}