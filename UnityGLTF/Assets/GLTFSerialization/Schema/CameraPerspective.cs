using System;
using Newtonsoft.Json;
using UnityEngine;

namespace GLTF.Schema
{
	/// <summary>
	/// A perspective camera containing properties to create a perspective projection
	/// matrix.
	/// </summary>
	[System.Serializable]
	public class CameraPerspective : GLTFProperty
	{
		/// <summary>
		/// The floating-point aspect ratio of the field of view.
		/// When this is undefined, the aspect ratio of the canvas is used.
		/// <minimum>0.0</minimum>
		/// </summary>
		public double aspectRatio;

		/// <summary>
		/// The floating-point vertical field of view in radians.
		/// <minimum>0.0</minimum>
		/// </summary>
		public double yfov;

		/// <summary>
		/// The floating-point distance to the far clipping plane. When defined,
		/// `zfar` must be greater than `znear`.
		/// If `zfar` is undefined, runtime must use infinite projection matrix.
		/// <minimum>0.0</minimum>
		/// </summary>
		public double zfar = double.PositiveInfinity;

		/// <summary>
		/// The floating-point distance to the near clipping plane.
		/// <minimum>0.0</minimum>
		/// </summary>
		public double znear;

		public CameraPerspective()
		{
		}

		public CameraPerspective(CameraPerspective cameraPerspective) : base(cameraPerspective)
		{
			if (cameraPerspective == null) return;

			aspectRatio = cameraPerspective.aspectRatio;
			yfov = cameraPerspective.yfov;
			zfar = cameraPerspective.zfar;
			znear = cameraPerspective.znear;
		}

		public static CameraPerspective Deserialize(GLTFRoot root, JsonReader reader)
		{
			var cameraPerspective = new CameraPerspective();

			if (reader.Read() && reader.TokenType != JsonToken.StartObject)
			{
				throw new Exception("Perspective camera must be an object.");
			}

			while (reader.Read() && reader.TokenType == JsonToken.PropertyName)
			{
				var curProp = reader.Value.ToString();

				switch (curProp)
				{
					case "aspectRatio":
						cameraPerspective.aspectRatio = reader.ReadAsDouble().Value;
						break;
					case "yfov":
						cameraPerspective.yfov = reader.ReadAsDouble().Value;
						break;
					case "zfar":
						cameraPerspective.zfar = reader.ReadAsDouble().Value;
						break;
					case "znear":
						cameraPerspective.znear = reader.ReadAsDouble().Value;
						break;
					default:
						cameraPerspective.DefaultPropertyDeserializer(root, reader);
						break;
				}
			}

			return cameraPerspective;
		}

		public override void Serialize(JsonWriter writer)
		{
			writer.WriteStartObject();

			if (aspectRatio != 0)
			{
				writer.WritePropertyName("aspectRatio");
				writer.WriteValue(aspectRatio);
			}

			writer.WritePropertyName("yfov");
			writer.WriteValue(yfov);

			if (zfar != double.PositiveInfinity)
			{
				writer.WritePropertyName("zfar");
				writer.WriteValue(zfar);
			}

			writer.WritePropertyName("znear");
			writer.WriteValue(znear);

			base.Serialize(writer);

			writer.WriteEndObject();
		}
	}
}
