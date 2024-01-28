using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalsManager : MonoBehaviour {
    private float propertyDamageCost = 0;
    private int tasksCompleted = 0;

    public GoalsManager Initialize() {
        propertyDamageCost = 0;
        tasksCompleted = 0;
        return this;
    }

    public void propertyDamaged(float cost) {
        propertyDamageCost += cost;
    }

    public void taskCompleted() {
        ++tasksCompleted;
    }
}
