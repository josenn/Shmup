using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
        MoveVertical();
    }

    private void MoveVertical()
    {
        //delta is referring to a change in where we are and where we want to be

        //TODO lots of refactoring and commenting (on other projects too)

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = transform.position.y + deltaY;
        transform.position = new Vector2(transform.position.x, newYPos);
    }

    //might need to change and merge these 2 later who knows
    private void MoveHorizontal()
    {
        //delta is referring to a change in where we are and where we want to be
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = transform.position.x + deltaX;
        transform.position = new Vector2(newXPos, transform.position.y);
    }
}
