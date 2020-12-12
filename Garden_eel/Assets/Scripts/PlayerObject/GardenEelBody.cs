using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenEelBody : MonoBehaviour
{
    public int speed;
    public float spaceOfBody;
    public bool isHead;
    public Transform parent;

    protected Vector3 _target;

    protected virtual void Start()
    {
        _target = parent.position;
    }

    protected virtual void FixedUpdate()
    {
        _target = parent.position;
        MoveToTarget(_target);
    }

    protected virtual bool MoveToTarget(Vector3 target)
    {
        target.z = transform.position.z;
        Vector2 move = (Vector2) (target - transform.position);
            
        if (move.sqrMagnitude > spaceOfBody)
        {
            transform.Translate(move.normalized * Time.deltaTime * speed);
            return true;
        }

        return false;
    }
}
