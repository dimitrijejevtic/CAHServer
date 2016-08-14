using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Service.Models
{
    [NotMapped]
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public User()
        {
            UserId = Guid.NewGuid().ToString();
        }
        public override bool Equals(object obj)
        {
            try
            {
                var ob = (User)obj;
                if (ob.Name == Name && ob.UserId == UserId)
                {
                    return true;
                }
                else return false;
            }catch(Exception)
            {
                return false;
            }
        }
        public override string ToString()
        {
            return UserId;
        }

    }
}