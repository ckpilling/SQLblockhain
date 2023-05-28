using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

namespace SQLchain.Pages.Customers
{
    public class IndexModel : PageModel
    {

        public List<ClientInfo> listClients = new List<ClientInfo>();
        public List<CustomerHistory> listHistory = new List<CustomerHistory>();

        public void OnGet()
        {

            try
            {
                String connectionString = "Server=tcp:verimy.database.windows.net,1433;Initial Catalog=SQLTables;Persist Security Info=False;User ID=verimysqladmin;Password=password0.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM customers";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();

                                listClients.Add(clientInfo);

                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception: " + ex.ToString());
            }

            try
            {
                String connectionString = "Data Source=localhost\\;Initial Catalog=clients;Persist Security Info=True;User ID=sa;Password=password0. ";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                   

                    String sql = "SELECT" +
                                    "t.[commit_time] AS[CommitTime]" +
                                    ", t.[principal_name] AS[UserName]" +
                                    ", l.[name]" +
                                    ", l.[email]" +
                                    ", l.[phone]" +
                                    ", l.[address]" +
                                    ", l.[ledger_operation_type_desc] AS Operation" +
                                    "FROM[dbo].[customers_Ledger] l" +
                                    "JOIN sys.database_ledger_transactions t" +
                                    "ON t.transaction_id = l.ledger_transaction_id" +
                                    "ORDER BY t.commit_time DESC;";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomerHistory customerHistory = new CustomerHistory();
                                customerHistory.CommitTime = reader.GetDateTime(0).ToString();
                                customerHistory.UserName = reader.GetString(1);
                                customerHistory.name = reader.GetString(2);
                                customerHistory.email = reader.GetString(3);
                                customerHistory.phone = reader.GetString(4);
                                customerHistory.address = reader.GetString(5);
                                customerHistory.operation = reader.GetString(6);

                                listHistory.Add(customerHistory);

                            }
                        }
                    }

                }
            }

            catch (Exception ex)
            {

                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;

    }

    public class CustomerHistory
    {
        public string CommitTime;
        public string UserName;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string operation;


    }
}


