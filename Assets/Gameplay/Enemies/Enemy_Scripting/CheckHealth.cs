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
        _controller.m_OnDeath.AddListener(() => _spawningManager.SpawnCollectable(_controller.gameObject.transform.position));
    }

    public override NodeState Evaluate()
    {
        if(_controller != null){



            if (_controller.Health == 0)
            {
                _controller.m_OnDeath.Invoke();
                state = NodeState.FAILURE;
                return state;
            }

         
        }

        state = NodeState.SUCCESS;
        return state;

    }
}
