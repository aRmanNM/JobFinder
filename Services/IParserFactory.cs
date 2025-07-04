
namespace JobFinder.Services
{
    public interface IParserFactory
    {
        IParser GetParser(string name);
    }
}