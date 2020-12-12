using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenEelBody : MonoBehaviour
{
    public int speed;
    public float spaceOfBody;
    public Transform parent;
    public Transform child;

    protected Vector3 _target;
    protected bool _isTail = false;

    public void SetIsTail(bool isTail) => _isTail = isTail;
    
    protected virtual void Start()
    {
        _target = parent.position;
    }

    protected virtual void FixedUpdate()
    {
        if (_isTail)
        {
            return;
        }
        
        _target = parent.position;
        MoveToTarget(_target);
    }

    protected virtual bool MoveToTarget(Vector3 target)
    {
        target.z = transform.position.z;
        Vector2 move = (Vector2) (target - transform.position);
        Vector2 space = (Vector2) (transform.position - child.position);
            
        if (move.magnitude > spaceOfBody && space.magnitude < spaceOfBody)
        {
            transform.Translate(move.normalized * Time.deltaTime * speed);
            return true;
        }

        return false;
    }
}
