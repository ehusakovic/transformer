﻿using System.Collections.Generic;
using System.IO;
using Transformer.Logging;
using Transformer.Model;

namespace Transformer.Template
{
    public class TemplateEngine
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(VariableResolver));

        public VariableResolver VariableResolver { get; set; }

        public int TransformDirectory(string path, Environment targetEnvironment, string subEnvironment = null, bool deleteTemplate = true)
        {
            if (subEnvironment == null)
                subEnvironment = string.Empty;

            Log.InfoFormat("Transform templates for environment {1} in {0} {2} deleting templates", path, targetEnvironment.Name, deleteTemplate ? "with" : "without");

            targetEnvironment.Variables.Add(new Variable() { Name = "subenv", Value = subEnvironment });

            VariableResolver = new VariableResolver(targetEnvironment.Variables);

            int templateCounter = 0;

            foreach (var templateFile in Directory.EnumerateFiles(path, "*.template.*", SearchOption.AllDirectories))
            {
                ++templateCounter;
                Log.Info(string.Format("  Transform template {0}", templateFile));


                var templateText = File.ReadAllText(templateFile);
                var transformed = VariableResolver.TransformVariables(templateText);

                ConvertTemplateNameToTargetName(templateFile).OverwriteContent(transformed);
     
                if (deleteTemplate)
                {
                    File.Delete(templateFile);
                }
            }

            Log.InfoFormat("Transformed {0} template(s) in {1}.", templateCounter, path);

            return templateCounter;
        }

        private string ConvertTemplateNameToTargetName(string templateName)
        {
            return templateName.Replace(".template.", ".").Replace(".Template.", ".");
        }
    }

    public class TransformResult
    {
        public int TransformedVariables { get; set; }
        public List<string> MissingVariables { get; set; }
    }
}
