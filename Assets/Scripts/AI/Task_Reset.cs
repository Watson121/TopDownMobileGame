using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Task_Reset : Node
{
    private Transform _transform;
    private Vector3 _resetPosition;
    private CroutonShip _croutonShip;

    public Task_Reset(Transform transform, Vector3 resetPosition, CroutonShip croutonShip)
    {
        this._transform = transform;
        this._resetPosition = resetPosition;
        this._croutonShip = croutonShip;
    }

    public override NodeState Evaluate()
    {
        _transform.position = _resetPosition;
        _croutonShip.IsActive = false;

        state = NodeState.SUCCESS;
        return state;
    }
}
