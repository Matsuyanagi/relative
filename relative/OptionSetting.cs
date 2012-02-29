using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace relative
{
	class OptionSetting
	{
//		public List<string > Wildcards { get; private set; }
//		public ExcelFileProcessor.TextFormatType TextFormatType { get; set; }
//		public bool FlagRecursive { get; set; }
//		public bool FlagQuartOnlyExistingComma { get; set; }

		public bool FlagHelp { get; set; }
		public List<string > Paths { get; private set; }
		public string TargetPath { get; set; }
		public string BasePath { get; set; }
		
		public OptionSetting()
		{
			FlagHelp = false;
			Paths = new List< string >();
			
//			TextFormatType = ExcelFileProcessor.TextFormatType.CSV;
//			FlagRecursive = false;
			


		}
	}
}
