using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour {
    public int needsPaperStacks = 0;

    public ItemCollector itemCollector;

    void Start() {
        if (itemCollector == null) {
            itemCollector = GetComponentInChildren<ItemCollector>();
        }

        itemCollector.listener += collected;
    }

    public void addNeededPaper(int stacks) {
        needsPaperStacks += stacks;
    }

    public bool collected(ItemProperty property, Item item, bool isTarget) {
        if (needsPaperStacks == 0) {
            return true;
        }

        needsPaperStacks--;

        Destroy(item);
        Debug.Log("Papered");

        return true;
    }
}
