using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EmotionType { BAD, GOOD }

public enum EmotionScale { ONE = 1, TWO = 3, THREE = 5, FOUR = 10, FIVE = 20 }


public class EmotionObject : MonoBehaviour
{
    public EmotionType emotionType = EmotionType.BAD;

    public EmotionScale emotionScale = EmotionScale.ONE;

    public int spawnerIdx = 0;

    [SerializeField]
    private bool isFinal = false;

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
