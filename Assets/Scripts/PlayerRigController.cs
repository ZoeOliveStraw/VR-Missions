using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigController : MonoBehaviour
{
    [SerializeField] private GameObject lhGrabObject;
    [SerializeField] private GameObject lhRayObject;
    
    [SerializeField] private GameObject rhGrabObject;
    [SerializeField] private GameObject rhRayObject;


    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchRayAndGrab(bool paused)
    {
        paused = !paused;
        
        lhGrabObject.SetActive(!paused);
        rhGrabObject.SetActive(!paused);
        
        lhRayObject.SetActive(paused);
        rhRayObject.SetActive(paused);
    }
}
