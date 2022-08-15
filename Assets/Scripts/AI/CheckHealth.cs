using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;


public class CheckHealth : Node
{

    private BaseEnemy _controller;

    public CheckHealth(BaseEnemy controller)
    {
        _controller = controller;
    }

    public override NodeState Evaluate()
    {
        if(_controller != null){
            
            if(_controller.Health == 0)
            {
                state = NodeState.FAILURE;
                return state;
            }

        }

        state = NodeState.SUCCESS;
        return state;

    }
}
