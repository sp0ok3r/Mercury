/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/

/* Written by Paul "Xetas" Abramov */
using System;

namespace Mercury.Logger
{
    public class Logger
    {
        private readonly string m_userName;

        public Logger(string _userName)
        {
            m_userName = _userName;
        }

        /// <summary>
        /// Route to the method log to print out an info
        /// </summary>
        /// <param name="_message"></param>
        public void Info(string _message)
        {
            Log(LoggingLevel.INFO, _message);
        }

        /// <summary>
        /// Route to the method log to print out a warning
        /// </summary>
        /// <param name="_message"></param>
        public void Warning(string _message)
        {
            Log(LoggingLevel.WARNING, _message);
        }

        /// <summary>
        /// Route to the method log to print out an error
        /// </summary>
        /// <param name="_message"></param>
        public void Error(string _message)
        {
            Log(LoggingLevel.ERROR, _message);
        }

        /// <summary>
        /// We want to keep it simple so we make the enum internal and this method private
        /// This will be called trough a routing method and route this further
        /// </summary>
        /// <param name="_loggingLevel"></param>
        /// <param name="_message"></param>
        private void Log(LoggingLevel _loggingLevel, string _message)
        {
            switch (_loggingLevel)
            {
                case LoggingLevel.INFO:
                case LoggingLevel.WARNING:
                case LoggingLevel.ERROR:
                    LogMessage(_loggingLevel, _message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_loggingLevel), _loggingLevel, null);
            }
        }
        
        /// <summary>
        /// Depending on the enumparameter we want to define a color
        /// Printout a message with the date, username, the logginglevel and the message
        /// </summary>
        /// <param name="_loggingLevel"></param>
        /// <param name="_message"></param>
        private void LogMessage(LoggingLevel _loggingLevel, string _message)
        {
            ConsoleColor color = Console.ForegroundColor;

            switch (_loggingLevel)
            {
                case LoggingLevel.WARNING:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LoggingLevel.ERROR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            Console.WriteLine($"{DateTime.Now} | {m_userName} | {_loggingLevel} | {_message}");

            Console.ForegroundColor = color;
        }
    }

    internal enum LoggingLevel
    {
        INFO = 0,
        WARNING = 1,
        ERROR = 2
    }
}
