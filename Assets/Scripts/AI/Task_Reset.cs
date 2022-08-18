using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;



public class Task_Reset : Node
{
    private Transform _transform;
    private Vector3 _resetPosition;
    private BaseEnemy _croutonShip;
   

    public Task_Reset(Transform transform, Vector3 resetPosition, BaseEnemy croutonShip)
    {
        this._transform = transform;
        this._resetPosition = resetPosition;
        this._croutonShip = croutonShip;

    }

    public override NodeState Evaluate()
    {
        _transform.position = _resetPosition;
        _croutonShip.IsActive = false;
        _croutonShip.GameManager.Points += (uint)_croutonShip.PointsValue;

        state = NodeState.SUCCESS;
        return state;
    }
}
