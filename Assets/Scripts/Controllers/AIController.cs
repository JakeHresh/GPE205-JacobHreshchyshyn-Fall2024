using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Guard, Scan, Chase, Attack, BackToPost, Flee, Patrol };

    public AIState currentState;

    protected float lastStateChangeTime;

    public GameObject target;

    // Start is called before the first frame update
    public override void Start()
    {
        ChangeState(currentState);

        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Make Decisions
        ProcessInputs();

        base.Update();
    }

    public override void ProcessInputs()
    {
        Debug.Log("Making Decisions");
        switch (currentState)
        {
            case AIState.Guard:
                // Do the behaviors associated with our Guard state
                Debug.Log("Do Guard");
                // Check for transitions OUT of our Guard state

                // If true, we transition OUT of the Guard state into another state
                break;
            case AIState.Scan:
                Debug.Log("Do Scan");
                break;
            case AIState.Chase:
                Debug.Log("Do Chase");
                // Do all the behaviors associated with our Chase state
                DoChaseState();
                // Check for transitions OUT of Chase
                if (!IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Guard);
                }
                // If true, we transition OUT of Chase
                break;
            case AIState.Attack:
                // Do all the behaviors associated with out Attack state
                DoAttackState();
                // Check for transitions OUT of Attack

                // If true, transition OUT of Attack
                break;
        }
    }

    // States
    protected void DoGuardState()
    {
        // Do Nothing
    }

    protected void DoChaseState()
    {
        // Seek in the Seek State
        Seek(target);
    }

    protected void DoAttackState()
    {
        // Chase after the target
        Seek(target.transform);
        // Tell our pawn to Shoot
        Shoot();
    }

    // Behaviors
    protected void Seek(GameObject target)
    {
        // Rotate towards the target
        pawn.RotateTowards(target.transform.position);
        // Move Forward
        pawn.MoveForward();
    }

    protected void Seek(Transform targetTransform)
    {
        Seek(targetTransform.gameObject);
    }

    protected void Shoot()
    {
        // Tell the pawn to shoot
        pawn.Shoot();
    }

    // Helper Methods and Transition Methods
    public virtual void ChangeState (AIState newState)
    {
        // Change the current state
        currentState = newState;
        // Save the time when we changed states
        lastStateChangeTime = Time.time;
    }

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
}
