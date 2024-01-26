using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public GameObject head;

    [InspectorLabel("Torso")]
    public GameObject chest;
    public GameObject abdomen;

    [InspectorLabel("Left Arm")]
    public GameObject left_upper_arm;
    public GameObject left_lower_arm;
    public GameObject left_hand;

    [InspectorLabel("Right Arm")]
    public GameObject right_upper_arm;
    public GameObject right_lower_arm;
    public GameObject right_hand;

    [InspectorLabel("Left Leg")]
    public GameObject left_upper_leg;
    public GameObject left_lower_leg;
    public GameObject left_foot;

    [InspectorLabel("Right Leg")]
    public GameObject right_upper_leg;
    public GameObject right_lower_leg;
    public GameObject right_foot;

    public void Awake() {
        
    }

}
