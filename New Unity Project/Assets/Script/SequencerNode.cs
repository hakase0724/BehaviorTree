using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


namespace BehaviorTree
{
    public class SequencerNode : Node
    {
        private IEnumerator<Node> actions;

        public SequencerNode(Node[] actions)
        {
            Init();
            this.actions = actions.ToList().GetEnumerator();
        }

        public override Result Excute(NodeInstanse instanse)
        {
            instanse.nodeStateDic[Key] = NodeState.Ready;
            instanse.nodeStateDic.ObserveReplace()
                .Where(x => x.Key == actions.Current.Key)
                .Subscribe(x => NextState(x.NewValue,instanse));
            actions.MoveNext();
            actions.Current.Excute(instanse);
            return new Result(true);
        }

        public override void Reset()
        {
            actions.Reset();
            while (actions.MoveNext())
            {
                actions.Current.Reset();
            }
            actions.Reset();
        }

        private void NextState(NodeState state,NodeInstanse instanse)
        {
            if (state == NodeState.Failed) instanse.nodeStateDic[Key] = NodeState.Failed;
            else if(state == NodeState.Success) actions.Current.Excute(instanse);
            else instanse.nodeStateDic[Key] = NodeState.Success;
        }
    }
}

