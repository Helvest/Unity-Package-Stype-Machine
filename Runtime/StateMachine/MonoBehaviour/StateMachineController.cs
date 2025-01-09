using System;
using UnityEngine;

namespace HFSM
{
	public abstract class StateMachineController<TStateId> : MonoBehaviour, IHoldStateMachine<TStateId>
	{

		#region Fields

		[Header("StateMachineController")]

		[SerializeField]
		protected bool apceptValuesNotIncluded;

		[SerializeField]
		protected bool canReenterSameState;

		[SerializeField]
		protected TStateId startState;

		public StateMachine<TStateId> StateMachine { get; private set; }

		protected bool hasStarted;

		#endregion

		#region Init

		protected virtual void Awake()
		{
			CreateStateMachine();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
			StateMachine.useDebug = useDebug;
#endif
		}

		protected virtual void Start()
		{
			hasStarted = true;
			ToStartState();
		}

		protected virtual void CreateStateMachine()
		{
			StateMachine = new StateMachine<TStateId>();
		}

		protected virtual void OnEnable()
		{
			ToStartState();
		}

		protected virtual void ToStartState()
		{
			if (!hasStarted || startState == null)
			{
				return;
			}

			this.SetState(startState);
		}

		#endregion

		#region Debug

#if UNITY_EDITOR || DEVELOPMENT_BUILD

		[Header("Debug")]
		[SerializeField]
		protected bool useDebug;

#endif

		#endregion

	}
}
