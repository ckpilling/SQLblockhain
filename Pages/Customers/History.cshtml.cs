using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using static SQLchain.Pages.Customers.HistoryModel;

namespace SQLchain.Pages.Customers
{
    public class HistoryModel : PageModel
    {

        public List<CustomerHistory> listHistory = new List<CustomerHistory>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Server=tcp:verimy.database.windows.net,1433;Initial Catalog=SQLTables;Persist Security Info=False;User ID=verimysqladmin;Password=password0.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

               




                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("dbo.usp_listHistory", connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    
                   // command.ExecuteNonQuery();
                   //  String sql = "EXECUTE usp_listHistory";
                   //using (SqlCommand command = new SqlCommand(sql, connection))
                        
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomerHistory customerHistory = new CustomerHistory();
                                //   customerHistory.CommitTime = reader.GetDateTime(0).ToString();
                                //   customerHistory.UserName = reader.GetString(0);
                                customerHistory.id = "" + reader.GetInt32(0); 
                                customerHistory.name = reader.GetString(1);
                                customerHistory.email = reader.GetString(2);
                                customerHistory.phone = reader.GetString(3);
                                customerHistory.address = reader.GetString(4);
                                customerHistory.created_at = reader.GetDateTime(5).ToString();
                                customerHistory.ledger_transaction_id = "" + reader.GetInt64(6);
                                customerHistory.ledger_sequence_number = "" + reader.GetInt64(7);
                                customerHistory.ledger_operation_type = "" + reader.GetInt32(8);
                                customerHistory.ledger_operation_desc = reader.GetString(9);
                                //   customerHistory.operation = reader.GetString(5);

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




        public class CustomerHistory
        {
            public string ledger_operation_desc;
            public string ledger_operation_type;
            public string ledger_sequence_number;
            public string ledger_transaction_id;
            public string created_at;
            public string id;
            public string CommitTime;
            public string name;
            public string email;
            public string phone;
            public string address;
            public string operation;


        }

    }

}


