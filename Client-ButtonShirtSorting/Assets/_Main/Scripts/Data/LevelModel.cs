using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelModel", menuName = "Models/LevelModel")]
public class LevelModel : ScriptableObject
{
    public int level;
    public int timeCountdown = 99;
    public int buttonCount = 5;

    public bool isRandom = false; //random or fixed

    [Header("Fixed")]
    public List<Vector2> buttonsPos;
    public List<Vector2> slotsPos;

    public List<Color> colors = new()
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.cyan
    };

}
