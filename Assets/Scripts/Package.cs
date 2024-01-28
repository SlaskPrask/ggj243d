using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : Item, NPCItem {
    public NPC sender { get; private set; }

    public override void delivered(ItemProperty tag) {
        if (sender) {
            sender.collected(tag, this, true);
        }
    }

    public void init(NPC npc) {
        sender = npc;
    }
}
