using System;
using System.Collections;
using AliceTools.Pool;
using UnityEngine;

namespace HordeProtocol.Gameplay.Enemy
{
    public class BulletDestroy : MonoBehaviour, IPoolDestroy
    {
        protected Action destroyAction;

        public void Destroy()
        {
            if(destroyAction is not null)
                destroyAction();
        }

        public void SetDestroyAction(Action action)
        {
            destroyAction ??= action;
        }

        private void OnEnable() => StartCoroutine(OnEnableIE());

        private IEnumerator OnEnableIE()
        {
            yield return new WaitForSeconds(5f);
            Destroy();
        }
    }
}