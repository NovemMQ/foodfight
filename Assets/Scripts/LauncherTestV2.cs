using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LauncherTestV2 : MonoBehaviour
{
    private enum GunSide
    {
        primary,
        secondary
    }
    
    private AudioSource launchSound;
    [SerializeField] private float shootHapticDuration;
    [SerializeField] private GameManager gameMnager;
    [SerializeField] private Gradient colors;
    [SerializeField] private Image ammoGuage;
    [SerializeField]
    private GameObject pivotPoint;
    [SerializeField]
    private GameObject launchPoint;
    public List<GameObject> foodPrefab;
    [SerializeField]
    [Range(2,40)]
    private float foodVelocity;
    [SerializeField]
    [Range(2, 20)]
    private float rotatePower;
    [SerializeField]
    private TrajectooryTrace trajectooryTrace;
    private float timer = 0f;
    [SerializeField]
    private GunSide gunSide;
    [SerializeField]
    private int maxAmmoCount;
    private int currentAmmo = 30;
    float hapticTimer;
    private bool hapticFlag = false;
    float timerwait = 0.3f;
    float reloadtimer = 0.05f;
    private void Awake()
    {
        currentAmmo = maxAmmoCount;
    }

    //scorekeeper
    ScoreKeeper scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreKeeper>();
        hapticTimer = shootHapticDuration;
        launchSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerwait -= Time.deltaTime;
        if (hapticFlag)
            hapticTimer -= Time.deltaTime;
        ammoGuage.fillAmount = (float)currentAmmo / maxAmmoCount;
        ammoGuage.color = colors.Evaluate(1 - ammoGuage.fillAmount);
        if (timerwait <= 0 && currentAmmo < maxAmmoCount)
        {
            reloadtimer -= Time.deltaTime;
            if (reloadtimer <= 0)
            {
                reloadtimer = 0.05f;
                currentAmmo++;
            }
        }
        
        if (timer <= 0&&currentAmmo>0)
        {
            switch (gunSide) {
                case GunSide.primary:
                    if (hapticTimer <= 0)
                    {
                        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
                        hapticTimer = shootHapticDuration;
                        hapticFlag = false;
                    }
                    if (VRDevice.Device.PrimaryInputDevice.GetButton(VRButton.Trigger))
                    {
                        Shoot();
                        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
                        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
                        
                    }
                    break;
                case GunSide.secondary:
                    if (hapticTimer <= 0)
                    {
                        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
                        hapticTimer = shootHapticDuration;
                        hapticFlag = false;
                    }
                    if (VRDevice.Device.SecondaryInputDevice.GetButton(VRButton.Trigger))
                    {
                        Shoot();
                        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
                        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
                    }
                    break;
            }
        }
    }
    public void Shoot()
    {
        hapticFlag = true;
        timer = 0.2f;    
        launchSound.Play();
        timerwait = 1f;
        GameObject food = FoodPoolManager.getRandomFoodPlayer();
        food.SetActive(true);
        food.transform.position = launchPoint.transform.position;
        Rigidbody foodRB = food.GetComponent<Rigidbody>();
        foodRB.velocity = (launchPoint.transform.position - pivotPoint.transform.position).normalized * foodVelocity;
        foodRB.AddTorque(new Vector3(Random.Range(0, rotatePower), Random.Range(0, rotatePower), Random.Range(0, rotatePower)), ForceMode.Impulse);
        currentAmmo--;
        scoreManager.addFoodThrown();
    }
}

