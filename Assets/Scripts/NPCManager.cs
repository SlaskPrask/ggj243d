using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {
    public List<NPC> npcs { get; private set; }

    public NPCManager Initialize() {
        npcs = new();
        return this;
    }

    public void add(NPC npc) {
        npcs.Add(npc);
    }

    public void remove(NPC npc) {
        npcs.Remove(npc);
    }
}
