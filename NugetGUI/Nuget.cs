using System;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
struct PackageVersionInfo
{
	public string PackageName;
	public string Version;
	public int DownloadCount;
}
class NugetUtility
{
	public static List<PackageVersionInfo> ListPackage(string PackageName)
    {
		List<PackageVersionInfo> packageVersions=new List<PackageVersionInfo>();
		string URL=string.Format(@"https://www.nuget.org/stats/reports/packages/{0}?groupby=Version",PackageName.Trim());
		string HTTPResult=HttpHelper.HttpGet(URL);
		JObject JResult=(JObject)JsonConvert.DeserializeObject(HTTPResult);
		IList<JToken> VersionResults=JResult["Table"].Children().ToList();
		foreach(JToken item in VersionResults)
		{
			PackageVersionInfo pvs=new PackageVersionInfo{
				PackageName=item[0]["Uri"].ToString().Split('/')[2],
				Version=item[0]["Uri"].ToString().Split('/')[3],
				DownloadCount=int.Parse(item[1]["Data"].ToString())
			};
			packageVersions.Add(pvs);
		}
		return packageVersions;
    }
}