using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpperBodyController : BodyPartController {
    public Transform leftHandAnchor;
    public Transform rightHandAnchor;
    public Transform leftHand;
    public Transform rightHand;

    public Transform centralShoulder;

    public Transform leftTrigger;
    public Transform rightTrigger;

    private Vector2 leftMove;
    private Vector2 rightMove;
    private float xOffset = .26f;
    private float armLength = .5f;
    private bool leftGrab;
    private bool rightGrab;

    List<ConfigurableJoint> leftHandObjects;
    List<ConfigurableJoint> rightHandObjects;

    private void Awake() {
        bodyPart = BodyPart.ARMS;
        GameManager.playerDetector.SetUpperBodyController(this);
        leftHandObjects = new List<ConfigurableJoint>();
        rightHandObjects = new List<ConfigurableJoint>();

        leftTrigger.GetComponent<HandCollider>().RegisterFunc(LeftTriggerEvent);
        rightTrigger.GetComponent<HandCollider>().RegisterFunc(RightTriggerEvent);
    }

    public void BodyUpdate(float deltaTime, Vector3 forward) {
        transform.forward = forward;
        MoveAppendage(leftHandAnchor, leftMove, -xOffset);
        MoveAppendage(rightHandAnchor, rightMove, xOffset);
        GrabHand(leftHandAnchor.position, leftGrab, leftTrigger);
        GrabHand(rightHandAnchor.position, rightGrab, rightTrigger);
    }

    void MoveAppendage(Transform hand, Vector2 movement, float offset) {
        Vector3 socket = centralShoulder.position + transform.right * offset;
        Vector3 spherePoint = new Vector3(
            movement.x,
            -Mathf.Sqrt(1 - Mathf.Min(movement.x * movement.x + movement.y * movement.y, 0f)),
            movement.y) * armLength;

        hand.position = socket + transform.rotation * spherePoint + Vector3.up * movement.magnitude * .7f;
    }

    void GrabHand(Vector3 hand, bool state, Transform trigger) {
        if (state) {
            trigger.position = hand + Vector3.down * .5f;
        }
    }

    ConfigurableJoint CreateJoint(Transform obj) {
        ConfigurableJoint joint = obj.AddComponent<ConfigurableJoint>();
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        joint.angularXMotion = ConfigurableJointMotion.Locked;
        joint.angularYMotion = ConfigurableJointMotion.Locked;
        joint.angularZMotion = ConfigurableJointMotion.Locked;

        JointDrive drivePos = new JointDrive();
        drivePos.positionSpring = 10000f;
        drivePos.positionDamper = 10f;
        drivePos.maximumForce = 1f;

        JointDrive driveRot = new JointDrive();
        driveRot.positionSpring = 1000f;
        driveRot.positionDamper = 2f;
        driveRot.maximumForce = 1f;

        /*joint.slerpDrive = driveRot;
        joint.xDrive = drivePos;
        joint.yDrive = drivePos;
        joint.zDrive = drivePos;*/

        return joint;
    }

    private void LeftTriggerEvent(Collider other) {
        Rigidbody rigidbody = other.GetComponentInParent<Rigidbody>();
        foreach (ConfigurableJoint existingJoint in leftHandObjects) {
            if (existingJoint.gameObject == rigidbody.gameObject) {
                return;
            }
        }

        ConfigurableJoint joint = CreateJoint(rigidbody.transform);
        rigidbody.transform.position = leftHand.position;
        joint.connectedBody = leftHand.GetComponent<Rigidbody>();
        leftHandObjects.Add(joint);
        GameManager.audioManager.PlayPickup(leftHand.position);
    }

    private void RightTriggerEvent(Collider other) {
        Rigidbody rigidbody = other.GetComponentInParent<Rigidbody>();
        foreach (ConfigurableJoint existingJoint in rightHandObjects) {
            if (existingJoint.gameObject == rigidbody.gameObject) {
                return;
            }
        }

        ConfigurableJoint joint = CreateJoint(rigidbody.transform);
        rigidbody.transform.position = rightHand.position;
        joint.connectedBody = rightHand.GetComponent<Rigidbody>();
        rightHandObjects.Add(joint);
        GameManager.audioManager.PlayPickup(rightHand.position);
    }

    public override void MoveLeftAppendage(Vector2 input) {
        leftMove = Vector2.ClampMagnitude(input, 1f);
    }

    public override void MoveRightAppendage(Vector2 input) {
        rightMove = Vector2.ClampMagnitude(input, 1f);
    }

    public override void LeftGrab(bool state) {
        leftGrab = state;
        leftTrigger.gameObject.SetActive(state);
        if (!state) {
            foreach (ConfigurableJoint joint in leftHandObjects) {
                if (joint) {
                    joint.connectedBody = null;
                    //joint.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    Destroy(joint);
                }
            }

            leftHandObjects.Clear();
        }
    }

    public override void RightGrab(bool state) {
        rightGrab = state;
        rightTrigger.gameObject.SetActive(state);
        if (!state) {
            foreach (ConfigurableJoint joint in rightHandObjects) {
                if (joint) {
                    joint.connectedBody = null;

                    //joint.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    Destroy(joint);
                }
            }

            rightHandObjects.Clear();
        }
    }
}
