using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.iOS;


public class AIController : Controller
{

    public enum AIState { Guard, Chase, Attack, Flee, Patrol };
    public AIState currentState;
    private float lastStateChangeTime;
    public GameObject target;

    public Transform[] waypoints;
    public float waypointStopDistance;

    private int currentWaypoint = 0;

    public float fleeDistance;

    public float hearingDistance;

    public float sightDistance;

    public float fieldOfView;

    public bool patrol;
    public bool flee;
    public bool attack;
    public bool chase;
    public bool cansee;
    public bool canhear;

   public bool loopPatrol;


    //obstacle avoidance 

    public float speed = 5f;
    public float avoidanceDistance = 2f;
    public LayerMask obstacleMask;


    // Start is called before the first frame update
    public override void Start()

    {
        TargetPlayerOne();

        if (patrol == true)
        {
        ChangeState(AIState.Patrol);
        }
        else
        {
        ChangeState(AIState.Guard);

        }
        

    
    
        // this runs the parent base start
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        TargetPlayerOne();

    
        // make descisions
        ProcessInputs();
        
        // this runs the parent base update
        base.Update();
    }
    // this is oging to be responsible for making AI descicions 
    public override void ProcessInputs()
    {
        Debug.Log("THINKING");

        switch (currentState)
        {

            case AIState.Guard:
                //do work for guard state
                DoGuardState();
                //check for transitions
                if (IsDistanceLessThan(target, 10) || (IsCanHear(target)) || (IsCanSee(target)))
                {
                    if (chase == true)
                    {
                        ChangeState(AIState.Chase);
                    }
                    if (flee == true)
                    {
                        ChangeState(AIState.Flee);
                    }
                }

                    
                    break;

            case AIState.Chase:
                //do work for chase
                if (IsHasTarget())
                {
                    DoChaseState();
                }
                else
                {
                    TargetPlayerOne();
                }

                //check for transitions
                if (!IsDistanceLessThan(target, 10) && (!IsCanHear(target)) && (!IsCanSee(target)))
                {
                    ChangeState(AIState.Patrol);
                }

                //if close enough, attack
                if (IsDistanceLessThan(target, 7))
                {
                    if (attack == true)
                        ChangeState(AIState.Attack);
                }
                break;


            case AIState.Attack:
                //do work for attack state
                DoAttackState();
                //check for transitions
                if (!IsDistanceLessThan(target, 7))
                {
                    ChangeState(AIState.Chase);
                }
                break;



            case AIState.Flee:
                //do work for flee
                DoFleeState();
                if (!IsDistanceLessThan(target, 10) && (!IsCanHear(target)) && (!IsCanSee(target)))
                {
                    ChangeState(AIState.Guard);
                }
                    break;


            //patrol state------------------
            case AIState.Patrol:
                //do work for patrol stat
                DoPatrolState();

                
                if (IsDistanceLessThan(target, 10) || (IsCanHear(target)) || (IsCanSee(target)))
                {
                    if (chase == true)
                    {
                        ChangeState(AIState.Chase);
                    }
                }

                break;


        }
    }


    protected void DoGuardState()
    {
        //doing guard state
        Debug.Log("I am now Guarding!");

        // Rotate the pawn clockwise in place
        float rotationSpeed = 45f;
        pawn.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }




    protected void DoChaseState()
    {
        if (chase == true)
        { 
        //doing chase state
        Debug.Log("I am now chasing!");
        Seek(target);
        }
    }




    public void DoAttackState()
    {

        if (attack == true)
        {
            //doing attack state
            Debug.Log("I am now attacking!");
            // chase
            Seek(target);
            //shoot
            Shoot();
        }
    }

    //RUNNING AWAY-------------------------------------------------------------------------

    public void DoFleeState()
    {
        Debug.Log("I am running away!");

        // Calculate the direction away from the target
        Vector3 fleeDirection = pawn.transform.position - target.transform.position;
        // Normalize the flee direction
        fleeDirection.Normalize();

        // Calculate the desired position to flee to
        Vector3 fleePosition = pawn.transform.position + fleeDirection * fleeDistance;

        // Check for obstacles in the way
        RaycastHit hit;
        if (Physics.Raycast(pawn.transform.position, fleeDirection, out hit, avoidanceDistance, obstacleMask))
        {
            // If an obstacle is detected, calculate a new flee direction away from the obstacle
            fleeDirection = Vector3.Reflect(fleeDirection, hit.normal);
            fleePosition = pawn.transform.position + fleeDirection * fleeDistance;
        }

        // Move the pawn towards the flee position smoothly (using its normal movement speed)
        float moveSpeed = pawn.moveSpeed;
        pawn.transform.position = Vector3.MoveTowards(pawn.transform.position, fleePosition, moveSpeed * Time.deltaTime);

        // Rotate towards the desired flee direction (quicker rotation)
        Quaternion targetRotation = Quaternion.LookRotation(fleeDirection);
        float rotationSpeed = 360f; 
        pawn.transform.rotation = Quaternion.RotateTowards(pawn.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    //------------------------------------------------------------------------------------

    public void DoPatrolState()
    {
        //if we have enough waypoints in our list to move to a current waypoint
        if (waypoints.Length > currentWaypoint)
        {
            Debug.Log("I am now patrolling!");
            // then seek that waypoint
            Seek(waypoints[currentWaypoint]);
            //if we are close enough, then increment to the next waypoint
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
            {
                currentWaypoint++;
            }
        }
        else
        {
            RestartPatrol();
        }

    }

    protected void RestartPatrol()
    {
        //is patrtol set to loop?
        if (loopPatrol == true)
        {
            //set waypoint index back to 0
            currentWaypoint = 0;
        }
        else
        {
            DoGuardState();
        }

    }


    public void Shoot()
    {
        pawn.Shoot();
    }


    public void Seek(GameObject target)
    {
        // Do seek
        //rotate towards target
        pawn.RotateTowards(target.transform.position);
        // move forward towards target
        pawn.MoveForward();

    }
    // ---------------------------------
    // NOW TO OVERLOAD THE SEEK FUNCTION
    //----------------------------------


    public void Seek(Transform targetTransform)
    {
        //seek the position of our target transform
        Seek(targetTransform.gameObject);
    }
    public void Seek(Vector3 targetPosition)
    {
        // Calculate direction away from the target
        Vector3 direction = pawn.transform.position - targetPosition;

        // Rotate towards the calculated direction
        pawn.RotateTowards(direction);

        // Move forward towards the calculated direction
        pawn.MoveForward();
    }


    //----------------------------------



    //check if player is in range
    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void ChangeState(AIState newState)
    {
        //change our current state
        currentState = newState;
        // save the time we changed states
        lastStateChangeTime = Time.time;


    }

    public void TargetPlayerOne()
    {
        // if the GM exists
        if (GameManager.instance != null)
        {
           

            //and there are players in it
            if (GameManager.instance.players.Count > 0)
            {
                //Then target the Game Object of the pawn of the first player in the list
                target = GameManager.instance.players[0].pawn.gameObject;
            }
        }
    }

    protected bool IsHasTarget()
    {
        //return true if we have a target, false if not
        return (target != null);
    }


    protected bool IsCanHear(GameObject target)
    {
        if (canhear == false)
        {
            return false;
        }
        //get the target's noise maker
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
        //if they dont have one, they cant make a noise so return false
        if (noiseMaker == null)
        {
            return false;
        }
        //if they are making 0 noise, they also cant be heard
        if(noiseMaker.volumeDistance <= 0)
        {
            return false;
        }
        //if they are making noise, add the volume distance to the hearing distanc of the AI
        float totalDistance = noiseMaker.volumeDistance + hearingDistance;
        //if the distance between our pawn and target is close than this:
        if(Vector3.Distance(pawn.transform.position, target.transform.position) < totalDistance)
        {
            Debug.Log("I HEAR YOU");
            //-then we can hear the target
            return true;
            
        }
        else
        {
            //otherwise, we are too far away
            return false;
        }
    }

    public bool IsCanSee(GameObject target)
    {
        if(cansee == false)
        {
            return false;
        }
        //find the vector from the agent to the target
        Vector3 agentToTargetVector = target.transform.position - pawn.transform.position;
        //find then angle between the direction our agent is facing, and vectorto target
        float angleToTarget = Vector3.Angle(agentToTargetVector, pawn.transform.forward);

        if (Vector3.Distance(pawn.transform.position, target.transform.position) < sightDistance && angleToTarget < fieldOfView)
            {
                Debug.Log("in field of view");
                return true;
            }
        else
        {

            return false;
        }


    }
}