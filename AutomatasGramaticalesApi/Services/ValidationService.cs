using AutomatasGramaticalesApi.Models;
using System.Text.RegularExpressions;

namespace AutomatasGramaticalesApi.Services
{
    public class ValidationService : IValidationService
    {
        public Compiler validate(string[] lines)
        {
            Compiler compiler = new Compiler();
            compiler.Messages = new List<string>();
            compiler.Result = true; 

            string pattern = @"^declare\s+[a-zA-Z_][a-zA-Z0-9_,\s]*\s+(entero|real|cadena|logico|fecha)\s*;\s*$";

            Dictionary<string, List<string>> variablesByType = new Dictionary<string, List<string>>();

            for(int i = 0; i < lines.Length; i++)
            {
                string line = lines[i]; 

                if(Regex.IsMatch(line, pattern, RegexOptions.IgnoreCase))
                {

                    Match match = Regex.Match(line, pattern, RegexOptions.IgnoreCase);
                    string tipoDato = match.Groups[1].Value.ToLower();

                    string pattern2 = @"declare\s(.*?)\s\w+;";
                    Match match2 = Regex.Match(line, pattern2);
                    string variablesPart = match2.Groups[1].Value;
                    string[] variables = variablesPart.Split(',');

                    foreach (string variable in variables)
                    {
                        string trimmedVariable = variable.Trim();
                        
                        if (IsValidIdentifier(trimmedVariable))
                        {
                            if (!variablesByType.ContainsKey(tipoDato))
                            {
                                variablesByType[tipoDato] = new List<string>();
                            }
                            variablesByType[tipoDato].Add(trimmedVariable);
                        }
                        else
                        {
                            compiler.Messages.Add($"Error en la línea {i + 1}: Identificador inválido - {variable}");
                            compiler.Result = false;
                        }
                    }
                }
                else
                {
                    compiler.Messages.Add($"Error en la línea {i + 1}: Declaración de variable no válida - {line}");
                    compiler.Result = false; 
                }  
            }

            compiler.variablesByType = variablesByType;

            return compiler;
        }

        private bool IsValidIdentifier(string identifier)
        {
            if (!char.IsLetter(identifier[0]))
            {
                return false; 
            }

            return Regex.IsMatch(identifier, @"^[a-zA-Z0-9_]{1,15}$"); 
        } 
    }
}
