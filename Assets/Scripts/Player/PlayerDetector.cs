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

    public PlayerDetector Initialize() {
        players = new PlayerController[2];
        return this;
    }

    public void SetLowerBodyController(LowerBodyController lowerBody) {
        lowerBodyController = lowerBody;
    }

    public void SetUpperBodyController(UpperBodyController upperBody) {
        upperBodyController = upperBody;
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
            players[0].Initialize(lowerBodyController);
        } else if (players[0] != null && players[1] == null) {
            players[1] = input.GetComponent<PlayerController>();
            switch (players[0].GetBodyPart()) {
                case BodyPartController.BodyPart.ARMS:
                    players[1].Initialize(lowerBodyController);
                    break;
                case BodyPartController.BodyPart.LEGS:
                    players[1].Initialize(upperBodyController);
                    break;
            }
            
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

    }

    public void OnPLayerLeft(PlayerInput input) {
        Debug.Log("Player " + connectedPlayers + " left");
        --connectedPlayers;
    }
}
