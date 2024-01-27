using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class PrinterManager : MonoBehaviour {
    private static readonly Random random = new Random();
    private List<NPC> requesters;
    private List<PaperSpawner> spawners;

    public void addRequest(NPC npc) {
        requesters.Add(npc);
    }

    public bool requestCompleted(Item item) {
        if (requesters.Count == 0) {
            return false;
        }

        if (spawners.Count > 0) {
            PaperSpawner spawner = spawners[random.Next(spawners.Count)];
            spawner.spawn();
        }

        requesters[0].collected(ItemProperty.Mail, item, true);

        requesters.RemoveAt(0);

        return true;
    }

    public PrinterManager Initialize() {
        requesters = new();
        spawners = new();
        return this;
    }

    public void addSpawner(PaperSpawner paperSpawner) {
        spawners.Add(paperSpawner);
    }
}
