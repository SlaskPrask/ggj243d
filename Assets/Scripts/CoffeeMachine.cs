using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class CoffeeMachine : MonoBehaviour {
    [FormerlySerializedAs("itemDetector")] [FormerlySerializedAs("detector")]
    public ItemCollector itemCollector;

    void Start() {
        if (itemCollector == null) {
            itemCollector = GetComponentInChildren<ItemCollector>();
        }

        itemCollector.listener += collected;

        itemCollector.setWants(ItemProperty.Mug);
    }

    public bool collected(ItemProperty property, Item item, bool isTarget) {
        item.properties.Remove(ItemProperty.Mug);
        item.properties.Add(ItemProperty.CoffeeCup);
        Debug.Log("Filled");

        return true;
    }
}
