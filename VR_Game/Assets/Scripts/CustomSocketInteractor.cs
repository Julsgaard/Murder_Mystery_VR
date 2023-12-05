using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomSocketInteractor : XRSocketInteractor
{
    /*protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        ScaleObject(args.interactableObject.transform, 0.5f); 
    }*/

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        ResetObjectScale(args.interactableObject.transform);
    }

    /*private void ScaleObject(Transform objectTransform, float scaleFactor)
    {
        Debug.Log("Scaling Object: " + objectTransform.name);
        Vector3 newScale = objectTransform.localScale * scaleFactor;
        if (newScale.x > 0.1f && newScale.y > 0.1f && newScale.z > 0.1f)
        {
            objectTransform.localScale = newScale;
        }
    }*/

    private void ResetObjectScale(Transform objectTransform)
    {
        var objectInfo = objectTransform.GetComponent<ObjectInfo>();
        if (objectInfo != null)
        {
            //Debug.Log("Resetting scale for: " + objectTransform.name);
            objectTransform.localScale = objectInfo.originalScale;
        }
        else
        {
            Debug.Log("ObjectInfo not found for: " + objectTransform.name);
        }
    }

}