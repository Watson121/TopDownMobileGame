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

    private void Update()
    {
        collectablePosition += new Vector3(0, 0, (3 * Time.deltaTime) * -1);
        transform.position = collectablePosition;
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
        }
    }

}
