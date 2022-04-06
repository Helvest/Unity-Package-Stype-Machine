using System;

namespace StypeMachine
{
	public interface IState
	{
		void EnterState(Type preState, Type nexState);
		void ExitState(Type preState, Type nexState);
		void UpdateState();
	}
}

