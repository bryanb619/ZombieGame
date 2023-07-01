using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "FpsGame/Guns/GunData", order = 1)]
public class GunData : ScriptableObject
{
   
    [SerializeField]    private float       damage; 
                        public float        Damage => damage;
                        
    [SerializeField]    private float       range;
                        public float        Range => range;
    
    [SerializeField]    private float       fireRate;
                        public float        FireRate => fireRate;
    [SerializeField]    private bool        isAutomatic;
                        public bool         IsAutomatic => isAutomatic;
                        
    [SerializeField]    private float       impactForce;
                        public float        ImpactForce => impactForce; 
    
    [SerializeField]    private float       maxAmmo;
                        public float        MaxAmmo => maxAmmo;
                        
    [SerializeField]    private float       currentAmmo;
                        public float        CurrentAmmo => currentAmmo;
                        
    [SerializeField]    private float       reloadTime;
                        public float        ReloadTime => reloadTime;
                        
    [SerializeField]   private bool         useMuzzleFlash;
                        public bool         UseMuzzleFlash => useMuzzleFlash;
                        
                        
    [SerializeField]    private GameObject  impactEffect;
                        public GameObject   ImpactEffect => impactEffect;
}
