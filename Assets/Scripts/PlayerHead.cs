using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }


    public void OnTriggerStay(Collider other) {
        if (other.gameObject.layer == 8) {
            NPC npc = other.GetComponentInParent<NPC>();
            npc.onTalk();
        }
    }
}
