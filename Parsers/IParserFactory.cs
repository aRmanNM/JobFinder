
namespace JobFinder.Parsers
{
    public interface IParserFactory
    {
        IParser GetParser(string name);
    }
}