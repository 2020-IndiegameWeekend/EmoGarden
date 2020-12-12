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

        if (_time > .1f)
        {
            _time = 0;
            _target = parent.position;
            MoveToTarget(_target);
        }
    }

    protected virtual bool MoveToTarget(Vector3 target)
    {
        target.z = transform.position.z;
        Vector2 move = (Vector2) (target - transform.position);
        
        Vector2 space = (Vector2) (child.position - transform.position);
        
        if (move.magnitude > spaceOfBody && space.magnitude <= spaceOfBody)
        {
            var normalMovePos = move.normalized;
            var curPos = transform.position;

            normalMovePos *= .8f;

            Vector2 movePos = new Vector2(curPos.x + normalMovePos.x, curPos.y + normalMovePos.y);
            transform.DOMove(movePos, 0.1f, false);
            //transform.Translate(move.normalized * Time.deltaTime * speed);
            return true;
        }

        if (space.magnitude > spaceOfBody)
        {
            /*
            var normalSpace = space.normalized;
            var curPos = transform.position;

            normalSpace *= 0.8f;
            
            Vector2 movePos = new Vector2(curPos.x + normalSpace.x, curPos.y + normalSpace.y);
            
            transform.DOMove(movePos, 0.1f, false);
            */
            transform.Translate(space.normalized * Time.deltaTime * speed);
        }

        return false;
    }
}
