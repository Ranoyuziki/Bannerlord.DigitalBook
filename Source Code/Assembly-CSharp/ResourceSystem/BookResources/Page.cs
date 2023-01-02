using System;
using System.Xml;

namespace TaleWorlds.CompanionBook.ResourceSystem.BookResources
{
	// Token: 0x0200003C RID: 60
	public class Page
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00006FC7 File Offset: 0x000051C7
		public int Index { get; } = -1;

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00006FCF File Offset: 0x000051CF
		public PageType PageType { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00006FD7 File Offset: 0x000051D7
		public float StartTime { get; } = -1f;

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00006FDF File Offset: 0x000051DF
		public float EndTime { get; } = -1f;

		// Token: 0x060001F5 RID: 501 RVA: 0x00006FE8 File Offset: 0x000051E8
		public Page(XmlNode node)
		{
			int num;
			if (int.TryParse(node.GetXmlNodeAttribute("Index"), ref num))
			{
				this.Index = num;
			}
			PageType pageType;
			if (Enum.TryParse<PageType>(node.GetXmlNodeAttribute("PageType"), ref pageType))
			{
				this.PageType = pageType;
			}
			float num2;
			this.StartTime = (Page.TryParseTime(node.GetXmlNodeAttribute("StartTime"), out num2) ? num2 : 0f);
			float num3;
			this.EndTime = (Page.TryParseTime(node.GetXmlNodeAttribute("EndTime"), out num3) ? num3 : float.MaxValue);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007094 File Offset: 0x00005294
		private static bool TryParseTime(string source, out float time)
		{
			time = -1f;
			string[] array = source.Split(":", 0);
			bool result = false;
			int num;
			int num2;
			int num3;
			if (array.Length == 3 && int.TryParse(array[0], ref num) && int.TryParse(array[1], ref num2) && int.TryParse(array[2], ref num3) && num >= 0 && num2 >= 0 && num3 >= 0 && num2 < 60 && num3 < 1000)
			{
				result = true;
				time = (float)(num * 60 + num2) + (float)num3 * 0.001f;
			}
			return result;
		}

		// Token: 0x04000182 RID: 386
		private const string _timeSeparator = ":";
	}
}
