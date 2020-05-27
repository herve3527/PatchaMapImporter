namespace PatchaMapImporter.UI
{
	using Models;
	using System;
	using System.Collections.Generic;
	using UnityEngine;

	/// <summary>
	/// Represent the map list
	/// </summary>
	class MapListWidget
	{
		/// <summary>
		/// Construct with maps list and delegate for load or edit
		/// </summary>
		/// <param name="scrollPos">scroll position to keep</param>
		/// <param name="maps">map list to show</param>
		/// <param name="onLoad">Action to call when a map wants to be loaded</param>
		/// <param name="onEdit">Action to call when a map wants to be edited</param>
		public MapListWidget(ref Vector2 scrollPos, List<Map> maps, Action<Map> onLoad, Action<Map> onEdit)
		{
			var style = new GUIStyle() {
				normal = new GUIStyleState() {
					background = ScrollBackground
				}
			};

			GUILayout.Label($"{maps.Count} maps.");

			scrollPos = GUILayout.BeginScrollView(scrollPos, style);
			{
				int i = 0;
				foreach (var map in maps) {
					var even = (++i % 2) == 0;
					var widget = new MapWidget(map, even);

					//call actions
					if (widget.Action == MapWidget.MAP_EDIT) onEdit(map);
					else if (widget.Action == MapWidget.MAP_LOAD) onLoad(map);

					GUILayout.Space(5);
				}
			}
			GUILayout.EndScrollView();
		}

		/// <summary>
		/// Background for all map list
		/// </summary>
		private static Texture2D ScrollBackground = TextureHelpers.MakeTexture(16, 16, new Color(.3f, .3f, .3f, .5f));
	}



}
