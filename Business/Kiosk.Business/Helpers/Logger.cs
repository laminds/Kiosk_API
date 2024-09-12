using Kiosk.Business.Model.Checkout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Helpers
{
    public partial class Logger
    {
        public void Log(string log, Exception ex = null)
        {
            string message = log + (ex != null ? " Exception:" + ex.Message.ToString() + (ex.InnerException != null ? "\n Inner Exception: " + ex.InnerException.Message : "") : "");
            Console.WriteLine(message);
            WriteLog(message);
        }

        private static void WriteLog(string pStrMsg)
        {
            string LogFilePath = Environment.CurrentDirectory + "\\LogFiles\\Log_" + DateTime.Today.ToString("MM-dd-yyyy") + ".txt";
            if (!File.Exists(LogFilePath))
            {
                File.Create(LogFilePath).Close();
            }
            StreamWriter file2 = new StreamWriter(LogFilePath, true);
            file2.WriteLine(DateTime.UtcNow.ToString());
            file2.WriteLine(pStrMsg);
            file2.Close();
        }

        public static void DeleteLog()
        {
            string LogFilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\LogFile.txt";
            if (File.Exists(LogFilePath))
            {
                File.Delete(LogFilePath);
            }
        }

        public void DBLog(string ex)
        {
            string message = ex;
            Console.WriteLine(message);
            WriteLog(message);
        }

        private void checkOutMessageWriteLog(string pStrMsg, string FirstName, string LastName)
        {
            string LogFilePath = Environment.CurrentDirectory + "\\LogFiles\\checkOutMessageLog_" + FirstName + "_" + LastName + "_" + DateTime.Today.ToString("MM-dd-yyyy") + ".txt";
            if (!File.Exists(LogFilePath))
            {
                File.Create(LogFilePath).Close();
            }
            StreamWriter file2 = new StreamWriter(LogFilePath, true);
            file2.WriteLine("\n" + DateTime.UtcNow.ToString());
            file2.WriteLine(pStrMsg);
            file2.Close();
        }

        public void checkOutMessageLogDetail(string log, string Message, string FirstName, string LastName, string Email, string MemberId, string clubNumber, string sourcename)
        {
            var txtmsg = Message != null ? Message : null;
            var msg = txtmsg.ToLower() == "success" ? "success(green)" : txtmsg.ToLower() == "-111" ? "warning(yellow)" : "error(red)";
            string message = log + ("ClubNumber : " + clubNumber, "\n Name : " + FirstName + " " + LastName, "\n Email : " + Email, "\n MemberId : " + MemberId, "\n SourceName : " + sourcename, "\n Message : " + Message, "\n");
            Console.WriteLine(message);
            checkOutMessageWriteLog(message, FirstName, LastName);
        }

        public void checkOutMessageLog(string log, MemberCheckOutResponseModel txt, PersonalInformationModel PostData, string clubNumber, string sourcename)
        {
            var txtmsg = txt.Message != null ? txt.Message : txt.PTMessage != null ? txt.PTMessage : txt.SGTMessage != null ? txt.SGTMessage : null;
            var msg = txtmsg.ToLower() == "success" ? "success(green)" : txtmsg.ToLower() == "-111" ? "warning(yellow)" : "error(red)";
            var EXMessage = txt.EXMessage != null ? txt.EXMessage : null;
            string message = log + ("ClubNumber : " + clubNumber, "\n Name : " + PostData.FirstName + " " + PostData.LastName, "\n Email : " + PostData.Email, "\n MemberId : " + PostData.MemberId, "\n SourceName : " + sourcename, "\n Message : " + txt.Message + (msg), "\n EXMessage : " + EXMessage, "\n");
            Console.WriteLine(message);
            checkOutMessageWriteLog(message,PostData.FirstName,PostData.LastName);
        }
    }
}
