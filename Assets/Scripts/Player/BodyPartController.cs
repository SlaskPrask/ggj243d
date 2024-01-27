using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BodyPartController : MonoBehaviour
{
    public enum BodyPart { LEGS, ARMS };
    public BodyPart bodyPart { get; protected set; }

    public enum AppendageState { NONE, LEFT, RIGHT };
    protected AppendageState state;

    public abstract void MoveLeftAppendage(Vector2 input); 
    public abstract void MoveRightAppendage(Vector2 input); 
}
