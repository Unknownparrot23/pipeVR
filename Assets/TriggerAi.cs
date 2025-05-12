//using UnityEngine;
//using UnityEngine.InputSystem;

//public class ObjectClicker : MonoBehaviour
//{
//    [Header("Input")]
//    [SerializeField] private InputActionAsset inputActions;
//    private InputAction clickAction;

//    [Header("Raycast")]
//    [SerializeField] private LayerMask interactableLayer;
//    [SerializeField] private float maxDistance = 100f;

//    private Camera mainCamera;

//    private void Awake()
//    {
//        // Get the click action from the input asset
//        var gameplayActionMap = inputActions.FindActionMap("Gameplay");
//        clickAction = gameplayActionMap.FindAction("Click");

//        mainCamera = Camera.main;
//    }

//    private void OnEnable()
//    {
//        clickAction.performed += OnClick;
//        clickAction.Enable();
//    }

//    private void OnDisable()
//    {
//        clickAction.performed -= OnClick;
//        clickAction.Disable();
//    }

//    private void OnClick(InputAction.CallbackContext context)
//    {
//        // Only proceed if we're pressing the button down (not releasing)
//        if (context.ReadValueAsButton())
//        {
//            PerformRaycast();
//        }
//    }

//    private void PerformRaycast()
//    {
//        // For mouse
//        Vector2 screenPosition = Mouse.current.position.ReadValue();

//        // For touchscreen (mobile)
//        // if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
//        // {
//        //     screenPosition = Touchscreen.current.primaryTouch.position.ReadValue();
//        // }

//        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

//        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactableLayer))
//        {
//            GameObject clickedObject = hit.collider.gameObject;
//            Debug.Log($"Clicked on: {clickedObject.name}");

//            // Option 1: If the object has a script with an OnClick method
//            //IClickable clickable = clickedObject.GetComponent<IClickable>();
//            if (clickable != null)
//            {
//                clickable.OnClick();
//            }

//            // Option 2: Send message (less performant but flexible)
//            // clickedObject.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
//        }
//    }
//}