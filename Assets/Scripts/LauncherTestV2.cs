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
    private enum ShootState
    {
        shooting,
        reloading,
        overheating
    }
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
    private ShootState shoot = ShootState.shooting;
    [SerializeField]
    private int maxAmmoCount;
    private int currentAmmo;
    float timerwait = 2f;
    float reloadtimer = 0.1f;
    private void Awake()
    {
        currentAmmo = maxAmmoCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer<=0)
            timer -= Time.deltaTime;
        if(timerwait<=0)
            timerwait -= Time.deltaTime;
        trajectooryTrace.UpdateTrajectory((launchPoint.transform.position - pivotPoint.transform.position).normalized * foodVelocity, foodPrefab[0].GetComponent<Rigidbody>(), launchPoint.transform.position);
        if (timer <= 0)
        {
            switch (gunSide) {
                case GunSide.primary:
                    if (VRDevice.Device.PrimaryInputDevice.GetButton(VRButton.Trigger))
                    {
                        Shoot();
                    }
                    break;
                case GunSide.secondary:
                    if (VRDevice.Device.SecondaryInputDevice.GetButton(VRButton.Trigger))
                    {
                        Shoot();
                    }
                    break;
            }
        }
        if (timerwait <= 0&&currentAmmo<maxAmmoCount)
        {
            reloadtimer -= Time.deltaTime;
            if (reloadtimer <= 0)
            {
                reloadtimer = 0.1f;
                currentAmmo++;
            }
        }

        ammoGuage.fillAmount = currentAmmo / maxAmmoCount;
        ammoGuage.color = colors.Evaluate(ammoGuage.fillAmount);
    }
    public void Shoot()
    {
        if (currentAmmo > 0)
        {
            timer = 0.2f;
            timerwait = 2f;
            GameObject randomFoodRandom = foodPrefab[(int)Random.Range(0, foodPrefab.Count - 0.01f)];
            GameObject food = Instantiate(randomFoodRandom);
            food.transform.position = launchPoint.transform.position;
            Rigidbody foodRB = food.GetComponent<Rigidbody>();
            foodRB.velocity = (launchPoint.transform.position - pivotPoint.transform.position).normalized * foodVelocity;
            foodRB.AddTorque(new Vector3(Random.Range(0, rotatePower), Random.Range(0, rotatePower), Random.Range(0, rotatePower)), ForceMode.Impulse);
            currentAmmo--;
        }
    }
}
