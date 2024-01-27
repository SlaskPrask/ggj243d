using UnityEngine;

public class UpperBodyController : BodyPartController {
    public Transform leftHandAnchor;
    public Transform rightHandAnchor;

    private Vector2 leftMove;
    private Vector2 rightMove;

    private void Awake() {
        bodyPart = BodyPart.ARMS;
        GameManager.playerDetector.SetUpperBodyController(this);
    }

    private void Update() {

    }

    public override void MoveLeftAppendage(Vector2 input) {
        leftMove = input;
    }

    public override void MoveRightAppendage(Vector2 input) {
        rightMove = input;
    }
}