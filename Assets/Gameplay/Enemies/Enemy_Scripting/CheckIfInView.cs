using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Viewport;

public class CheckIfInView : Node
{
    private Transform _transform;

    public CheckIfInView(Transform transform)
    {
        this._transform = transform;
    }

    public override NodeState Evaluate()
    {

        if(_transform.position.z < ViewportBoundaries.cameraPositionZ)
        {
            state = NodeState.FAILURE;
            return state;
        }
        else if(_transform.position.z > ViewportBoundaries.cameraPositionZ)
        {
            state = NodeState.SUCCESS;
            return state;
        } 

        state = NodeState.SUCCESS;
        return state;
    }
}
