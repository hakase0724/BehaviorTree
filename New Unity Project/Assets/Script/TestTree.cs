using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace BehaviorTree
{
    public class TestTree : MonoBehaviour
    {
        private NodeInstanse nodeInstanse;

        void Start()
        {
            var attack = new DecoratorNode(IsHpLessThan, new ActionNode(Attack));

            var move = new SequencerNode(new Node[] { new ActionNode(MoveNearEnemy), new ActionNode(Wait) });

            var root = new SelectorNode(new Node[] { attack, move });

            nodeInstanse = new NodeInstanse(root);
            nodeInstanse
                .endRP
                .Where(x => x != NodeState.Ready)
                .Do(x => Debug.Log("処理が終わった模様"))
                .Subscribe(_=> CoroutineStart());
            nodeInstanse.Excute();
        }

        private Result IsHpLessThan(NodeInstanse instanse)
        {
            var hp = Random.Range(0, 10);
            Debug.Log(hp);
            if (hp <= 4)
            {
                Debug.Log("体力が4以下だ。突撃―ッ！");
                return new Result(true);
            }
            else
            {
                Debug.Log("体力が4より大きい。警戒を怠るな！");
                return new Result(false);
            }
        }

        private Result Attack(NodeInstanse instanse)
        {
            Debug.Log("攻撃！");
            return new Result(true);
        }

        private Result MoveNearEnemy(NodeInstanse instanse)
        {
            Debug.Log("最も近い敵へ向かう！");
            return new Result(true);
        }

        private Result Wait(NodeInstanse instanse)
        {
            Debug.Log("待機");
            return new Result(true);
        }

        private void CoroutineStart()
        {
            StartCoroutine(WaitCoroutine());
        }

        private IEnumerator WaitCoroutine()
        {
            Debug.Log("1秒待機");
            yield return new WaitForSeconds(1f);
            Debug.Log("1秒待機終了。次の動作へ移行する");
            nodeInstanse.Reset();
        }
    }
}

