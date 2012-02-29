using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mono.Options;

namespace relative
{
	class Program
	{
		static void Main( string[] args )
		{

			var optionSetting = new OptionSetting();

			var p = new OptionSet{
			                     	{"h|?|help", v => optionSetting.FlagHelp = v != null},
/*
			                     	{
			                     		"format=",
			                     		v =>
			                     		optionSetting.TextFormatType =
			                     		(ExcelFileProcessor.TextFormatType)
			                     		Enum.Parse( typeof( ExcelFileProcessor.TextFormatType ), v )
			                     		},
			                     	{"r", v => optionSetting.FlagRecursive = v != null},
									{"c", v => optionSetting.FlagQuartOnlyExistingComma = v != null},
*/
			                     };
			optionSetting.Paths.AddRange( p.Parse( args ) );

			// ヘルプ表示
			if ( optionSetting.FlagHelp || args.Count() == 0 ){
				DispHelp( p );
				return;
			}
			
			
			if ( optionSetting.Paths.Count > 0 ){
				//	相対パスを求めるターゲットファイル
				optionSetting.TargetPath = optionSetting.Paths[ 0 ];
			}
			if ( optionSetting.Paths.Count > 1 ){
				//	ベースパスが指定されていれば、ベースとなるディレクトリ名に修正する
				
				if ( System.IO.Directory.Exists( optionSetting.Paths[ 1 ] ) ){
					//	既存のディレクトリであれば
					//		path/directory
					//		path/directory/
					//	の両方を受け付ける
					
					if ( ! optionSetting.Paths[ 1 ].EndsWith( @"\" ) ){
						//	最後に '\' が付いてなければつける
						optionSetting.Paths[ 1 ] += @"\";
					}
					optionSetting.BasePath = optionSetting.Paths[ 1 ];
					
				}else{
					//	ファイル名ならディレクトリ部分を取り出す。最後に '\' をつける
					optionSetting.BasePath = System.IO.Path.GetDirectoryName( optionSetting.Paths[ 1 ] ) + @"\";
				}
				
			}else{
				//	オプションで指定されなければカレントディレクトリ
				optionSetting.BasePath = System.IO.Directory.GetCurrentDirectory() + @"\";
			}
			
			Uri basePath = new Uri( optionSetting.BasePath );
			Uri targetPath = new Uri( optionSetting.TargetPath );
			
			//	相対パスを求める
			//		ベースディレクトリには末尾に '\' が付いている必要がある
			Uri relativePath = basePath.MakeRelativeUri( targetPath );
			
			
			//	出力
//			Console.WriteLine( "base     : " + basePath.ToString() );
//			Console.WriteLine( "target   : " + targetPath.ToString() );
//			Console.WriteLine( "relative : " + relativePath.ToString() );
			Console.WriteLine( relativePath.ToString() );
			
			return ;
		}
		
		//	ヘルプ表示
		private static void DispHelp( OptionSet p )
		{
			Console.WriteLine( "usage: relative target [base]" );
			p.WriteOptionDescriptions( Console.Out );
		}
		
	}
}
