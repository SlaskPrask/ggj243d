using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Exclamation : MonoBehaviour {
    public NPC npc;
    public TMP_Text text;

    public string subs;

    public void setSubs(string subs) {
        this.subs = subs;
        StartCoroutine(stopSubs());
    }

    private IEnumerator stopSubs() {
        yield return new WaitForSeconds(4);
        subs = "";
        showQuest(false);
    }

    public void showQuest(bool near) {
        if (subs.Length > 0) {
            text.text = subs;
        }

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
