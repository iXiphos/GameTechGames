using UnityEngine;
using System.Collections;
namespace StateStuff
{
    public class StateMachine<T> : MonoBehaviour
    {
        public State<T> currentState { get; private set; }
        public T Owner;

        public StateMachine(T _o)
        {
            Owner = _o;
            currentState = null;
        }
        
        public void ChangeState(State<T> _newState)
        {
            if (currentState != null) currentState.exitState(Owner);
            currentState = _newState;
            currentState.enterState(Owner);
        }

        public void Update()
        {
            if (currentState != null) currentState.updateState(Owner);
        }

    }

    public abstract class State<T>
    {
        public abstract void enterState(T _owner);
        public abstract void exitState(T _owner);
        public abstract void updateState(T _owner);

    }
}
