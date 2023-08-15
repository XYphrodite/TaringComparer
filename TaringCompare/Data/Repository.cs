using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TaringCompare.Models;

namespace TaringCompare.Data
{
    public class Repository
    {
        public static IEnumerable<Taring> GetTarings()
        {
            IDbConnection dbConnection = new SqlConnection(AppConnection.ConnectionString);
            if (dbConnection.State == ConnectionState.Closed) dbConnection.Open();
            string sql = @"select * from Taring";
            var tars = dbConnection.Query<Taring>(sql, commandType: CommandType.Text);
            foreach (var tar in tars)
            {
                sql = $"select * from TaringItem where TaringItem.TaringID = {tar.TaringID}";
                tar.TaringList = dbConnection.Query<TaringItem>(sql, commandType: CommandType.Text).ToList();
            }
            return tars;
        }
    }
}
