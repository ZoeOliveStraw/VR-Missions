using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    [Header("ScrollView variables")]
    [SerializeField] private ScrollRect listView;
    [SerializeField] private GameObject listViewContents;
    [SerializeField] private GameObject levelItemPrefab;

    [Header("Level details variables")] 
    [SerializeField] private TextMeshProUGUI currentLevelName;
    [SerializeField] private TextMeshProUGUI currentLevelDescription;

    private LevelInfo[] levels;

    // Start is called before the first frame update
    void Start()
    {
        GetLevelInfo();
        PopulateLevelListView();
    }

    //This method will populate the ListView contents with items to represent the levels in the game
    private void PopulateLevelListView()
    {
        foreach(LevelInfo level in levels)
        {
            //Instantiate and configure transform of menu item
            GameObject currentItem = Instantiate(levelItemPrefab);
            currentItem.transform.SetParent(listViewContents.transform);
            currentItem.transform.localScale = Vector3.one;
            currentItem.transform.localPosition = Vector3.zero;
            currentItem.transform.rotation = listViewContents.transform.rotation;
            
            //Load level name into Text Mesh Pro
            LevelButton button = currentItem.GetComponent<LevelButton>();
            button.Initialize(level, this);
        }
    }

    private void GetLevelInfo()
    {
        levels = Resources.LoadAll<LevelInfo>("Levels");
        UpdateCurrentLevel(levels[0]);
    }

    public void UpdateCurrentLevel(LevelInfo newLevelInfo)
    {
        currentLevelDescription.text = newLevelInfo.description;
        currentLevelName.text = newLevelInfo.name;
    }

    public void LoadLevelButtonPushed()
    {
        Debug.Log($"Attempting to load level {currentLevelName.text}");
        GameManager.instance.LoadSceneByName(currentLevelName.text);
    }
}
