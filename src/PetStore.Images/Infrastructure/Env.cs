using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PetStore.Images.Infrastructure
{

    /// <summary>
    /// this class is a workaround
    /// </summary>
    public static class Env
    {
        private static readonly Dictionary<string, string> variables = new Dictionary<string, string>();


        public static Dictionary<string, string> Variables
        {
            get
            {
                lock (variables)
                {
                    if (variables.Any()) return variables;

                    foreach (DictionaryEntry entry in Environment.GetEnvironmentVariables())
                    {
                        Console.WriteLine($"{entry.Key} - {entry.Value}");
                        variables.Add(entry.Key.ToString(), entry.Value?.ToString());
                    }

                    return variables;
                }
            }
        }
    }
}