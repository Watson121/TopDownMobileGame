using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Behviour Tree Class
/// </summary>

namespace BehaviourTree
{

    public abstract class Tree : MonoBehaviour
    {
        protected Node rootNode = null;

        protected void Start()
        {
            rootNode = SetupTree();
        }

        protected void Update()
        {
            if(rootNode != null)
            {
                rootNode.Evaluate();
            }
        }

        protected abstract Node SetupTree();
    }

}
