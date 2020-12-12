using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Deco : MonoBehaviour
{
    [Range(0.1f, 0.5f)]
    public float distance = 0.1f;
    public float moveRange = 0.25f;
    public float moveSpeed = 1f;
    public float nextMoveTime = 2f;

    private float checkDistance;

    private void Start()
    {
        checkDistance = moveRange * distance;
        StartCoroutine(FlyCoroutine());
    }

    private IEnumerator FlyCoroutine()
    {
        while (true)
        {
            float x = Random.Range(-moveRange, moveRange);
            float y = Random.Range(-moveRange, moveRange);

            Vector2 destination = (Vector2)transform.position + new Vector2(x, y);

            Vector2 diff = destination - (Vector2)transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.DORotate(new Vector3(0, 0, rot_z - 90), 0.5f);

            while (Vector2.Distance(transform.position, destination) >= checkDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(nextMoveTime);
        }
    }
}
