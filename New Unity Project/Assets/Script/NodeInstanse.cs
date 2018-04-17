using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace BehaviorTree
{
    /// <summary>
    /// ノードの状態
    /// </summary>
    public enum NodeState
    {
        //処理中
        Ready,
        //処理成功
        Success,
        //処理失敗
        Failed
    }


    public class NodeInstanse
    {
        public ReactiveProperty<NodeState> endRP = new ReactiveProperty<NodeState>(NodeState.Ready);

        public ReactiveDictionary<string, NodeState> nodeStateDic = new ReactiveDictionary<string, NodeState>();

        public NodeInstanse(Node node)
        {

        }
    }
}


