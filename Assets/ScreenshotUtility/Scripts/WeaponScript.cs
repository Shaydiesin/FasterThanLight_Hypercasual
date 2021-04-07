using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
[SelectionBase]
public class WeaponScript : MonoBehaviour
{
    
    public bool active = true;
    public bool reloading;

    private Rigidbody rb;
    private Collider collider;

    
    public float reloadTime = .3f;
    public int bulletAmount = 6;
    public TextMeshProUGUI bulletAmountText;
    public TextMeshProUGUI totalBulletAmountText;
    public int totalBulletAmount = 9;
    private int count = 0;
    private int flag = 0;
    




    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        rb.useGravity = false;
        ChangeSettings();
    }

    void ChangeSettings()
    {
        if (transform.parent != null)
            return;
        rb.isKinematic = (SuperHotScript.instance.weapon == this) ? true : false;
        rb.interpolation = (SuperHotScript.instance.weapon == this) ? RigidbodyInterpolation.None : RigidbodyInterpolation.Interpolate;
        collider.isTrigger = (SuperHotScript.instance.weapon == this);
        collider.isTrigger = false;
    }

    public void Shoot(Vector3 pos,Quaternion rot, bool isEnemy)
    {
        if (reloading)
            return;

        if (bulletAmount <= 0)
            return;

        else
            bulletAmount--;

        GameObject bullet = Instantiate(SuperHotScript.instance.bulletPrefab, pos, rot);
        Destroy(bullet, 10f);
        GameObject.Find("Shoot_Sound").GetComponent<AudioSource>().Play();

        if (GetComponentInChildren<ParticleSystem>() != null)
            GetComponentInChildren<ParticleSystem>().Play();

        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.1f, .01f, 10, 90, false, true).SetUpdate(true);

        if(SuperHotScript.instance.weapon == this)
            transform.DOLocalMoveZ(-.1f, .025f).OnComplete(()=>transform.DOLocalMoveZ(0,.2f));
    }

    public void Throw()
    {
        
        GameObject.Find("Whoosh").GetComponent<AudioSource>().Play();
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOMove(transform.position - transform.forward, .01f)).SetUpdate(true);
        s.AppendCallback(() => transform.parent = null);
        s.AppendCallback(() => transform.position = transform.position + (new Vector3(0, .1f, 1) * 2f));
        s.AppendCallback(() => ChangeSettings());
        s.AppendCallback(() => rb.AddForce(Camera.main.transform.forward * 15, ForceMode.Impulse));
        s.AppendCallback(() => rb.AddTorque(transform.transform.right + transform.transform.up * 20, ForceMode.Impulse));
        flag = 1;
        GetComponent<WeaponScript>().enabled = false;
    }

    private void Update()
    {
 
        totalBulletAmountText.text = totalBulletAmount.ToString();

        if (bulletAmount == 0)
            bulletAmountText.color = new Color32(209,86,84,100);
        else
            bulletAmountText.color = Color.white;

        if (totalBulletAmount == 0)
            totalBulletAmountText.color = new Color32(209, 86, 84, 255);
        else
            totalBulletAmountText.color = Color.white;

        if(count==0 && active==true)
        {
            bulletAmount = Random.Range(3,6);
            totalBulletAmount = Random.Range(3, 9);
            count = 1;
        }
    }

    public void Pickup()
    {
        GameObject.Find("Whoosh").GetComponent<AudioSource>().Play();
        rb.useGravity = true;

        if (!active)
            return;

        SuperHotScript.instance.weapon = this;
        ChangeSettings();

        transform.parent = SuperHotScript.instance.weaponHolder;
        transform.DOLocalMove(Vector3.zero, .25f).SetEase(Ease.OutBack).SetUpdate(true);
        transform.DOLocalRotate(Vector3.zero, .25f).SetUpdate(true);

        if (this.gameObject.transform.localScale.x<0.8f && this.gameObject.transform.localScale.x > 0.7f)
        {
            this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (transform.tag == "glass")
        {
            BoxCollider[] colliders = GetComponentsInChildren<BoxCollider>();
            foreach (BoxCollider collider in colliders)
            {
                collider.enabled = true;
            }
        }
    }

    public void Release()
    {
        active = true;
        transform.parent = null;
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        collider.isTrigger = false;

        this.gameObject.tag = "Gun";
        rb.AddForce((Camera.main.transform.position - transform.position) * 2, ForceMode.Impulse);
        rb.AddForce(Vector3.up * 2, ForceMode.Impulse);
        
    } 

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy") && collision.relativeVelocity.magnitude < 15)
        {
            BodyPartScript bp = collision.gameObject.GetComponent<BodyPartScript>();

            if (!bp.enemy.dead)
                Instantiate(SuperHotScript.instance.hitParticlePrefab, transform.position, transform.rotation);

            if (transform.name == "Chaku(Clone)" || transform.name == "KatanaK(Clone)")
            {
                bp.enemy.Slice();
                
            }
            else
            {
                bp.enemy.Ragdoll();
                bp.HidePartAndReplace();                
            }

                if (transform.tag == "glass")
            {
                GameObject.Find("GlassBreak").GetComponent<AudioSource>().Play();

                Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
                foreach (Rigidbody rb in rbs)
                {
                    rb.interpolation = RigidbodyInterpolation.Interpolate;  
                    rb.isKinematic = false;
                }
                
                BoxCollider[] colliders = GetComponentsInChildren<BoxCollider>();
                foreach (BoxCollider collider in colliders)
                {
                    collider.isTrigger = false;
                    collider.enabled = true;
                }
                
            }

            if(flag==1 && transform.tag == "glass")
            {
                Destroy(this.gameObject,0.2f);
            }
        }
        

    }

}
