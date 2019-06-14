using System;
using System.IO;
using System.Threading;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Security.Policy;
using System.Globalization;
using Microsoft.CSharp;

namespace RFCProxyBuilder
{
	public class RFCProxyBuild
	{
		public RFCProxyBuild()
		{
		}
		
		protected int BuildRFCProxyDLL(string[] strArrayProxySource, string strFilePath, bool bClientProxy)
		{
			CSharpCodeProvider  oCSProvider = null;
			CompilerParameters  oParameters = null; 
			TempFileCollection  oTMPCollect = null; 
			string              strTempDir  = null; 

			CompilerResults     oCResults   = null; 
			Evidence            evidence    = null; 
			string              strResult   = null; 

			try 
			{
				oCSProvider = new CSharpCodeProvider();     
				oParameters = new CompilerParameters();     

				oParameters.GenerateExecutable = false;           
				oParameters.GenerateInMemory = false;             
				oParameters.TreatWarningsAsErrors = false;        
				oParameters.CompilerOptions = "/target:library";  
				
				strTempDir = Path.GetTempPath();                  
				oTMPCollect = new TempFileCollection(strTempDir); 
				oCResults = new CompilerResults(oTMPCollect);     

				evidence = new Evidence(null);
				Url url = new Url("http://rfcschemaproxygenerator");
				evidence.AddHost(url);
				
				oParameters.Evidence = evidence;
				oParameters.ReferencedAssemblies.Add("System.dll");
				oParameters.ReferencedAssemblies.Add("System.Data.dll");
				oParameters.ReferencedAssemblies.Add("System.XML.dll");
				oParameters.ReferencedAssemblies.Add("SAP.Connector.dll");
				oParameters.ReferencedAssemblies.Add("System.Web.Services.dll");
				if (!bClientProxy) 
					oParameters.ReferencedAssemblies.Add("sap.connector.rfc.dll");

				oParameters.OutputAssembly = strFilePath.ToString(new CultureInfo(Thread.CurrentThread.CurrentUICulture.ToString()));

                oCResults = oCSProvider.CompileAssemblyFromSource(oParameters, strArrayProxySource);
				oCResults.Evidence.AddHost(url);

				if (!oCResults.Errors.Count.Equals(0))
				{
					for (int idx = 0; idx < oCResults.Errors.Count; ++idx)
					{
						strResult = strResult + oCResults.Errors[idx].ToString() + " ";
					}
					throw new Exception(strResult);
				}
			} 
			catch (Exception exp)
			{
				throw exp;
			}
			finally 
			{
				oTMPCollect.Delete();
			}
			return oCResults.Errors.Count;
		}
	} // class
}