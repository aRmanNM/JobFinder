using System.Collections.Generic;

namespace JobFinder.Services
{
    public class ParserFactory : IParserFactory
    {
        private readonly IEnumerable<IParser> _parsers;
        public ParserFactory(IEnumerable<IParser> parsers)
        {
            _parsers = parsers;
        }
        public List<IParser> GetParsers()
        {
            List<IParser> parsers = new List<IParser>();

            foreach (var item in _parsers)
            {
                parsers.Add(item);
            }

            return parsers;
        }
    }
}