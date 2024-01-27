using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LowerBodyController : BodyPartController {
    public Transform leftFootAnchor;
    public Transform rightFootAnchor;

    private Vector2 leftMove;
    private Vector2 rightMove;
    private Vector3 newForward;
    private float moveSpeed = 3;
    private float camSpeed = .5f;
    private float stepDistance = .33f;
    private const float xOffset = 0.1f;

    private void Awake() {
        bodyPart = BodyPart.LEGS;
        GameManager.playerDetector.SetLowerBodyController(this);
    }

    private void Update() {
        if (state == AppendageState.NONE) {
            Vector3 pos = (leftFootAnchor.position + rightFootAnchor.position) * .5f;
            pos.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * moveSpeed);
            // to do: fix rotation
            transform.forward = Vector3.RotateTowards(transform.forward, newForward, Time.deltaTime * camSpeed, 0f);
        } else if (state == AppendageState.LEFT) {
            MoveAppendage(leftFootAnchor, leftMove, -xOffset);
        } else {
            MoveAppendage(rightFootAnchor, rightMove, xOffset);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);
    }

    void MoveAppendage(Transform foot, Vector2 movement, float offset) {
        Vector3 anchor = transform.position;
        anchor += transform.right * offset;

        
        Vector3 newMove = transform.rotation * new Vector3(movement.x, 0, movement.y) * stepDistance;
        Vector3 move3d = new Vector3(anchor.x + newMove.x, foot.position.y, anchor.z + newMove.z);
        foot.position = move3d;

    }

    public override void MoveLeftAppendage(Vector2 input) {
        if (state == AppendageState.LEFT || state == AppendageState.NONE) {
            float inputMagnitude = input.magnitude;

            if (inputMagnitude < .1f) {
                leftMove = Vector2.zero;
                state = AppendageState.NONE;
                return;
            }

            if (!(Vector2.Dot(input.normalized, leftMove.normalized) >= .9f && inputMagnitude < leftMove.magnitude)) {
                leftMove = input;
                newForward = input.normalized;
                newForward.z = newForward.y;
                newForward.y = 0;
                newForward = transform.rotation * newForward;
                state = AppendageState.LEFT;
            } 
            
        }
    }

    public override void MoveRightAppendage(Vector2 input) {
        if (state == AppendageState.RIGHT || state == AppendageState.NONE) {
            float inputMagnitude = input.magnitude;
            if (inputMagnitude < .1f) {
                state = AppendageState.NONE;
                rightMove = Vector2.zero;
                return;
            }
 
            if (!(Vector2.Dot(input.normalized, rightMove.normalized) >= .9f && inputMagnitude < rightMove.magnitude)) {
                rightMove = input;
                newForward = input.normalized;
                newForward.z = newForward.y;
                newForward.y = 0;
                newForward = transform.rotation * newForward;
                state = AppendageState.RIGHT;
            }
            
        }
    }
}
