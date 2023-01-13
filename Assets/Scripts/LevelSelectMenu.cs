using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
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

    private void PopulateLevelListView()
    {
        for (int i = 1; i <= 10; i++)
        {
            GameObject currentItem = Instantiate(levelItemPrefab);
            currentItem.transform.SetParent(listViewContents.transform);
        }
    }
}
