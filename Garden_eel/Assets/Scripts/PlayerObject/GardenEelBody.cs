using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GardenEelBody : MonoBehaviour
{
    public int speed;
    public float spaceOfBody;
    public Transform parent;
    public Transform child;

    protected Vector3 _target;
    protected bool _isTail = false;

    private float _time = 0;

    public void SetIsTail(bool isTail) => _isTail = isTail;
    
    protected virtual void Start()
    {
        _target = parent.position;
    }

    protected virtual void FixedUpdate()
    {
        _time += Time.deltaTime;
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
        
        Vector2 space = (Vector2) (child.position - transform.position);
        
        if (move.sqrMagnitude > spaceOfBody && space.sqrMagnitude <= spaceOfBody)
        {
            //transform.DOMove(target, 0.5f, false);
            transform.Translate(move.normalized * Time.deltaTime * speed);
            return true;
        }

        if (space.sqrMagnitude > spaceOfBody)
        {
            //transform.DOMove(child.position, 0.5f, false);
            transform.Translate(space.normalized * Time.deltaTime * speed);
        }

        return false;
    }
}
