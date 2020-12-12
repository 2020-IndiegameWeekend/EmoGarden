using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public EmotionObject badEmotionObjectPrefab;
    public EmotionObject goodEmotionObjectPrefab;

    private Stack<EmotionObject> _stack_GoodEmotionObejct;

    private Stack<EmotionObject> _stack_BadOneEmotionObejct;
    private Stack<EmotionObject> _stack_BadTwoEmotionObejct;
    private Stack<EmotionObject> _stack_BadThreeEmotionObejct;
    private Stack<EmotionObject> _stack_BadFourEmotionObejct;
    private Stack<EmotionObject> _stack_BadFiveEmotionObejct;

    [SerializeField]
    private GameObject goodEmotionObjectParent;
    [SerializeField]
    private GameObject badEmotionObjectParent;

    protected override void OnAwake()
    {
        base.OnAwake();

        _stack_GoodEmotionObejct = new Stack<EmotionObject>();
        _stack_BadOneEmotionObejct = new Stack<EmotionObject>();
        _stack_BadTwoEmotionObejct = new Stack<EmotionObject>();
        _stack_BadThreeEmotionObejct = new Stack<EmotionObject>();
        _stack_BadFourEmotionObejct = new Stack<EmotionObject>();
        _stack_BadFiveEmotionObejct = new Stack<EmotionObject>();
    }

    public EmotionObject GetBadEmotionObject(EmotionScale scale, int idx)
    {
        EmotionObject emotionObject = null;

        int len = 0;

        switch (scale)
        {
            case EmotionScale.ONE:
                len = _stack_BadOneEmotionObejct.Count;

                if(len == 0)
                {
                    MakeBadEmotionObject(1, EmotionScale.ONE);
                }

                emotionObject = _stack_BadOneEmotionObejct.Pop();
                break;
            case EmotionScale.TWO:
                len = _stack_BadTwoEmotionObejct.Count;

                if (len == 0)
                {
                    MakeBadEmotionObject(1, EmotionScale.TWO);
                }

                emotionObject = _stack_BadTwoEmotionObejct.Pop();
                break;
            case EmotionScale.THREE:
                len = _stack_BadThreeEmotionObejct.Count;

                if (len == 0)
                {
                    MakeBadEmotionObject(1, EmotionScale.THREE);
                }

                emotionObject = _stack_BadThreeEmotionObejct.Pop();
                break;
            case EmotionScale.FOUR:
                len = _stack_BadFourEmotionObejct.Count;

                if (len == 0)
                {
                    MakeBadEmotionObject(1, EmotionScale.FOUR);
                }

                emotionObject = _stack_BadFourEmotionObejct.Pop();
                break;
            case EmotionScale.FIVE:
                len = _stack_BadFiveEmotionObejct.Count;

                if (len == 0)
                {
                    MakeBadEmotionObject(1, EmotionScale.FIVE);
                }

                emotionObject = _stack_BadFiveEmotionObejct.Pop();
                break;
        }

        return emotionObject;
    }

    public EmotionObject GetGoodEmotionObject()
    {
        EmotionObject emotionObject = null;

        int len = _stack_GoodEmotionObejct.Count;

        if(len == 0)
        {
            MakeGoodEmotionObject(1);
        }

        emotionObject = _stack_GoodEmotionObejct.Pop();

        return emotionObject;
    }

    private void MakeBadEmotionObject(int count, EmotionScale scale, int idx = 0)
    {
        for (int i = 0; i < count; i++)
        {
            EmotionObject newEmotionObject = Instantiate(badEmotionObjectPrefab);
            newEmotionObject.emotionType = EmotionType.BAD;
            newEmotionObject.spawnerIdx = idx;

            switch (scale)
            {
                case EmotionScale.ONE:
                    newEmotionObject.emotionScale = EmotionScale.ONE;
                    _stack_BadOneEmotionObejct.Push(newEmotionObject);
                    break;
                case EmotionScale.TWO:
                    newEmotionObject.emotionScale = EmotionScale.TWO;
                    _stack_BadTwoEmotionObejct.Push(newEmotionObject);
                    break;
                case EmotionScale.THREE:
                    newEmotionObject.emotionScale = EmotionScale.THREE;
                    _stack_BadThreeEmotionObejct.Push(newEmotionObject);
                    break;
                case EmotionScale.FOUR:
                    newEmotionObject.emotionScale = EmotionScale.FOUR;
                    _stack_BadFourEmotionObejct.Push(newEmotionObject);
                    break;
                case EmotionScale.FIVE:
                    newEmotionObject.emotionScale = EmotionScale.FIVE;
                    _stack_BadFiveEmotionObejct.Push(newEmotionObject);
                    break;
            }

            newEmotionObject.transform.SetParent(goodEmotionObjectParent.transform);
            newEmotionObject.gameObject.SetActive(false);
        }
    }

    private void MakeGoodEmotionObject(int count)
    {
        for (int i = 0; i < count; i++)
        {
            EmotionObject newEmotionObject = Instantiate(badEmotionObjectPrefab);
            newEmotionObject.emotionType = EmotionType.GOOD;
            _stack_GoodEmotionObejct.Push(newEmotionObject);
            newEmotionObject.transform.SetParent(badEmotionObjectParent.transform);
            newEmotionObject.gameObject.SetActive(false);
        }
    }

    public void ReturnEmotionObject(EmotionObject emotionObject)
    {
        switch (emotionObject.emotionType)
        {
            case EmotionType.BAD:

                switch (emotionObject.emotionScale)
                {
                    case EmotionScale.ONE:
                        _stack_BadOneEmotionObejct.Push(emotionObject);
                        break;
                    case EmotionScale.TWO:
                        _stack_BadTwoEmotionObejct.Push(emotionObject);
                        break;
                    case EmotionScale.THREE:
                        _stack_BadThreeEmotionObejct.Push(emotionObject);
                        break;
                    case EmotionScale.FOUR:
                        _stack_BadFourEmotionObejct.Push(emotionObject);
                        break;
                    case EmotionScale.FIVE:
                        _stack_BadFiveEmotionObejct.Push(emotionObject);
                        break;
                }

                if (emotionObject.gameObject.activeSelf)
                    emotionObject.gameObject.SetActive(false);

                break;
            case EmotionType.GOOD:

                _stack_GoodEmotionObejct.Push(emotionObject);

                if (emotionObject.gameObject.activeSelf)
                    emotionObject.gameObject.SetActive(false);

                break;
        }
    } 
}
