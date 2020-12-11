using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionObjectSpawner : MonoBehaviour
{
    private BoxCollider2D _boxCollider2d;

    [SerializeField]
    private float spawnTime = 1f;

    [SerializeField]
    private EmotionScale emotionScale;

    private void Start()
    {
        _boxCollider2d = GetComponent<BoxCollider2D>();

        StartCoroutine(SpawnCoroutine());
    }

    private void SpawnEmotionObject()
    {
        Vector2 min = _boxCollider2d.bounds.min;
        Vector2 max = _boxCollider2d.bounds.max;

        Vector2 randomVec = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

        EmotionObject eo = ObjectPoolManager.instance.GetGoodEmotionObject(emotionScale);
        eo.transform.position = randomVec;
        eo.gameObject.SetActive(true);
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            SpawnEmotionObject();
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
