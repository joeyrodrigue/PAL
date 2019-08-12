using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLogger
{
  public class LogFile
  {
    private string fileName;
    public List<string> PAL;

    public LogFile()
    {
      fileName = "C:\\PAL\\Performance\\" + System.Environment.MachineName + string.Format("_{0:yyyyMMdd@HHmmss}.txt",
	    DateTime.Now);
    }

    public LogFile(string fileName)
    {
      this.fileName = fileName;
    }


    public void Add(int TestID, int Iteration, string Scenerio, string ScCheckPoint, TimeSpan duration)
    {
    	string PALData = TestID.ToString() + "," + Iteration.ToString() + "," + Scenerio + "," + ScCheckPoint + "," + System.Environment.MachineName + "," + (duration.TotalMilliseconds/1000).ToString() + "," + DateTime.Now.ToString();
    	using (StreamWriter writer = new StreamWriter(new FileStream(fileName, FileMode.Append)))
      	{
    		writer.WriteLine(PALData);
      	}
    }

    public void Write()
    {
      // Store the script names and test results in a output text file.
      using (StreamWriter writer = new StreamWriter(new FileStream(fileName, FileMode.Append)))
      {
      	foreach(string s in PAL)
      	{
      		writer.WriteLine(s.ToString());
      	}
      }
    }
  }
}