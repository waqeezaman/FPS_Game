using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

   
    [SerializeField]
    private float SearchRange;

    [SerializeField]
    private float MaxShootingDistance;

    [SerializeField]
    private float MinShootingDistance;

    private Transform PlayerBody;




    private NavMeshAgent Controller;


    [SerializeField ]
    private  float ForgetTime;

    private float TimeNotSeen;


    [SerializeField]
    private EnemyNavigation Navigator;

    [SerializeField]
    private Transform Eye;

    private bool InPursuit;

    [SerializeField]
    private  LayerMask EnemyLayerMask;

    [SerializeField]
    private Fire Weapon;

    [SerializeField]
    private float SpawnChance;

    [SerializeField]
    private float DestinationLeeway; // variable to decide the bounds within the agent must be to determine whether it 
                                     // has reached the destination or not

    // Start is called before the first frame update
    void Start()
    {

        Navigator = GameObject.Find("Enemy Pathfinder").GetComponent<EnemyNavigation>();
        Controller = GetComponent<NavMeshAgent>();
        Controller.SetDestination(Navigator.GetNode());
        PlayerBody = GameObject.FindGameObjectWithTag("Player").transform.Find("Body");

        if (Eye == null)
        {
            Eye = transform.Find("Eye");
        }
        if (Weapon == null)
        {
            Weapon = transform.GetComponentInChildren<Fire>();
        }

        InPursuit = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Node" + Controller.destination);
        if (!InPursuit && (transform.position.x >= Controller.destination.x - DestinationLeeway && transform.position.x <= Controller.destination.x + DestinationLeeway)&& 
            (transform.position.z >= Controller.destination.z - DestinationLeeway && transform.position.z <= Controller.destination.z + DestinationLeeway) && 
            (transform.position.y >= Controller.destination.y-DestinationLeeway && transform.position.y <= Controller.destination.y + DestinationLeeway))
        {
            Controller.SetDestination(Navigator.GetNode());
        }


        
        if (InPursuit)
        {
            Pursue();
        }
        else if (InSight())
        {
            InPursuit = true;
        }
    }


    private bool InSight()
    {
        if (InRange(SearchRange))
        {
            RaycastHit hit;
            if (!Physics.Linecast(PlayerBody.position,Eye.position, out hit,EnemyLayerMask))
            {
                Eye.LookAt(PlayerBody.position);
                return true;

                
            }
        
         }
        return false;
    }


    

 

    private bool InRange(float range)
    {
        if (Vector3.Distance(Eye.position, PlayerBody.position) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    private void Pursue()
    {
        Controller.SetDestination(PlayerBody.position);

        
        if (!InSight())
        {
            TimeNotSeen += Time.deltaTime;
            Controller.isStopped = false;
        }
        else
        {
            TimeNotSeen = 0;
            if (InRange(Weapon.Range*MaxShootingDistance))
            {
                Weapon.Shoot(Eye);
            }
            if (InRange(Weapon.Range * MinShootingDistance))
            {
                Controller.isStopped=true;
            }
            else
            {
                Controller.isStopped = false;
            }
        }

        if (TimeNotSeen > ForgetTime)
        {
            TimeNotSeen = 0;
            InPursuit = false;
            Controller.SetDestination(Navigator.GetNode());
            return;
        }

    }


    public bool WillSpawn()
    {
        float r = Random.value;
        if (r < SpawnChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
