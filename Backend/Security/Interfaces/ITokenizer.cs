using System.Collections.Generic;

namespace Backend.Security.Interfaces
{
    public interface ITokenizer
    {
        string Tokenize(IDictionary<string, string> claims);
        IDictionary<string, string> DeTokenize(string token);
    }
}