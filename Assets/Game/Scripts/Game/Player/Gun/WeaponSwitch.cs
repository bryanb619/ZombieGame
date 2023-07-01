using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    private int _selectedWeapon = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon(); 

    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = _selectedWeapon;
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(_selectedWeapon >= transform.childCount - 1)
                _selectedWeapon = 0;
            else
                _selectedWeapon++;
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if(_selectedWeapon <= 0)
                _selectedWeapon = transform.childCount - 1;
            else
                _selectedWeapon--;
        }

        if (previousSelectedWeapon != _selectedWeapon)
        {
            SelectWeapon();
        }
           
    }
    
    private void SelectWeapon()
    {
        int i = 0;
        
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(i == _selectedWeapon);
            i++;
        }
    }
}
