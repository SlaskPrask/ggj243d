using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GoalsManager : MonoBehaviour {
    public float propertyDamageCost { get; private set; } = 0;
    public int tasksCompleted { get; private set; } = 0;

    public UIDocument scoreHud;

    private Label tasksDone;
    private Label costsDone;

    public GoalsManager Initialize() {
        propertyDamageCost = 0;
        tasksCompleted = 0;

        return this;
    }

    public void OnEnable() {
        Debug.Log(tasksDone);
        Debug.Log(scoreHud);
        Debug.Log(scoreHud.rootVisualElement);
        Debug.Log(scoreHud.rootVisualElement.Q<Label>("done"));
        tasksDone = scoreHud.rootVisualElement.Q<Label>("done");
        costsDone = scoreHud.rootVisualElement.Q<Label>("cost");

        scoreHud.rootVisualElement.style.opacity = 0;
    }

    public void activate() {
        scoreHud.rootVisualElement.style.opacity = 100;
    }

    public void propertyDamaged(float cost) {
        propertyDamageCost += cost;
        costsDone.text = $"{propertyDamageCost:0.00}€ property damage";
    }

    public void taskCompleted() {
        ++tasksCompleted;
        tasksDone.text = $"{tasksCompleted} work done";
        GameManager.audioManager.PlaySuccess();
    }
}
