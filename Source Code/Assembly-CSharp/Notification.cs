using System;
using UnityEngine;
using UnityEngine.Localization;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000017 RID: 23
	[RequireComponent(typeof(CanvasGroup))]
	public class Notification : MonoBehaviour
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003ECF File Offset: 0x000020CF
		public string DefaultColorCode
		{
			get
			{
				return "<color=#FFFFFF>";
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003ED6 File Offset: 0x000020D6
		public string OnColorCode
		{
			get
			{
				return "<color=#FFB404>";
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003EDD File Offset: 0x000020DD
		public string OffColorCode
		{
			get
			{
				return "<color=#FE2B2B>";
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003EE4 File Offset: 0x000020E4
		private void Awake()
		{
			this._animationHandler = new AutoEndingAnimationHandler(2f, 1f, new Action<float>(this.OnFadeOut));
			this._canvasGroup = base.GetComponent<CanvasGroup>();
			this._canvasGroup.alpha = 0f;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003F23 File Offset: 0x00002123
		private void Update()
		{
			this._animationHandler.Update(Time.deltaTime);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003F35 File Offset: 0x00002135
		public void Display(LocalizedString localizedString)
		{
			this._canvasGroup.alpha = 1f;
			this._text.SetReference(localizedString);
			if (this._animationHandler != null)
			{
				this._animationHandler.WakeUp();
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003F66 File Offset: 0x00002166
		private void OnFadeOut(float progress)
		{
			this._canvasGroup.alpha = 1f - progress;
		}

		// Token: 0x0400006F RID: 111
		[SerializeField]
		private LocalizedText _text;

		// Token: 0x04000070 RID: 112
		private CanvasGroup _canvasGroup;

		// Token: 0x04000071 RID: 113
		private AutoEndingAnimationHandler _animationHandler;

		// Token: 0x04000072 RID: 114
		private const float _notificationStayDuration = 2f;

		// Token: 0x04000073 RID: 115
		private const float _notificationFadeOutDuration = 1f;
	}
}
