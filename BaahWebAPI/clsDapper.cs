using MySqlConnector;

namespace BaahWebAPI
{
    public class clsDapper
    {
        public MySqlConnection Con()
        {
            var connection = new MySqlConnection("Server=54.183.92.125;Database=baahstore;Uid=baahstore;Pwd=s#z%R79S$kYG25yp;AllowUserVariables=true; ");
            return connection;
        }
    }
}
