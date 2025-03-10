using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using System.IO;
#endif



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

    public Color GetRandomColor()
    {
        return colors[Random.Range(0,colors.Count)];
    }



    public LevelModel CreateNewLevel(int _level)
    {
        LevelModel newLevel = ScriptableObject.CreateInstance<LevelModel>();
        newLevel.level = _level;
        newLevel.timeCountdown = 99;
        newLevel.buttonCount = Random.Range(5, 8) + _level / 5;
        newLevel.isRandom = true;

        string path = "Assets/_Main/Resources/Levels/Lv" + _level + ".asset";

#if UNITY_EDITOR
        if (!Directory.Exists("Assets/_Main/Resources/Levels"))
        {
            Directory.CreateDirectory("Assets/_Main/Resources/Levels");
        }

        UnityEditor.AssetDatabase.CreateAsset(newLevel, path);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
#endif

        return newLevel;
    }
}
