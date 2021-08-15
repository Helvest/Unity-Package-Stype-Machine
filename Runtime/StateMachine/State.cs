using UnityEngine;

namespace StypeMachine
{
	public class State : MonoBehaviour, IState
	{
		public virtual void EnterState() { }

		public virtual void ExitState() { }

		public virtual void UpdateState() { }
	}
}

