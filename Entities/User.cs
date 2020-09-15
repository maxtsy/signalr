using System.Collections.Generic;

namespace Entities
{
    public class User
    {
        public User()
        {
            Roles = new List<Role>();
            PresenseInfo = new List<Presense>();
        }        

        #region Properties
        public long Id { get; set; }
        // секретка
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        // id соединения в БД
        public long ConnectionLogId { get; set; }
        // id соединения для signalR
        public string ConnectionId { get; set; }
        public List<Role> Roles { get; set; }
        // откуда
        public Region Region { get; set; }
        public ReferenceItem Company { get; set; }
        public List<Presense> PresenseInfo { get; set; }
        // где        
        public ReferenceItem DeliveryCenter { get; set; }
        // перс данные
        public long ClientId { get; set; }
        public string Firstname { get; set; }
        public string Secondname { get; set; }
        public string Lastname { get; set; }
        public bool? IsBlocked { get; set; }                
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool isBroker { get; set; }        
        #endregion        
              
        #region Methods
        public string Fullname
        {
            get
            {
                return string.Format("{0} {1} {2}",
                    Lastname ?? string.Empty,
                    Firstname ?? string.Empty,
                    Secondname ?? string.Empty);
            }
        }        
        #endregion
    }
}