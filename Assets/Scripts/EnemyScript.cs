using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[SelectionBase]
public class EnemyScript : MonoBehaviour
{
    Animator anim;
    public bool dead;    
    public bool death;    
    public bool front;    
    public Transform weaponHolder;
    public float MinActivationTime;
    public float MaxActivationTime;
    public bool delayActivate = false;
    public float InactiveDistance = 5f;
    public bool stoppedShooting = false;
    GameObject player;
    bool once;
    bool onceAgain;
    void Start()
    {
        stoppedShooting = false;
        anim = GetComponent<Animator>();
        player = GameObject.Find("player");
        if (weaponHolder.GetComponentInChildren<WeaponScript>() != null)
            weaponHolder.GetComponentInChildren<WeaponScript>().active = false;

        /*        GetComponentInChildren<Outline>().enabled = false;     */
        anim.SetBool("transition", false);
        once = true;
    }


    void Update()
    {
        if (Mathf.Abs(transform.position.z - player.transform.position.z) < 5)
        {
            stoppedShooting = true;
            anim.SetBool("transition", false);
        }

        /*Debug.Log(death);*/
        if(once==true && death==true)
        {
            SuperHotScript.instance.enemiesKilled++;
            once = false;
        }
    }

    public void Ragdoll()
    {
        /*transform.Find("LowManSkeleton").GetComponent<MeeleeDetector>().meelee();
        Destroy(gameObject);*/

        death = true;

        BodyPartScript[] parts = GetComponentsInChildren<BodyPartScript>();
        foreach (BodyPartScript bp in parts)
        {
            bp.rb.isKinematic = false;
            bp.rb.interpolation = RigidbodyInterpolation.Interpolate;
        }

        GetComponent<Animator>().enabled = false;
        

        if (weaponHolder.GetComponentInChildren<WeaponScript>() != null)
        {
            WeaponScript w = weaponHolder.GetComponentInChildren<WeaponScript>();
            w.Release();

        }

        
        Destroy(gameObject, 3f);
    }

    public void Slice()
    {
        if (onceAgain == false)
        {
            SuperHotScript.instance.enemiesKilled++;
            transform.Find("LowManSkeleton").GetComponent<MeeleeDetector>().meelee();
            Destroy(gameObject);
            onceAgain = true;
        }
    }

    public void Shoot()
    {
        if (dead)
            return;

        if (weaponHolder.GetComponentInChildren<WeaponScript>() != null)
        {

            weaponHolder.GetComponentInChildren<WeaponScript>().Shoot(GetComponentInChildren<ParticleSystem>().transform.position, transform.rotation, true);
            GameObject.Find("Shoot_Sound").GetComponent<AudioSource>().Play();
        }
    }

    IEnumerator RandomAnimation()
    {
        anim.enabled = false;
        yield return new WaitForSecondsRealtime(1f);
        anim.enabled = true;
    }

    public void PlayParticle()
    {
        if (GetComponentInChildren<ParticleSystem>() != null)
            GetComponentInChildren<ParticleSystem>().Play();
    }

    public IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(Random.Range(MinActivationTime, MaxActivationTime));
        if (!stoppedShooting)
            anim.SetBool("transition", true);
        // Code to execute after the delay
    }

    

}
