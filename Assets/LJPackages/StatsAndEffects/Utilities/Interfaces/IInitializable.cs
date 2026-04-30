using UnityEngine;

namespace LJ.Utilities
{
    public delegate void Initialize();
    public interface IInitializable
    {
        public event Initialize onInitializationComplete;

        public void BroadcastInitializationComplete(); // this interface allows scripts to broadcast an event "initialized" so that there is no problem with script execution order
    }

}
