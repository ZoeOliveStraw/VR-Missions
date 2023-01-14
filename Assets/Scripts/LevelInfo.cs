using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Level Info")]
public class LevelInfo : ScriptableObject
{
    [SerializeField] public string sceneName;
    [SerializeField] public string description;
    [SerializeField] public Image screenshot;
}
