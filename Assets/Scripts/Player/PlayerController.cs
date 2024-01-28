using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    private BodyPartController controller;
    private byte playerIndex;

    public void Initialize(BodyPartController bodyController) {
        controller = bodyController;
    }

    public BodyPartController.BodyPart GetBodyPart() {
        return controller.bodyPart;
    }

    public void ShowBinder(InputAction.CallbackContext ctx) {
        if (ctx.ReadValueAsButton() && ctx.phase == InputActionPhase.Performed) {
            GameManager.questManager.binder.toggle();
        }
    }

    public void MoveLeftStick(InputAction.CallbackContext ctx) {
        controller.MoveLeftAppendage(ctx.ReadValue<Vector2>());
    }

    public void MoveRightStick(InputAction.CallbackContext ctx) {
        controller.MoveRightAppendage(ctx.ReadValue<Vector2>());
    }

    public void LeftGrab(InputAction.CallbackContext ctx) {
        controller.LeftGrab(ctx.ReadValueAsButton());
    }

    public void RightGrab(InputAction.CallbackContext ctx) {
        controller.RightGrab(ctx.ReadValueAsButton());
    }
}
