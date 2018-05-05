using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace BehaviorTree
{
    public class DecoratorNode : Node
    {
        private Func<NodeInstanse, Result> func;
        private Node node;

        public DecoratorNode(Func<NodeInstanse, Result> func, Node node)
        {
            Init();
            this.func = func;
            this.node = node;
        }

        public override Result Excute(NodeInstanse instanse)
        {
            Debug.Log("ノードキー" + node.Key);
            if (func(instanse).result)
            {
                Debug.Log("あ");
                instanse.nodeStateDic.ObserveReplace()
                    .Where(x => x.Key == node.Key)
                    .Subscribe(x => NextState(x.NewValue, instanse));
                Debug.Log("い");
                node.Excute(instanse);
                Debug.Log("う");
            }
            else
            {
                Debug.Log("え");
                instanse.nodeStateDic[Key] = NodeState.Failed;
                Debug.Log("お");
            }
            return new Result(true);
        }

        public override void Reset()
        {

        }

        private void NextState(NodeState state, NodeInstanse instanse)
        {
            Debug.Log("ねくすと");
            if (state == NodeState.Failed) instanse.nodeStateDic[Key] = NodeState.Failed;
            else if (state == NodeState.Success) instanse.nodeStateDic[Key] = NodeState.Success;
        }
    }
}

