using System.Collections;
using UnityEngine;

namespace LJ.Stats.Demos
{
    public class LookAtSource : MonoBehaviour
    {
        Vector3 source; // the source position to look at
        float stareTime = 1f;
        Coroutine stare;
        [SerializeField] PropertyKey alertKey;
        public void ReceiveTrigger(EffectContext context)
        {
            ContextProperty property = context.GetProperty<Vector3>(alertKey); //get a vector3 property from the effect called sourceposition
            source = (Vector3)property.value;
            if (stare != null)
            {
                StopCoroutine(stare);
            }
            stare = StartCoroutine(StareForABit());
            
        }

        IEnumerator StareForABit()
        {
            transform.LookAt(source);   
            yield return new WaitForSeconds(stareTime);
            transform.rotation = Quaternion.identity;
        }
    }

}
