using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GunScript : Equip
{
    [Header("Locations")]
    public Vector3 bulletSpread;
    public float bulletSpreadModifier;
    public Camera gunCamera;
    public Transform tracerOrigin;
    public Animator animator;
    public Tracer tracer;
    private int ammo = 30;
    private int maxAmmo = 30;

    public FirstPersonController fpsController;
    public Player player;
    public float shotCooldown;
    private float nextShot;


    public void SetCamera(Camera camera)
    {
        gunCamera = camera;
    }

    public override void PrimaryUse()
    {
        Fire();
    }

    public override void SecondaryUse()
    {
        ADS();
    }

    public override void Reload()
    {
        Reload1();
    }

    public void ADS()
    {
        /*
        if (animator.GetBool("ADS") == false)
        {
            animator.SetBool("ADS", true);
        }
        else
        {
            animator.SetBool("ADS", false);
        }
        */
    }

    public void Fire()
    {

        Vector3 destination;
        if (Time.time > nextShot && ammo > 0)
        {
            //StopAllCoroutines();
            nextShot = Time.time + shotCooldown;

            bulletSpreadModifier += 0.11f;

            //gunAudio.PlayOneShot(gunshotClip)

            Vector3 direction = GetDirection();

            Ray bulletRay = new Ray(gunCamera.transform.position, direction);

            animator.SetTrigger("Shoot");

            ammo -= 1;

  
            if (Physics.Raycast(bulletRay, out RaycastHit hit, float.MaxValue))
            {

                destination = hit.point;

                if (hit.collider.gameObject.GetComponent<Player>())
                {
                    //
                    Player enemy = hit.collider.gameObject.GetComponent<Player>();
                    int teamNum = player.GetTeamNumber();
                    enemy.WasHit(teamNum);
                }
            }
            else
            {
                destination = bulletRay.GetPoint(20);
            }


            if (!IsServer) {
                Tracer trail = Instantiate(tracer, tracerOrigin.position, Quaternion.identity);
                trail.Travel(destination);
            }

            SpawnTrailServerRPC(destination);

        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnTrailServerRPC(Vector3 destination)
    {
        Tracer trail = Instantiate(tracer, tracerOrigin.position, Quaternion.identity);
        trail.GetComponent<NetworkObject>().Spawn();
        trail.Travel(destination);
    }

    public void Reload1()
    {
        animator.SetTrigger("Reload");
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        ammo = maxAmmo;
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = gunCamera.transform.forward;
        float finalModifier = bulletSpreadModifier;
        if (fpsController.isZoomed == true) finalModifier = bulletSpreadModifier * 0.4f;
        if (fpsController.isSprinting == true) finalModifier = bulletSpreadModifier * 1.5f + 0.75f;
        if (fpsController.isCrouched == true) finalModifier = bulletSpreadModifier * 0.5f;

        if (true)
        {
            direction += new Vector3(
                UnityEngine.Random.Range(-bulletSpread.x * finalModifier, bulletSpread.x * finalModifier),
                UnityEngine.Random.Range(-bulletSpread.x * finalModifier, bulletSpread.x * finalModifier),
                UnityEngine.Random.Range(-bulletSpread.x * finalModifier, bulletSpread.x * finalModifier)
            );
        }

        return direction;
    }

    // Start is called before the first frame update
    void Awake()
    {

        //gunCamera = transform.parent.gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletSpreadModifier = Mathf.Min(bulletSpreadModifier, 1f);
        if (Time.time > nextShot)
        {
            bulletSpreadModifier = Mathf.Max(0f, bulletSpreadModifier - Time.deltaTime);
        }
    }
}

