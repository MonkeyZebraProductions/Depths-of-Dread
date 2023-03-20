using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;

public class AirArmour : MonoBehaviour
{
    public float MaxAir = 100;
    public float AirDecreaceRate = 1;

    public float BaseDamageMultiplier = 1;
    public float MaxDamage = 10;

    private float air, damage,lowAirDecreaseRate;


    //Air Management
    public TextMeshProUGUI AirText;
    public Slider AirBar1, AirBar2;
    public Transform Needle;
    public Material AirMat,DialMat;

    //Getting Hit
    private ParticleSystem Sparks;
    public AudioSource SparkSfx,GruntSfx;
    public Animator HitAnimator;

    public CinemachineBrain Brain;
    private CinemachineVirtualCamera Cam;
    private CinemachineBasicMultiChannelPerlin Noise;
    public float AmplitudeChange, FrequencyChange;


    //Audio Changes
    public AudioMixer Muffle,Breathing;
    public float LowPassCuttoffPercent,MinVolume,MaxVolume, MinPitch,MaxPitch,MinBreathingLowCut, MaxBreathingLowCut;
    private float _currentCutoff, _currentCutoffPercent,_currentVolume,_currentPitch,_currentBreathingCutoff;

    //Post Processing Changes
    public PostProcessVolume PP;
    private ChromaticAberration cA;
    private DepthOfField dOF;
    private LensDistortion lD;

    //Cracked Glass Assets
    public List<Image> Cracks;
    private int currentIndex, prevIndex;
    private float crackAlpha = 0;

    // Start is called before the first frame update
    void Start()
    {
        air = MaxAir;
        damage = BaseDamageMultiplier;
        Sparks = GetComponentInChildren<ParticleSystem>();
        //Brain = FindObjectOfType<CinemachineBrain>();
        lowAirDecreaseRate = 1;
        _currentCutoff = 3500;
        _currentCutoffPercent = 100;
        prevIndex=currentIndex;
    }

    // Update is called once per frame
    void Update()
    {

        Cam= Brain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        Noise = Cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        //_currentAmplitude = Noise.m_AmplitudeGain;
        //_currentFrequency = Noise.m_FrequencyGain;

        //checks if current values are beyond their limits
        if (damage<BaseDamageMultiplier)
        {
            damage = BaseDamageMultiplier;
        }

        if(air>MaxAir)
        {
            air = MaxAir;
        }

        if (damage > MaxDamage)
        {
            damage = MaxDamage;
        }

        //lowers air amount by the amount of damage taken
        air -= AirDecreaceRate * lowAirDecreaseRate * damage*Time.deltaTime;

        //updates air UI
        AirText.text = "Air: " + Mathf.Round(air*100)/100;
        AirBar1.maxValue = AirBar2.maxValue = MaxAir;
        AirBar1.value = AirBar2.value = MaxAir - air;

        Muffle.SetFloat("CutoffFreq", _currentCutoff);
        _currentCutoff = MapFunction(_currentCutoffPercent, 0, 100, 0, 3500);

        if (air<MaxAir/4)
        {
            //low air mode
            lowAirDecreaseRate = 0.75f;
            _currentPitch = MapFunction(air*4, MaxAir, 0, MinPitch, MaxPitch);
            _currentBreathingCutoff = MapFunction(air*4, MaxAir, 0, MinBreathingLowCut, MaxBreathingLowCut);
            if (_currentCutoffPercent> LowPassCuttoffPercent)
            {
                _currentCutoffPercent -= 20 * Time.deltaTime;
            }
            ChangePostProcessing();

        }
        else
        {
            ZeroPP();
            lowAirDecreaseRate = 1f;
            _currentPitch = 0.3f;
            _currentBreathingCutoff = 500f;
            if (_currentCutoffPercent < LowPassCuttoffPercent)
            {
                _currentCutoffPercent += 20*Time.deltaTime;
            }
        }

        if(air<=0)
        {
            //Kill
            Destroy(this.gameObject);
        }

        //changes needle roation based on damage
        Needle.localRotation =Quaternion.Euler(0, 0, MapFunction(damage, BaseDamageMultiplier, MaxDamage, -30f, 210f));
        AirMat.SetColor("_EmissionColor", new Color(1-MapFunction(air, MaxAir, MaxAir/5, 1, 0),MapFunction(air, MaxAir, MaxAir / 5, 1, 0), 0)*1.25f);
        DialMat.SetColor("_EmissionColor", new Color(MapFunction(damage, MaxDamage, MaxDamage / 5, 1, 0), 1-MapFunction(damage, MaxDamage, MaxDamage / 5, 1, 0), 0) * 1.25f);

        _currentVolume = MapFunction(air, MaxAir, MaxAir * 0.25f, MinVolume, MaxVolume);
        Breathing.SetFloat("BreathingVolume", _currentVolume);

        
        Breathing.SetFloat("BreathingPitch", _currentPitch);

        Breathing.SetFloat("BreathingLowPass", _currentBreathingCutoff);

        Cracks[prevIndex].color = new Color(1, 1, 1, crackAlpha);
        if(crackAlpha>0)
        {
            crackAlpha -= Time.deltaTime;
        }
    }

    public void RecieveArmourDamage(float damageRecieved)
    {
        damage += damageRecieved;
        Sparks.Play();
        SparkSfx.Play();
        GruntSfx.Play();
        crackAlpha = 1;
        prevIndex = currentIndex;
        if(currentIndex==Cracks.Count-1)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex += 1;
        }
        HitAnimator.Play("HitA");
        //StartCoroutine(CamaeraShake());
    }

    public void RecieveArmourRepair(float repairAmount)
    {
        damage -= repairAmount;
        
    }

    public void RefillAir(float refillAmount)
    {
        air += refillAmount;
    }

    public void IncreaseAirCapacity(float UpgradeAmount)
    {
        MaxAir += UpgradeAmount;
        air = MaxAir;
    }

    public void DecreaseRate(float UpgradeAmount)
    {
        AirDecreaceRate -= UpgradeAmount;
        damage = BaseDamageMultiplier;
    }

    private float MapFunction(float x, float from_min, float from_max, float to_min, float to_max)
    {
        return (x - from_min) * (to_max - to_min) / (from_max - from_min) + to_min;
    }

    IEnumerator CamaeraShake()
    {
        Noise.m_AmplitudeGain = AmplitudeChange;    
        Noise.m_FrequencyGain = FrequencyChange;
        yield return new WaitForSeconds(0.5f);
    }

    public void CancelShake()
    {
    }

    void ChangePostProcessing()
    {
        PP.profile.TryGetSettings(out cA);
        cA.intensity.value = MapFunction(air * 4, MaxAir, 0, 0, 1);
        PP.profile.TryGetSettings(out dOF);
        dOF.aperture.value = MapFunction(air * 4, MaxAir, 0, 3, 0.5f);
        PP.profile.TryGetSettings(out lD);
        lD.intensity.value = MapFunction(air * 4, MaxAir, 0, 0, 30);
    }
     void ZeroPP()
    {
        PP.profile.TryGetSettings(out cA);
        cA.intensity.value = 0;
        PP.profile.TryGetSettings(out dOF);
        dOF.aperture.value = 3;
        PP.profile.TryGetSettings(out lD);
        lD.intensity.value = 0;
    }
}
