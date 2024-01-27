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

    public void emptyCoffeeCup() {
        properties.Remove(ItemProperty.CoffeeCup);
        properties.Add(ItemProperty.Mug);
    }

    public void fillCoffeeCup() {
        properties.Remove(ItemProperty.Mug);
        properties.Add(ItemProperty.CoffeeCup);
    }

    public virtual void delivered(ItemProperty tag) {
    }
}
