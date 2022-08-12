using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Task_MoveAndFire : Node
{
    private Transform _transform;

    
    public Task_MoveAndFire(Transform transform)
    {
        this._transform = transform;
    }

    public override NodeState Evaluate()
    {
        //bool onScreen = (bool)GetData("OnScreen");
       
       /* if(onScreen == true)
        {*/
            _transform.position += Vector3.back * CroutonShip.moveSpeed * Time.deltaTime;
            
            state = NodeState.RUNNING;
            return state;
        //}
/*
        state = NodeState.FAILURE;
        return state;*/

    }

}
