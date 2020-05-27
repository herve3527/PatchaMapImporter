namespace PatchaMapImporter.UI
{
	using Models;
	using UnityEngine;

	/// <summary>
	/// Represent a map representation
	/// </summary>
	class MapWidget
	{
		public const string MAP_EDIT = "edit";
		public const string MAP_LOAD = "load";

		/// <summary>
		/// construct a map widget
		/// </summary>
		/// <param name="map"></param>
		/// <param name="even"></param>
		public MapWidget(Map map, bool even)
		{
			Map = map;

			var style = new GUIStyle {
				padding = new RectOffset() { top = 10, left = 10, bottom = 10, right = 10 },
			};

			//change background all 2 maps
			if (even) style.normal.background = EvenMapBackground;

			using (new GUILayout.HorizontalScope(style)) {
				//Infos
				using (new GUILayout.VerticalScope(GUILayout.ExpandWidth(true))) {
					using (new GUILayout.HorizontalScope()) {
						var mapNameStyle = new GUIStyle();
						mapNameStyle.fontSize = 20;
						mapNameStyle.normal.textColor = Color.white;
						GUILayout.Label(map.DisplayName, mapNameStyle);
						if (!string.IsNullOrEmpty(map.Authors)) GUILayout.Label($"by {map.Authors}");
						GUILayout.FlexibleSpace();
					}

					using (new GUILayout.HorizontalScope(GUILayout.ExpandWidth(false))) {
						GUILayout.Label(map.Flags.ToString().Replace(",", " /"));
						GUILayout.FlexibleSpace();
						GUILayout.Label($"Rating : {map.Rating}");
					}
				}

				//GUILayout.FlexibleSpace();
				GUILayout.Space(20);

				//buttons
				using (new GUILayout.VerticalScope(GUILayout.ExpandWidth(false))) {
					Action = "";
					using (new GUILayout.HorizontalScope()) {
						if (GUILayout.Button(new GUIContent("edit properties", "edit map properties"))) {
							Action = MAP_EDIT;
						}
					}

					//TODO: highlight the whole control and make it clickable to load the map + hover effect and then remove the 'load' button
					using (new GUILayout.HorizontalScope()) {
						if (GUILayout.Button(new GUIContent("load this map!", "load map"), GUILayout.ExpandWidth(false))) {
							Action = MAP_LOAD;
						}
					}
				}
			}
		}

		/// <summary>
		/// Storing selected map reference
		/// </summary>
		public readonly Map Map;

		/// <summary>
		/// What bouton was pressed
		/// </summary>
		public string Action { get; private set; }

		/// <summary>
		/// Alternate color for maps
		/// </summary>
		private static Texture2D EvenMapBackground = TextureHelpers.MakeTexture(16, 16, new Color(.6f, .6f, .6f, .5f));
	}
}
