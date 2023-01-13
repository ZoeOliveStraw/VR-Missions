using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    [Header("ScrollView variables")]
    [SerializeField] private ScrollRect listView;
    [SerializeField] private GameObject listViewContents;
    [SerializeField] private GameObject levelItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PopulateLevelListView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //This method will populate the ListView contents with items to represent the levels in the game
    private void PopulateLevelListView()
    {
        for (int i = 1; i <= 10; i++)
        {
            GameObject currentItem = Instantiate(levelItemPrefab);
            currentItem.transform.SetParent(listViewContents.transform);
            currentItem.transform.localScale = Vector3.one;
            currentItem.transform.localPosition = Vector3.zero;
            currentItem.transform.rotation = listViewContents.transform.rotation;
        }
    }

    private void GetLevelNames()
    {
        
    }

    public void LoadLevelButtonPushed()
    {
        
    }
}
