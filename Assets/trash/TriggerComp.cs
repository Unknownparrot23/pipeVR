//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.InputSystem;
//public class TriggerComp : MonoBehaviour
//{


//    private Cameracon inputActions;

//    void OnEnable()
//    {
//        inputActions = new Cameracon();
//        inputActions.Enable();

//        inputActions.def.Move.performed += OnMove;
//        inputActions.def.Move.canceled += OnMove;

//        inputActions.def.Look.performed += OnLook;
//        inputActions.def.Look.canceled += OnLook;

//        inputActions.def.MoveUp.performed += OnMoveUp;
//        inputActions.def.MoveUp.canceled += OnMoveUp;

//        inputActions.def.MoveDown.performed += OnMoveDown;
//        inputActions.def.MoveDown.canceled += OnMoveDown;

//        inputActions.def.Boost.performed += OnBoost;
//        inputActions.def.Boost.canceled += OnBoost;

//        inputActions.def.RightClickHold.performed += OnRightClickHold;
//        inputActions.def.RightClickHold.canceled += OnRightClickHold;
//    }


//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
