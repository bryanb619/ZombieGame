using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombie : MonoBehaviour
{
    
    
                        private NavMeshAgent    _agent;
                        private float           _pathUpdateTimer = 0.2f;

                        private float           _health = 100f; 
                        
    [SerializeField]    private PlayerMovement[] _playerMovement;
                        private bool            _compareBoth; 
                        
                        
    void Awake()
    {
        _agent              = GetComponent<NavMeshAgent>();
        _playerMovement     = FindObjectsOfType<PlayerMovement>();
        
        if(_playerMovement.ElementAtOrDefault(1) != null)
        {
            _compareBoth = true;
        }
        else
        {
            _compareBoth = false;
        }
        
        
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (_pathUpdateTimer > 0)
        {
            _pathUpdateTimer -= Time.deltaTime;
        }
        else
        {

            if (_compareBoth)
            {
                if ((transform.position - _playerMovement[0].transform.position).magnitude < 
                    (transform.position - _playerMovement[1].transform.position).magnitude)
                {
                    _agent.SetDestination(_playerMovement[0].transform.position);
                }
                
                else if ((transform.position - _playerMovement[0].transform.position).magnitude >
                         (transform.position - _playerMovement[1].transform.position).magnitude)
                {
                    _agent.SetDestination(_playerMovement[1].transform.position);
                }
            }
            
            else if (!_compareBoth)
            {
                _agent.SetDestination(_playerMovement[0].transform.position);
            }

            
            _pathUpdateTimer = 0.2f;
        }
        
       


    }


    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die(); 
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
