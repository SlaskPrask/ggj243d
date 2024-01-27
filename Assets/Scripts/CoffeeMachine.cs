using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class CoffeeMachine : MonoBehaviour {
    public ItemCollector itemCollector;

    void Start() {
        if (itemCollector == null) {
            itemCollector = GetComponentInChildren<ItemCollector>();
        }

        itemCollector.listener += collected;
    }

    public bool collected(ItemProperty property, Item item, bool isTarget) {
        if (item is CoffeeCup coffee) {
            coffee.fillCoffeeCup();
        }

        Debug.Log("Filled");

        return true;
    }
}
