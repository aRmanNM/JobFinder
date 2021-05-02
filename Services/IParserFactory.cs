using System.Collections.Generic;

namespace JobFinder.Services
{
    public interface IParserFactory
    {
        List<IParser> GetParsers();
    }
}