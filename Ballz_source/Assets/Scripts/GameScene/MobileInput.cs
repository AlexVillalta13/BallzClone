using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoSingleton<MobileInput>
{
    public bool tap, release, hold;
    public Vector2 swipeDelta;

    private Vector2 initialPosition;

    private void Update()
    {
        release = tap = false;

        if (Input.GetMouseButtonDown(0))
        {
            //initialPosition = Input.mousePosition;
            hold = tap = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            release = true;
            hold = false;
        }

        if (hold)
        {
            //swipeDelta = (Vector2)Input.mousePosition - initialPosition;
            swipeDelta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Ball.Instance.transform.position;
        }
    }
}
