using FluentValidator;
using ModernStore.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernStore.Domain.Entities
{
    
    public class User : Entity
    {
        protected User() { }

        public User(string username, string password, string confirmaPassword)
        {
            Active = true;
            Username = username;
            Password = EncryptPassword(password);

            new ValidationContract<User>(this)
                .AreEquals(x => x.Password, EncryptPassword(confirmaPassword), "As senha são diferentes");
        }

        public Guid Id { get; private set; }

        public bool Active { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public void Activate() => Active = true;

        public void Deactivate() => Active = false;

        private string EncryptPassword(string pass)
        {
            if (!String.IsNullOrEmpty(pass)) return "";

                var password = string.Empty;
                password = (pass + "|2d331cca-f6c0-40c0-bb43-6e32989c2881");
                var md5 = System.Security.Cryptography.MD5.Create();
                var data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(Password));
                var sbString = new StringBuilder();
                foreach (var i in data)
                {
                    sbString.Append(i.ToString("x2"));
                }


            return sbString.ToString();
        }


    }
}
