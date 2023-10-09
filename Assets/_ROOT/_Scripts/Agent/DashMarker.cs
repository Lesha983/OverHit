using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Settings;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Agent
{
	public class DashMarker : MonoBehaviour
	{
		[Header("Pointers")]
		[SF] SpriteRenderer _spriteRenderer;
		[SF] LineRenderer _lineRenderer;

		public Transform SpriteTransform => _spriteRenderer.transform;

		public void Show(Color color, Sprite sprite)
		{
			UpdateView(color, sprite);
			UpdatePosition(transform.position);
			_spriteRenderer.gameObject.SetActive(true);
			_lineRenderer.gameObject.SetActive(true);
		}

		public void UpdateView(Color color, Sprite sprite)
		{
			_spriteRenderer.sprite = sprite;
			_spriteRenderer.color = color;

			var startAlpha = _lineRenderer.startColor.a;
			var startColor = color;
			startColor.a = startAlpha;

			_lineRenderer.startColor = startColor;
			_lineRenderer.endColor = color;
		}

		public void UpdatePosition(Vector3 targetPos)
		{
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
			UpdatePosition(transform.position);
		}

		private void Awake()
		{
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
