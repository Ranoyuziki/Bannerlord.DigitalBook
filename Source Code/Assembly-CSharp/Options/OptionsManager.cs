using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CompanionBook.ResourceSystem;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace TaleWorlds.CompanionBook.Options
{
	// Token: 0x02000042 RID: 66
	public class OptionsManager
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000071F7 File Offset: 0x000053F7
		public IReadOnlyDictionary<OptionType, float> OptionValues
		{
			get
			{
				return this._optionValues;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000071FF File Offset: 0x000053FF
		private List<Locale> _locales
		{
			get
			{
				return LocalizationSettings.AvailableLocales.Locales;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000720C File Offset: 0x0000540C
		public OptionsManager()
		{
			if (Game.Instance.ResourceProvider.IsLoading)
			{
				ResourceProvider resourceProvider = Game.Instance.ResourceProvider;
				resourceProvider.OnPersistentResourcesLoadingFinished = (Action)Delegate.Combine(resourceProvider.OnPersistentResourcesLoadingFinished, new Action(this.Initialize));
				return;
			}
			this.Initialize();
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007278 File Offset: 0x00005478
		public IEnumerable<OptionBase> GetAvailableOptions()
		{
			if (SystemInfo.deviceType == 3)
			{
				this._resolutions.Clear();
				List<string> list = new List<string>();
				int num = 0;
				int value = num;
				using (IEnumerator<Resolution> enumerator = Enumerable.ThenByDescending<Resolution, int>(Enumerable.OrderByDescending<Resolution, int>(Screen.resolutions, (Resolution x) => x.width), (Resolution x) => x.height).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Resolution resolution = enumerator.Current;
						if (!this._resolutions.Exists((Resolution x) => x.width == resolution.width && x.height == resolution.height))
						{
							this._resolutions.Add(resolution);
							list.Add(string.Format("{0}x{1}", resolution.width, resolution.height));
							ValueTuple<int, int> valueTuple = (Screen.fullScreenMode == 3) ? new ValueTuple<int, int>(Screen.width, Screen.height) : new ValueTuple<int, int>(Screen.currentResolution.width, Screen.currentResolution.height);
							if (resolution.width == valueTuple.Item1 && resolution.height == valueTuple.Item2)
							{
								value = this._resolutions.Count - 1;
							}
						}
					}
				}
				int fullscreenModeIndex = (int)this.GetSupportedFullscreenMode(Screen.fullScreenMode);
				yield return new SelectionOption(EngineOptionType.Resolution, value, num, new Action<GenericOptionType, float>(this.OnGenericOptionChanged), list);
				yield return new SelectionOption(EngineOptionType.FullscreenMode, fullscreenModeIndex, 0, new Action<GenericOptionType, float>(this.OnGenericOptionChanged), 3);
			}
			int defaultValue = this._locales.FindIndex((Locale x) => x == this._defaultLocale);
			int value2 = Mathf.RoundToInt(this.OptionValues[OptionType.Language]);
			yield return new SelectionOption(OptionType.Language, value2, defaultValue, new Action<GenericOptionType, float>(this.OnGenericOptionChanged), this.GetLanguageNames(this._locales));
			yield return new NumericOption(OptionType.MasterVolume, this.OptionValues[OptionType.MasterVolume], 1f, new Vector2(0f, 100f), new Action<GenericOptionType, float>(this.OnGenericOptionChanged));
			yield return new NumericOption(OptionType.MusicVolume, this.OptionValues[OptionType.MusicVolume], 1f, new Vector2(0f, 100f), new Action<GenericOptionType, float>(this.OnGenericOptionChanged));
			yield break;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007288 File Offset: 0x00005488
		public void SaveConfig()
		{
			PlayerPrefs.SetString(OptionType.Language.ToString(), LocalizationSettings.SelectedLocale.Identifier.Code);
			PlayerPrefs.SetString(OptionType.MasterVolume.ToString(), this.OptionValues[OptionType.MasterVolume].ToString());
			PlayerPrefs.SetString(OptionType.MusicVolume.ToString(), this.OptionValues[OptionType.MusicVolume].ToString());
			PlayerPrefs.Save();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007310 File Offset: 0x00005510
		private void Initialize()
		{
			this._defaultLocale = LocalizationSettings.SelectedLocale;
			string selectedLocaleCode = PlayerPrefs.GetString(OptionType.Language.ToString(), this._defaultLocale.Identifier.Code);
			Locale selectedLocale = Enumerable.FirstOrDefault<Locale>(this._locales, (Locale x) => x.Identifier.Code == selectedLocaleCode);
			if (selectedLocale == null)
			{
				selectedLocale = this._defaultLocale;
			}
			int num = this._locales.FindIndex((Locale x) => x == selectedLocale);
			float num3;
			float num2 = float.TryParse(PlayerPrefs.GetString(OptionType.MasterVolume.ToString(), "1f"), ref num3) ? num3 : 1f;
			float num5;
			float num4 = float.TryParse(PlayerPrefs.GetString(OptionType.MusicVolume.ToString(), "1f"), ref num5) ? num5 : 1f;
			foreach (OptionType optionType in (OptionType[])Enum.GetValues(typeof(OptionType)))
			{
				this._optionValues[optionType] = 0f;
			}
			this._optionValues[OptionType.Language] = (float)num;
			this._optionValues[OptionType.MasterVolume] = num2;
			this._optionValues[OptionType.MusicVolume] = num4;
			QualitySettings.vSyncCount = 1;
			LocalizationSettings.SelectedLocale = selectedLocale;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007482 File Offset: 0x00005682
		private void OnGenericOptionChanged(GenericOptionType optionType, float value)
		{
			if (optionType.IsEngineOption)
			{
				this.OnEngineOptionChanged((EngineOptionType)optionType.Type, value);
				return;
			}
			this.OnOptionChanged((OptionType)optionType.Type, value);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000074B4 File Offset: 0x000056B4
		private void OnOptionChanged(OptionType optionType, float value)
		{
			if (optionType == OptionType.Language)
			{
				this.SetLanguage(value);
				this.SetOptionValue(optionType, value);
				return;
			}
			if (optionType - OptionType.MasterVolume > 1)
			{
				return;
			}
			this.SetOptionValue(optionType, value);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000074D8 File Offset: 0x000056D8
		private void OnEngineOptionChanged(EngineOptionType engineOptionType, float value)
		{
			if (engineOptionType == EngineOptionType.Resolution)
			{
				this.SetResolution(value);
				return;
			}
			if (engineOptionType != EngineOptionType.FullscreenMode)
			{
				return;
			}
			this.SetFullscreenMode(value);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000074F1 File Offset: 0x000056F1
		private void SetOptionValue(OptionType optionType, float value)
		{
			if (this._optionValues[optionType] != value)
			{
				this._optionValues[optionType] = value;
				Action<OptionType> onOptionValueChangedEvent = this.OnOptionValueChangedEvent;
				if (onOptionValueChangedEvent == null)
				{
					return;
				}
				onOptionValueChangedEvent.Invoke(optionType);
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007520 File Offset: 0x00005720
		private void SetLanguage(float indexAsFloat)
		{
			int num = Mathf.RoundToInt(indexAsFloat);
			if (num >= 0 && num < this._locales.Count)
			{
				LocalizationSettings.SelectedLocale = this._locales[num];
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007558 File Offset: 0x00005758
		private void SetResolution(float indexAsFloat)
		{
			int num = Mathf.RoundToInt(indexAsFloat);
			if (num >= 0 && num < this._resolutions.Count)
			{
				Resolution resolution = this._resolutions[num];
				Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000075A4 File Offset: 0x000057A4
		private void SetFullscreenMode(float indexAsFloat)
		{
			int num = Mathf.RoundToInt(indexAsFloat);
			if (num >= 0 && num < 3)
			{
				Screen.fullScreenMode = this.GetEngineFullscreenMode((OptionsManager.SupportedFullscreenMode)num);
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000075CC File Offset: 0x000057CC
		private FullScreenMode GetEngineFullscreenMode(OptionsManager.SupportedFullscreenMode supportedFullscreenMode)
		{
			switch (supportedFullscreenMode)
			{
			case OptionsManager.SupportedFullscreenMode.ExclusiveFullscreen:
				return 0;
			case OptionsManager.SupportedFullscreenMode.FullscreenWindow:
				return 1;
			case OptionsManager.SupportedFullscreenMode.Windowed:
				return 3;
			default:
				return 0;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000075E9 File Offset: 0x000057E9
		private OptionsManager.SupportedFullscreenMode GetSupportedFullscreenMode(FullScreenMode fullscreenMode)
		{
			switch (fullscreenMode)
			{
			case 0:
				return OptionsManager.SupportedFullscreenMode.ExclusiveFullscreen;
			case 1:
			case 2:
				return OptionsManager.SupportedFullscreenMode.FullscreenWindow;
			case 3:
				return OptionsManager.SupportedFullscreenMode.Windowed;
			default:
				return OptionsManager.SupportedFullscreenMode.ExclusiveFullscreen;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000760A File Offset: 0x0000580A
		private IEnumerable<string> GetLanguageNames(List<Locale> locales)
		{
			foreach (Locale locale in locales)
			{
				string text = string.Empty;
				string code = locale.Identifier.Code;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(code);
				if (num <= 1195724803U)
				{
					if (num <= 1162757945U)
					{
						if (num != 1092248970U)
						{
							if (num != 1111292255U)
							{
								if (num != 1162757945U)
								{
									goto IL_29F;
								}
								if (!(code == "pl"))
								{
									goto IL_29F;
								}
								text = "Polski";
							}
							else
							{
								if (!(code == "ko"))
								{
									goto IL_29F;
								}
								text = "한국어";
							}
						}
						else
						{
							if (!(code == "en"))
							{
								goto IL_29F;
							}
							text = "English";
						}
					}
					else if (num != 1176137065U)
					{
						if (num != 1194886160U)
						{
							if (num != 1195724803U)
							{
								goto IL_29F;
							}
							if (!(code == "tr"))
							{
								goto IL_29F;
							}
							text = "Türkçe";
						}
						else
						{
							if (!(code == "it"))
							{
								goto IL_29F;
							}
							text = "Italiano";
						}
					}
					else
					{
						if (!(code == "es"))
						{
							goto IL_29F;
						}
						text = "Español";
					}
				}
				else if (num <= 1545391778U)
				{
					if (num != 1213488160U)
					{
						if (num != 1461901041U)
						{
							if (num != 1545391778U)
							{
								goto IL_29F;
							}
							if (!(code == "de"))
							{
								goto IL_29F;
							}
							text = "Deutsch";
						}
						else
						{
							if (!(code == "fr"))
							{
								goto IL_29F;
							}
							text = "Français";
						}
					}
					else
					{
						if (!(code == "ru"))
						{
							goto IL_29F;
						}
						text = "Русский";
					}
				}
				else if (num <= 1816099348U)
				{
					if (num != 1565420801U)
					{
						if (num != 1816099348U)
						{
							goto IL_29F;
						}
						if (!(code == "ja"))
						{
							goto IL_29F;
						}
						text = "日本語";
					}
					else
					{
						if (!(code == "pt"))
						{
							goto IL_29F;
						}
						text = "Português";
					}
				}
				else if (num != 2197937899U)
				{
					if (num != 2281825994U)
					{
						goto IL_29F;
					}
					if (!(code == "zh-hans"))
					{
						goto IL_29F;
					}
					text = "简体中文";
				}
				else
				{
					if (!(code == "zh-hant"))
					{
						goto IL_29F;
					}
					text = "繁體中文";
				}
				IL_2A5:
				yield return text;
				continue;
				IL_29F:
				text = "Missing";
				goto IL_2A5;
			}
			List<Locale>.Enumerator enumerator = default(List<Locale>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x04000191 RID: 401
		public Action<OptionType> OnOptionValueChangedEvent;

		// Token: 0x04000192 RID: 402
		private Dictionary<OptionType, float> _optionValues = new Dictionary<OptionType, float>();

		// Token: 0x04000193 RID: 403
		private Locale _defaultLocale;

		// Token: 0x04000194 RID: 404
		private List<Resolution> _resolutions = new List<Resolution>();

		// Token: 0x020000AC RID: 172
		private enum SupportedFullscreenMode
		{
			// Token: 0x040003D7 RID: 983
			ExclusiveFullscreen,
			// Token: 0x040003D8 RID: 984
			FullscreenWindow,
			// Token: 0x040003D9 RID: 985
			Windowed,
			// Token: 0x040003DA RID: 986
			Count
		}
	}
}
