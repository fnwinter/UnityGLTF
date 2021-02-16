using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTF
{
	class GLTFJsonParser
	{

		public static void ReadObject(TextReader tr, out string ret)
		{
			int ch = 0;
			ret = "";
			while ((ch = tr.Read()) != -1)
			{
				if (ch != '}')
				{
					ret += (char)ch;
				}
				else
				{
					break;
				}
			}
		}
	}
}
