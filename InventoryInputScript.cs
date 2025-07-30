using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryInputScript : MonoBehaviour
{
    public bool GrabToggle { get; private set; } = false;
    public Vector2 ItemDrag { get; private set; } = Vector2.zero;



    InputMap _Input = null;

    private void OnEnable() //subscribe to inputs
    {

        _Input = new InputMap();

        _Input.InventoryMap.Enable();

        _Input.InventoryMap.GrabToggle.started += SetGrab;
        _Input.InventoryMap.GrabToggle.canceled += SetGrab;

        _Input.InventoryMap.ItemDrag.performed += SetDrag;
        _Input.InventoryMap.ItemDrag.canceled += SetDrag;

    }

    private void OnDisable() //unsubscribe to inputs
    {

        _Input.InventoryMap.GrabToggle.started -= SetGrab;
        _Input.InventoryMap.GrabToggle.canceled -= SetGrab;

        _Input.InventoryMap.ItemDrag.performed -= SetDrag;
        _Input.InventoryMap.ItemDrag.canceled -= SetDrag;

        _Input.InventoryMap.Disable();

    }
    void SetGrab(InputAction.CallbackContext ctx)
    {
        GrabToggle = ctx.started;
    }

    void SetDrag(InputAction.CallbackContext ctx)
    {
        ItemDrag = ctx.ReadValue<Vector2>();
    }
    
    

}
