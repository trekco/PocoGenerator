#region Using

using System.Configuration;
using System.Linq;
using PocoGenerator.Generators;
using PocoGenerator.Generators.Interfaces;
using PocoGenerator.Models;

#endregion

namespace PocoGenerator
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            var options = new GenerationOptions();

            options.BaseClass = ConfigurationManager.AppSettings["BaseClass"];
            options.ConnectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            options.DatabaseName = ConfigurationManager.AppSettings["DatabaseName"];
            options.Using = ConfigurationManager.AppSettings["Using"].Split(';').ToList();
            options.Namespace = ConfigurationManager.AppSettings["Namespace"];
            options.OutputPath = ConfigurationManager.AppSettings["OutputPath"];
            options.OutputFileExtention = ConfigurationManager.AppSettings["OutputFileExtention"];
            options.GenerateDapperExtentionsMapperClass = bool.Parse(ConfigurationManager.AppSettings["GenerateDapperExtentionsMapperClass"]);

            IGenerator generator = new MsSqlGenerator();

            generator.LoadOptions(options);
            generator.Generate();
        }
    }
}