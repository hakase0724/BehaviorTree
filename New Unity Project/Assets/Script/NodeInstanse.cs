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
        //終了検知
        public ReactiveProperty<NodeState> endRP = new ReactiveProperty<NodeState>(NodeState.Ready);
        //各nodeの変化を検知
        public ReactiveDictionary<string, NodeState> nodeStateDic = new ReactiveDictionary<string, NodeState>();

        private Node node;

        public NodeInstanse(Node node)
        {
            this.node = node;

            //ディクショナリ―内の値が変更されたとき
            nodeStateDic.ObserveReplace()
                .Where(x => x.Key == node.Key)
                .Where(x => x.NewValue == NodeState.Success || x.NewValue == NodeState.Failed)
                .Do(x => Debug.Log("ステート" + x))
                .Subscribe(x => End(x.NewValue));

        }

        public void Excute() => node.Excute(this);

        /// <summary>
        /// 初期化してリスタート
        /// </summary>
        public void Reset()
        {
            nodeStateDic.Clear();
            endRP.Value = NodeState.Ready;
            node.Reset();
            node.Excute(this);
        }

        private void End(NodeState state) => endRP.Value = state;
    }
}


