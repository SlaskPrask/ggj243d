using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GameEnder : MonoBehaviour {
    public UIDocument uiDocument;

    private Label done;
    private Label cost;
    private Label resolution;
    private Label resolution2;
    private Label explanation;

    private bool canEnd;

    void Start() {
        StartCoroutine(canEndGame());

        done = uiDocument.rootVisualElement.Q<Label>("done");
        cost = uiDocument.rootVisualElement.Q<Label>("cost");
        resolution = uiDocument.rootVisualElement.Q<Label>("resolution");
        resolution2 = uiDocument.rootVisualElement.Q<Label>("resolution2");
        explanation = uiDocument.rootVisualElement.Q<Label>("explanation");

        done.text = $"{GameManager.goalsManager.tasksCompleted}";
        cost.text = $"{GameManager.goalsManager.propertyDamageCost:0.00}€";

        int pointsHaha = GameManager.goalsManager.tasksCompleted * 10;
        pointsHaha -= costPoint(GameManager.goalsManager.propertyDamageCost);

        if (pointsHaha <= 0) {
            resolution.text = "FIRED";
            resolution.style.backgroundColor = new Color(0.8f, 0, 0);
            resolution2.text = "";
            string costNote = GameManager.goalsManager.propertyDamageCost > 0
                ? $"You have caused the company {cost.text}€ damage."
                : "";
            explanation.text =
                $"Congratulations! You didn't get fired! {costNote}";
        }
        else if (pointsHaha >= 100) {
            resolution.text = "PROMOTED";
            resolution.style.backgroundColor = new Color(0, 0.65f, 0);
            resolution2.text = "to senior useless task assistant";
            string costNote = GameManager.goalsManager.propertyDamageCost > 0
                ? $"Even though you caused the company {cost.text}€ damage."
                : "";
            explanation.text =
                $"Congratulations! You got promoted to Senior Useless Task Person! {costNote}";
        }
        else {
            resolution.text = "ACCEPTABLE";
            resolution.style.backgroundColor = new Color(0.1f, 0.1f, 0.1f);
            resolution2.text = "see me";
            string costNote = GameManager.goalsManager.propertyDamageCost > 0
                ? $"But you caused the company {cost.text} damage."
                : "";
            explanation.text =
                $"Congratulations! You didn't get fired! {costNote}";
        }
    }

    private IEnumerator canEndGame() {
        yield return new WaitForSeconds(1);
        canEnd = true;
    }

    private int costPoint(float cost) {
        return (int)cost / 1000;
    }


    public void onInput(InputAction.CallbackContext ctx) {
        if (ctx.ReadValueAsButton() && ctx.phase == InputActionPhase.Performed && canEnd) {
            Application.Quit();
        }
    }
}
