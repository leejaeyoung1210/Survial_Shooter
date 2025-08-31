using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,//탄발사가능
        Empty,//비어있음
        Reloading,//재장전중 
    }

    private State currentState = State.Ready;

    public State CurrentState
    {
        get { return currentState; }
        set
        {
            currentState = value;
            switch (currentState)
            {
                case State.Ready:
                    break;
                case State.Empty:
                    break;
                case State.Reloading:
                    break;
            }
        }
    }


    public GunData gunData;

    public ParticleSystem muzzleEffect;    

    private LineRenderer lineRenderer;
    private AudioSource audioSource;

    public Transform firePosition;
        
    private float lastFireTime;
    public int Ammo;
    public int reAmmo;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();

        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
        lastFireTime = 0;
    }

    private void OnEnable()
    {
        reAmmo = gunData.startAmmo;
        Ammo = gunData.ammo;
    }


    public void Fire()
    {
        if (CurrentState == State.Ready && Time.time > (lastFireTime + gunData.timeBetFire))
        {
            lastFireTime = Time.time;
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 hitposition = Vector3.zero;


        if (Physics.Raycast
            (firePosition.position, firePosition.forward, out RaycastHit hit, gunData.firedistance))
        {
            hitposition = hit.point;

            var target = hit.collider.GetComponent<IamDamage>();
            if (target != null)
            {
                target.OnDamage(gunData.damage, hitposition, hit.normal);
            }
        }
        else
        {
            hitposition = firePosition.position + firePosition.forward * gunData.firedistance;
        }
        StartCoroutine(CoShotEffect(hitposition));

        --Ammo;
        //uiManager.SetAmmpText(Ammo, reAmmo);
        if (Ammo == 0)
        {
            CurrentState = State.Empty;
        }

    }

    public bool Reload()
    {
        if (CurrentState == State.Reloading || reAmmo == 0 || Ammo == gunData.ammo)
        { return false; }

        StartCoroutine(CoReload());

        return true;

    }

    IEnumerator CoReload()
    {
        CurrentState = State.Reloading;
        
        yield return new WaitForSeconds(gunData.reloadTime);

        Ammo += reAmmo;
        if(Ammo >= gunData.ammo)
        {            
            Ammo = gunData.ammo;
            reAmmo -= Ammo;            
        }
        else
        {
            reAmmo -= Ammo; 
            if(reAmmo < 0)
            {
                reAmmo = 0; 
            }
        }
            CurrentState = State.Ready;
    }

    IEnumerator CoShotEffect(Vector3 hitposition)
    {
        audioSource.PlayOneShot(gunData.shootClip);

        muzzleEffect.Play();        
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePosition.position);
        Vector3 endPos = hitposition;
        lineRenderer.SetPosition(1, endPos);

        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
    }






}
