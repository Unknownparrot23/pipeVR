using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class freeFlycamera : MonoBehaviour
{

    [Header("movement setting")]
    [SerializeField] private float normalSpeed = 1f;
    [SerializeField] private float boostedSpeed = 1.5f;
    [SerializeField] private float verticalSpeed = 0.3f;

    [Header("Mouse setting")]
    [SerializeField] private float lookSensitivity= 1f;
    [SerializeField] private bool invertY = false;


    [Header("moving")]
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Vector2 lookInput;
    [SerializeField] private bool isBoosting;
    [SerializeField] private bool isLooking;
    [SerializeField] private Vector3 verticalMovement = Vector3.zero;
    [SerializeField] private float xRotation =0f;
    [SerializeField] private float yRotation =0f;




    private Camera cam;
    private Cameracon inputActions;

    // Start is called before the first frame update
    private void Awake()
    {
        //   cam = Camera.main;
        Vector3 initialRotation = transform.localEulerAngles;
        xRotation = initialRotation.x;
        yRotation = initialRotation.y;
    }
    void OnEnable()
    {
        inputActions = new Cameracon();
        inputActions.Enable();

        inputActions.def.Move.performed += OnMove;
        inputActions.def.Move.canceled += OnMove;

        inputActions.def.Look.performed += OnLook;
        inputActions.def.Look.canceled += OnLook;

        inputActions.def.MoveUp.performed += OnMoveUp;
        inputActions.def.MoveUp.canceled += OnMoveUp;

        inputActions.def.MoveDown.performed += OnMoveDown;
        inputActions.def.MoveDown.canceled += OnMoveDown;

        inputActions.def.Boost.performed += OnBoost;
        inputActions.def.Boost.canceled += OnBoost;

        inputActions.def.RightClickHold.performed += OnRightClickHold;
        inputActions.def.RightClickHold.canceled += OnRightClickHold;
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    public void OnBoost(InputAction.CallbackContext context)
    {
        isBoosting = context.performed;
    }
    public void OnMoveUp(InputAction.CallbackContext context)
    {
        verticalMovement = context.performed ?Vector3.up:Vector3.zero;
    }
    public void OnMoveDown(InputAction.CallbackContext context)
    {
        verticalMovement = context.performed ? Vector3.down : Vector3.zero;
    }
    public void OnRightClickHold(InputAction.CallbackContext context)
    {
        isLooking = context.performed;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLooking)
        {
            LookAround();
        }
        MoveCamera();

    }
    void MoveCamera()
    {
        float speed = isBoosting ? boostedSpeed:normalSpeed;

        Vector3 forwardMovement = Vector3.Normalize(transform.forward)*moveInput.y*speed*Time.deltaTime;
        Vector3 rightMovement = Vector3.Normalize(transform.right) * moveInput.x * speed * Time.deltaTime;

        transform.position += forwardMovement + rightMovement + (verticalMovement * verticalSpeed * Time.deltaTime);
    }
    void LookAround()
    {
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity * (invertY ?-1:1);

        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation - mouseY, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation,yRotation,0f);

    }
}
