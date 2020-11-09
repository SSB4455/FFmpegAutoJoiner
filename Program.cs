/*
SSBB4455 2020-11-09
*/
using System;
using System.IO;

//dotnet new console -o F:\FFmpegAutoJoiner
namespace FFmpegAutoJoiner
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args?.Length > 0 && Directory.Exists(args[0]))
			{
				Console.WriteLine(args[0]);
				string[] files = Directory.GetFiles(args[0]);
				string playlistContent = "";
				for (int i = 0; i < files?.Length; i++)
				{
					playlistContent += "file '" + Path.GetFileName(files[i]) + "'\n";
					Console.WriteLine(files[i]);
				}
				string playlistPath = args[0] + Path.DirectorySeparatorChar + "playlist";
				File.WriteAllText(playlistPath, playlistContent);
				System.Diagnostics.Process p = new System.Diagnostics.Process();
				p.StartInfo.WorkingDirectory = args[0];
				p.StartInfo.FileName = "cmd.exe";
				p.StartInfo.CreateNoWindow = true;              //不创建新窗口
				p.StartInfo.UseShellExecute = false;       		//不启用shell启动进程
				p.StartInfo.RedirectStandardInput = true;       //接受来自调用程序的输入信息
				p.StartInfo.RedirectStandardOutput = true;      //输出信息
				p.StartInfo.RedirectStandardError = true;       //输出错误
				Console.WriteLine("Start");

				p.Start();
				p.StandardInput.WriteLine("ffmpeg -f concat -i playlist -c copy output.mp4");
				Console.WriteLine("若长时间未结束 可手动关闭");
				p.WaitForExit();

				File.Delete(playlistPath);
			}
			Console.WriteLine("end");
			Console.Read();
		}
	}
}
