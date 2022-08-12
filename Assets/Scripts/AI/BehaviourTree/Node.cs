using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Base Node Class
/// </summary>

namespace BehaviourTree
{

    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class Node
    {
        // Current State of the node
        protected NodeState state;

        // The parent node
        public Node parent;
        protected List<Node> childrenNodes = new List<Node>();

        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        #region Constructors

        // Base Constructor
        public Node()
        {
            parent = null;
        }

        // Node Consturctor so that it can have child nodes
        public Node(List<Node> children)
        {
            foreach(Node child in children)
            {
                Attach(child);
            }
        }

        #endregion

        // Attaching child nodes to the parent
        private void Attach(Node node)
        {
            node.parent = this;
            childrenNodes.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if(_dataContext.TryGetValue(key, out value))
            {
                return value;
            }

            Node node = parent;
            while(node != null)
            {
                value = node.GetData(key);
                if(value != null)
                {
                    return value;
                }
                node = node.parent;
            }
            return null; 

        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }
                node = node.parent;
            }
            return false;

        }

    }

}
