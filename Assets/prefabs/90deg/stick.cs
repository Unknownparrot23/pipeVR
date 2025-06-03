using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using static UnityEngine.GraphicsBuffer;

// Returns the pipe end to the model
// Keeps the pipe end at a specified radius from the axis and offset from the pipe start (possible positions form a circle)
// Requires input from the rotation script where this object is set as Target
// Might be removed from rotation script eventually

//возвращает конец трубы к модели
//Сохраняет конец трубы на радиусе от оси и отступе от старта трубы(возможные позиции это окружность).
//Для работы требует передачу от скрипта rotation в котором данный обьект Target
//возмжно будет удалён по идеи из rotation



[RequireComponent(typeof(Interactable))]




public class stick : MonoBehaviour
{
    
    public GameObject StartObject ;
    public Vector3 DisFromAxis ;
    public Vector3 otstup;



    private void OnDetachedFromHand(Hand hand)
    {
        hand.HoverUnlock(null);
        Invoke(nameof(SnapToPosition), 0.2f); // Small delay to ensure physics settles and give a chance to regrab 
    }


    private void SnapToPosition()
    {

        RotationToPoint ROTcomponent = StartObject.GetComponent<RotationToPoint>();
        float lenght = DisFromAxis.magnitude;
        Vector3 newPos = DisFromAxis * ROTcomponent.Radius / lenght;
        transform.localPosition = newPos+ (otstup * ROTcomponent.Radius);
    }


}
