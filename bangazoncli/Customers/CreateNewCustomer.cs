using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace bangazoncli.Customers
{
    class CreateNewCustomer
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["SNQHM_bangazoncli_db"].ConnectionString;



        public bool InsertCustomer(string firstName, string lastName, string streetAddress, string city, string state, string zipCode, string phoneNumber)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO Customer 
                                (FirstName, 
                                LastName, 
                                CreatedDate, 
                                LastActiveDate, 
                                StreetAddress, 
                                City, 
                                State, 
                                ZipCode, 
                                PhoneNumber)
                                VALUES (@FirstName, @LastName, @CreatedDate, @LastActiveDate, @StreetAddress, @City, @State, @ZipCode, @PhoneNumber)";

                var FirstNameParam = new SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar);
                FirstNameParam.Value = firstName;
                cmd.Parameters.Add(FirstNameParam);

                var LastNameParam = new SqlParameter("@LastName", System.Data.SqlDbType.NVarChar);
                LastNameParam.Value = lastName;
                cmd.Parameters.Add(LastNameParam);

                var CreatedDateParam = new SqlParameter("@CreatedDate", System.Data.SqlDbType.Date);
                CreatedDateParam.Value = DateTime.Now;
                cmd.Parameters.Add(CreatedDateParam);

                var LastActiveParam = new SqlParameter("@LastActiveDate", System.Data.SqlDbType.Date);
                LastActiveParam.Value = DateTime.Now;
                cmd.Parameters.Add(LastActiveParam);

                var StreetAddressParam = new SqlParameter("@StreetAddress", System.Data.SqlDbType.NVarChar);
                StreetAddressParam.Value = streetAddress;
                cmd.Parameters.Add(StreetAddressParam);

                var CityParam = new SqlParameter("@City", System.Data.SqlDbType.NVarChar);
                CityParam.Value = city;
                cmd.Parameters.Add(CityParam);

                var StateParam = new SqlParameter("@State", System.Data.SqlDbType.Char);
                StateParam.Value = state;
                cmd.Parameters.Add(StateParam);

                var ZipCodeParam = new SqlParameter("@ZipCode", System.Data.SqlDbType.Int);
                ZipCodeParam.Value = int.Parse(zipCode);
                cmd.Parameters.Add(ZipCodeParam);

                var PhoneNumberParam = new SqlParameter("@PhoneNumber", System.Data.SqlDbType.VarChar);
                PhoneNumberParam.Value = Int64.Parse(phoneNumber);
                cmd.Parameters.Add(PhoneNumberParam);


                connection.Open();

                var result = cmd.ExecuteNonQuery();

                return result == 1;
               
            }
            
        }
    
    }

    public class Test
    {
        public static bool Testy()
        {
            var customerQuery = new CreateNewCustomer();

            Console.WriteLine("Please type your first name:");
            var firstName = Console.ReadLine();

            Console.WriteLine("Type your last name:");
            var lastName = Console.ReadLine();

            Console.WriteLine("Type your street address:");
            var streetAddress = Console.ReadLine();

            Console.WriteLine("City:");
            var city = Console.ReadLine();

            Console.WriteLine("State:");
            var state = Console.ReadLine();

            Console.WriteLine("Zip Code:");
            var zipCode = Console.ReadLine();

            Console.WriteLine("Phone Number:");
            var phoneNumber = Console.ReadLine();

            var result = customerQuery.InsertCustomer(firstName, lastName, streetAddress, city, state, zipCode, phoneNumber);

            Console.WriteLine("Type [0] to return to the main menu.");

            return result;
        }
       
    }
}
