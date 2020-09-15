using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Entities;
using System.Data;
using System;

namespace Data
{
    public class CallsContext
    {
        public CallsContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = GetConnection();
        }
        
        private readonly IConfiguration _configuration;
        private readonly OracleConnection _connection;
        
        public List<Call> GetCallsList(string innerPhone)
        {
            using (OracleConnection connection = GetConnection())
            {
                using (OracleCommand cmd = connection.CreateCommand())
                {
                    List<Call> items = new List<Call>();

                    connection.Open();
                    
                    cmd.CommandType = CommandType.Text;
                    cmd.BindByName = true;

                    cmd.CommandText = "select * from clm_calls where 1=1 and nvl(uniqueid,linkedid) = linkedid and (connected_number = :i_call_number or first_number = :i_call_number) and trunc(startdate) = trunc(sysdate)";

                    cmd.Parameters.Add(new OracleParameter { ParameterName = "i_call_number", OracleDbType = OracleDbType.Varchar2, Value = innerPhone });  
                    // cmd.Parameters.Add(new OracleParameter() { ParameterName = "House_objects", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output });

                    OracleDataReader reader = cmd.ExecuteReader();
                    
                    if (reader != null)
                    {                        
                        while (reader.Read())
                        {
                            var item = new Call()
                            {
                                Id = reader.GetInt64("ID"),
                                LinkedId = reader.IsDBNull("LINKEDID") ? string.Empty : reader.GetString("LINKEDID"),
                                Name = reader.IsDBNull("NAME") ? string.Empty : reader.GetString("NAME"),
                                State = reader.IsDBNull("STATE") ? string.Empty : reader.GetString("STATE"),
                                CallerName = reader.IsDBNull("CALLER_NAME") ? string.Empty : reader.GetString("CALLER_NAME"),
                                CallerNumber = reader.IsDBNull("CALLER_NUMBER") ? string.Empty : reader.GetString("CALLER_NUMBER"),
                                ConnectedName = reader.IsDBNull("CONNECTED_NAME") ? string.Empty : reader.GetString("CONNECTED_NAME"),
                                ConnectedNumber = reader.IsDBNull("CONNECTED_NUMBER") ? string.Empty : reader.GetString("CONNECTED_NUMBER"),
                                StartDate = reader.GetDateTime("STARTDATE"),
                                Did = reader.IsDBNull("DID") ? string.Empty : reader.GetString("DID"),
                                UniqueId = reader.IsDBNull("UNIQUEID") ? string.Empty : reader.GetString("UNIQUEID"),
                                CallDate = reader.IsDBNull("CALLDATE") ? (DateTime?) null : reader.GetDateTime("CALLDATE"),
                                Dst = reader.IsDBNull("DST") ? string.Empty : reader.GetString("DST"),
                                Channel = reader.IsDBNull("CHANNEL") ? string.Empty : reader.GetString("CHANNEL"),
                                DstChannel = reader.IsDBNull("DSTCHANNEL") ? string.Empty : reader.GetString("DSTCHANNEL"),
                                Duration = reader.IsDBNull("DURATION") ? (int?) null : reader.GetInt32("DURATION"),
                                BillSec = reader.IsDBNull("BILLSEC") ? (int?) null : reader.GetInt32("BILLSEC"),
                                CdRok = reader.GetInt32("CDROK"),
                                RecordingFile = reader.IsDBNull("RECORDINGFILE") ? string.Empty : reader.GetString("RECORDINGFILE"),
                                FinishDate = reader.GetDateTime("FINISHDATE"),
                                Agree1 = reader.IsDBNull("DOCID1") ? (long?) null : reader.GetInt64("DOCID1"),
                                Agree2 = reader.IsDBNull("DOCID2") ? (long?) null : reader.GetInt64("DOCID2"),
                                Agree3 = reader.IsDBNull("DOCID3") ? (long?) null : reader.GetInt64("DOCID3"),
                                RegionId = reader.IsDBNull("REGIONID") ? (long?) null : reader.GetInt64("REGIONID"),
                                CompanyId = reader.IsDBNull("COMPANYID") ? (long?) null : reader.GetInt64("COMPANYID"),
                                ReciptTypeId = reader.IsDBNull("RECIPTTYPEID") ? (long?) null  : reader.GetInt32("RECIPTTYPEID"),
                                Note = reader.IsDBNull("NOTE") ? string.Empty : reader.GetString("NOTE"),
                                FirstNumber = reader.IsDBNull("FIRST_NUMBER") ? string.Empty : reader.GetString("FIRST_NUMBER"),
                            };

                            items.Add(item);
                        }

                        reader.Dispose();
                    }

                    return items;
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