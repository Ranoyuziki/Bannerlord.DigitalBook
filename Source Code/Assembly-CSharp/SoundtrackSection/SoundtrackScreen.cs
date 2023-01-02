using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace TaleWorlds.CompanionBook.SoundtrackSection
{
	// Token: 0x02000020 RID: 32
	public class SoundtrackScreen : MonoBehaviour
	{
		// Token: 0x06000115 RID: 277 RVA: 0x0000553D File Offset: 0x0000373D
		private void Awake()
		{
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000553F File Offset: 0x0000373F
		private void OnEnable()
		{
			SoundtrackList soundtrackList = this._soundtrackList;
			soundtrackList.OnHoveredItemChanged = (Action)Delegate.Combine(soundtrackList.OnHoveredItemChanged, new Action(this.RefreshInfoText));
			this.RefreshInfoText();
			this._globalBackground.SetIsBlurryEnabled(true);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000557A File Offset: 0x0000377A
		private void OnDisable()
		{
			SoundtrackList soundtrackList = this._soundtrackList;
			soundtrackList.OnHoveredItemChanged = (Action)Delegate.Remove(soundtrackList.OnHoveredItemChanged, new Action(this.RefreshInfoText));
			this._globalBackground.SetIsBlurryEnabled(false);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000055AF File Offset: 0x000037AF
		private void Update()
		{
			this._backToMenuButton.CheckActions();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000055BC File Offset: 0x000037BC
		private void RefreshInfoText()
		{
			if (this._soundtrackList.HoveredItem != null)
			{
				string infoTextID = this._soundtrackList.HoveredItem.InfoTextID;
				IReadOnlyDictionary<string, string> infoTextVariables = this._soundtrackList.HoveredItem.InfoTextVariables;
				LocalizedString localizedString = LocalizedText.CreateLocalizedString(infoTextID);
				localizedString.Add("newline", new StringVariable
				{
					Value = "<br>"
				});
				foreach (KeyValuePair<string, string> keyValuePair in infoTextVariables)
				{
					localizedString.Add(keyValuePair.Key, new StringVariable
					{
						Value = keyValuePair.Value
					});
				}
				this._infoText.SetReference(localizedString);
				return;
			}
			this._infoText.SetReference("empty_entry");
		}

		// Token: 0x040000DD RID: 221
		[SerializeField]
		private GlobalBackground _globalBackground;

		// Token: 0x040000DE RID: 222
		[SerializeField]
		private BackToMenuButton _backToMenuButton;

		// Token: 0x040000DF RID: 223
		[SerializeField]
		private SoundtrackList _soundtrackList;

		// Token: 0x040000E0 RID: 224
		[SerializeField]
		private LocalizedText _infoText;
	}
}
