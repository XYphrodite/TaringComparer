using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaringCompare.Models;

namespace TaringCompare.Data
{
    public class Repository
    {
        public static IEnumerable<Taring> GetTarings()
        {
            IDbConnection dbConnection = new SqlConnection(AppConnection.ConnectionString);
            if(dbConnection.State == ConnectionState.Closed) dbConnection.Open();
            var tars = dbConnection.Query<Taring>("select * from Taring", commandType: CommandType.Text);
            return tars;
        }
    }
}
