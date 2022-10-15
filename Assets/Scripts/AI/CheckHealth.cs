using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;


public class CheckHealth : Node
{

    private BaseEnemy _controller;
    private SpawningManager _spawningManager;

    public CheckHealth(BaseEnemy controller)
    {
        _controller = controller;
        _spawningManager = GameObject.Find("SpawningManager").GetComponent<SpawningManager>();
    }

    public override NodeState Evaluate()
    {
        if(_controller != null){



            if (_controller.Health == 0)
            {
                _controller.GameManager.PointUpdateHandler((uint)_controller.PointsValue);
                _spawningManager.SpawnCollectable(_controller.gameObject.transform.position);
                state = NodeState.FAILURE;
                return state;
            }

         
        }

        state = NodeState.SUCCESS;
        return state;

    }
}
