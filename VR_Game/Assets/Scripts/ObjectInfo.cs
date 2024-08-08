using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    public Vector3 originalScale;

    void Start()
    {
        // Store the original scale at the start
        originalScale = transform.localScale;
    }
}


