using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Scriptable Objects/NPC Quest List")]
public class QuestList : ScriptableObject {
    [field: SerializeField] public List<QuestData> itemQuests { get; private set; }
}

[System.Serializable]
public struct QuestData {
    public ItemProperty wantedItem;
    public GameObject spawnItem;

    [Tooltip("{from} for who assigned the task, {to} for who the item is brought to")]
    public string noteText;

    public string formatted(string from, string to) {
        return noteText.Replace("{to}", to).Replace("{from}", from);
    }
}
