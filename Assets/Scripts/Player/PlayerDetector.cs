using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerDetector : MonoBehaviour
{
    private byte connectedPlayers = 0;
    PlayerController[] players;
    LowerBodyController lowerBodyController;
    UpperBodyController upperBodyController;
    private bool p1Bottom;

    public PlayerDetector Initialize() {
        players = new PlayerController[2];
        p1Bottom = true;
        return this;
    }

    public void SetLowerBodyController(LowerBodyController lowerBody) {
        lowerBodyController = lowerBody;
    }

    public void SetUpperBodyController(UpperBodyController upperBody) {
        upperBodyController = upperBody;
    }

    public void SwitchPlayer(Material playerSuit) {
        //Get suit material, switch colors too
        p1Bottom = !p1Bottom;
        players[0].Initialize(p1Bottom ? lowerBodyController : upperBodyController);
        players[1].Initialize(p1Bottom ? upperBodyController : lowerBodyController);
        playerSuit.SetFloat("_ColorSwitch", p1Bottom ? 0f : 1f);
    }


    // if a player connects and the array is empty,
    // give the first player the legs
    //if a player connects and any slot in the array is filled,
    //check their body part and give the other to connecting player
    public void OnPlayerJoined(PlayerInput input) {
        ++connectedPlayers;
        Debug.Log("Player " + connectedPlayers + " joined");
        if (players[0] == null && players[1] == null) {
            players[0] = input.GetComponent<PlayerController>();
        } else if (players[0] != null && players[1] == null) {
            players[1] = input.GetComponent<PlayerController>();            
        } else if (players[0] == null && players[1] != null) {
            players[0] = input.GetComponent<PlayerController>();
            switch (players[1].GetBodyPart()) {
                case BodyPartController.BodyPart.ARMS:
                    players[0].Initialize(lowerBodyController);
                    break;
                case BodyPartController.BodyPart.LEGS:
                    players[0].Initialize(upperBodyController);
                    break;
            }
        }

        if (connectedPlayers == 2) {
            GameManager.instance.StartGame();
            players[0].Initialize(lowerBodyController);
            players[1].Initialize(upperBodyController);
        }

    }

    public void OnPLayerLeft(PlayerInput input) {
        Debug.Log("Player " + connectedPlayers + " left");
        --connectedPlayers;
    }
}
