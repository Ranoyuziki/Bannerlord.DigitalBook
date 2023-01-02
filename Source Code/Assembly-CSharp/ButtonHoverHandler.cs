using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000007 RID: 7
	public class ButtonHoverHandler : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000025EF File Offset: 0x000007EF
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000025F7 File Offset: 0x000007F7
		public bool IsHovered
		{
			get
			{
				return this._isHovered;
			}
			private set
			{
				if (value != this._isHovered)
				{
					this._isHovered = value;
					Action<bool> onIsHoveredChanged = this.OnIsHoveredChanged;
					if (onIsHoveredChanged == null)
					{
						return;
					}
					onIsHoveredChanged.Invoke(value);
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000261C File Offset: 0x0000081C
		private void OnEnable()
		{
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Combine(instance.OnControlSchemeChanged, new Action(this.Refresh));
			this._isGamepadHovered = (EventSystem.current.currentSelectedGameObject == base.gameObject);
			this.Refresh();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002670 File Offset: 0x00000870
		private void OnDisable()
		{
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Remove(instance.OnControlSchemeChanged, new Action(this.Refresh));
			this._isPointerIn = false;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000269F File Offset: 0x0000089F
		private void Refresh()
		{
			this.IsHovered = (Game.Instance.IsCurrentSchemeGamepad ? this._isGamepadHovered : this._isPointerIn);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026C1 File Offset: 0x000008C1
		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			this._isPointerIn = true;
			this.Refresh();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026D0 File Offset: 0x000008D0
		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			this._isPointerIn = false;
			this.Refresh();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026DF File Offset: 0x000008DF
		void ISelectHandler.OnSelect(BaseEventData eventData)
		{
			this._isGamepadHovered = true;
			this.Refresh();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026EE File Offset: 0x000008EE
		void IDeselectHandler.OnDeselect(BaseEventData eventData)
		{
			this._isGamepadHovered = false;
			this.Refresh();
		}

		// Token: 0x04000020 RID: 32
		public Action<bool> OnIsHoveredChanged;

		// Token: 0x04000021 RID: 33
		private bool _isPointerIn;

		// Token: 0x04000022 RID: 34
		private bool _isGamepadHovered;

		// Token: 0x04000023 RID: 35
		private bool _isHovered;
	}
}
