using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneList : MonoBehaviour
{
    //  Creates a list of bones from the children of current gameobject
    public List<Transform> Bones;
    // Start is called before the first frame update
    void Awake()
    {
        // initialise bones list
        Bones = new List<Transform>();
        // get children recursively from 
        GetRecursiveChildren(transform);
    }

    private void GetRecursiveChildren(Transform parenttransform)
    {
        // get children one level down
        foreach (Transform child in parenttransform)
        {
            // add children to list
            Bones.Add(child.transform);
            if (child.transform.childCount > 0)
            {
                // recurse in children
                GetRecursiveChildren(child);
            }
        }
    }
}
