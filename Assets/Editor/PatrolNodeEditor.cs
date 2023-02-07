using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PatrolNode))]
public class PatrolNodeEditor : UnityEditor.Editor
{
    private PatrolNode targetNode;
    protected void OnSceneGUI()
    {
        targetNode = (PatrolNode) target;
        Vector3 spawnPosition = targetNode.transform.position + new Vector3(0.5f,0.5f,0);
        Vector3 deletePosition = targetNode.transform.position + new Vector3(-0.5f,0.5f,0);
        float size = 0.3f;
        float pickSize = size * 2f;
    
        if (Handles.Button(spawnPosition, Quaternion.identity, size, pickSize, Handles.CubeHandleCap))
        {
            SpawnNewNode();
        }
        
        if (Handles.Button(deletePosition, Quaternion.identity, size, pickSize, Handles.SphereHandleCap))
        {
            DeleteNode();
        }
    }
    
    private void SpawnNewNode()
    {
        PatrolNode newNode = Instantiate(targetNode.patrolNodePrefab, targetNode.transform.position, Quaternion.identity).GetComponent<PatrolNode>();
    
        newNode.ConfigureNode(targetNode); 
    
        Selection.activeGameObject = newNode.gameObject;
    }
    
    private void DeleteNode()
    {
        targetNode.DeleteNode();
        DestroyImmediate(targetNode.gameObject);
    }
}

