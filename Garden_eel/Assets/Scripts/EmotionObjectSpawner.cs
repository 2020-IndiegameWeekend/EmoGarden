using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionObjectSpawner : MonoBehaviour
{
    private BoxCollider2D _boxCollider2d;

    [SerializeField]
    private int _maxEmotionObjectCount;
    private int _curEmotionObjectCount;

    [SerializeField]
    private int _spawnerIdx = 0;

    [SerializeField]
    private int[] spawnPercents = new int[6];

    [SerializeField]
    private float spawnTime = 1f;

    [SerializeField]
    private EmotionScale emotionScale;

    private void Start()
    {
        _boxCollider2d = GetComponent<BoxCollider2D>();

        StartCoroutine(SpawnCoroutine());
    }

    public void SubCurCount()
    {
        _curEmotionObjectCount--;
    }

    private void SpawnEmotionObject()
    {
        Vector2 min = _boxCollider2d.bounds.min;
        Vector2 max = _boxCollider2d.bounds.max;

        Vector2 randomVec = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

        int sum = 0;
        int idx = 0;
        int len = spawnPercents.Length;

        for(int i = 0; i < len; i++)
        {
            sum += spawnPercents[i];
        }

        int randomIdx = Random.Range(0, sum);

        for (int i = 0; i < len; i++)
        {
            if(randomIdx < spawnPercents[i])
            {
                idx = i;
                break;
            }

            randomIdx -= spawnPercents[i];
        }

        EmotionObject eo = null;

        switch (idx)
        {
            case 0:
                eo = ObjectPoolManager.instance.GetBadEmotionObject(EmotionScale.ONE, _spawnerIdx);
                break;
            case 1:
                eo = ObjectPoolManager.instance.GetBadEmotionObject(EmotionScale.TWO, _spawnerIdx);
                break;
            case 2:
                eo = ObjectPoolManager.instance.GetBadEmotionObject(EmotionScale.THREE, _spawnerIdx);
                break;
            case 3:
                eo = ObjectPoolManager.instance.GetBadEmotionObject(EmotionScale.FOUR, _spawnerIdx);
                break;
            case 4:
                eo = ObjectPoolManager.instance.GetBadEmotionObject(EmotionScale.FIVE, _spawnerIdx);
                break;
            case 5:
                eo = ObjectPoolManager.instance.GetGoodEmotionObject();
                break;
            default:
                Debug.LogError("Idx 오류 : " + idx);
                return;
        }


        eo.transform.position = randomVec;
        eo.gameObject.SetActive(true);
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            if(_curEmotionObjectCount <= _maxEmotionObjectCount)
            {
                _curEmotionObjectCount++;
                SpawnEmotionObject();
                yield return new WaitForSeconds(spawnTime);

            }
        }
    }
}
