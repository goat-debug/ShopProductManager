using System.Data;
using System.Data.SqlClient;

namespace ShopProductManager.Data;

public static class Db
{
    public static IDbConnection GetConnection()
    {
        return new SqlConnection("Server=(LocalDb)\\MSSQLLocalDB;Database=ShopDb;Trusted_Connection=True;");
    }
}
