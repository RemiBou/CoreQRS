using System.Threading.Tasks;

public interface IQuery
{
    Task<object> GetResultsAsync();
}