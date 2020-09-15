using System.Collections.Generic;
using System.Data;
using Entities;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace Data
{
    public class UsersContext
    {
        public UsersContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = GetConnection();
        }
        
        private readonly IConfiguration _configuration;
        private readonly OracleConnection _connection;

        public User GetByLogin(string login)
        {
            using(OracleConnection connection = GetConnection()) 
            {
                using (OracleCommand cmd = connection.CreateCommand())
                {
                    User user = new User();
                    
                    connection.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;
                    cmd.CommandText = "PKG_WEB_PORTAL.User_GetByLogin";
                    
                    cmd.Parameters.Add(new OracleParameter() { ParameterName = "p_userLogin", OracleDbType = OracleDbType.Varchar2, Value = login });
                    cmd.Parameters.Add(new OracleParameter() { ParameterName = "userList", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output });

                    OracleDataReader reader = cmd.ExecuteReader();
                    if (reader != null) 
                    {
                        while (reader.Read())
                        {
                            user = new User()
                            {
                                Id = reader.GetInt64("ID"),
                                Login = reader.IsDBNull("LOGIN") ? string.Empty : reader.GetString("LOGIN"),
                                PasswordHash =  reader.IsDBNull("PASSWORDHASH") ? string.Empty : reader.GetString("PASSWORDHASH"),
                                ClientId =  reader.GetInt64("CLNID"),

                                Lastname =  reader.IsDBNull("LASTNAME") ? string.Empty : reader.GetString("LASTNAME"),
                                Firstname =  reader.IsDBNull("FIRSTNAME") ? string.Empty : reader.GetString("FIRSTNAME"),
                                Secondname =  reader.IsDBNull("SECONDNAME") ? string.Empty : reader.GetString("SECONDNAME"),
                                
                                Email = reader.GetString("EMAIL"),

                                Region = new Region() 
                                {
                                    Id = reader.GetInt64("REGIONID"),
                                    Code = reader.IsDBNull("REGIONCODE") ? string.Empty : reader.GetString("REGIONCODE"),
                                    Name = reader.IsDBNull("REGIONNAME") ? string.Empty : reader.GetString("REGIONNAME"),
                                    AOGUID = reader.IsDBNull("REGIONAOGUID") ? string.Empty : reader.GetString("REGIONAOGUID")
                                },
                                Company = new ReferenceItem()
                                {
                                    Id = reader.GetInt64("COMPANYID"),
                                    Code = reader.IsDBNull("COMPANYCODE") ? string.Empty : reader.GetString("COMPANYCODE"),
                                    Name = reader.IsDBNull("COMPANYNAME") ? string.Empty : reader.GetString("COMPANYNAME")
                                },
                                DeliveryCenter = new ReferenceItem()
                                {
                                    Id = reader.GetInt64("DEPARTMENTID"),
                                    Code = reader.IsDBNull("DEPARTMENTCODE") ? string.Empty : reader.GetString("DEPARTMENTCODE"),
                                    Name = reader.IsDBNull("DEPARTMENTNAME") ? string.Empty : reader.GetString("DEPARTMENTNAME")
                                }
                            };
                        }

                        reader.Dispose();
                    }

                    return user;
                }
            }                        
        } 

        public ContactInfo GetContactInfo(long clientId)
        {                        
            using(OracleConnection connection = GetConnection())
            {
                using(OracleCommand cmd = connection.CreateCommand())
                {
                    ContactInfo contactInfo = new ContactInfo();

                    connection.Open();

                    cmd.CommandType = CommandType.Text;
                    cmd.BindByName = true;
                    cmd.CommandText = "select * from table(pkg_clients_util.Contacts_Get(:i_client_id))";

                    cmd.Parameters.Add(new OracleParameter() { ParameterName = "i_client_id", OracleDbType = OracleDbType.Int64, Value = clientId });

                    OracleDataReader reader = cmd.ExecuteReader();
                    
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            var cntTypeId = reader.GetInt32("CNTTYPEID");
                            // если значение = 0 значит номер норм
                            var invalid = reader.GetInt64("INVALID");

                            if (cntTypeId == 1) 
                            {                
                                if (invalid == 0) 
                                    contactInfo.Phone = reader.GetString("CONTACT");                
                                else 
                                    contactInfo.PhoneBad = reader.GetString("CONTACT");                
                            }                
                            else if (cntTypeId == 2)
                            {
                                contactInfo.PhoneJob = reader.GetString("CONTACT");
                            }
                            else if (cntTypeId == 3)
                            {
                                if (invalid == 0)
                                    contactInfo.Email = reader.GetString("CONTACT");
                                else
                                    contactInfo.EmailBad = reader.GetString("CONTACT");
                            }                
                            else if (cntTypeId == 40)
                            {
                                contactInfo.PhoneInner = reader.GetString("CONTACT");
                            }
                        }

                        reader.Dispose();
                    }

                    

                    return contactInfo;
                }
            }            
        }        

        private OracleConnection GetConnection() 
        {
            var connectionString = _configuration.GetSection("ConnectionString").GetSection("resomed").Value;
            
            return new OracleConnection(connectionString);
        }
    }
}