using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
namespace NugetGUI
{
	public partial class MainForm : Form
	{
		string OutPath=Environment.CurrentDirectory+"/"+"NugetPackages";
		ListViewColumnSorter lvwColumnSorter = new ListViewColumnSorter();
		public MainForm()
		{
			InitializeComponent();
			listView1.ListViewItemSorter=lvwColumnSorter;
		}
		void SearchNuget(string KeyWord)
		{
			if(!string.IsNullOrEmpty(KeyWord))
			{
				listView1.Items.Clear();
				List<PackageVersionInfo> packageVersions=NugetUtility.ListPackage(KeyWord);
				foreach(PackageVersionInfo pvs in packageVersions)
				{
					 ListViewItem item = new ListViewItem(pvs.PackageName);
	            	 item.SubItems.Add(pvs.Version);
	            	 item.SubItems.Add(pvs.DownloadCount.ToString());
	            	  listView1.Items.Add(item);
				}
			}
		}
		void DownloadNuget(string PackageName,string Version)
		{
			if(!string.IsNullOrEmpty(PackageName))
			{
				string Args=string.Format(@" install {0} -Version {1} -DirectDownload",PackageName,Version);
				ProcessUtility.Run("nuget.exe", Args,richTextBox_result,OutPath);
			}
		}
		void ToolStripButton1Click(object sender, EventArgs e)
		{
			SearchNuget(toolStripTextBox1.Text.Trim());
		}
		void ListView1MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ListViewItem CurrentListViewItem=listView1.SelectedItems[0];
			DownloadNuget(CurrentListViewItem.Text,CurrentListViewItem.SubItems[1].Text);
		}
		void ListView1ColumnClick(object sender, ColumnClickEventArgs e)
		{
            if (e.Column == lvwColumnSorter.SortColumn) {
                if (lvwColumnSorter.Order == SortOrder.Ascending) {
                    lvwColumnSorter.Order = SortOrder.Descending;
                } else {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            } else {
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }
            this.listView1.Sort();  
		}
	}
		class MyListViewItemSorter : IComparer
		{
		    public int Compare(object x, object y)
		    {
		        ListViewItem itemX = (ListViewItem)x;
		        ListViewItem itemY = (ListViewItem)y;
		        int columnIndex = 2;
		        int m=int.Parse(itemX.SubItems[columnIndex].Text);
		        int n=int.Parse(itemX.SubItems[columnIndex].Text);
		        return m.CompareTo(n);
		    }
		}
}
