using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchBones : MonoBehaviour
{
    BoneList bonelist;

    // Start is called before the first frame update
    void Awake()
    {
        bonelist = transform.GetChild(1).GetComponent<BoneList>();
    }

    // Copy the transforms from one bone group to another
    public void copy_transforms(List<Transform> target)
    {
        int j = 0;
        // iterate over the bones and copy the transforms
        for (int i = 0; i < bonelist.Bones.Count && j < target.Count; i++)
        {
            while (j < target.Count)
            {
                if (bonelist.Bones[i].name != target[j].name)
                    j++;
                else
                    break;
            }
            if (j<target.Count)
            {
                bonelist.Bones[i].position = target[j].position;
                bonelist.Bones[i].rotation = target[j].rotation;
            }
        }
    }
}
