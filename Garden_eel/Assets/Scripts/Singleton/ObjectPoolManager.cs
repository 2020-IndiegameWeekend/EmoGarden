using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public EmotionObject emotionObjectPrefab;

    private Stack<EmotionObject> _stack_BadEmotionObejct = new Stack<EmotionObject>();

    private Stack<EmotionObject> _stack_GoodOneEmotionObejct = new Stack<EmotionObject>();
    private Stack<EmotionObject> _stack_GoodTwoEmotionObejct = new Stack<EmotionObject>();
    private Stack<EmotionObject> _stack_GoodThreeEmotionObejct = new Stack<EmotionObject>();
    private Stack<EmotionObject> _stack_GoodFourEmotionObejct = new Stack<EmotionObject>();
    private Stack<EmotionObject> _stack_GoodFiveEmotionObejct = new Stack<EmotionObject>();

    [SerializeField]
    private GameObject goodEmotionObjectParent;
    [SerializeField]
    private GameObject badEmotionObjectParent;

    void Start()
    {
        
    }

    public EmotionObject GetGoodEmotionObject(EmotionScale scale)
    {
        EmotionObject emotionObject = null;

        int len = 0;

        switch (scale)
        {
            case EmotionScale.ONE:
                len = _stack_GoodOneEmotionObejct.Count;

                if(len == 0)
                {
                    MakeGoodEmotionObject(1, EmotionScale.ONE);
                }

                emotionObject = _stack_GoodOneEmotionObejct.Pop();
                break;
            case EmotionScale.TWO:
                len = _stack_GoodTwoEmotionObejct.Count;

                if (len == 0)
                {
                    MakeGoodEmotionObject(1, EmotionScale.TWO);
                }

                emotionObject = _stack_GoodOneEmotionObejct.Pop();
                break;
            case EmotionScale.THREE:
                len = _stack_GoodThreeEmotionObejct.Count;

                if (len == 0)
                {
                    MakeGoodEmotionObject(1, EmotionScale.THREE);
                }

                emotionObject = _stack_GoodOneEmotionObejct.Pop();
                break;
            case EmotionScale.FOUR:
                len = _stack_GoodFourEmotionObejct.Count;

                if (len == 0)
                {
                    MakeGoodEmotionObject(1, EmotionScale.FOUR);
                }

                emotionObject = _stack_GoodOneEmotionObejct.Pop();
                break;
            case EmotionScale.FIVE:
                len = _stack_GoodFiveEmotionObejct.Count;

                if (len == 0)
                {
                    MakeGoodEmotionObject(1, EmotionScale.FIVE);
                }

                emotionObject = _stack_GoodOneEmotionObejct.Pop();
                break;
        }

        return emotionObject;
    }

    public EmotionObject GetBadEmotionObject()
    {
        EmotionObject emotionObject = null;

        int len = _stack_BadEmotionObejct.Count;

        if(len == 0)
        {
            MakeBadEmotionObject(1);
        }

        emotionObject = _stack_BadEmotionObejct.Pop();

        return emotionObject;
    }

    private void MakeGoodEmotionObject(int count, EmotionScale scale)
    {
        for (int i = 0; i < count; i++)
        {
            EmotionObject newEmotionObject = Instantiate(emotionObjectPrefab);
            newEmotionObject.emotionType = EmotionType.GOOD;

            switch (scale)
            {
                case EmotionScale.ONE:
                    newEmotionObject.emotionScale = EmotionScale.ONE;
                    _stack_GoodOneEmotionObejct.Push(newEmotionObject);
                    break;
                case EmotionScale.TWO:
                    newEmotionObject.emotionScale = EmotionScale.TWO;
                    _stack_GoodTwoEmotionObejct.Push(newEmotionObject);
                    break;
                case EmotionScale.THREE:
                    newEmotionObject.emotionScale = EmotionScale.THREE;
                    _stack_GoodThreeEmotionObejct.Push(newEmotionObject);
                    break;
                case EmotionScale.FOUR:
                    newEmotionObject.emotionScale = EmotionScale.FOUR;
                    _stack_GoodFourEmotionObejct.Push(newEmotionObject);
                    break;
                case EmotionScale.FIVE:
                    newEmotionObject.emotionScale = EmotionScale.FIVE;
                    _stack_GoodFiveEmotionObejct.Push(newEmotionObject);
                    break;
            }

            newEmotionObject.transform.SetParent(goodEmotionObjectParent.transform);
            newEmotionObject.gameObject.SetActive(false);
        }
    }

    private void MakeBadEmotionObject(int count)
    {
        for (int i = 0; i < count; i++)
        {
            EmotionObject newEmotionObject = Instantiate(emotionObjectPrefab);
            newEmotionObject.emotionType = EmotionType.BAD;
            _stack_BadEmotionObejct.Push(newEmotionObject);
            newEmotionObject.transform.SetParent(badEmotionObjectParent.transform);
            newEmotionObject.gameObject.SetActive(false);
        }
    }

    public void ReturnEmotionObject(EmotionObject emotionObject)
    {
        switch (emotionObject.emotionType)
        {
            case EmotionType.GOOD:

                switch (emotionObject.emotionScale)
                {
                    case EmotionScale.ONE:
                        _stack_GoodOneEmotionObejct.Push(emotionObject);
                        break;
                    case EmotionScale.TWO:
                        _stack_GoodTwoEmotionObejct.Push(emotionObject);
                        break;
                    case EmotionScale.THREE:
                        _stack_GoodThreeEmotionObejct.Push(emotionObject);
                        break;
                    case EmotionScale.FOUR:
                        _stack_GoodFourEmotionObejct.Push(emotionObject);
                        break;
                    case EmotionScale.FIVE:
                        _stack_GoodFiveEmotionObejct.Push(emotionObject);
                        break;
                }

                if (emotionObject.gameObject.activeSelf)
                    emotionObject.gameObject.SetActive(false);

                break;
            case EmotionType.BAD:

                _stack_BadEmotionObejct.Push(emotionObject);

                if (emotionObject.gameObject.activeSelf)
                    emotionObject.gameObject.SetActive(false);

                break;
        }
    } 
}
