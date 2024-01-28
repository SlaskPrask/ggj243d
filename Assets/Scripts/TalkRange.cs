using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkRange : MonoBehaviour {
    public NPC npc;

    public void Awake() {
        npc = GetComponentInParent<NPC>();
    }
}
