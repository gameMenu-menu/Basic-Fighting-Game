using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joistick : MonoBehaviour
{
    public Transform Outer, Inner;

    public bool ResultExists;
    public Vector3 ResultDirection;

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;

            Vector3 direction = Inner.position - mousePos;

            ResultDirection = direction.normalized;

            

            direction = direction.normalized*80f;

            

            Outer.position = Inner.position - direction;

            ResultExists = true;
        }
        else
        {
            ResultExists = false;
        }


    }
}
