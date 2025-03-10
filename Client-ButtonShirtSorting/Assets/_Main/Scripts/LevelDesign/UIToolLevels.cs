using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIToolLevels : MonoBehaviour
{

    public LevelModel lvModel;
    public List<LevelModel> levels = new List<LevelModel>();

    [Header("HUD")]
    [SerializeField] private TMP_Dropdown ddLevel;
    [SerializeField] private Toggle togIsRandom;
    [SerializeField] private TMP_InputField inF_Remain;
    [SerializeField] private TMP_InputField inF_TimeCD;

    [SerializeField] private UIButton btnAddNew;
    [SerializeField] private UIButton btnAutoGen;
    [SerializeField] private UIButton btnSave;
    [SerializeField] private UIButton btnAddPoint;
    [SerializeField] private UIButton btnRmvPoint;
    [SerializeField] private UIButton btnPlay;


    [Header("Prefabs Ref")]
    [SerializeField] private ShirtSlot prefab_ShirtSlot;
    [SerializeField] private ButtonCtrl prefab_Button;

    [Header("ReadOnly")]
    [SerializeField] private SpriteRenderer spawnArea_Button;
    [SerializeField] private SpriteRenderer spawnArea_Slot;
    [SerializeField] private List<ButtonCtrl> buttonCtrls;
    [SerializeField] private List<ShirtSlot> shirtSlots;
    [SerializeField] private int buttonRemain;
    [SerializeField] private List<Color> colors;
    private int lvCurrent => lvModel.level;
    private void Start()
    {
        //Load data
        levels = Resources.LoadAll<LevelModel>("Levels").ToList();
        ddLevel.ClearOptions();
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        foreach (var k in levels)
        {
            options.Add(new TMP_Dropdown.OptionData(k.name));
        }
        ddLevel.AddOptions(options);


        //Event
        btnAddNew.SetUpEvent(AddNewLevel);
        btnAutoGen.SetUpEvent(AutoGenLevel);
        btnPlay.SetUpEvent(PlayLevel);
        btnSave.SetUpEvent(SaveLevel);
        btnAddPoint.SetUpEvent(AddPoint);
        btnRmvPoint.SetUpEvent(RemovePoint);

        ddLevel.onValueChanged.AddListener(x => LoadLevel(x));
        togIsRandom.onValueChanged.AddListener(x => ChangeRandom(x));
        inF_Remain.onValueChanged.AddListener(x => ChangeRemain(int.Parse(x)));
        inF_TimeCD.onValueChanged.AddListener(x => ChangeTimeCD(int.Parse(x)));

        ddLevel.value = 0;
        LoadLevel(0);
    }

    public void ChangeRandom(bool _isRandom)
    {
        lvModel.isRandom = _isRandom;
    }

    public void ChangeRemain(int _remain)
    {
        lvModel.buttonCount = _remain;
    }

    public void ChangeTimeCD(int _time)
    {
        lvModel.timeCountdown = _time;
    }

    public void Refresh()
    {
        ddLevel.ClearOptions();
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        foreach (var k in levels)
        {
            options.Add(new TMP_Dropdown.OptionData(k.name));
        }

        options.Sort();
        ddLevel.AddOptions(options);

    }


    public void LoadLevel(int _lv)
    {

        lvModel = levels[_lv];
        togIsRandom.isOn = lvModel.isRandom;
        inF_Remain.text = lvModel.buttonCount.ToString("00");
        inF_TimeCD.text = lvModel.timeCountdown.ToString("00");
        if (!lvModel.isRandom)
        {
            //Despawns
            foreach (var k in buttonCtrls)
                SimplePool.Despawn(k.gameObject);

            foreach (var k in shirtSlots)
                SimplePool.Despawn(k.gameObject);

            spawns_ButtonPos.Clear();
            spawns_SlotPos.Clear();
            buttonCtrls.Clear();
            shirtSlots.Clear();

            //Spawns
            buttonRemain = lvModel.buttonCount;
            for (int i = 0; i < lvModel.buttonCount; i++)
            {
                ShirtSlot slot = SimplePool.Spawn(prefab_ShirtSlot, lvModel.slotsPos[i], Quaternion.identity);
                slot.SetColor(lvModel.colors[i]);
                slot.transform.SetParent(spawnArea_Slot.transform);
                shirtSlots.Add(slot);

                ButtonCtrl button = SimplePool.Spawn(prefab_Button, lvModel.buttonsPos[i], Quaternion.identity);
                button.SetColor(lvModel.colors[i]);
                button.transform.SetParent(spawnArea_Button.transform);
                buttonCtrls.Add(button);
            }
        }
        else
        {
            AutoGenLevel();
        }

    }


    public void AddNewLevel()
    {
        int _lv = levels.Count + 1;
        LevelModel newLevel = new LevelModel().CreateNewLevel(_lv);
        newLevel.colors = colors;
        levels.Add(newLevel);
        lvModel = newLevel;


        Refresh();

        ddLevel.value = _lv - 1;

    }

    public void AutoGenLevel()
    {
        //Despawns
        foreach (var k in buttonCtrls)
        {
            SimplePool.Despawn(k.gameObject);
        }

        foreach (var k in shirtSlots)
        {
            SimplePool.Despawn(k.gameObject);
        }
        spawns_ButtonPos.Clear();
        spawns_SlotPos.Clear();
        buttonCtrls.Clear();
        shirtSlots.Clear();

        //Spawns
        buttonRemain = lvModel.buttonCount;

        for (int i = 0; i < buttonRemain; i++)
        {
            Color color = lvModel.GetRandomColor();
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

    public void AddPoint()
    {
        buttonRemain++;
        inF_Remain.text = buttonRemain.ToString("00");

        Color color = lvModel.GetRandomColor();
        ShirtSlot slot = SimplePool.Spawn(prefab_ShirtSlot, GetRandomPositionInSprite(ETypeObject.Slot), Quaternion.identity);
        slot.SetColor(color);
        slot.transform.SetParent(spawnArea_Slot.transform);
        shirtSlots.Add(slot);

        ButtonCtrl button = SimplePool.Spawn(prefab_Button, GetRandomPositionInSprite(ETypeObject.Button), Quaternion.identity);
        button.SetColor(color);
        button.transform.SetParent(spawnArea_Button.transform);
        buttonCtrls.Add(button);
    }

    public void RemovePoint()
    {
        if (buttonRemain <= 0)
            return;

        buttonRemain--;
        inF_Remain.text = buttonRemain.ToString("00");

        ShirtSlot slot = shirtSlots[buttonRemain];
        ButtonCtrl button = buttonCtrls[buttonRemain];

        shirtSlots.Remove(slot);
        buttonCtrls.Remove(button);

        SimplePool.Despawn(slot.gameObject);
        SimplePool.Despawn(button.gameObject);
    }

    public void SaveLevel()
    {
        lvModel.timeCountdown = int.Parse(inF_TimeCD.text);
        lvModel.buttonCount = int.Parse(inF_Remain.text);
        lvModel.isRandom = togIsRandom.isOn;

        if (!lvModel.isRandom)
        {
            lvModel.slotsPos.Clear();
            foreach (var slot in shirtSlots)
            {
                lvModel.slotsPos.Add(slot.transform.position);
            }

            lvModel.buttonsPos.Clear();
            foreach (var button in buttonCtrls)
            {
                lvModel.buttonsPos.Add(button.transform.position);
            }
        }


        lvModel.SetDirty();


        Debug.LogError(string.Format("Save level {0} done", lvCurrent));
    }

    public void PlayLevel()
    {
        Module.crLevel = lvCurrent;
        Module.GameMode = EGameMode.Level;
        Module.LoadScene("MainGame");
    }

    #region coppy random in gameplayCtrl
    float spacing = 1f;
    List<Vector3> spawns_ButtonPos = new List<Vector3>();
    List<Vector3> spawns_SlotPos = new List<Vector3>();
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

    [ContextMenu("Gen100Level")]
    public void Gen100Level()
    {
        int lvmin = 21;
        int lvMax = 100;
        for(int i = lvmin; i <= lvMax; i++)
        {
            LevelModel newLevel = new LevelModel().CreateNewLevel(i);
            newLevel.colors = colors;

        }
    }
}
