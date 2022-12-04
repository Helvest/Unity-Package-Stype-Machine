using System;

namespace StypeMachine
{
	[Serializable]
	public class TypeMachine : StateMachine<Type>
	{

		#region Constructor

		public TypeMachine(Type firstState, bool acceptStateNotIncluded = true, bool canReenterSameState = false) :
			base(firstState, acceptStateNotIncluded, canReenterSameState)
		{ }

		#endregion

		#region Is State

		public bool IsState<t>()
		{
			return State == typeof(t);
		}

		public bool IsState<t>(t instance)
		{
			return State == instance.GetType();
		}

		#endregion

		#region Set State

		public void SetState<t>()
		{
			State = typeof(t);
		}

		public void SetState<t>(t instance)
		{
			State = instance.GetType();
		}

		#endregion

		#region Add State

		public void AddState<t>(Action<Type, Type> enterAction = default, Action<Type, Type> exitAction = default, Action updateAction = default)
		{
			base.AddState(typeof(t), enterAction, exitAction, updateAction);
		}

		public void AddState<t>(t type, Action<Type, Type> enterAction = default, Action<Type, Type> exitAction = default, Action updateAction = default)
		{
			base.AddState(type.GetType(), enterAction, exitAction, updateAction);
		}

		#endregion

		#region Remove State

		public void RemoveState<t>()
		{
			base.RemoveState(typeof(t));
		}

		public void RemoveState<t>(t type)
		{
			base.RemoveState(type.GetType());
		}

		#endregion

	}
}
