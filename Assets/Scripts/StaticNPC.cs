using UnityEngine;

public class StaticNPC : NPC {
    public void Awake() {
        itemCollector = gameObject.AddComponent<ItemCollector>();
        exclamation = GetComponentInChildren<Exclamation>();
    }
}
