using UnityEngine;

namespace StypeMachine
{

	public abstract class TypeMachineControllerIState<T> : TypeMachineController<T> where T : IState
	{

		#region Variables

		[SerializeField]
		protected T[] _states = default;

		#endregion

		#region Init

		protected override void Awake()
		{
			base.Awake();

			foreach (var state in _states)
			{
				AddState(state, state.EnterState, state.ExitState, state.UpdateState);
			}
		}

		#endregion

	}

}

