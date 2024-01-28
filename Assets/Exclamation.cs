using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Exclamation : MonoBehaviour {
    public NPC npc;
    public TMP_Text text;

    public void showQuest(bool near) {
        if (npc.hasQuest() || near) {
            if (!near) {
                text.text = "!";
            }
            else {
                text.text = npc.name;
            }
        }
        else {
            text.text = "";
        }
    }

    void Update() {
        Vector3 forward = GameManager.camera.transform.forward;
        forward.y = 0;
        transform.forward = forward.normalized;
    }
}
