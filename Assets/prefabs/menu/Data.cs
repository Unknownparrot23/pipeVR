using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//I expect that the menu will require storing variables, so I created a separate component, but for now, there's only one variable here.

//я рассчитываю что для меню понадобиться хранение переменных и создал отдельный компонент но пока тут только одна переменная 

public class Data : MonoBehaviour


{

    [Header("local data")]
    [SerializeField] public GameObject pointOfCreation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
