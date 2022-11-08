using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.NameOperations
{
    public static class NameOperation
    {

        public static string CharacterRegulatory(string name)     
            => name.Replace("!", "")
                .Replace("'", "")
                .Replace("^", "")
                .Replace("+", "")
                .Replace("$", "")
                .Replace("%", "")
                .Replace("&", "")
                .Replace("/", "")
                .Replace("{", "")
                .Replace("(", "")
                .Replace("[", "")
                .Replace(")", "")
                .Replace("]", "")
                .Replace("=", "")
                .Replace("}", "")
                .Replace("*", "")
                .Replace("?", "")
                .Replace("\"", "")
                .Replace("-", "")
                .Replace("_", "")
                .Replace("Ö", "o")
                .Replace("ö", "o")
                .Replace("Ü", "u")
                .Replace("ü", "ü")
                .Replace("ı", "i")
                .Replace("İ", "i")
                .Replace("Ş", "s")
                .Replace("ş", "s")
                .Replace("Ç", "c")
                .Replace("ç", "c")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "")
                .Replace("æ", "c")
                .Replace("â", "a")
                .Replace("Ğ", "g")
                .Replace("ğ", "g")
                .Replace("@", "g")
                .Replace("€", "g")
                .Replace(".", "-");
        
    }
}
