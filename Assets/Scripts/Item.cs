using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public List<ItemProperty> properties;

    public ItemCollector target;

    void Start() {
    }

    void Update() {
    }

    public virtual void delivered(ItemProperty tag) {
    }
}
