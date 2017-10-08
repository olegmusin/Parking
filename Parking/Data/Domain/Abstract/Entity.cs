using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingApp.Data.Domain.Abstract
{
    public abstract class Entity<T> : IEntity<T> where T : class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }

        object IEntity.Id
        {
            get => Id;
            set => Id = value as T;
        }

        public string Name { get; set; }

        private DateTime? _createdDate;
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate
        {
            get => _createdDate ?? DateTime.UtcNow;
            set => _createdDate = value;
        }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
