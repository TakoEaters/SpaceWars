using UnityEngine;

namespace _Project.Scripts.AI
{
    public class Bot : AIController
    {
        public void Initialize()
        {
            Animator = GetComponent<Animator>();
            
            FindServices();
            
        }
    }
}
