using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x0200001C RID: 28
	public class VerticalScrollablePanel : ScrollRect
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00004CF4 File Offset: 0x00002EF4
		protected override void Awake()
		{
			this._bindings = base.GetComponent<VerticalScrollablePanelBindings>();
			this._gamepadScrollAction = Game.Instance.PlayerActions.UI.GamepadScroll;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004D2C File Offset: 0x00002F2C
		private void Update()
		{
			this._canScroll = (base.content.rect.height > base.viewport.rect.height);
			this.HandleGamepadScroll();
			Vector3 localPosition = base.content.localPosition;
			localPosition.y += this._scrollSpeed * Time.deltaTime;
			base.content.localPosition = localPosition;
			this._scrollSpeed *= Mathf.Max(0f, 1f - 8f * Time.deltaTime);
			this.RefreshGamepadBindings();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004DCA File Offset: 0x00002FCA
		public void ResetSpeed()
		{
			this._scrollSpeed = 0f;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004DD8 File Offset: 0x00002FD8
		private void HandleGamepadScroll()
		{
			if (this._canScroll && (this._bindings == null || !this._bindings.IsGamepadScrollDisabled))
			{
				float num = this._gamepadScrollAction.ReadValue<float>();
				float num2 = (Mathf.Abs(num) > 0.25f) ? num : 0f;
				this._scrollSpeed += num2 * 200f * base.scrollSensitivity;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004E45 File Offset: 0x00003045
		private void RefreshGamepadBindings()
		{
			if (this._bindings != null)
			{
				this._bindings.CanScroll = this._canScroll;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004E66 File Offset: 0x00003066
		public override void OnScroll(PointerEventData data)
		{
			if (this._canScroll)
			{
				this._scrollSpeed -= Mathf.Sign(data.scrollDelta.y) * 800f * base.scrollSensitivity;
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004E9A File Offset: 0x0000309A
		public override void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004E9C File Offset: 0x0000309C
		public override void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004E9E File Offset: 0x0000309E
		public override void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004EA0 File Offset: 0x000030A0
		public override void OnInitializePotentialDrag(PointerEventData eventData)
		{
		}

		// Token: 0x040000C4 RID: 196
		private VerticalScrollablePanelBindings _bindings;

		// Token: 0x040000C5 RID: 197
		private InputAction _gamepadScrollAction;

		// Token: 0x040000C6 RID: 198
		private bool _canScroll;

		// Token: 0x040000C7 RID: 199
		private float _scrollSpeed;

		// Token: 0x040000C8 RID: 200
		private const float _scrollDeltaSpeedMultiplierKeyboardMouse = 800f;

		// Token: 0x040000C9 RID: 201
		private const float _scrollDeltaSpeedMultiplierGamepad = 200f;
	}
}
