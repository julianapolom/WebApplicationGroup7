using System;
using System.IO;

namespace Tools
{
    public sealed class Log
    {
        private static Log? _instance;
        private string _path;
        private static object _protect = new();

        /// <summary>
        /// Método para instanciar el log
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Log GetInstance(string path)
        {
            lock (_protect)
            {
                _instance ??= new Log(path);
            }

            return _instance;
        }

        /// <summary>
        /// Optienen la ruta del Log
        /// </summary>
        /// <param name="path"></param>
        private Log(string path) =>
            _path = path;

        /// <summary>
        /// Guarda el mensaje en el Log
        /// </summary>
        /// <param name="message"></param>
        public void Save(string message) =>
            File.AppendAllText(_path, message + Environment.NewLine);
    }
}
