using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class FTLMeter : MonoBehaviour
{
    bool isSlowMo;    
    public float SlowMoInSpeed = 50.0f;
    public float SlowMoOutSpeed = 50.0f;
    public float SlowMoTimeScale = 0.1f;
    public float SlowFOV = 50.0f;
    public Transform Vignette;
    public float VignetteScale = 3.0f;
    private Vector3 originalVignetteScale;
    private Vector3 SloMoVignetteScale;
    public float regularFOV = 70.0f;
    public CinemachineVirtualCamera cam;
    void Start()
    {
        isSlowMo = false;
        originalVignetteScale = Vignette.localScale;
        SloMoVignetteScale = originalVignetteScale;
        originalVignetteScale = VignetteScale*SloMoVignetteScale;
    }

    void TimeController()
    {
        var scrSup = GameObject.Find("player").GetComponent<SuperHotScript>();

        float time = (isSlowMo) ? SlowMoTimeScale : 1f;
        float lerpTime = (isSlowMo) ? SlowMoInSpeed : SlowMoOutSpeed;
        float FOV = (isSlowMo) ? SlowFOV : regularFOV;
        Vector3 scale = (isSlowMo) ? SloMoVignetteScale : originalVignetteScale;
        time = scrSup.action ? 1 : time;
        lerpTime = scrSup.action ? .1f : lerpTime;

        Time.timeScale = Mathf.Lerp(Time.timeScale, time, lerpTime*Time.unscaledDeltaTime);
        cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView, FOV, lerpTime * Time.unscaledDeltaTime);
        Vignette.localScale = Vector3.Lerp(Vignette.localScale, scale, lerpTime * Time.unscaledDeltaTime * 3f);
        //Debug.Log(Time.timeScale);
    }


    void Update()
    {
        TimeController();

        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if (Input.GetMouseButton(0))
            {
                isSlowMo = true;
                GameObject.Find("SlowMo").GetComponent<AudioSource>().enabled = true;
            }
            else
            {
                isSlowMo = false;
                GameObject.Find("SlowMo").GetComponent<AudioSource>().enabled = false;
            }
            
        }
        
        else
        {
            if (Time.timeSinceLevelLoad > 2.3f)
                isSlowMo = true;
        }

        Time.fixedDeltaTime = Time.timeScale / 50;
        //Debug.Log(Time.timeScale);
    }

    public void AdjustFOV(float fov)
    {
        regularFOV = fov;
        SlowFOV = fov*5/7;
    }

    public void AdjustSlowMo(float ts)
    {
        SlowMoTimeScale = 1/ts;
    }

}
