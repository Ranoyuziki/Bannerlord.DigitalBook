using System;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	public class ChineseLocaleSelector : IStartupLocaleSelector
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000028F9 File Offset: 0x00000AF9
		public Locale GetStartupLocale(ILocalesProvider availableLocales)
		{
			if (Application.systemLanguage != 6)
			{
				return null;
			}
			return availableLocales.GetLocale(40);
		}
	}
}
