using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;
using Microsoft.Data.SqlClient;

namespace Karnel_Travel_Admin_Api.Services;

public class DiscountServiceImpl : IDiscountService
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public DiscountServiceImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }
    public bool create(Discount discount)
    {
        
        string connectionString = configuration["ConnectionStrings:DefaultConnection"];

        string insertQuery = "insert into discount(discount_code, discount_percent) values (@code, @percent)";
        

     
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                
                connection.Open();

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                   
                    command.Parameters.AddWithValue("@code", discount.DiscountCode);
                    command.Parameters.AddWithValue("@percent", discount.DiscountPercent);

                    
                    int rowsAffected = command.ExecuteNonQuery();

                    
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                
                //Console.WriteLine(ex.Message);
                return false;
            }
        }

    }

    public List<Discount> findAll()
    {
        return db.Discounts.ToList();
    }

    public bool update(Discount discount)
    {
        string connectionString = configuration["ConnectionStrings:DefaultConnection"];

        string insertQuery = "update discount set discount_percent = @percent  where discount_code = @code";



        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {

                connection.Open();

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {

                    command.Parameters.AddWithValue("@code", discount.DiscountCode);
                    command.Parameters.AddWithValue("@percent", discount.DiscountPercent);


                    int rowsAffected = command.ExecuteNonQuery();


                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
