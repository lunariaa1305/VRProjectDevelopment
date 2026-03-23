using LJ.Utilities;
using UnityEngine;
namespace LJ.Stats
{
    [System.Serializable]
    public class StatModifier : INameable
    {
        [HideInInspector] public string name;
        public StatModifierType modType;
        public float floatValue;

        float timeStamp;
        public void SetName()
        {
            name = modType.ToString();
        }
        public StatModifier(StatModifierType modType, float floatValue, Vector3 vectorValue = default(Vector3))
        {
            this.modType = modType;
            this.floatValue = floatValue;
        }
        public void Evaluate(object inputStat, out object output)
        {
            if (inputStat.GetType() == typeof(float))
            {
                float val = (float)inputStat;
                switch (modType)
                {
                    case StatModifierType.Add:
                        output = val + floatValue;
                        return;
                    case StatModifierType.Multiply:
                        output = val * floatValue;
                        return;
                    case StatModifierType.Set:
                        output = floatValue;
                        return;
                }
            }
            output = inputStat;
        }
    }
}