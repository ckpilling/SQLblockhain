using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SQLchain.Pages.Subscribers
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];


            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||
            clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "All fields must be completed";
                return;
            }


            try
            {
                String connectionString = "Server=tcp:verimy.database.windows.net,1433;Initial Catalog=SQLTables;Persist Security Info=False;User ID=verimysqladmin;Password=password0.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO subscribers" +
                        "(name, email, phone, address) VALUES" +
                        "(@name, @email, @phone, @address);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("name", clientInfo.name);
                        command.Parameters.AddWithValue ("email", clientInfo.email);
                        command.Parameters.AddWithValue("address", clientInfo.address);
                        command.Parameters.AddWithValue("phone", clientInfo.phone);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }

            clientInfo.name = " "; clientInfo.email = " "; clientInfo.phone = " "; clientInfo.address = " ";
            successMessage = "New client has been added";

            Response.Redirect("/Subscribers/Index");

        }
    }

}


