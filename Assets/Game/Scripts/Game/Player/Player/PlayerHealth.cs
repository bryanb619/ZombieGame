using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]    private float health        = 100f;
                        private float _maxHealth    = 100f;
    
    [SerializeField]    private float healthRegen   = 0.5f;
    [SerializeField]    private Image vignette;
                        private Color _vignetteColor;

    private void Start()
    {
        _vignetteColor = vignette.color;
    }

    private void Update()
    {
        float percentage = health / _maxHealth;
       
        _vignetteColor.a = 1f - percentage;
        vignette.color = _vignetteColor;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Die();
        }
        else
        {
            
            
            
#if UNITY_EDITOR

            Debug.Log("Player Health: " + health);
#endif
            
        }
    }
    
    private void Die()
    {
        
    }
}
