using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x0200001A RID: 26
	public class StickNavigationManager : MonoBehaviour
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000049CD File Offset: 0x00002BCD
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000049D5 File Offset: 0x00002BD5
		public bool IsEnabled { get; set; } = true;

		// Token: 0x060000E2 RID: 226 RVA: 0x000049E0 File Offset: 0x00002BE0
		private void Start()
		{
			this._stickNavigationAction = Game.Instance.PlayerActions.UI.StickNavigation;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004A0C File Offset: 0x00002C0C
		private void Update()
		{
			GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
			Selectable selectable = null;
			Selectable selectable2;
			if (this.IsEnabled && Game.Instance.IsCurrentSchemeGamepad && currentSelectedGameObject != null && currentSelectedGameObject.TryGetComponent<Selectable>(ref selectable2))
			{
				Vector2 vector = this._stickNavigationAction.ReadValue<Vector2>();
				if (vector.x > 0.5f)
				{
					selectable = selectable2.FindSelectableOnRight();
				}
				if (selectable == null && vector.x < -0.5f)
				{
					selectable = selectable2.FindSelectableOnLeft();
				}
				if (selectable == null && vector.y > 0.5f)
				{
					selectable = selectable2.FindSelectableOnUp();
				}
				if (selectable == null && vector.y < -0.5f)
				{
					selectable = selectable2.FindSelectableOnDown();
				}
			}
			if (selectable != null)
			{
				if (this._state == StickNavigationManager.StickNavigationState.Idle)
				{
					EventSystem.current.SetSelectedGameObject(selectable.gameObject);
					this._state = StickNavigationManager.StickNavigationState.WaitingForContinousNavigation;
					this._timer = 0f;
					return;
				}
				if (this._state == StickNavigationManager.StickNavigationState.WaitingForContinousNavigation)
				{
					this._timer += Time.deltaTime;
					if (this._timer > 0.5f)
					{
						EventSystem.current.SetSelectedGameObject(selectable.gameObject);
						this._state = StickNavigationManager.StickNavigationState.InContinousNavigation;
						this._timer = 0f;
						return;
					}
				}
				else
				{
					this._timer += Time.deltaTime;
					if (this._timer > 0.2f)
					{
						EventSystem.current.SetSelectedGameObject(selectable.gameObject);
						this._timer = 0f;
						return;
					}
				}
			}
			else
			{
				this._state = StickNavigationManager.StickNavigationState.Idle;
				this._timer = 0f;
			}
		}

		// Token: 0x040000B7 RID: 183
		private InputAction _stickNavigationAction;

		// Token: 0x040000B8 RID: 184
		private StickNavigationManager.StickNavigationState _state;

		// Token: 0x040000B9 RID: 185
		private float _timer;

		// Token: 0x040000BA RID: 186
		private const float _waitingForContinousDelay = 0.5f;

		// Token: 0x040000BB RID: 187
		private const float _continousDelay = 0.2f;

		// Token: 0x040000BC RID: 188
		private const float _minStickValue = 0.5f;

		// Token: 0x02000090 RID: 144
		private enum StickNavigationState
		{
			// Token: 0x04000375 RID: 885
			Idle,
			// Token: 0x04000376 RID: 886
			WaitingForContinousNavigation,
			// Token: 0x04000377 RID: 887
			InContinousNavigation
		}
	}
}
