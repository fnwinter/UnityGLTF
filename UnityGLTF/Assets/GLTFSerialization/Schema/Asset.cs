using System;
using Newtonsoft.Json;
using UnityEngine;

namespace GLTF.Schema
{
	/// <summary>
	/// Metadata about the glTF asset.
	/// </summary>
	[SerializeField]
	public class Asset : GLTFProperty
	{
		/// <summary>
		/// A copyright message suitable for display to credit the content creator.
		/// </summary>
		public string copyright;

		/// <summary>
		/// Tool that generated this glTF model. Useful for debugging.
		/// </summary>
		public string generator;

		/// <summary>
		/// The glTF version.
		/// </summary>
		public string version;

		/// <summary>
		/// The minimum glTF version that this asset targets.
		/// </summary>
		public string minVersion;

		public Asset()
		{
		}

		public Asset(Asset asset) : base(asset)
		{
			if (asset == null) return;

			copyright = asset.copyright;
			generator = asset.generator;
			version = asset.version;
			minVersion = asset.minVersion;
		}

		public static Asset Deserialize(GLTFRoot root, JsonReader reader)
		{
			var asset = new Asset();

			if (reader.Read() && reader.TokenType != JsonToken.StartObject)
			{
				throw new Exception("Asset must be an object.");
			}

			while (reader.Read() && reader.TokenType == JsonToken.PropertyName)
			{
				var curProp = reader.Value.ToString();

				switch (curProp)
				{
					case "copyright":
						asset.copyright = reader.ReadAsString();
						break;
					case "generator":
						asset.generator = reader.ReadAsString();
						break;
					case "version":
						asset.version = reader.ReadAsString();
						break;
					case "minVersion":
						asset.minVersion = reader.ReadAsString();
						break;
					default:
						asset.DefaultPropertyDeserializer(root, reader);
						break;
				}
			}

			return asset;
		}

		public override void Serialize(JsonWriter writer)
		{
			writer.WriteStartObject();

			if (copyright != null)
			{
				writer.WritePropertyName("copyright");
				writer.WriteValue(copyright);
			}

			if (generator != null)
			{
				writer.WritePropertyName("generator");
				writer.WriteValue(generator);
			}

			writer.WritePropertyName("version");
			writer.WriteValue(version);

			base.Serialize(writer);

			writer.WriteEndObject();
		}
	}
}
