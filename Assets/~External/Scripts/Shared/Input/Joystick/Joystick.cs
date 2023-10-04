namespace Chillplay.Input.Joystick
{
    using System;
    using Input;
    using Drags;
    using UnityEngine;
    using Zenject;

    public class Joystick : IInitializable
    {
        [Inject]
        public IInputProvider InputProvider { get; set; }

        public event Action OnStart;

        public event Action OnEnd;

        public event Action<Vector2> OnDrag;

        public float Size { get; set; }

        public float Threshold { get; set; }

        public Vector2 Direction => Distance <= Threshold ? Vector2.zero : (Position - StartPosition).normalized;

        public float Distance => (Position - StartPosition).magnitude;

        public Vector2 StartPosition { get; private set; }

        public Vector2 Position { get; private set; }

        private bool isDragging;

        public const bool IsFloating = false;

        public void Initialize()
        {
            InputProvider.OnDragStart += OnDragStart;
            InputProvider.OnDrag += ProcessDrag;
            InputProvider.OnDragEnd += OnDragEnd;
        }

        private void OnDragStart(Drag drag)
        {
            isDragging = true;
            StartPosition = Position = drag.StartPosition;
            OnStart?.Invoke();
        }

        private void ProcessDrag(Drag drag)
        {
            Position = drag.CurrentPosition;
            var delta = Position - StartPosition;
            if (IsFloating && delta.magnitude > Size)
            {
                StartPosition = Position - delta.normalized * Size;
            }
            delta = Vector2.ClampMagnitude(delta, Size);
            Position = StartPosition + delta;
            OnDrag?.Invoke(Direction * Distance / Size);
        }

        private void OnDragEnd(Drag drag)
        {
            EndDrag();
        }

        private void EndDrag()
        {
            isDragging = false;
            OnEnd?.Invoke();
        }

        public void Enable()
        {
            InputProvider.OnDragStart += OnDragStart;
            InputProvider.OnDrag += ProcessDrag;
            InputProvider.OnDragEnd += OnDragEnd;
        }

        public void Disable()
        {
            if (isDragging) EndDrag();
            InputProvider.OnDragStart -= OnDragStart;
            InputProvider.OnDrag -= ProcessDrag;
            InputProvider.OnDragEnd -= OnDragEnd;
        }
    }
}