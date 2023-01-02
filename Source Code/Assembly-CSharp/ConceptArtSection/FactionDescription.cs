using System;
using TaleWorlds.CompanionBook.ResourceSystem.ConceptArtResources;
using UnityEngine;

namespace TaleWorlds.CompanionBook.ConceptArtSection
{
	// Token: 0x02000069 RID: 105
	public class FactionDescription : MonoBehaviour
	{
		// Token: 0x06000348 RID: 840 RVA: 0x0000C3AA File Offset: 0x0000A5AA
		private void Awake()
		{
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000C3AC File Offset: 0x0000A5AC
		public void OnFactionChanged(ConceptArtFactionType factionType)
		{
			string text = factionType.ToString().ToLower();
			this._title.SetReference("ui_generic_faction_" + text);
			this._text.SetReference("concept_arts_description_" + text);
		}

		// Token: 0x040002A7 RID: 679
		[SerializeField]
		private LocalizedText _title;

		// Token: 0x040002A8 RID: 680
		[SerializeField]
		private LocalizedText _text;
	}
}
