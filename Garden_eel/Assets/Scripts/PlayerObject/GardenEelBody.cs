using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenEelBody : MonoBehaviour
{
    public int speed;
    public bool isHead;
    public Transform parent;
    
    private Vector3 target;
    
    void Start()
    {
        if (!isHead)
        {
            target = parent.position;
        }
    }

    void FixedUpdate()
    {
        if (isHead)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            target = parent.position;
        }
        
        if (Input.GetMouseButton(0) || Input.touchCount > 0 || !isHead)
        {
            MoveToTarget(target);
        }
    }

    private bool MoveToTarget(Vector3 target)
    {
        target.z = transform.position.z;
        Vector2 move = (Vector2) (target - transform.position);
            
        if (move.sqrMagnitude > 3f)
        {
            transform.Translate(move.normalized * Time.deltaTime * speed);
            return true;
        }

        return false;
    }
}
