using System;
using TaleWorlds.CompanionBook.ScreenSystem;
using UnityEngine;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x0200000F RID: 15
	public class GlobalBackground : MonoBehaviour
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00002D64 File Offset: 0x00000F64
		private void Start()
		{
			LayoutElement layoutElement;
			if (this._backgroundImagePrefab.GetComponent<Image>() != null && this._backgroundImagePrefab.TryGetComponent<LayoutElement>(ref layoutElement))
			{
				ScreenManager screenManager = Game.Instance.ScreenManager;
				this._backgroundImageWidth = (float)this._backgroundSprite.texture.width * (screenManager.ReferenceResolution.y / (float)this._backgroundSprite.texture.height);
				layoutElement.preferredWidth = this._backgroundImageWidth;
			}
			this._isInitialized = true;
			this._blurryBackgroundCanvasGroup.alpha = 0f;
			this._backgroundCanvasGroup.alpha = 1f;
			this.RefreshBackground();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002E0C File Offset: 0x0000100C
		private void OnEnable()
		{
			ScreenManager screenManager = Game.Instance.ScreenManager;
			screenManager.OnCurrentResolutionChanged = (Action)Delegate.Combine(screenManager.OnCurrentResolutionChanged, new Action(this.OnResolutionChanged));
			this.RefreshBackground();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002E40 File Offset: 0x00001040
		private void OnDisable()
		{
			ScreenManager screenManager = Game.Instance.ScreenManager;
			screenManager.OnCurrentResolutionChanged = (Action)Delegate.Remove(screenManager.OnCurrentResolutionChanged, new Action(this.OnResolutionChanged));
			this._backgroundSpeed = 0f;
			this._backgroundTargetSpeed = 0f;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002E8E File Offset: 0x0000108E
		private void Update()
		{
			this.HandleBackgroundMovement();
			this.HandleBlurryTransition();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002E9C File Offset: 0x0000109C
		public void SetIsMoveEnabled(bool isEnabled)
		{
			this._backgroundTargetSpeed = (isEnabled ? 100f : 0f);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002EB4 File Offset: 0x000010B4
		public void SetIsBlurryEnabled(bool isEnabled)
		{
			if (isEnabled != this._isBlurryEnabled)
			{
				this._isBlurryEnabled = isEnabled;
				if (isEnabled)
				{
					this._blurryTransitionAnimation = new AnimationProgress(1f, AnimationProgressType.EaseOutQuint);
					return;
				}
				this._blurryTransitionAnimation = null;
				this._blurryBackgroundCanvasGroup.alpha = 0f;
				this._backgroundCanvasGroup.alpha = 1f;
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002F10 File Offset: 0x00001110
		private void HandleBlurryTransition()
		{
			if (this._blurryTransitionAnimation != null)
			{
				this._blurryTransitionAnimation.Progress(Time.deltaTime);
				float progression = this._blurryTransitionAnimation.Progression;
				this._blurryBackgroundCanvasGroup.alpha = progression;
				this._backgroundCanvasGroup.alpha = 1f - progression;
				if (this._blurryTransitionAnimation.IsFinished)
				{
					this._blurryTransitionAnimation = null;
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002F74 File Offset: 0x00001174
		private void HandleBackgroundMovement()
		{
			this._backgroundSpeed = Mathf.MoveTowards(this._backgroundSpeed, this._backgroundTargetSpeed, 100f * Time.deltaTime);
			this._backgroundListPanel.localPosition += new Vector3(-this._backgroundSpeed, 0f, 0f) * Time.deltaTime;
			if (this._backgroundListPanel.localPosition.x < -this._backgroundImageWidth)
			{
				this._backgroundListPanel.localPosition += new Vector3(this._backgroundImageWidth, 0f, 0f);
			}
			this._blurryBackgroundListPanel.localPosition = this._backgroundListPanel.localPosition;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003034 File Offset: 0x00001234
		private void RefreshBackground()
		{
			ScreenManager screenManager = Game.Instance.ScreenManager;
			if (Vector2.Distance(screenManager.CurrentResolution, this._lastCheckedResolution) > Mathf.Epsilon && this._isInitialized)
			{
				this._lastCheckedResolution = screenManager.CurrentResolution;
				foreach (object obj in this._backgroundListPanel.transform)
				{
					Object.Destroy(((Transform)obj).gameObject);
				}
				foreach (object obj2 in this._blurryBackgroundListPanel.transform)
				{
					Object.Destroy(((Transform)obj2).gameObject);
				}
				float num = this._backgroundImageWidth * screenManager.ResolutionScale;
				int num2 = Mathf.RoundToInt(Mathf.Ceil(screenManager.CurrentResolution.x * 1.2f / num)) + 1;
				for (int i = 0; i < num2; i++)
				{
					Object.Instantiate<GameObject>(this._backgroundImagePrefab, this._backgroundListPanel.transform).GetComponent<Image>().sprite = this._backgroundSprite;
					Object.Instantiate<GameObject>(this._backgroundImagePrefab, this._blurryBackgroundListPanel.transform).GetComponent<Image>().sprite = this._blurryBackgroundSprite;
				}
				Vector3 localPosition = this._backgroundListPanel.localPosition;
				localPosition.x = 0f;
				this._backgroundListPanel.localPosition = localPosition;
				this._backgroundSpeed = 0f;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000031EC File Offset: 0x000013EC
		private void OnResolutionChanged()
		{
			this.RefreshBackground();
		}

		// Token: 0x04000042 RID: 66
		[SerializeField]
		private CanvasGroup _backgroundCanvasGroup;

		// Token: 0x04000043 RID: 67
		[SerializeField]
		private CanvasGroup _blurryBackgroundCanvasGroup;

		// Token: 0x04000044 RID: 68
		[SerializeField]
		private GameObject _backgroundImagePrefab;

		// Token: 0x04000045 RID: 69
		[SerializeField]
		private RectTransform _backgroundListPanel;

		// Token: 0x04000046 RID: 70
		[SerializeField]
		private RectTransform _blurryBackgroundListPanel;

		// Token: 0x04000047 RID: 71
		[SerializeField]
		private Sprite _backgroundSprite;

		// Token: 0x04000048 RID: 72
		[SerializeField]
		private Sprite _blurryBackgroundSprite;

		// Token: 0x04000049 RID: 73
		private bool _isInitialized;

		// Token: 0x0400004A RID: 74
		private Vector2 _lastCheckedResolution = Vector2.zero;

		// Token: 0x0400004B RID: 75
		private float _backgroundImageWidth;

		// Token: 0x0400004C RID: 76
		private float _backgroundTargetSpeed;

		// Token: 0x0400004D RID: 77
		private float _backgroundSpeed;

		// Token: 0x0400004E RID: 78
		private const float _backgroundMaxSpeed = 100f;

		// Token: 0x0400004F RID: 79
		private const float _backgroundAcceleration = 100f;

		// Token: 0x04000050 RID: 80
		private bool _isBlurryEnabled;

		// Token: 0x04000051 RID: 81
		private AnimationProgress _blurryTransitionAnimation;

		// Token: 0x04000052 RID: 82
		private const float _blurryTransitionAnimationDuration = 1f;
	}
}
