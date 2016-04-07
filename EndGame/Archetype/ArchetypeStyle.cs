using HDT.Plugins.EndGame.Enums;

namespace HDT.Plugins.EndGame.Archetype
{
	public static class ArchetypeStyles
	{
		public static readonly ArchetypeStyle CONTROL = new ArchetypeStyle("Control", PlayStyle.CONTROL);
		public static readonly ArchetypeStyle AGGRO = new ArchetypeStyle("Aggro", PlayStyle.AGGRO);
		public static readonly ArchetypeStyle COMBO = new ArchetypeStyle("Combo", PlayStyle.COMBO);
		public static readonly ArchetypeStyle MIDRANGE = new ArchetypeStyle("Control", PlayStyle.MIDRANGE);
	}

	public class ArchetypeStyle
	{
		public string Name { get; private set; }
		public PlayStyle Style { get; private set; }

		public ArchetypeStyle(string name, PlayStyle style)
		{
			Name = name;
			Style = style;
		}
	}
}