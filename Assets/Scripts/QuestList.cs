using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Scriptable Objects/NPC Quest List")]
public class QuestList : ScriptableObject {
    [field: SerializeField] public List<ItemProperty> itemQuests { get; private set; }
}
