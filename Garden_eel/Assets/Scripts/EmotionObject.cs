using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EmotionType { BAD, GOOD }

public enum EmotionScale { ONE = 1, TWO = 3, THREE = 5, FOUR = 10, FIVE = 20 }


public class EmotionObject : MonoBehaviour
{
    public float[] badScale = new float[5] { 0.3f, 0.5f, 0.7f, 0.9f, 1.1f };
    public float goodScale = 1f;

    public EmotionType emotionType = EmotionType.BAD;

    public EmotionScale emotionScale = EmotionScale.ONE;

    public int spawnerIdx = 0;

    [SerializeField]
    private bool isFinal = false;

    private void OnEnable()
    {
        switch (emotionType)
        {
            case EmotionType.BAD:
                if (!isFinal)
                    transform.localScale = new Vector3(badScale[(int)emotionScale], badScale[(int)emotionScale], 1);
                break;
            case EmotionType.GOOD:
                transform.localScale = new Vector3(goodScale, goodScale, 1);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isFinal)
            {
                InGameManager.instance.GameOver(true);
            }

            else
            {
                switch (emotionType)
                {
                    case EmotionType.BAD:
                        switch (spawnerIdx)
                        {
                            case 0:
                                ObjectSpawnerManager.instance.firstEmotionSpawner.SubCurCount();
                                break;
                            case 1:
                                ObjectSpawnerManager.instance.secondEmotionSpawner.SubCurCount();
                                break;
                            case 2:
                                ObjectSpawnerManager.instance.thirdEmotionSpawner.SubCurCount();
                                break;
                            case 3:
                                ObjectSpawnerManager.instance.fourthEmotionSpawner.SubCurCount();
                                break;
                        }

                        switch (emotionScale)
                        {
                            case EmotionScale.ONE:
                                InGameManager.instance.AddScore(30);
                                break;
                            case EmotionScale.TWO:
                                InGameManager.instance.AddScore(50);
                                break;
                            case EmotionScale.THREE:
                                InGameManager.instance.AddScore(100);
                                break;
                            case EmotionScale.FOUR:
                                InGameManager.instance.AddScore(200);
                                break;
                            case EmotionScale.FIVE:
                                InGameManager.instance.AddScore(1000);
                                break;
                        }

                        SoundManager.instance.PlayEffectSound("Eat_Effect");
                        InGameManager.instance.AddProgress((int)emotionScale);
                        ObjectPoolManager.instance.ReturnEmotionObject(this);
                        break;
                    case EmotionType.GOOD:
                        InGameManager.instance.GameOver(false);
                        break;
                }
            }
        }
    }
}
