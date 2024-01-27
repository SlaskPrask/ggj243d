using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeCup : Item {
    public SkinnedMeshRenderer renderer;

    public void emptyCoffeeCup() {
        renderer.SetBlendShapeWeight(0, 100);
        properties.Remove(ItemProperty.CoffeeCup);
        properties.Add(ItemProperty.Mug);
    }

    public void fillCoffeeCup() {
        renderer.SetBlendShapeWeight(0, 0);
        properties.Remove(ItemProperty.Mug);
        properties.Add(ItemProperty.CoffeeCup);
    }
}
