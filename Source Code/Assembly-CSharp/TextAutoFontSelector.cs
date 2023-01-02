using System;
using TaleWorlds.CompanionBook.Options;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x0200001B RID: 27
	[RequireComponent(typeof(TMP_Text))]
	public class TextAutoFontSelector : MonoBehaviour
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00004BAB File Offset: 0x00002DAB
		private void Awake()
		{
			this._text = base.GetComponent<TMP_Text>();
			this.UpdateFont();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004BBF File Offset: 0x00002DBF
		private void OnEnable()
		{
			OptionsManager optionsManager = Game.Instance.OptionsManager;
			optionsManager.OnOptionValueChangedEvent = (Action<OptionType>)Delegate.Combine(optionsManager.OnOptionValueChangedEvent, new Action<OptionType>(this.OnOptionChanged));
			this.UpdateFont();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004BF2 File Offset: 0x00002DF2
		private void OnDisable()
		{
			OptionsManager optionsManager = Game.Instance.OptionsManager;
			optionsManager.OnOptionValueChangedEvent = (Action<OptionType>)Delegate.Remove(optionsManager.OnOptionValueChangedEvent, new Action<OptionType>(this.OnOptionChanged));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004C1F File Offset: 0x00002E1F
		private void OnOptionChanged(OptionType optionType)
		{
			if (optionType == OptionType.Language)
			{
				this.UpdateFont();
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004C2C File Offset: 0x00002E2C
		private void UpdateFont()
		{
			TMP_FontAsset tmp_FontAsset = this._defaultFont;
			string code = LocalizationSettings.SelectedLocale.Identifier.Code;
			if (!(code == "ja"))
			{
				if (!(code == "ko"))
				{
					if (!(code == "zh-hans"))
					{
						if (!(code == "zh-hant"))
						{
							if (!(code == "ru"))
							{
								tmp_FontAsset = this._defaultFont;
							}
							else
							{
								tmp_FontAsset = this._russianFont;
							}
						}
						else
						{
							tmp_FontAsset = this._traditionalChineseFont;
						}
					}
					else
					{
						tmp_FontAsset = this._simplifiedChineseFont;
					}
				}
				else
				{
					tmp_FontAsset = this._koreanFont;
				}
			}
			else
			{
				tmp_FontAsset = this._japaneseFont;
			}
			if (this._text.font != tmp_FontAsset)
			{
				this._text.font = tmp_FontAsset;
			}
		}

		// Token: 0x040000BD RID: 189
		[SerializeField]
		private TMP_FontAsset _defaultFont;

		// Token: 0x040000BE RID: 190
		[SerializeField]
		private TMP_FontAsset _russianFont;

		// Token: 0x040000BF RID: 191
		[SerializeField]
		private TMP_FontAsset _koreanFont;

		// Token: 0x040000C0 RID: 192
		[SerializeField]
		private TMP_FontAsset _japaneseFont;

		// Token: 0x040000C1 RID: 193
		[SerializeField]
		private TMP_FontAsset _simplifiedChineseFont;

		// Token: 0x040000C2 RID: 194
		[SerializeField]
		private TMP_FontAsset _traditionalChineseFont;

		// Token: 0x040000C3 RID: 195
		private TMP_Text _text;
	}
}
