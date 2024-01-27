using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour {
    public ItemCollector itemCollector;

    void Start() {
        if (itemCollector == null) {
            itemCollector = GetComponentInChildren<ItemCollector>();
        }

        itemCollector.listener += collected;
    }

    public bool collected(ItemProperty property, Item item, bool isTarget) {
        Destroy(item);
        Debug.Log("Papered");

        return true;
    }
}
