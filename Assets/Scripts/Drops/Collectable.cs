using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    EMoney,
    EShield,
    EHealth
}


public class Collectable : MonoBehaviour
{
    private Vector3 collectablePosition;
    private Transform collectable;

    private bool collectableMoving;
    private float collectableSpeed = 3.0f;
    private float collectableLifetime = 5.0f;

    private Vector3 poolZone;

    public bool IsActive
    {
        get { return isActive; }
    }
    private bool isActive = false;

    private void Awake()
    {
        collectable = this.transform;
        poolZone = collectable.position;
    }

    public CollectableType TypeOfCollectable
    {
        get { return typeOfCollectable; }
    }
    protected CollectableType typeOfCollectable;


    protected virtual void CollectableReaction()
    {
        Debug.Log("This item has been collected");
    }

    protected void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject.GetComponent<ICollectable>();

        if(obj != null)
        {
            obj.Collect(this);
            ResetCollectable();
        }
    }

    public IEnumerator CollectableMovement()
    {
        float elaspedTime = 0;
        collectableMoving = true;
        isActive = true;

        collectablePosition = transform.position;

        while ((elaspedTime < collectableLifetime) && collectableMoving)
        {
            collectablePosition += new Vector3(0, 0, (3 * Time.deltaTime) * -1);
            transform.position = collectablePosition;
            elaspedTime += Time.deltaTime;

            yield return null;
        }

        ResetCollectable();
    }

    /// <summary>
    /// Reseting the Collectable, so that it can be respawned
    /// </summary>
    private void ResetCollectable()
    {
        collectable.position = poolZone;
        collectableMoving = false;
        isActive = false;
        StopAllCoroutines();
    }
}
