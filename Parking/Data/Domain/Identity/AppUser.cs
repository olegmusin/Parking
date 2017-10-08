using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using ParkingApp.Data.Domain.Abstract;

namespace ParkingApp.Data.Domain.Identity
{
    public class AppUser : IdentityUser, IEntity<string>
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string Name { get; set; }
        object IEntity.Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Version { get; set; }
    }
}
