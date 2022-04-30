using System;
using UnityEngine;

namespace StypeMachine
{
	public abstract class StateMachineController<T> : MonoBehaviour, IHoldStateMachine<T> where T : Enum
	{

		#region Fields

		[Header("StateMachineController")]

		[SerializeField]
		protected bool apceptValuesNotIncluded = false;

		[SerializeField]
		protected bool canReenterSameState = false;

		[SerializeField]
		protected T startState = default;

		public StateMachine<T> StateMachine { get; private set; }

		protected bool hasStarted = false;

		#endregion

		#region Init

		protected virtual void Awake()
		{
			CreateTypeMachine();

#if UNITY_EDITOR
			StateMachine.useDebug = useDebug;
#endif
		}

		protected virtual void Start()
		{
			hasStarted = true;
			ToStartState();
		}

		protected virtual void CreateTypeMachine()
		{
			StateMachine = new StateMachine<T>(default(T), apceptValuesNotIncluded, canReenterSameState);
		}

		protected virtual void OnEnable()
		{
			ToStartState();
		}

		protected virtual void OnDisable()
		{
			StateMachine.ToDefaultState();
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

#if UNITY_EDITOR

		[Header("Debug")]
		[SerializeField]
		protected bool useDebug = false;

#endif

		#endregion

	}
}

