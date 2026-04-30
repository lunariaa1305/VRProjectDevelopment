using UnityEngine;
using UnityEngine.Events;
namespace LJ.Stats
{
    public class ProgressiveResponder : MonoBehaviour, IRespond
    {
        [Tooltip("The progressive responder invokes events based on a normalized value provided by the 'stat evaluator'" +
            " The 'occurences' array should be filled starting with the first thing to happen, then all the way to the last. " +
            " The occurences are invoked at percentages, progressing through the stages as the stat changes.")]
        [SerializeField] OccurenceDataPair[] occurences;
        [System.Serializable]
        public class OccurenceDataPair
        {
            public string name;
            public UnityEvent<float> statOccurence;
            [Range(0f,1f)]public float floor;
            [Range(0f,1f)]public float ceiling;
        }

        public void StatRespond(float statValue)
        {
            foreach (OccurenceDataPair pair in occurences)
            {
                if (statValue >= pair.floor && statValue <= pair.ceiling)
                {
                    pair.statOccurence?.Invoke(statValue);
                }
            }
        }

        public void EffectRespond(EffectContext token)
        {
            throw new System.NotImplementedException();
        }
    }

}
