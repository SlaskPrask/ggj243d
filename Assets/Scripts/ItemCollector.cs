using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ItemCollector : MonoBehaviour {
    public Collider trigger;

    public ItemProperty wants;

    public event Func<ItemProperty, Item, bool, bool> listener;

    public void OnTriggerEnter(Collider other) {
        if (wants == ItemProperty.None) {
            return;
        }

        Item item = other.GetComponentInParent<Item>();
        if (item.properties.Contains(wants)) {
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

    public void OnValidate() {
        if (!trigger) {
            trigger = GetComponentsInChildren<Collider>().First(c => c.isTrigger);
        }

        if (trigger && trigger.gameObject.layer != 7) {
            trigger.gameObject.layer = 7;
        }
    }
}
