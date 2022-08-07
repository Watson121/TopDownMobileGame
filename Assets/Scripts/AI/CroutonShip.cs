using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class CroutonShip : Tree
{
    public static float moveSpeed = 10.0f;
    public static float fireSpeed = 3.0f;
    public static float damage = 5.0f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node> 
        { 
            new Sequence(new List<Node>
            {
                new Task_MoveAndFire(transform),
            }),
        });

        return root;
    }
}
