﻿using System;
using System.Collections.Generic;
using System.Linq;
using Kesmai.WorldForge.Editor;
using Kesmai.WorldForge.Models;

namespace Kesmai.WorldForge.Editor
{
#if (CanImport)

	public class WebConversion : ConversionPass
	{
		private List<int> _targets;

		/// <summary>
		/// Initializes a new instance of the <see cref="WebConversion"/> class.
		/// </summary>
		public WebConversion(List<int> targets)
		{
			_targets = targets;
		}

		/// <inheritdoc />
		public void Process(ISegmentImporter importer, IImportedRegion convertibleRegion, SegmentRegion convertedRegion)
		{
			var matches = convertedRegion.GetTiles(tile => true);
			
			foreach (var tile in matches)
			{
				var staticComponents = tile.GetComponents<StaticComponent>(component => _targets.Contains(component.Static));

				if (staticComponents.Count() > 1)
					throw new Exception("Encountered multiple web candidates.");

				foreach (var component in staticComponents)
					tile.ReplaceComponent(component, new StaticComponent(component.Static));
			}
		}
	}

#endif
}
