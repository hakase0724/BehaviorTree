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
            Init();
            this.func = func;
        }

        /// <summary>
        /// 処理を実行しその結果を返す
        /// </summary>
        /// <param name="instanse"></param>
        /// <returns></returns>
        public override Result Excute(NodeInstanse instanse)
        {
            Debug.Log(1);
            instanse.nodeStateDic[Key] = NodeState.Ready;
            Debug.Log(2);
            var result = func(instanse);
            Debug.Log(3);
            if (result.result)
            {
                Debug.Log(4);
                instanse.nodeStateDic[Key] = NodeState.Success;
                Debug.Log(5);
            }
            else
            {
                Debug.Log(6);
                instanse.nodeStateDic[Key] = NodeState.Failed;
                Debug.Log(7);
            }
            Debug.Log(8);
            return result;

        }

        public override void Reset()
        {
            return;
        }
    }
}


