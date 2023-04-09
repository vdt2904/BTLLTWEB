using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTL.Models;

public partial class QlkhachSanAspContext : DbContext
{
    public QlkhachSanAspContext()
    {
    }

    public QlkhachSanAspContext(DbContextOptions<QlkhachSanAspContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Csvc> Csvcs { get; set; }

    public virtual DbSet<DatPhong> DatPhongs { get; set; }

    public virtual DbSet<DichVu> DichVus { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<LoaiPhong> LoaiPhongs { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<Phong> Phongs { get; set; }

    public virtual DbSet<SuDungDichVu> SuDungDichVus { get; set; }

    public virtual DbSet<SuDungThietBi> SuDungThietBis { get; set; }

    public virtual DbSet<ThietBi> ThietBis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=VDT\\SQLEXPRESS;Initial Catalog=QLKhachSanASP;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.Idblog);

            entity.ToTable("Blog");

            entity.Property(e => e.Idblog)
                .HasMaxLength(10)
                .HasColumnName("IDblog");
            entity.Property(e => e.Anh).HasMaxLength(50);
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayDang).HasColumnType("date");
            entity.Property(e => e.ThongTin).HasColumnType("text");
            entity.Property(e => e.TieuDe).HasMaxLength(100);

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_Blog_NhanVien");
        });

        modelBuilder.Entity<Csvc>(entity =>
        {
            entity.ToTable("CSVC");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<DatPhong>(entity =>
        {
            entity.HasKey(e => new { e.MaPhong, e.SoHoaDon });

            entity.ToTable("DatPhong");

            entity.Property(e => e.MaPhong).HasMaxLength(10);
            entity.Property(e => e.SoHoaDon).HasMaxLength(10);
            entity.Property(e => e.NgayDen).HasColumnType("date");
            entity.Property(e => e.NgayDi).HasColumnType("date");

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.DatPhongs)
                .HasForeignKey(d => d.MaPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DatPhong_Phong");

            entity.HasOne(d => d.SoHoaDonNavigation).WithMany(p => p.DatPhongs)
                .HasForeignKey(d => d.SoHoaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DatPhong_HoaDon");
        });

        modelBuilder.Entity<DichVu>(entity =>
        {
            entity.HasKey(e => e.MaDv);

            entity.ToTable("DichVu");

            entity.Property(e => e.MaDv)
                .HasMaxLength(10)
                .HasColumnName("MaDV");
            entity.Property(e => e.Anh).HasMaxLength(50);
            entity.Property(e => e.TenDv)
                .HasMaxLength(50)
                .HasColumnName("TenDV");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.SoHoaDon);

            entity.ToTable("HoaDon");

            entity.Property(e => e.SoHoaDon).HasMaxLength(10);
            entity.Property(e => e.MaKh)
                .HasMaxLength(10)
                .HasColumnName("MaKH");
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayThanhToan).HasColumnType("date");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_HoaDon_KhachHang");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_HoaDon_NhanVien");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh);

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKh)
                .HasMaxLength(10)
                .HasColumnName("MaKH");
            entity.Property(e => e.Cccd)
                .HasMaxLength(50)
                .HasColumnName("CCCD");
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.GioiTinh).HasMaxLength(50);
            entity.Property(e => e.LoaiKhachHang).HasMaxLength(50);
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
            entity.Property(e => e.TenKh)
                .HasMaxLength(50)
                .HasColumnName("TenKH");
        });

        modelBuilder.Entity<LoaiPhong>(entity =>
        {
            entity.HasKey(e => e.MaLp);

            entity.ToTable("LoaiPhong");

            entity.Property(e => e.MaLp)
                .HasMaxLength(10)
                .HasColumnName("MaLP");
            entity.Property(e => e.Anh).HasMaxLength(50);
            entity.Property(e => e.KichThuoc).HasMaxLength(50);
            entity.Property(e => e.LoaiPhong1)
                .HasMaxLength(50)
                .HasColumnName("LoaiPhong");
            entity.Property(e => e.ThongTin).HasMaxLength(100);
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Login_1");

            entity.ToTable("Login");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Logins)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_Login_NhanVien");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv);

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");
            entity.Property(e => e.Anh).HasMaxLength(50);
            entity.Property(e => e.Cccd)
                .HasMaxLength(50)
                .HasColumnName("CCCD");
            entity.Property(e => e.ChucVu).HasMaxLength(50);
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.GioiTinh).HasMaxLength(50);
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
            entity.Property(e => e.TenNv)
                .HasMaxLength(50)
                .HasColumnName("TenNV");
        });

        modelBuilder.Entity<Phong>(entity =>
        {
            entity.HasKey(e => e.MaPhong);

            entity.ToTable("Phong");

            entity.Property(e => e.MaPhong).HasMaxLength(10);
            entity.Property(e => e.Anh).HasMaxLength(50);
            entity.Property(e => e.MaLp)
                .HasMaxLength(10)
                .HasColumnName("MaLP");
            entity.Property(e => e.TenPhong).HasMaxLength(50);
            entity.Property(e => e.TinhTrang).HasMaxLength(50);

            entity.HasOne(d => d.MaLpNavigation).WithMany(p => p.Phongs)
                .HasForeignKey(d => d.MaLp)
                .HasConstraintName("FK_Phong_LoaiPhong");
        });

        modelBuilder.Entity<SuDungDichVu>(entity =>
        {
            entity.HasKey(e => new { e.MaDv, e.SoHoaDon });

            entity.ToTable("SuDungDichVu");

            entity.Property(e => e.MaDv)
                .HasMaxLength(10)
                .HasColumnName("MaDV");
            entity.Property(e => e.SoHoaDon).HasMaxLength(10);
            entity.Property(e => e.NgayMua).HasColumnType("date");

            entity.HasOne(d => d.MaDvNavigation).WithMany(p => p.SuDungDichVus)
                .HasForeignKey(d => d.MaDv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SuDungDichVu_DichVu");

            entity.HasOne(d => d.SoHoaDonNavigation).WithMany(p => p.SuDungDichVus)
                .HasForeignKey(d => d.SoHoaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SuDungDichVu_HoaDon");
        });

        modelBuilder.Entity<SuDungThietBi>(entity =>
        {
            entity.HasKey(e => new { e.MaTb, e.MaPhong });

            entity.ToTable("SuDungThietBi", tb => tb.HasTrigger("trg_CSVC"));

            entity.Property(e => e.MaTb)
                .HasMaxLength(10)
                .HasColumnName("MaTB");
            entity.Property(e => e.MaPhong).HasMaxLength(10);
            entity.Property(e => e.NgaySD)
                .HasColumnType("date")
                .HasColumnName("NgaySD");
            entity.Property(e => e.TinhTrang).HasMaxLength(50);

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.SuDungThietBis)
                .HasForeignKey(d => d.MaPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SuDungThietBi_Phong");

            entity.HasOne(d => d.MaTbNavigation).WithMany(p => p.SuDungThietBis)
                .HasForeignKey(d => d.MaTb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SuDungThietBi_ThietBi");
        });

        modelBuilder.Entity<ThietBi>(entity =>
        {
            entity.HasKey(e => e.MaTb);

            entity.ToTable("ThietBi");

            entity.Property(e => e.MaTb)
                .HasMaxLength(10)
                .HasColumnName("MaTB");
            entity.Property(e => e.Anh).HasMaxLength(50);
            entity.Property(e => e.TenTb)
                .HasMaxLength(50)
                .HasColumnName("TenTB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
