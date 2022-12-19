using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sequence Class, only if all of the child nodes succeeds, then the node itself will be successful
/// </summary>


namespace BehaviourTree
{

    public class Selector : Node
    {

        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }


        public override NodeState Evaluate()
        {
            foreach (Node node in childrenNodes)
            {
     
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }
    }

}
