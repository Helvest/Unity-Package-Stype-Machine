using System.Collections.Generic;
using UnityEngine;

namespace StypeMachine
{
	public class MonoTypeMachine : TypeMachineController<MonoBehaviour>
	{
		[Header("MonoTypeMachine")]

		[SerializeField]
		private Transform _instanceParent = default;

		[SerializeField]
		protected List<MonoBehaviour> statesPrefab = default;

		[SerializeField]
		protected List<MonoBehaviour> states = default;

		protected override void CreateTypeMachine()
		{
			base.CreateTypeMachine();

			foreach (var state in states)
			{
				if (state != null)
				{
					AddState(state,
						enterAction: (P, N) => state.gameObject.SetActive(true),
						exitAction: (P, N) => state.gameObject.SetActive(false),
						updateAction: null
					);

					state.gameObject.SetActive(false);
				}
			}

			if (_instanceParent == null)
			{
				TryGetComponent(out _instanceParent);
			}

			foreach (var state in statesPrefab)
			{
				if (state != null)
				{
					var prefab = state;
					MonoBehaviour instance = null;

					AddState(state,
						enterAction: (P, N) =>
						{
							if (P == N && instance != null)
							{
								instance.gameObject.SetActive(true);
							}
							else if (prefab != null)
							{
								instance = Instantiate(prefab, _instanceParent);
							}
						},
						exitAction: (P, N) =>
						{
							if (instance != null)
							{
								if (P == N)
								{
									instance.gameObject.SetActive(false);
								}
								else
								{
									Destroy(instance);
								}
							}
						}
					);
				}
			}
		}
	}
}
