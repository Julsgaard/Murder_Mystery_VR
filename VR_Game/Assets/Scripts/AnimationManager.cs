using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    public NpcInteraction nonVrnpcInteraction, vrNpcInteraction;
    public GameManager gameManager;
    void Start()
    {
        if (gameManager.enableVR)
        {
            //GameObject playerObject = vrPlayerObject;
        }
        else if (gameManager.enableVR == false)
        {
            //GameObject playerObject = nonPlayerObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
