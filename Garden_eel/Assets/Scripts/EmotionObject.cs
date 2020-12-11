using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EmotionType { GOOD, BAD }

public enum EmotionScale { ONE, TWO, THREE, FOUR, FIVE }


public class EmotionObject : MonoBehaviour
{

    public EmotionType emotionType = EmotionType.GOOD;

    public EmotionScale emotionScale = EmotionScale.ONE;


}
