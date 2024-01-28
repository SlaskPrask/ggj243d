using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class BinderUI : MonoBehaviour {
    private VisualElement visuals;
    private ListView taskList;
    private UIDocument document;

    private List<BinderListEntry> quests = new();

    public float showSpeed = 10f;

    private IEnumerator showing;
    private float showDirection = -1;
    private float showAmount = 0;

    void Start() {
        document = GetComponent<UIDocument>();
        visuals = document.rootVisualElement.Q("binder");
        taskList = document.rootVisualElement.Q<ListView>("todo-items");

        taskList.makeItem = createElement;
        taskList.bindItem = bindElement;
        taskList.itemsSource = quests;

        updateShown();
    }

    private void updateShown() {
        visuals.style.top = Length.Percent(100f - 100f * showAmount);
    }

    public void toggle() {
        if (showDirection < 0) {
            show();
        }
        else {
            hide();
        }
    }

    private void bindElement(VisualElement element, int index) {
        Label label = element.Q<Label>(className: "binder-note-text");
        BinderListEntry entry = quests[index];
        if (entry.done) {
            label.text = $"<s>{entry.text}</s>";
        }
        else {
            label.text = entry.text;
        }
    }

    private VisualElement createElement() {
        VisualElement root = new VisualElement();
        root.AddToClassList("binder-note");
        root.style.height = new StyleLength(StyleKeyword.Auto);
        Label label = new Label();
        label.AddToClassList("binder-note-text");

        root.Add(label);

        return root;
    }

    public BinderListEntry addQuest(string task) {
        BinderListEntry entry = new BinderListEntry(task);
        quests.Add(entry);
        taskList.Rebuild();
        return entry;
    }

    public void finishQuest(BinderListEntry entry) {
        entry.setDone();
        taskList.Rebuild();
    }

    public void show() {
        if (showAmount <= 0.01f) {
            for (int i = 0; i < quests.Count; i++) {
                if (quests[i].done && quests[i].finishTime + 10f < Time.time) {
                    quests.RemoveAt(i);
                    --i;
                }
            }

            taskList.Rebuild();
        }

        showDirection = 1;
        if (showing == null) {
            StartCoroutine(showing = showAnim());
        }
    }

    public void hide() {
        showDirection = -1;
        if (showing == null) {
            StartCoroutine(showing = showAnim());
        }
    }

    private IEnumerator showAnim() {
        for (;;) {
            if (showDirection > 0) {
                showAmount += Time.deltaTime * showSpeed;
                if (showAmount >= 1) {
                    showAmount = 1;
                    updateShown();
                    break;
                }

                updateShown();
            }
            else {
                showAmount -= Time.deltaTime * showSpeed;
                if (showAmount <= 0) {
                    showAmount = 0;
                    updateShown();
                    break;
                }

                updateShown();
            }

            yield return null;
        }

        showing = null;
    }
}
