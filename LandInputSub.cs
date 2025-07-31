using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LandInputSub : MonoBehaviour
{
    //Variables

    public Vector2 MovementInput { get; private set; } = Vector2.zero;
    public bool MenuInput { get; private set; } = false;
    public bool InspectInput { get; private set; } = false;
    public bool AttackInput { get; private set; } = false;

    InputMap _Input = null;

    private void OnEnable() //subscribe to inputs
    {

        _Input = new InputMap();

        _Input.PlayerInputMapLAND.Enable();

        _Input.PlayerInputMapLAND.MovementInput.performed += SetMovement;
        _Input.PlayerInputMapLAND.MovementInput.canceled += SetMovement;

        _Input.PlayerInputMapLAND.InspectInput.started += SetAction;
        _Input.PlayerInputMapLAND.InspectInput.canceled += SetAction;

        _Input.PlayerInputMapLAND.AttackInput.started += SetAttack;
        _Input.PlayerInputMapLAND.AttackInput.canceled += SetAttack;


    }

    private void OnDisable() //unsubscribe to inputs
    {

        _Input.PlayerInputMapLAND.MovementInput.performed -= SetMovement;
        _Input.PlayerInputMapLAND.MovementInput.canceled -= SetMovement;

        _Input.PlayerInputMapLAND.InspectInput.started -= SetAction;
        _Input.PlayerInputMapLAND.InspectInput.canceled -= SetAction;

        _Input.PlayerInputMapLAND.AttackInput.started -= SetAttack;
        _Input.PlayerInputMapLAND.AttackInput.canceled -= SetAttack;



        _Input.PlayerInputMapLAND.Disable();
    }

    private void Update()
    {
        MenuInput = _Input.PlayerInputMapLAND.MenuInput.WasPressedThisFrame();
    }

    void SetMovement(InputAction.CallbackContext ctx)
    {
        MovementInput = ctx.ReadValue<Vector2>();
    }

    void SetAction(InputAction.CallbackContext ctx)
    {
        InspectInput = ctx.started;
    }

    void SetAttack(InputAction.CallbackContext ctx)
    {
        AttackInput = ctx.started;
    }



}
