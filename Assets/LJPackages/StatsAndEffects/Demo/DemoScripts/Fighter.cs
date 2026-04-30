using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace LJ.Stats.Demos
{
    public class Fighter : MonoBehaviour
    {
        int hits;
        public UnityEvent OnPunch;
        public Animator anims;
        public UnityEvent OnStartBlock;
        public UnityEvent OnEndBlock;
        public void Punch(InputAction.CallbackContext callback)
        {
            if (callback.started)
            {
                DoPunch();
            }
        }
        void DoPunch()
        {
            hits += 1;
            if (hits % 2 == 0)
            {
                anims.SetTrigger("Punch2");
            }
            else anims.SetTrigger("Punch1");
            OnPunch?.Invoke();
        }
        public void EvaluateBlock(InputAction.CallbackContext callback)
        {
            if (callback.started) StartBlock();
            else if (callback.canceled) EndBlock();
        }
        public void StartBlock()
        {
            OnStartBlock?.Invoke();
            anims.SetBool("Blocking", true);
        }
        public void EndBlock()
        {
            OnEndBlock?.Invoke();
            anims.SetBool("Blocking", false);
        }
    }
}


