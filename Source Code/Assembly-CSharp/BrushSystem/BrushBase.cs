using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook.BrushSystem
{
	// Token: 0x0200006D RID: 109
	[RequireComponent(typeof(RectTransform))]
	public class BrushBase : MonoBehaviour
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000CC0F File Offset: 0x0000AE0F
		public BrushState CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000CC18 File Offset: 0x0000AE18
		protected void Initialize()
		{
			Button button;
			if (base.TryGetComponent<Button>(ref button))
			{
				button.transition = 0;
			}
			this._stateDefinitions = new Dictionary<BrushState, BrushStateDefinition>();
			foreach (BrushStateDefinition brushStateDefinition in this._definitions)
			{
				if (!this._stateDefinitions.ContainsKey(brushStateDefinition.State))
				{
					this._stateDefinitions.Add(brushStateDefinition.State, brushStateDefinition);
				}
			}
			foreach (BrushState brushState in (BrushState[])Enum.GetValues(typeof(BrushState)))
			{
				if (!this._stateDefinitions.ContainsKey(brushState))
				{
					this._stateDefinitions.Add(brushState, new BrushStateDefinition());
				}
			}
			this._image = base.GetComponent<Image>();
			this._text = base.GetComponent<TMP_Text>();
			this._rectTransform = base.GetComponent<RectTransform>();
			Vector2 offsetMin = this._rectTransform.offsetMin;
			Vector2 offsetMax = this._rectTransform.offsetMax;
			this._defaultMargins = new Vector4(offsetMin.y, offsetMin.x, -offsetMax.y, -offsetMax.x);
			if (this._image != null)
			{
				this._defaultSprite = this._image.sprite;
			}
			this._currentState = BrushState.Default;
			this._previousState = BrushState.Default;
			this.ApplyTransitionedDefinitionValues(this._stateDefinitions[BrushState.Default], this._stateDefinitions[BrushState.Default], 0f);
			this.ApplyImmediateDefinitionValues(this._stateDefinitions[BrushState.Default]);
			this._isInitialized = true;
			if (this._initializationState != BrushState.Default)
			{
				this.ChangeState(this._initializationState);
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000CDD8 File Offset: 0x0000AFD8
		protected void UpdateTransition(float dt)
		{
			if (this._transitionAnimation != null)
			{
				this._transitionAnimation.Progress(dt);
				this.ApplyTransitionedDefinitionValues(this._stateDefinitions[this._previousState], this._stateDefinitions[this._currentState], this._transitionAnimation.Progression);
				if (this._transitionAnimation.IsFinished)
				{
					this._transitionAnimation = null;
				}
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000CE40 File Offset: 0x0000B040
		protected void ChangeState(BrushState state)
		{
			if (this._isInitialized)
			{
				if (this._currentState != state)
				{
					this._previousState = this._currentState;
					this._currentState = state;
					this.ApplyImmediateDefinitionValues(this._stateDefinitions[this._currentState]);
					this._transitionAnimation = new AnimationProgress(this.GetTransitionDuration(), AnimationProgressType.ExpoEaseOut);
					Action<BrushState> onStateChanged = this.OnStateChanged;
					if (onStateChanged == null)
					{
						return;
					}
					onStateChanged.Invoke(state);
					return;
				}
			}
			else
			{
				this._initializationState = state;
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000CEB3 File Offset: 0x0000B0B3
		protected void SkipTransition()
		{
			this._transitionAnimation = null;
			this.ApplyTransitionedDefinitionValues(this._stateDefinitions[this._previousState], this._stateDefinitions[this._currentState], 1f);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		private void ApplyTransitionedDefinitionValues(BrushStateDefinition from, BrushStateDefinition to, float transitionProgress)
		{
			if (this._image != null)
			{
				this._image.color = Color.Lerp(from.Color, to.Color, transitionProgress);
			}
			if (this._text != null)
			{
				this._text.color = Color.Lerp(from.Color, to.Color, transitionProgress);
			}
			if (from.UseMargins || to.UseMargins)
			{
				Vector4 vector = from.UseMargins ? from.Margins : this._defaultMargins;
				Vector4 vector2 = to.UseMargins ? to.Margins : this._defaultMargins;
				Vector4 vector3 = Vector4.Lerp(vector, vector2, transitionProgress);
				this._rectTransform.offsetMin = new Vector2(vector3.y, vector3.x);
				this._rectTransform.offsetMax = new Vector2(-vector3.w, -vector3.z);
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000CFCE File Offset: 0x0000B1CE
		private void ApplyImmediateDefinitionValues(BrushStateDefinition newStateDefinition)
		{
			if (this._image != null && this._useDefinitionSprites)
			{
				this._image.sprite = ((newStateDefinition.Sprite != null) ? newStateDefinition.Sprite : this._defaultSprite);
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000D00D File Offset: 0x0000B20D
		private float GetTransitionDuration()
		{
			if (!this._useCustomTransitionDuration)
			{
				return 0.5f;
			}
			return this._transitionDuration;
		}

		// Token: 0x040002BD RID: 701
		public Action<BrushState> OnStateChanged;

		// Token: 0x040002BE RID: 702
		[SerializeField]
		private bool _useCustomTransitionDuration;

		// Token: 0x040002BF RID: 703
		[SerializeField]
		private float _transitionDuration;

		// Token: 0x040002C0 RID: 704
		[SerializeField]
		private bool _useDefinitionSprites;

		// Token: 0x040002C1 RID: 705
		[SerializeField]
		private List<BrushStateDefinition> _definitions;

		// Token: 0x040002C2 RID: 706
		private Dictionary<BrushState, BrushStateDefinition> _stateDefinitions;

		// Token: 0x040002C3 RID: 707
		private Sprite _defaultSprite;

		// Token: 0x040002C4 RID: 708
		private Vector4 _defaultMargins;

		// Token: 0x040002C5 RID: 709
		private RectTransform _rectTransform;

		// Token: 0x040002C6 RID: 710
		private Image _image;

		// Token: 0x040002C7 RID: 711
		private TMP_Text _text;

		// Token: 0x040002C8 RID: 712
		private BrushState _currentState;

		// Token: 0x040002C9 RID: 713
		private BrushState _previousState;

		// Token: 0x040002CA RID: 714
		private AnimationProgress _transitionAnimation;

		// Token: 0x040002CB RID: 715
		private const float _defaultTransitionDuration = 0.5f;

		// Token: 0x040002CC RID: 716
		private bool _isInitialized;

		// Token: 0x040002CD RID: 717
		private BrushState _initializationState;
	}
}
