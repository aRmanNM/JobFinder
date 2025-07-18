using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace JobFinder.Parsers
{
    public class ParserFactory : IParserFactory
    {
        private readonly IEnumerable<IParser> _parsers;
        private readonly IConfiguration _configuration;

        public ParserFactory(
            IEnumerable<IParser> parsers,
            IConfiguration configuration)
        {
            _parsers = parsers;
            _configuration = configuration;
        }

        public IParser GetParser(string name)
        {
            if (_configuration["UseFakeServices"] == "true")
                return _parsers.First(p => p.Name == "Fake");
            else
                return _parsers.First(p => p.Name == name);
        }
    }
}