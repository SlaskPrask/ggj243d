using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class NPCManager : MonoBehaviour {
    private static readonly Random random = new Random();

    public List<NPC> npcs { get; private set; }

    public string[] names = {
        "Matti",
        "Henrik",
        "Jan",
        "Wilhelm",
        "Pontus",
        "Martin",
        "Lars",
        "Gulliver",
        "Mia",
        "Magnus",
        "Gunnar",
        "Saga",
        "Tom",
        "Sanna",
        "Hilda",
        "Ingela"
    };

    public List<string> availableNames;

    public NPCManager Initialize() {
        npcs = new();

        availableNames = new List<string>(names);

        return this;
    }

    public string getName() {
        if (availableNames.Count == 0) {
            return "Slask";
        }

        int index = random.Next(availableNames.Count);
        string name = availableNames[index];
        availableNames.RemoveAt(index);
        return name;
    }

    public void add(NPC npc) {
        npcs.Add(npc);
    }

    public void remove(NPC npc) {
        npcs.Remove(npc);
    }
}
