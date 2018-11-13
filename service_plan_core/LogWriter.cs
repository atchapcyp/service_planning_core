using System;
using System.IO;
using System.Reflection;
namespace service_plan_core
{
    public class LogWriter
    {
        private string m_exePath = String.Empty;
        public LogWriter(string logMessage)
        {
            LogWrite(logMessage);
        }
        public void LogWrite(string logMessage){
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try{
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt")) 
                {
                    Log(logMessage, w);
                }
            }
            catch(AmbiguousMatchException){

            }
        }
        public void Log(string logMessage,TextWriter txtWriter){
            try{
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch(AmbiguousMatchException){

            }
        }
    }
}
