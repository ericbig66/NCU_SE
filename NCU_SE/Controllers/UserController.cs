﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using NCU_SE.Data;
using NCU_SE.Models;
// <summary>
// 使用者相關控制器
// </summary>
namespace NCU_SE.Controllers
{
    public class UserController : Controller
    {
        //private readonly ILogger<UserController> _logger;
        /*
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        */
        //injector

        private readonly ApplicationDbContext _db; //使用資料庫實體

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Register(Member obj)
        {
            //驗證Email是否已被註冊-->SQL語法轉換 = SELECT count(Email) from Member where Email ='[使用者填寫的Email]'
            int accountExist = obj.Email == null ? 0: _db.Member.Where(u => u.Email == obj.Email.ToString()).Count();
            Debug.Print("帳戶存在數量"+accountExist);
            if(accountExist == 0 && obj.Email!=null)
            {
                obj.RegisterDate = System.DateTime.Today;//將註冊日期加入
                _db.Member.Add(obj);//相當於SQL的insert語法
                _db.SaveChanges();//儲存資料庫變更
                return RedirectToAction("Login", "Home");//註冊成功時將導回登入頁面
            }
            else if(obj.Email!=null)//需檢驗Email欄位是否為空-->第一次進入是空的
            {
                ModelState.AddModelError(nameof(Member.Email), "此帳號已被註冊");//將錯誤訊息附加到欄位上               
            }
            return View();           

        }

        public IActionResult PersonalInfo()
        {
            ViewData["login"] = "登入/註冊";
            return View();
        }

        public IActionResult UserTicket()
        {
            ViewData["login"] = "登入/註冊";
            //讀取資料語法
            IEnumerable<Flight> objList = _db.Flight;
            return View(objList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public int validation(string sql)
        {
            /*
            //測試有沒有連到
            string connectionstring = configuration.GetConnectionString("DefaultConnection");

            //找到SQLCONNECTION，新增了NuGet套件
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand com = new SqlCommand("Select count(*) from Member", connection);
            var count = (int)com.ExecuteScalar();

            ViewData["TotalData"] = count; //Member資料表的資料數量

            connection.Close(); 
            */
            return 0;
           
        }
    }
}
