using LJ.Stats.Custom;
using System.Linq.Expressions;
using UnityEngine;

namespace LJ.Stats.Demos
{
    public class FallDamage : MonoBehaviour
    {
        [SerializeField] float damageVelocity; // velocity at which damage kicks in
        [SerializeField] StatEffectData defaultImpactData;
        [SerializeField] float defaultFallDamage;
        LocalStatHandler stats;
        EffectContext context; 
        private void Start()
        {
            stats = transform.StatHandler(); // short way to grab stat handler of a transform
            context = new EffectContext(null, this, Time.time);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.relativeVelocity.magnitude >= damageVelocity)
            {
                context.timeApplied = Time.time;
                StatModifier modifier = new StatModifier(StatModifierType.Add, -defaultFallDamage*collision.relativeVelocity.magnitude/damageVelocity);
                stats.TryApplySingleDataEffect(defaultImpactData.WithModifier(modifier), context); // you can apply modifiers with extension methods
            }
        }
    }

}
