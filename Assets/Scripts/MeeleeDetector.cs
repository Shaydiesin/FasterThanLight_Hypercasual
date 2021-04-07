using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleeDetector : MonoBehaviour
{
    WeaponScript weaponScr;
    //public GameObject[] enemyDeadPrefabs;
    public List<Transform> enemyAliveTranforms = new List<Transform>();
    //List<Transform> enemyDeadTranforms = new List<Transform>();
    public GameObject left_ragdoll, right_ragdoll;
    BoneList Blist;
    public float RagdollDestroyAfter = 5.0f;
    
    void Start()
    {
        weaponScr = GameObject.Find("player").GetComponentInChildren<WeaponScript>();
        Blist = GetComponent<BoneList>();
        //meelee();
    }

    public void meelee()
    {
            GameObject leftRD = Instantiate(left_ragdoll);
            GameObject rightRD = Instantiate(left_ragdoll);
            leftRD.GetComponent<MatchBones>().copy_transforms(Blist.Bones);
            rightRD.GetComponent<MatchBones>().copy_transforms(Blist.Bones);
            Destroy(leftRD, RagdollDestroyAfter);
            Destroy(rightRD, RagdollDestroyAfter);

    }
}
