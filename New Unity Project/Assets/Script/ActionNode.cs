using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// 処理を実行する
    /// </summary>
    public class ActionNode : Node
    {
        //実行する処理
        private Func<NodeInstanse, Result> func;

        public ActionNode(Func<NodeInstanse, Result> func)
        {
            this.func = func;
        }

        /// <summary>
        /// 処理を実行しその結果を返す
        /// </summary>
        /// <param name="instanse"></param>
        /// <returns></returns>
        public override Result Excute(NodeInstanse instanse)
        {
            instanse.nodeStateDic[Key] = NodeState.Ready;
            var result = func(instanse);
            if (result.result) instanse.nodeStateDic[Key] = NodeState.Success;
            else instanse.nodeStateDic[Key] = NodeState.Failed;
            return result;

        }

        public override void Reset()
        {
            return;
        }
    }
}


