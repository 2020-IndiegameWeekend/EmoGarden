using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public EmotionObject emotionObjectPrefab;

    private Stack<EmotionObject> _stack_BadEmotionObejct;

    private Stack<EmotionObject> _stack_GoodOneEmotionObejct;
    private Stack<EmotionObject> _stack_GoodTwoEmotionObejct;
    private Stack<EmotionObject> _stack_GoodThreeEmotionObejct;
    private Stack<EmotionObject> _stack_GoodFourEmotionObejct;
    private Stack<EmotionObject> _stack_GoodFiveEmotionObejct;

    private GameObject goodEmotionObjectParent;

    private GameObject badEmotionObjectParent;

    void Start()
    {
        
    }

    public EmotionObject GetGoodEmotionObject(EmotionObject.EmotionScale scale)
    {
        EmotionObject emotionObject = null;

        int len = 0;

        switch (scale)
        {
            case EmotionObject.EmotionScale.ONE:
                len = _stack_GoodOneEmotionObejct.Count;

                if(len == 0)
                {
                    MakeGoodEmotionObject(1, EmotionObject.EmotionScale.ONE);
                }

                emotionObject = _stack_GoodOneEmotionObejct.Peek();
                break;
            case EmotionObject.EmotionScale.TWO:
                len = _stack_GoodTwoEmotionObejct.Count;

                if (len == 0)
                {
                    MakeGoodEmotionObject(1, EmotionObject.EmotionScale.TWO);
                }

                emotionObject = _stack_GoodOneEmotionObejct.Peek();
                break;
            case EmotionObject.EmotionScale.THREE:
                len = _stack_GoodThreeEmotionObejct.Count;

                if (len == 0)
                {
                    MakeGoodEmotionObject(1, EmotionObject.EmotionScale.THREE);
                }

                emotionObject = _stack_GoodOneEmotionObejct.Peek();
                break;
            case EmotionObject.EmotionScale.FOUR:
                len = _stack_GoodFourEmotionObejct.Count;

                if (len == 0)
                {
                    MakeGoodEmotionObject(1, EmotionObject.EmotionScale.FOUR);
                }

                emotionObject = _stack_GoodOneEmotionObejct.Peek();
                break;
            case EmotionObject.EmotionScale.FIVE:
                len = _stack_GoodFiveEmotionObejct.Count;

                if (len == 0)
                {
                    MakeGoodEmotionObject(1, EmotionObject.EmotionScale.FIVE);
                }

                emotionObject = _stack_GoodOneEmotionObejct.Peek();
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

        emotionObject = _stack_BadEmotionObejct.Peek();

        return emotionObject;
    }

    private void MakeGoodEmotionObject(int count, EmotionObject.EmotionScale scale)
    {
        for (int i = 0; i < count; i++)
        {
            EmotionObject newEmotionObject = Instantiate(emotionObjectPrefab);
            newEmotionObject.emotionType = EmotionObject.EmotionType.GOOD;

            switch (scale)
            {
                case EmotionObject.EmotionScale.ONE:
                    newEmotionObject.emotionScale = EmotionObject.EmotionScale.ONE;
                    _stack_GoodOneEmotionObejct.Push(newEmotionObject);
                    break;
                case EmotionObject.EmotionScale.TWO:
                    newEmotionObject.emotionScale = EmotionObject.EmotionScale.TWO;
                    _stack_GoodTwoEmotionObejct.Push(newEmotionObject);
                    break;
                case EmotionObject.EmotionScale.THREE:
                    newEmotionObject.emotionScale = EmotionObject.EmotionScale.THREE;
                    _stack_GoodThreeEmotionObejct.Push(newEmotionObject);
                    break;
                case EmotionObject.EmotionScale.FOUR:
                    newEmotionObject.emotionScale = EmotionObject.EmotionScale.FOUR;
                    _stack_GoodFourEmotionObejct.Push(newEmotionObject);
                    break;
                case EmotionObject.EmotionScale.FIVE:
                    newEmotionObject.emotionScale = EmotionObject.EmotionScale.FIVE;
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
            newEmotionObject.emotionType = EmotionObject.EmotionType.BAD;
            _stack_BadEmotionObejct.Push(newEmotionObject);
            newEmotionObject.transform.SetParent(badEmotionObjectParent.transform);
            newEmotionObject.gameObject.SetActive(false);
        }
    }

    public void ReturnEmotionObject(EmotionObject emotionObject)
    {
        switch (emotionObject.emotionType)
        {
            case EmotionObject.EmotionType.GOOD:

                switch (emotionObject.emotionScale)
                {
                    case EmotionObject.EmotionScale.ONE:
                        _stack_GoodOneEmotionObejct.Push(emotionObject);
                        break;
                    case EmotionObject.EmotionScale.TWO:
                        _stack_GoodTwoEmotionObejct.Push(emotionObject);
                        break;
                    case EmotionObject.EmotionScale.THREE:
                        _stack_GoodThreeEmotionObejct.Push(emotionObject);
                        break;
                    case EmotionObject.EmotionScale.FOUR:
                        _stack_GoodFourEmotionObejct.Push(emotionObject);
                        break;
                    case EmotionObject.EmotionScale.FIVE:
                        _stack_GoodFiveEmotionObejct.Push(emotionObject);
                        break;
                }

                if (emotionObject.gameObject.activeSelf)
                    emotionObject.gameObject.SetActive(false);

                break;
            case EmotionObject.EmotionType.BAD:

                _stack_BadEmotionObejct.Push(emotionObject);

                if (emotionObject.gameObject.activeSelf)
                    emotionObject.gameObject.SetActive(false);

                break;
        }
    } 
}
