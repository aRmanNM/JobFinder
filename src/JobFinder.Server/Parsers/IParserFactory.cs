
namespace JobFinder.Server.Parsers;

public interface IParserFactory
{
    IParser GetParser(string name);
}