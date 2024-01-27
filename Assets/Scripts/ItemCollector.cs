using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ItemCollector : MonoBehaviour {
    public ItemProperty wants;

    public event Func<ItemProperty, Item, bool, bool> listener;

    public void OnCollisionEnter(Collision other) {
        if (wants == ItemProperty.None) {
            return;
        }

        Item item = other.gameObject.GetComponentInParent<Item>();
        if (item && item.properties.Contains(wants)) {
            if (listener(wants, item, item.target == null || item.target == this)) {
                item.delivered(wants);
            }
        }
    }

    public bool hasWants() {
        return wants != ItemProperty.None;
    }

    public void setWants(ItemProperty property) {
        wants = property;
    }
}
