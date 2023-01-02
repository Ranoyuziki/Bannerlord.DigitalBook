using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.BrushSystem
{
	// Token: 0x0200006C RID: 108
	public class Brush : BrushBase, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000C96D File Offset: 0x0000AB6D
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000C975 File Offset: 0x0000AB75
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					this.UpdateState();
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000C98D File Offset: 0x0000AB8D
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000C995 File Offset: 0x0000AB95
		public bool IsDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				if (value != this._isDisabled)
				{
					this._isDisabled = value;
					this.UpdateState();
				}
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000C9AD File Offset: 0x0000ABAD
		private void Awake()
		{
			this._simpleState = BrushState.Default;
			base.Initialize();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000C9BC File Offset: 0x0000ABBC
		private void Update()
		{
			base.UpdateTransition(Time.deltaTime);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000C9C9 File Offset: 0x0000ABC9
		private void OnDisable()
		{
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Remove(instance.OnControlSchemeChanged, new Action(this.OnControlSchemeChanged));
			this.ChangeSimpleState(BrushState.Default);
			base.SkipTransition();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000CA00 File Offset: 0x0000AC00
		private void OnEnable()
		{
			Game instance = Game.Instance;
			instance.OnControlSchemeChanged = (Action)Delegate.Combine(instance.OnControlSchemeChanged, new Action(this.OnControlSchemeChanged));
			GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
			if (Game.Instance.IsCurrentSchemeGamepad && currentSelectedGameObject == base.gameObject)
			{
				this.ChangeSimpleState(BrushState.Hovered);
			}
			this.UpdateState();
			base.SkipTransition();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		public static void SetButtonEnabled(Button button, bool isEnabled)
		{
			if (button != null)
			{
				button.interactable = isEnabled;
				button.image.raycastTarget = isEnabled;
				Brush brush;
				if (button.TryGetComponent<Brush>(ref brush))
				{
					brush.IsDisabled = !isEnabled;
				}
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000CAAC File Offset: 0x0000ACAC
		private void UpdateState()
		{
			BrushState state = this._simpleState;
			if (this.IsDisabled)
			{
				state = BrushState.Disabled;
			}
			else
			{
				bool flag = this._simpleState == BrushState.Default;
				bool flag2 = !this._isSelectNotDominant;
				if (this.IsSelected && (flag2 || flag))
				{
					state = BrushState.Selected;
				}
			}
			base.ChangeState(state);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000CAF6 File Offset: 0x0000ACF6
		private void ChangeSimpleState(BrushState state)
		{
			if (this._simpleState != state)
			{
				this._simpleState = state;
				this.UpdateState();
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000CB10 File Offset: 0x0000AD10
		private void OnControlSchemeChanged()
		{
			this.ChangeSimpleState((Game.Instance.IsCurrentSchemeGamepad && EventSystem.current.currentSelectedGameObject == base.gameObject) ? BrushState.Hovered : BrushState.Default);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000CB4F File Offset: 0x0000AD4F
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (!Game.Instance.IsCurrentSchemeGamepad && eventData.button == null)
			{
				this.ChangeSimpleState(BrushState.Pressed);
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (!Game.Instance.IsCurrentSchemeGamepad && eventData.button == null)
			{
				this.ChangeSimpleState(this._isPointerIn ? BrushState.Hovered : BrushState.Default);
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000CB94 File Offset: 0x0000AD94
		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			if (!Game.Instance.IsCurrentSchemeGamepad)
			{
				this._isPointerIn = true;
				if (this._simpleState == BrushState.Default)
				{
					this.ChangeSimpleState(BrushState.Hovered);
				}
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000CBB8 File Offset: 0x0000ADB8
		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			if (!Game.Instance.IsCurrentSchemeGamepad)
			{
				this._isPointerIn = false;
				if (this._simpleState == BrushState.Hovered)
				{
					this.ChangeSimpleState(BrushState.Default);
				}
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000CBDD File Offset: 0x0000ADDD
		void ISelectHandler.OnSelect(BaseEventData eventData)
		{
			if (Game.Instance.IsCurrentSchemeGamepad)
			{
				this.ChangeSimpleState(BrushState.Hovered);
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000CBF2 File Offset: 0x0000ADF2
		void IDeselectHandler.OnDeselect(BaseEventData eventData)
		{
			if (Game.Instance.IsCurrentSchemeGamepad)
			{
				this.ChangeSimpleState(BrushState.Default);
			}
		}

		// Token: 0x040002B8 RID: 696
		[SerializeField]
		private bool _isSelectNotDominant;

		// Token: 0x040002B9 RID: 697
		private bool _isPointerIn;

		// Token: 0x040002BA RID: 698
		private bool _isSelected;

		// Token: 0x040002BB RID: 699
		private bool _isDisabled;

		// Token: 0x040002BC RID: 700
		private BrushState _simpleState;
	}
}
