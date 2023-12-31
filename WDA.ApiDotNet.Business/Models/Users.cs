﻿#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace WDA.ApiDotNet.Business.Models
{
    public class Users
    {
        public Users() { }
        public Users(int id, string name, string city, string address, string email)
        {
            Id = id;
            Name = name;
            City = city;
            Address = address;
            Email = email;
        }

        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string City { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }
    }
}
