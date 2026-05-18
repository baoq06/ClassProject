namespace ClassProject.Models
{
    public class Score
    {
        public string Mssv { get; set; } = "";
        public string Mamh { get; set; } = "";
        public decimal Diemqt { get; set; }
        public decimal Diemck { get; set; }
        public decimal Diemtk => Math.Round(Diemqt * 0.4m + Diemck * 0.6m, 2);
        public string? Mota { get; set; }
        public int? Sotc { get; set; }
        public string? Tenmh { get; set; }
        public string? TenSV { get; set; }
    }
}
