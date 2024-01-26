using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NPC : MonoBehaviour {
    [FormerlySerializedAs("itemDetector")] [FormerlySerializedAs("detector")]
    public ItemCollector itemCollector;

    // Start is called before the first frame update
    void Start() {
        if (itemCollector == null) {
            itemCollector = GetComponentInChildren<ItemCollector>();
        }

        itemCollector.listener += collected;
    }

    // Update is called once per frame
    void Update() {
    }

    public bool collected(ItemProperty property, Item item, bool isTarget) {
        if (!isTarget) {
            return false;
        }

        if (property == ItemProperty.CoffeeCup) {
            item.properties.Remove(ItemProperty.CoffeeCup);
            item.properties.Add(ItemProperty.Mug);
            Debug.Log("Got Coffee");
        }

        item.target = null;

        return true;
    }

    public void setQuest(ItemProperty property) {
        if (property == ItemProperty.None) {
            itemCollector.setWants(ItemProperty.None);
            return;
        }

        if (!itemCollector.hasWants()) {
            itemCollector.wants = property;
        }
    }
}
