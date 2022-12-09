using Dapper;
using DataAPI.Data.Models;
using System.Data.SqlClient;

namespace DataAPI.Data.Access
{
    public class AdminDB
    {
        public static AdminDB _object;
        /// <summary>
        /// Initiate AdminDB
        /// </summary>
        public static AdminDB Instance
        {
            get
            {
                if (_object == null)
                {
                    _object = new AdminDB();
                }
                return _object;
            }
        }
        private SqlConnection connection = null;
        private AdminDB()
        {
            // Get connection string from appsettings.json file
            var appsettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectString = appsettings.GetSection("ConnectionStrings")["DB"];
            connection = new SqlConnection(connectString);
        }
        public Admin GetAdmin(string email)
        {
            string sql = "select * from [Admins] where [Email]=@email";
            connection.Open();
            Admin admin = connection.Query<Admin>(sql, new { email = email }).FirstOrDefault();
            connection.Close();
            return admin;
        }
        public bool CreateAdmin(Admin admin)
        {
            string sql = "insert into [Admins](Name, Email, PasswordSalt, PasswordHash, Role, Details) values(@Name, @Email, @PasswordSalt, @PasswordHash, @Role, @Details)";
            var param = new
            {
                Name = admin.Name,
                Email = admin.Email,
                PasswordSalt = admin.PasswordSalt,
                PasswordHash = admin.PasswordHash,
                Role = admin.Role,
                Details = admin.Details
            };
            connection.Open();
            bool result = connection.Execute(sql, param) == 1;
            connection.Close();
            return result;
        }
        public bool UpdateAdmin(Admin admin)
        {
            string sql = "update [Admins] set Name=@Name, Email=@Email, PasswordSalt=@PasswordSalt, PasswordHash=@PasswordHash, Role=@Role, Details=@Details where Id=@Id";
            var param = new
            {
                Name = admin.Name,
                Email = admin.Email,
                PasswordSalt = admin.PasswordSalt,
                PasswordHash = admin.PasswordHash,
                Role = admin.Role,
                Details = admin.Details,
                Id = admin.Id
            };
            connection.Open();
            bool result = connection.Execute(sql, param) == 1;
            connection.Close();
            return result;
        }
        public bool DeleteAdmin(string id)
        {
            string sql = "delete from [Admins] where Id=" + id;
            connection.Open();
            bool result = connection.Execute(sql) == 1;
            connection.Close();
            return result;
        }

        public List<Admin> GetAllAdmins(string keySearch = "")
        {
            string sql = "select * from [Admins]";
            if (!string.IsNullOrWhiteSpace(keySearch))
            {
                sql += " where Email like " + keySearch + " or Name like N'%" + keySearch + "%'";
            }
            connection.Open();
            List<Admin> result = connection.Query<Admin>(sql).ToList();
            connection.Close();
            return result;
        }
    }
}
