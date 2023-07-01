using System.Collections;
using UnityEngine;
using FMODUnity;

public class Gun : MonoBehaviour
{
    #region Variables
    
    [Header("Gun Data")]
    [SerializeField]    private GunData             gunData;
    
                        private float               _damage; 
                        private float               _range;
                        private float               _fireRate;
                        private bool                _isAutomatic;
                        private float               _nextFire;
                        private float               _impactForce;
    
                        private float               _maxAmmo;
                        private float               _currentAmmo;
                        private float               _reloadTime;
                        
                        private bool                _isReloading;
                        private bool                _isAiming;
                        
                        private bool                _useMuzzleFlash;
    [SerializeField]    private ParticleSystem      muzzleFlash;
                        private GameObject          _impactEffect;
                        
                        private Camera              _fpsCam;
                        
    [SerializeField]    private Animator            animator;

    [Header("Audio")] 
    [SerializeField]    private EventReference      fire; 
    [SerializeField]    private EventReference      reload, empty;
    
    #endregion

    #region Awake
    
    // Start is called before the first frame update
    private void Awake()
    {
        CollectGunData(); 
    }

    private void CollectGunData()
    {
        
        _damage         = gunData.Damage;
        _range          = gunData.Range;
        _fireRate       = gunData.FireRate;
        _isAutomatic    = gunData.IsAutomatic;
        _impactForce    = gunData.ImpactForce;
        _maxAmmo        = gunData.MaxAmmo;
        _currentAmmo    = gunData.CurrentAmmo;
        _reloadTime     = gunData.ReloadTime;
        _useMuzzleFlash = gunData.UseMuzzleFlash;
        //_muzzleFlash    = gunData.MuzzleFlash;
        _impactEffect   = gunData.ImpactEffect;
        


        //GameObject.FindWithTag
        //("MainCamera").GetComponent<Camera>();
    }
    #endregion


    private void OnEnable()
    {
        _fpsCam         = GetComponentInParent<Camera>();
        
        _isReloading    = false;
        _isAiming       = false;
        animator.SetBool("Reloading", false);

    }


    private void Update()
    {
        ShootCall();
        
    }

    private void ShootCall()
    {
        if (_isReloading) return;
        
        if(_currentAmmo <=0 || Input.GetKeyDown(KeyCode.R))
        {
            // RELOAD
            StartCoroutine(Reload());
            return;
        }
        
        if (Input.GetButtonDown("Fire1") && Time.time >= _fireRate)
        {
            _nextFire = Time.time + 1f / _fireRate;
            
            Shoot();
            
#if UNITY_EDITOR

            print("Shooting!"); 
#endif
        }
        
    }
    
    private IEnumerator Reload()
    {
        
#if UNITY_EDITOR
        
        Debug.Log("Reloading...");
#endif
        // set reloading to true
        _isReloading = true;
        
        // play reload animation
        animator.SetBool("Reloading", true);
        
        // wait for reload time
        RuntimeManager.PlayOneShot(empty, transform.position);
        yield return new WaitForSeconds(_reloadTime); 
        RuntimeManager.PlayOneShot(reload, transform.position);
        // set ammo 
        _currentAmmo = _maxAmmo;
        
        // stop reload animation
        animator.SetBool("Reloading", false);
        
        // set reloading to false
        _isReloading = false;
        
    }
    
    private void Shoot()
    {
        if (_useMuzzleFlash)
        {
            muzzleFlash.Play();
        }

        RuntimeManager.PlayOneShot(fire, transform.position);
        
        _currentAmmo--;
        InitiateRay(); 

        
    }

    private void InitiateRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(_fpsCam.transform.position, _fpsCam.transform.forward, out hit, _range))
        {
            
#if UNITY_EDITOR
            Debug.Log(hit.transform.name);
            Debug.DrawRay(_fpsCam.transform.position, _fpsCam.transform.forward * _range, Color.red, 1f);
#endif
            
            Zombie target = hit.transform.GetComponent<Zombie>();
            
            if (target != null)
            {
                target.TakeDamage(_damage);
                GameObject impactGO = Instantiate(_impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                            Destroy(impactGO, 2f);
            }
            
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * _impactForce);
            }
            
            
        }
    }

    private void Aim()
    {
        if (!_isAiming && Input.GetButton("Fire2"))
        {
            _isAiming = true;
            animator.SetBool("Aiming", true);
            return;
        }
        else
        {
            _isAiming = false;
            animator.SetBool("Aiming", false);
            return;
        }
        

    }
    
    /*
     private float nextFire = 0.0f;
public float fireRate = 0.5f;

void Update()
{
    nextFire -= Time.deltaTime;
    nextFire -= Time.deltaTime;
    if (Input.GetButton("Fire1") && nextFire <= 0)
    {
        nextFire = fireRate;
        Fire();
    }
}

void Fire()
{
    // Code to fire goes here
}
    */
    
}
