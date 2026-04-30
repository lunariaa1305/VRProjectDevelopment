using UnityEngine;
namespace LJ.Stats
{
    public interface IRespond
    {
        public void StatRespond(float statValue);

        public void EffectRespond(EffectContext token);
    }

}
