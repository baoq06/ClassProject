namespace ClassProject.Models
{
    public class Course
    {
        private string _mamh;
        private string _tenmh;
        private int _sotc;

        public string Mamh
        {
            get => _mamh;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Mã môn học không được để trống!");
                _mamh = value.Trim().ToUpper();
            }
        }

        public string Tenmh
        {
            get => _tenmh;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Tên môn học không được để trống!");
                _tenmh = value.Trim();
            }
        }

        public int Sotc
        {
            get => _sotc;
            set
            {
                if (value <= 0 || value > 10)
                    throw new Exception("Số tín chỉ phải từ 1 đến 10!");
                _sotc = value;
            }
        }

        public int Tuan { get; set; }
        public int Hocky { get; set; }
        public string Description { get; set; } = "";
    }
}
