namespace Chillplay.Tools.SimpleStateMachine
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using NaughtyAttributes;
	using UnityEngine;
	using UnityEngine.Assertions;

	public class SimpleStateMachine<TState> : MonoBehaviour where TState : State
	{
		[SerializeField, ReadOnly]
		private TState[] States;

		[SerializeField, ReadOnly]
		protected TState activeState;

		[SerializeField]
		private TState initialState;

		private Dictionary<Type, TState> states;

		private void OnValidate()
		{
			FindReferences();
		}

		[Button]
		private void FindReferences()
		{
			States = GetComponentsInChildren<TState>();

			foreach (var state in States)
			{
				state.FindReferences();
			}
		}

		private void Awake() => Initialize();

		private void Initialize()
		{
			states = new Dictionary<Type, TState>();
			foreach (var state in States)
			{
				state.gameObject.SetActive(false);
				states.Add(state.GetType(), state);
			}
		}

		private void Start()
		{
			EnterInitialState();
		}

		public void Enter<TTarget>() where TTarget : TState => Enter(typeof(TTarget));

		public void Enter(Type type)
		{
			Assert.IsNotNull(type);
			Assert.IsTrue(type.IsSubclassOf(typeof(TState)));
			
			TState state = ChangeState(type);
			state.gameObject.SetActive(true);
			state.Enter();
		}

		public bool StateExists(Type type)
		{
			if (type == null) return false;
			if (!type.IsSubclassOf(typeof(TState))) return false;
			if (states.ContainsKey(type)) return true;
			if (states.Any(pair => pair.Key.IsSubclassOf(type))) return true;

			return false;
		}

		private void EnterInitialState()
		{
			activeState = initialState;
			activeState.gameObject.SetActive(true);
			activeState.Enter();
		}

		// private TTarget ChangeState<TTarget>() where TTarget : TState
		// {
		// 	ExitActiveState();
		// 	TTarget state = GetState<TTarget>();
		// 	activeState = state;

		// 	return state;
		// }

		private TState ChangeState(Type type)
		{
			ExitActiveState();
			TState state = GetState(type);
			activeState = state;

			return state;
		}

		private void ExitActiveState()
		{
			if (activeState != null)
			{
				activeState.Exit();
				activeState.gameObject.SetActive(false);
			}
		}

		// private TTarget GetState<TTarget>() where TTarget : TState
		// {
		// 	return states[typeof(TTarget)] as TTarget;
		// }

		private TState GetState(Type type)
		{
			if (states.ContainsKey(type)) return states[type];
			return states.First(pair => pair.Key.IsSubclassOf(type)).Value;
		}

		private TReference FindReference<TReference>() where TReference : MonoBehaviour
		{
			return GetComponentsInParent<TReference>(true).FirstOrDefault();
		}
	}
}