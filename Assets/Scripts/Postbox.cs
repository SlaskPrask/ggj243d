using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Postbox : MonoBehaviour {
    public ItemCollector itemCollector;

    void Start() {
        if (itemCollector == null) {
            itemCollector = GetComponentInChildren<ItemCollector>();
        }

        itemCollector.listener += collected;
    }

    public bool collected(ItemProperty property, Item item, bool isTarget) {
        Package mail = item.GetComponentInParent<Package>();
        if (mail != null) {
            mail.delivered(property);
            Destroy(mail.gameObject);
        }

        Debug.Log("Mailed");

        return true;
    }
}
