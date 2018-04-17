using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{

    /// <summary>
    /// すべてのノードで一意のもの
    /// </summary>
    public class Unique
    {
        public static int num = 0;
    }

    /// <summary>
    /// 各ノードが継承するベースクラス
    /// </summary>
    public abstract class Node
    {
        //自分を一意に特定するためのキー
        public string Key { get; private set; }

        /// <summary>
        /// 初期メソッド
        /// </summary>
        public virtual void Init()
        {
            //自分の識別用番号
            int myNum = Unique.num;
            Unique.num++;
            Key = myNum.ToString();
        }

        /// <summary>
        /// 初期化メソッド
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// 実行
        /// </summary>
        /// <returns></returns>
        public abstract Result Excute(NodeInstanse instanse);


    }

   
}

