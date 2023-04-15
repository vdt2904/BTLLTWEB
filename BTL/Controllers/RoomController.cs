using Azure;
using BTL.Models;
using BTL.Models.RoomModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using X.PagedList;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BTL.Controllers
{
    public class RoomController : Controller
    {

        QlkhachSanAspContext db = new QlkhachSanAspContext();

        public string connectionString = @"Data Source=VDT\SQLEXPRESS;Initial Catalog=QLKhachSanASP;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstLoaiPhong = db.LoaiPhongs.AsNoTracking().OrderBy(x => x.MaLp);

            PagedList<LoaiPhong> lst = new PagedList<LoaiPhong>(lstLoaiPhong, pageNumber, pageSize);
            return View(lst);
        }

        public IActionResult PhongTheoLoai(string maLoai, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstPhong = db.Phongs.AsNoTracking().Where(x => x.MaLp == maLoai).OrderBy(x => x.MaLp).ToList();

            var phong = db.LoaiPhongs.Where(x => x.MaLp == maLoai).FirstOrDefault();
            ViewBag.Gia = phong.Gia;

            ViewBag.maloai = maLoai;

            PagedList<Phong> lst = new PagedList<Phong>(lstPhong, pageNumber, pageSize);
            return View(lst);
        }


        public IActionResult ChiTietPhong(string maphong, string maloai)
        {
            HttpContext.Session.SetString("maphong1", maphong);
            var Lphong = db.LoaiPhongs.SingleOrDefault(x => x.MaLp == maloai);

            var phong = db.Phongs.Where(x => x.MaPhong == maphong && x.MaLp == maloai).FirstOrDefault();

            ViewBag.TenPhong = phong.TenPhong;
            ViewBag.vote = phong.Slvote;

            ViewBag.TinhTrang = phong.TinhTrang;

            var ctAnh = db.Ctanhs.Where(x => x.MaLp == maloai).ToList();
            ViewBag.ctAnh = ctAnh;

            return View(Lphong);
        }

        private static Models.RoomModels.GetRoom getRoom;

        public IActionResult GetForm(Models.RoomModels.GetRoom gf)
        {
            getRoom = gf;
            return RedirectToAction("GetRoom");
        }


        public IActionResult GetRoom(int? page)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            string sql = "";
            string MaLp = getRoom.MaLp;
            string NgayDen = getRoom.NgayDen.Value.ToString("yyyy/MM/dd");
            string NgayDi = getRoom.NgayDi.Value.ToString("yyyy/MM/dd");

            List<string> lst = new List<string>();

            List<string> lstMaPhong = new List<string>();

            List<string> lstMaLp = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (MaLp != "all")
                {
                    sql = "select dp.MaPhong from DatPhong dp inner join Phong p on dp.MaPhong = p.MaPhong" +
                        " where ((dp.NgayDen >= CONVERT(datetime, '" + NgayDen + "') and dp.NgayDen <= CONVERT(datetime, '" + NgayDi + "') and dp.NgayDi >= CONVERT(datetime, '" + NgayDi + "')) " +
                        " or(dp.NgayDen <= CONVERT(datetime, '" + NgayDen + "') and dp.NgayDi >= CONVERT(datetime, '" + NgayDi + "')) " +
                        " or(dp.NgayDen <= CONVERT(datetime, '" + NgayDen + "') and dp.NgayDi >= CONVERT(datetime, '" + NgayDen + "') and dp.NgayDi <= CONVERT(datetime, '" + NgayDi + "')) " +
                        " or(dp.NgayDen >= CONVERT(datetime, '" + NgayDen + "') and dp.NgayDi <= CONVERT(datetime, '" + NgayDi + "')))" +
                        " and p.MaLP = '" + MaLp + "'";

                }
                else
                {
                    sql = "select dp.MaPhong from DatPhong dp inner join Phong p on dp.MaPhong = p.MaPhong " +
                        " where ((dp.NgayDen >= CONVERT(datetime, '" + NgayDen + "') and dp.NgayDen <= CONVERT(datetime, '" + NgayDi + "') and dp.NgayDi >= CONVERT(datetime, '" + NgayDi + "')) " +
                        " or(dp.NgayDen <= CONVERT(datetime, '" + NgayDen + "') and dp.NgayDi >= CONVERT(datetime, '" + NgayDi + "')) " +
                        " or(dp.NgayDen <= CONVERT(datetime, '" + NgayDen + "') and dp.NgayDi >= CONVERT(datetime, '" + NgayDen + "') and dp.NgayDi <= CONVERT(datetime, '" + NgayDi + "')) " +
                        " or(dp.NgayDen >= CONVERT(datetime, '" + NgayDen + "') and dp.NgayDi <= CONVERT(datetime, '" + NgayDi + "')))";
                }

                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lst.Add(reader.GetString(0));
                    }
                }
                connection.Close();

                if (lst.Count() == 0)
                {
                    if (MaLp != "all")
                    {
                        sql = "select MaPhong, MaLP from Phong where MaLP = '" + MaLp + "'";
                    }
                    else
                    {
                        sql = "select MaPhong, MaLP from Phong";
                    }
                }
                else
                {
                    sql = "select MaPhong, MaLP from Phong where MaPhong != '" + lst[0].ToString() + "'";
                    for (int i = 1; i < lst.Count; i++)
                    {
                        sql += " and MaPhong != '" + lst[i].ToString() + "'";
                    }

                    if (MaLp != "all")
                    {
                        sql += " and MaLP = '" + MaLp + "'";
                    }
                }

                command = new SqlCommand(sql, connection);
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lstMaPhong.Add(reader.GetString(0));

                        lstMaLp.Add(reader.GetString(1));
                    }
                }
                connection.Close();
            }

            List<Phong> lstPhong = new List<Phong>();

            List<DatPhong> datphongs = new List<DatPhong>();

            foreach (var item in lstMaPhong)
            {
                var tmp = db.Phongs.Where(x => x.MaPhong == item).FirstOrDefault();
                lstPhong.Add(tmp);
            }

            List<LoaiPhong> loaiPhongs = new List<LoaiPhong>();

            foreach (var item in lstMaLp)
            {
                var tmp = db.LoaiPhongs.Where(x => x.MaLp == item).FirstOrDefault();
                loaiPhongs.Add(tmp);
            }

            ViewBag.lstPhongs = loaiPhongs;

            PagedList<Phong> pageList = new PagedList<Phong>(lstPhong, pageNumber, pageSize);

            return View(pageList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult datphong()
        {
            var c = db.HoaDons.Max(x => x.SoHoaDon);
            string ma = maHdTd(c.ToString());
            HoaDon a = new HoaDon();
            a.SoHoaDon = ma;
            db.HoaDons.Add(a);
            db.SaveChanges();
            ViewBag.MaPhong = new SelectList(db.Phongs.ToList(), "MaPhong", "TenPhong");
            ViewBag.SoHoaDon = new SelectList(db.HoaDons.ToList(), "SoHoaDon", "SoHoaDon");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult datphong(DatPhong dp)
        {
            if (ModelState.IsValid)
            {
                ViewBag.MaPhong = new SelectList(db.Phongs.ToList(), "MaPhong", "TenPhong");
                ViewBag.SoHoaDon = new SelectList(db.HoaDons.ToList(), "SoHoaDon", "SoHoaDon");
                db.DatPhongs.Add(dp);
                db.SaveChanges();
                DateTime dt = DateTime.Now;
                string sql = "update hoadon set ngaythanhtoan = '" + dt.ToString("yyyy/MM/dd") + "' where sohoadon = '" + dp.SoHoaDon.ToString()+"'";
                db.Database.ExecuteSqlRaw(sql);
                return RedirectToAction("Index","Home");
            }
            return View(dp);
        }
        public string maHdTd(string a)
        {
            var c = db.HoaDons.Max(x => x.SoHoaDon);
            string maHD = a;
            Match match = Regex.Match(maHD, @"\d+");
            int soHD = int.Parse(match.Value);
            soHD++;
            string soHDString = soHD.ToString().PadLeft(match.Value.Length, '0');
            string maHDMoi = maHD.Substring(0, match.Index) + soHDString;
            return maHDMoi;
        }

        public IActionResult Booking()
        {
            string sql = "delete hoadon where sohoadon not in (select sohoadon from datphong)";

            db.Database.ExecuteSqlRaw(sql);
            db.SaveChanges();
            return RedirectToAction("Datphong", "datphong");
        }
    }
}
