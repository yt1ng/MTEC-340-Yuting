using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [SerializeField]
    public Vector3 targetPos1;
    [SerializeField]
    public Vector3 targetPos2;
    [SerializeField]
    public float speed = 1f;
    public bool canMove;
    public bool firstMove;
    void Start()
    {

        firstMove = true;

    }
    void Update()
    {
        if (transform.position == targetPos1)
        {
            firstMove = false;
        }
        if (transform.position == targetPos2)
        {
            firstMove = true;
        }
        if (canMove)
        {
            if (firstMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos1, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos2, speed * Time.deltaTime);
            }
        }

    }
}