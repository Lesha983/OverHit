using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Settings;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Agent
{
	public class DashMarker : MonoBehaviour
	{
		public enum Type
		{
			None,
			Move,
			Attack
		}

		[Header("Settings")]
		[SF] DashMarkerSettings _settings;
		[Header("Pointers")]
		[SF] SpriteRenderer _spriteRenderer;
		[SF] LineRenderer _lineRenderer;

		private Type _currentType;

		public Transform SpriteTransform { get; private set; }

		public void UpdateType(Type type)
		{
			if (_currentType == type)
				return;

			switch (type)
			{
				case Type.Move:
					SetView(_settings.MarkerMoveColor, _settings.MarkerMoveSprite);
					break;
				case Type.Attack:
					SetView(_settings.MarkerAttackColor, _settings.MarkerAttackSprite);
					break;
			}

			_currentType = type;
		}

		public void Show()
		{
			UpdatePosition(transform.position);
			_spriteRenderer.gameObject.SetActive(true);
			_lineRenderer.gameObject.SetActive(true);
		}

		public void UpdatePosition(Vector3 targetPos)
		{
			if (_currentType == Type.None)
				return;

			SpriteTransform.position = targetPos;
			var positions = new Vector3[]
			{
				transform.position,
				targetPos
			};
			_lineRenderer.SetPositions(positions);
		}

		public void Hide()
		{
			_spriteRenderer.gameObject.SetActive(false);
			_lineRenderer.gameObject.SetActive(false);
		}

		private void Awake()
		{
			SpriteTransform = _spriteRenderer.transform;
			Hide();
		}

		private void SetView(Color color, Sprite sprite)
		{
			_spriteRenderer.sprite = sprite;
			_spriteRenderer.color = color;

			var startAlpha = _lineRenderer.startColor.a;
			var startColor = color;
			startColor.a = startAlpha;

			_lineRenderer.startColor = startColor;
			_lineRenderer.endColor = color;
		}
	}
}
