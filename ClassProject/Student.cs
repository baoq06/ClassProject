using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ClassProject.Models
{
    public class Student
    {
        private int _userId;
        private string _mssv;
        private string _firstName;
        private string _lastName;
        private DateTime _dateOfBirth;
        private string _gender;
        private string _phone;
        private string _address;
        private string _hometown;
        private string _email;
        private byte[] _picture;

        public int Id { get; set; }

        public int UserId
        {
            get => _userId;
            set => _userId = value;
        }

        public string Mssv
        {
            get => _mssv;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("MSSV không hợp lệ!");

                _mssv = value.Trim();
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Tên không được để trống!");

                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Họ không được để trống!");

                _lastName = value;
            }
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (value > DateTime.Now)
                    throw new Exception("Ngày sinh không hợp lệ!");

                _dateOfBirth = value;
            }
        }

        public string Gender
        {
            get => _gender;
            set
            {
                if (value != "Nam" && value != "Nữ")
                    throw new Exception("Giới tính không hợp lệ!");

                _gender = value;
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Số điện thoại không hợp lệ!");

                _phone = value;
            }
        }

        public string Address
        {
            get => _address;
            set => _address = value;
        }

        public string Hometown
        {
            get => _hometown;
            set => _hometown = value;
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                    throw new Exception("Email không hợp lệ!");

                _email = value;
            }
        }

        public byte[] Picture
        {
            get => _picture;
            set => _picture = value;
        }

        public string FullName => $"{LastName} {FirstName}";

        public override string ToString()
        {
            return $"{Mssv} - {FullName}";
        }

        public static SqlParameter CreatePictureParameter(string name, byte[] picture)
        {
            return new SqlParameter(name, SqlDbType.VarBinary)
            {
                Value = picture ?? (object)DBNull.Value
            };
        }
    }
}