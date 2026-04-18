using System.Data;

namespace Services.Data;

public interface IDbConnectionFactory
{
    IDbConnection Create();
}
