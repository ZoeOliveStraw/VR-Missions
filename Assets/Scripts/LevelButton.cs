using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    private LevelInfo myLevelInfo;
    [SerializeField] private TextMeshProUGUI text;
    private LevelSelectMenu menu;

    public void Initialize(LevelInfo _info, LevelSelectMenu _menu)
    {
        myLevelInfo = _info;
        text.text = myLevelInfo.name;
        menu = _menu;
    }

    public void ButtonPushed()
    {
        menu.UpdateCurrentLevel(myLevelInfo);
    }
}
