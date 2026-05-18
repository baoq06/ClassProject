using ClassProject.DataAccess.Db;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ClassProject
{
    public class User
    {
        private string _id;
        private string _firstName;
        private string _lastName;
        private string _username;
        private string _password;
        private string _email;
        private byte[] _picture;

        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Họ và tên đệm không được để trống!");
                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Tên không được để trống!");
                _lastName = value;
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Tên đăng nhập không được để trống!");
                _username = value;
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Mật khẩu không được để trống!");
                _password = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrEmpty(value) || !value.Contains("@"))
                    throw new Exception("Email không hợp lệ!");
                _email = value;
            }
        }

        public byte[] Picture
        {
            get => _picture;
            set
            {
                if (value != null && value.Length > 5 * 1024 * 1024)
                    throw new Exception("Ảnh không được vượt quá 5MB!");
                _picture = value;
            }
        }

        public string FullName => $"{FirstName} {LastName}";

        public User() { }

        public User(string id, string firstName, string lastName,
                     string username, string password, string email,
                     byte[] picture = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Email = email;
            Picture = picture;
        }

        public override string ToString()
        {
            return $"ID        : {Id}\n" +
                   $"Họ tên    : {FullName}\n" +
                   $"Username  : {Username}\n" +
                   $"Email     : {Email}\n" +
                   $"Ảnh       : {(Picture != null ? "Có" : "Chưa có")}";
        }
    }
}
