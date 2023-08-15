using MySql.Data.MySqlClient;

namespace Backend.DAO
{
    public class ConnectionFactory
    {
        public static MySqlConnection Build()
        {
            return new MySqlConnection("Server=localhost;Database=CompanyService;Uid=root;Pwd=root;");
        }
    }
}
