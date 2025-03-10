using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<LevelModel> levelModels = new List<LevelModel>();

    private void Start()
    {
        levelModels = Resources.LoadAll<LevelModel>("Levels").ToList();
    }

}
