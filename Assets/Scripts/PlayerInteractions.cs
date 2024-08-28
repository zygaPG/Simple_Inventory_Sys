using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerInteractions : MonoBehaviour
{
    public InputActionAsset actionsAsset; // Przypisz swój plik Input Actions tutaj
    private InputAction takeAction;
    private InputAction dropAction;
    private InputAction nextItemAction;
    private InputAction previousItemAction;

    [System.Serializable]
    public class PressEvent : UnityEvent<InputAction.CallbackContext> { }

    public PressEvent onPressTake;
    public PressEvent onPressDrop;
    public PressEvent onSelectNextItem;
    public PressEvent onSelectPreviousItem;



    private void Awake()
    {
        takeAction = actionsAsset.FindAction("Take");
        dropAction = actionsAsset.FindAction("Drop");
        nextItemAction = actionsAsset.FindAction("NextItemSlot");
        previousItemAction = actionsAsset.FindAction("PreviousItemSlot");

        takeAction.performed += OnPressTake;
        dropAction.performed += OnPressDrop;
        nextItemAction.performed += OnPressNextItemSlot;
        previousItemAction.performed += OnPressPreviousItemSlot;
    }

    void OnPressTake(InputAction.CallbackContext callback) => onPressTake.Invoke(callback);
    void OnPressDrop(InputAction.CallbackContext callback) => onPressDrop.Invoke(callback);
    void OnPressNextItemSlot(InputAction.CallbackContext callback) => onSelectNextItem.Invoke(callback);
    void OnPressPreviousItemSlot(InputAction.CallbackContext callback) => onSelectPreviousItem.Invoke(callback);
    
}
