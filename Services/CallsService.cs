using System.Collections.Generic;
using Data;
using Entities;

namespace Services
{
    public class CallsService
    {
        public CallsService(UsersContext usersContext, CallsContext callsContext)
        {
            _usersContext = usersContext;
            _callsContext = callsContext;
        }
        public UsersContext _usersContext { get; set; }
        public CallsContext _callsContext { get; set; }

        public List<Call> GetCallsList(string login)
        {
            if (!string.IsNullOrEmpty(login))
            {
                var user = _usersContext.GetByLogin(login);
                
                var contactInfo = _usersContext.GetContactInfo(user.ClientId);

                if (!string.IsNullOrEmpty(contactInfo.PhoneInner))
                {
                    return _callsContext.GetCallsList(contactInfo.PhoneInner);
                }

                return new List<Call>();
            }
            
            return new List<Call>();
        }
    }
}