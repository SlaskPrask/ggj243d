using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ExpensiveItem : MonoBehaviour {
    public GameObject damageNumberPrefab;

    public float cost;
    private float brokenAmount = 0;

    public float toughness = 1000;
    public float minHitForce = 8;

    public void OnCollisionEnter(Collision collision) {
        float force = collision.impulse.magnitude;

        if (force < minHitForce) {
            return;
        }

        float damage = force / toughness;
        float expenses = damage * cost;

        expenses = Mathf.Round(expenses * 100f / 5f) * 5f / 100f;
        float newAmount = Mathf.Min(cost, brokenAmount + expenses);

        expenses = newAmount - brokenAmount;

        if (expenses <= 0) {
            return;
        }

        GameManager.goalsManager.propertyDamaged(expenses);
        brokenAmount = newAmount;

        DamageNumberText text = Instantiate(damageNumberPrefab, collision.contacts[0].point,
            Quaternion.identity).GetComponent<DamageNumberText>();

        text.setText($"-{expenses:0.00}€");
    }
}
