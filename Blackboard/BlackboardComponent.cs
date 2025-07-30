using UnityEngine;

namespace Pampero.Blackboard
{
    public class BlackboardComponent : MonoBehaviour
    {
        public Blackboard Blackboard { get; protected set; } = new Blackboard();  

        private void OnEnable()
        {
            //// Add test values
            //Blackboard.Set("MyFloat", 3.14f);
            //Blackboard.Set("MyInt", 42);
            //Blackboard.Set("MyBool", true);
            //Blackboard.Set("MySelf", this);
            //Blackboard.Set("MyFloat1", 3.14f);
            //Blackboard.Set("MyInt1", 42);
            //Blackboard.Set("MyBool1", true);
            //Blackboard.Set("MySelf1", this);
            //Blackboard.Set("MyFloat2", 3.14f);
            //Blackboard.Set("MyInt2", 42);
            //Blackboard.Set("MyBool2", true);
            //Blackboard.Set("MySelf2", this);
        }
    }
}
//EOF.