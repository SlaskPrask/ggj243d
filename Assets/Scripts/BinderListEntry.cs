using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class BinderListEntry {
    public readonly string text;
    public bool done;
    public float finishTime;

    public BinderListEntry(string text) {
        this.text = text;
        done = false;
    }

    public void setDone() {
        done = true;
        finishTime = Time.time;
    }
}
