using System;
using System.Diagnostics;
using System.Windows.Forms;
public class ProcessUtility
{
	public static string  Execute(string App,string  Arguments)
	{
  		Process p = new Process();
		p.StartInfo.FileName = App;
		p.StartInfo.Arguments = Arguments;
		p.StartInfo.UseShellExecute = false;
		p.StartInfo.CreateNoWindow = true;
		p.StartInfo.RedirectStandardError = true;
		p.StartInfo.RedirectStandardOutput = true;
		p.Start();
		string output = p.StandardOutput.ReadToEnd();
		p.WaitForExit();
		return output;
	}
	public static void Run(string App,string  Arguments,RichTextBox rtb,string OutPutDir)
	{
        Process process = new Process();
        RedirectExcuteProcess(
        	process, 
        	App, 
        	Arguments, 
        	delegate(object s, DataReceivedEventArgs e) {
            rtb.AppendText(e.Data+"\n");
        	},
        	delegate(object s, System.EventArgs e)
        	{
           		System.Diagnostics.Process.Start(OutPutDir);
        	}
       );
	}
    static void RedirectExcuteProcess(Process p, string exe, string arg, DataReceivedEventHandler output,System.EventHandler Exit)
    {
        p.StartInfo.FileName = exe;
        p.StartInfo.Arguments = arg;
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.CreateNoWindow = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.OutputDataReceived += output;
        p.ErrorDataReceived += output;
        p.Exited+=Exit;
        p.EnableRaisingEvents = true;
        p.Start();
        p.BeginOutputReadLine();
        p.BeginErrorReadLine();
    }
}