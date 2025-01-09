using System.Collections.Generic;
using UnityEngine;

namespace HFSM
{
	public abstract class FlagMachineController<T> : MonoBehaviour, IHoldFlagMachine<T>
	{

		#region Fields

		[Header("StateMachineController")]

		[SerializeField]
		protected bool apceptFlagNotIncluded;

		[SerializeField]
		protected bool canReenterSameFlag;

		[SerializeField]
		protected List<T> startFlags;

		public FlagMachine<T> FlagMachine { get; private set; }

		protected bool hasStarted;

		#endregion

		#region Init

		protected virtual void Awake()
		{
			CreateFlagMachine();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
			FlagMachine.useDebug = useDebug;
#endif
		}

		protected virtual void Start()
		{
			hasStarted = true;
			ToStartState();
		}

		protected virtual void CreateFlagMachine()
		{
			FlagMachine = new FlagMachine<T>(apceptFlagNotIncluded, canReenterSameFlag);
		}

		protected virtual void OnEnable()
		{
			ToStartState();
		}

		protected virtual void ToStartState()
		{
			if (!hasStarted)
			{
				return;
			}

			this.SetFlags(startFlags);
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
