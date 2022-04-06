using System;
using UnityEngine;

namespace StypeMachine
{
	public abstract class State : MonoBehaviour, IState
	{
		public virtual void EnterState(Type preState, Type nexState) { }

		public virtual void ExitState(Type preState, Type nexState) { }

		public virtual void UpdateState() { }
	}
}

