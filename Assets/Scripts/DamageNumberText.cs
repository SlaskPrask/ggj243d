using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class DamageNumberText : MonoBehaviour {
    public float speed;
    public float duration = 2f;

    public TMP_Text text;

    public void Start() {
        StartCoroutine(disappear());
    }

    private IEnumerator disappear() {
        float lifeTime = Time.time + duration;
        while (Time.time < lifeTime) {
            yield return null;
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update() {
        transform.position += new Vector3(0, Time.deltaTime * speed, 0);
        transform.forward = GameManager.camera.transform.forward;
    }

    public void setText(string text) {
        this.text.text = text;
    }
}
