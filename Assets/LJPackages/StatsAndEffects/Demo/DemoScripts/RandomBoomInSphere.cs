using System.Collections.Generic;
using UnityEngine;

namespace LJ.Stats.Demos
{
    public class RandomBoomInSphere : MonoBehaviour
    {
        [SerializeField] float timeBetween;
        [SerializeField] StatEffect scareEffect;
        [SerializeField] float radius;
        [SerializeField] LayerMask ignoreLayers;
        [SerializeField] ParticleSystem boomFX;
        [SerializeField] private PropertyKey alertKey;
        float timer;
        EffectContext context;
        ContextProperty alertProperty;
       
        List<ContextProperty> properties = new List<ContextProperty>();
        
        private void Start()
        {
            timer = timeBetween; // Initialize the timer to the time between booms
            alertProperty = new ContextProperty(alertKey, Vector3.zero); // Initialize the alert property with a default value
            properties.Add(alertProperty);

            context = new EffectContext(scareEffect, this, Time.time, 1, properties);
        }
        void Boom()
        {
            Vector3 randomPosition = Random.insideUnitSphere * radius + transform.position; // Random position within a sphere of radius
            boomFX.transform.position = randomPosition;
            boomFX.Play();
            alertProperty.value = (Vector3)randomPosition; // Update the alert property with the new random position
            context.timeApplied = Time.time;
            StatApplierUtility.ApplyEffectInRadius(randomPosition, radius*2, scareEffect, ignoreLayers, context); // Apply the effect at the random position
        }
        private void Update()
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = timeBetween;
                Boom();
            }
        }
    }

}
