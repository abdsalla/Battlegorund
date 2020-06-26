using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitscreenCamera : MonoBehaviour
{
    [SerializeField] Camera playerCam;
    [SerializeField] TankData playerData;
    private GameManager instance;

    void Start()
    {
        instance = GameManager.Instance;
        playerCam = GetComponent<Camera>();
        playerData = GetComponentInParent<TankData>();

        if (instance.numOfPlayers > 1)
        {

            if (playerData.playerNum == 1)
            {
                playerCam.rect = new Rect(0f, .5f, 1f, .5f);
            }
            else if(playerData.playerNum == 2)
            {
                playerCam.rect = new Rect(0f, 0f, 1f, .5f);
            }
        }
    }
}