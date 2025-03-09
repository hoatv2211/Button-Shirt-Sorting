using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameplayCtrl : Singleton<GameplayCtrl>
{
    [Header("Info level")]
    public int level = 1;
    public LevelModel levelModel;
    public GameObject trGamelevel;
    public int timeRemaining = 100;
    public int buttonRemain = 10;

    [Header("Prefabs Ref")]
    [SerializeField] private ShirtSlot prefab_ShirtSlot;
    [SerializeField] private ButtonCtrl prefab_Button;

    [Header("ReadOnly - Endless")]
    [SerializeField] private SpriteRenderer spawnArea_Button;
    [SerializeField] private SpriteRenderer spawnArea_Slot;
    [SerializeField] private List<ButtonCtrl> buttonCtrls;
    [SerializeField] private List<ShirtSlot> shirtSlots;

    private EGameState state = EGameState.Playing;


    #region Events
    private void OnEnable()
    {
        state = EGameState.Playing;
        switch (Module.GameMode)
        {
            case EGameMode.Level:
                LoadGameLevel();

                if (ctTimeRemain != null)
                    StopCoroutine(ctTimeRemain);
                ctTimeRemain = StartCoroutine(IeTimerCountdown());
                break;
            case EGameMode.Endless:
                level = Module.crLevelEndLess;
                LoadGameEndless();

                timeRemaining = 1000;
                if (ctTimeRemain != null)
                    StopCoroutine(ctTimeRemain);
                ctTimeRemain = StartCoroutine(IeTimerCountdown());
                break;
            default:
                break;
        }
    }

    #endregion

    #region Game - by Level
    public void LoadGameLevel()
    {

    }

    #endregion

    #region Game - by EndLess

    private float spacing = 1f;
    List<Vector3> spawns_ButtonPos = new List<Vector3>();
    List<Vector3> spawns_SlotPos = new List<Vector3>();
    [SerializeField]
    List<Color> colors = new()
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.cyan,
        Color.magenta
    };

    public void LoadGameEndless()
    {
        //Spawn
        buttonRemain = Random.Range(5, 8) + level / 5;
        UIMainGame.Instance.ShowButtonRemain(buttonRemain);
        List<Color> selectedColors = colors.OrderBy(x => Random.value).Take(buttonRemain).ToList();
        for (int i = 0; i < buttonRemain; i++)
        {
            Color color = selectedColors[i];
            //Buttons Spawn
            Vector3 randomPos_Btn;
            int attempt = 0;
            do
            {
                randomPos_Btn = GetRandomPositionInSprite(ETypeObject.Button);
                attempt++;
            } while (IsOverlapping(randomPos_Btn, ETypeObject.Button) && attempt < 50);

            spawns_ButtonPos.Add(randomPos_Btn);
            ButtonCtrl button = SimplePool.Spawn(prefab_Button, randomPos_Btn, Quaternion.identity);
            button.SetColor(color);
            button.transform.parent = spawnArea_Button.transform;
            buttonCtrls.Add(button);


            //Slots Spawn
            Vector3 randomPos_Slot;
            int attempts = 0;
            do
            {
                randomPos_Slot = GetRandomPositionInSprite(ETypeObject.Slot);
                attempts++;
            } while (IsOverlapping(randomPos_Slot, ETypeObject.Slot) && attempts < 50);

            spawns_SlotPos.Add(randomPos_Slot);
            ShirtSlot slot = SimplePool.Spawn(prefab_ShirtSlot, randomPos_Slot, Quaternion.identity);
            slot.SetColor(color);
            slot.transform.parent = spawnArea_Slot.transform;
            shirtSlots.Add(slot);
        }
    }

    private enum ETypeObject
    {
        Slot,
        Button
    }

    Vector3 GetRandomPositionInSprite(ETypeObject type = ETypeObject.Button)
    {
        Bounds bounds = new Bounds();
        float buttonRadius = 0;

        switch (type)
        {
            case ETypeObject.Slot:
                bounds = spawnArea_Slot.bounds;
                buttonRadius = prefab_ShirtSlot.GetComponent<SpriteRenderer>().bounds.extents.x;
                break;
            case ETypeObject.Button:
                bounds = spawnArea_Button.bounds;
                buttonRadius = prefab_Button.GetComponent<SpriteRenderer>().bounds.extents.x;
                break;
        }


        float x = Random.Range(bounds.min.x + buttonRadius, bounds.max.x - buttonRadius);
        float y = Random.Range(bounds.min.y + buttonRadius, bounds.max.y - buttonRadius);

        return new Vector3(x, y, -1);
    }

    bool IsOverlapping(Vector3 position, ETypeObject type = ETypeObject.Button)
    {
        switch (type)
        {
            case ETypeObject.Slot:
                foreach (Vector3 existingPos in spawns_SlotPos)
                {
                    if (Vector3.Distance(existingPos, position) < spacing)
                    {
                        return true;
                    }
                }
                break;
            case ETypeObject.Button:
                foreach (Vector3 existingPos in spawns_ButtonPos)
                {
                    if (Vector3.Distance(existingPos, position) < spacing)
                    {
                        return true;
                    }
                }
                break;
        }


        return false;
    }

    #endregion

    #region Actions
    Coroutine ctTimeRemain;
    IEnumerator IeTimerCountdown()
    {
        UIMainGame.Instance.ShowTimeRemain(timeRemaining);
        while (timeRemaining > 0 && state == EGameState.Playing)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
            UIMainGame.Instance.ShowTimeRemain(timeRemaining);
        }

        if (timeRemaining <= 0)
        {
            UIMainGame.Instance.ShowUIGameOver(false);
        }
    }

    public void RemainChecking(ShirtSlot _slot, ButtonCtrl _btn)
    {
        _slot.HideHintEffect();
        _btn.HideHintEffect();
        shirtSlots.Remove(_slot);
        buttonCtrls.Remove(_btn);

        buttonRemain -= 1;
        UIMainGame.Instance.ShowButtonRemain(buttonRemain);
        if (buttonRemain <= 0)
        {
            //Show Win
            if (ctTimeRemain != null)
                StopCoroutine(ctTimeRemain);

            state = EGameState.GameOver;
            UIMainGame.Instance.ShowUIGameOver(true);
        }
    }

    #endregion

    #region Hint
    public void ShowHint()
    {
        ButtonCtrl btn = buttonCtrls.First(x => x.IsPlaced == false);
        btn.ShowHintEffect();

        ShirtSlot slot = shirtSlots.First(x=>x.id == btn.id);
        slot.ShowHintEffect();
    }

    public void AutoSort()
    {

    }

    #endregion

}
