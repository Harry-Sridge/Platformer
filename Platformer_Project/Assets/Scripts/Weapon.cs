using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    WeaponTransformManager weaponTransformManager;
    public Transform weaponHolder;
    
    public enum FireMode { Auto, Single, Burst};
    public FireMode fireMode;
    public Transform[] muzzles;
    public Projectile[] projectiles;
    public int maxBurstCount;
    public int pelletCount;
    private int currentBurstCount;
    float nextShotTime;
    bool releasedSinceLastShot;
    public int damage;
    public float fireRate;
    public float velocity;
    public float spread;

    [Header("Recoil")]
    public float minKick = .2f;
    public float maxKick = .5f;
    public float minRecoilAngle = .2f;
    public float maxRecoilAngle = .3f;
    public float recoilMoveTime = .1f;
    public float recoilRotationTime = .1f;

    float recoilAngle;
    Vector3 recoilSmoothDampVelocity;
    float rotationSmoothDampVelocity;

    [Header("Effects")]
    public GameObject[] muzzleFlashes;
    public GameObject flash;
    public Transform shell;
    public Transform ejection;
    public AudioClip[] gunshot;

    private void Start()
    {
        for(int i = 0; i < muzzleFlashes.Length; i++)
        {
            muzzleFlashes[i].SetActive(false);
        }

        flash.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
            OnTriggerHold();

        if (Input.GetButtonUp("Fire1"))
            OnTriggerRelease();
    }

    private void LateUpdate()
    {
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, new Vector3(0, 0, 0), ref recoilSmoothDampVelocity, recoilMoveTime);
        recoilAngle = Mathf.Lerp(recoilAngle, 0, Time.deltaTime * recoilRotationTime);
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, recoilAngle));
    }

    private void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            if(fireMode == FireMode.Burst)
            {
                if(currentBurstCount == 0)
                    return;

                currentBurstCount--;
            }

            else if(fireMode == FireMode.Single)
            {
                if (!releasedSinceLastShot)
                    return;
            }
            
            for (int i = 0; i < muzzles.Length; i++)
            {            
                nextShotTime = Time.time + fireRate / 1000;
                for(int j = 0; j < pelletCount; j++)
                {
                    float spreadAmount = Random.Range(-spread, spread);
                    Quaternion projectileRotation = weaponHolder.localRotation * Quaternion.Euler(0, 0, spreadAmount);
                    int projectileIndex = Random.Range(0, projectiles.Length);
                    Projectile newProjectile = Instantiate(projectiles[projectileIndex], muzzles[i].position, projectileRotation) as Projectile;
                    newProjectile.SetupProjectile(velocity);
                }
            }

            //Simulate recoil
            transform.localPosition -= Vector3.right * Random.Range(minKick, maxKick);
            recoilAngle += Random.Range(minRecoilAngle, maxRecoilAngle);
            recoilAngle = Mathf.Clamp(recoilAngle, 0, 30);

            //Spawn shell
            float rotateAmount = Random.Range(-10, 10);
            Quaternion shellRotation = ejection.rotation * Quaternion.Euler(0, 0, rotateAmount);
            Transform newShell = Instantiate(shell, ejection.position, shellRotation);

            //Flash
            StartCoroutine(Flash(Random.Range(0, muzzleFlashes.Length)));
        }
    }

    IEnumerator Flash (int index)
    {
        muzzleFlashes[index].SetActive(true);
        flash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        muzzleFlashes[index].SetActive(false);
        flash.SetActive(false);
    }

    private void OnTriggerHold()
    {
        Shoot();
        releasedSinceLastShot = false;
    }

    private void OnTriggerRelease()
    {
        releasedSinceLastShot = true;
        currentBurstCount = maxBurstCount;
    }
}
