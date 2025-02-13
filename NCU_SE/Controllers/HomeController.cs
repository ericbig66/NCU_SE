﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NCU_SE.Models;
using NCU_SE.Data;
using System.Linq;
using Microsoft.AspNetCore.Session;



//連線資料庫
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace NCU_SE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db; //使用資料庫實體
        private IHttpContextAccessor session;

        public HomeController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            session = httpContextAccessor;
            //httpContextAccessor.HttpContext.Session.Set("account", null);
        }


        //private readonly IConfiguration configuration;

        //public HomeController(IConfiguration config)
        //{
        //    this.configuration = config;
        //}

        //這邊控制了navbar點什麼會顯示什麼頁面
        //return View 要看的是上面的名稱 他回去找Home資料夾有沒有相對應的頁面(VIEW)
        //少邦的影片
        public IActionResult Index()
        {
            //跟_db衝突了 暫時用不到我先註解掉 但留著參考
            ////測試有沒有連到
            //string connectionstring = configuration.GetConnectionString("DefaultConnection");

            ////找到SQLCONNECTION，新增了NuGet套件
            //SqlConnection connection = new SqlConnection(connectionstring);
            //connection.Open();
            //SqlCommand com = new SqlCommand("Select count(*) from Member", connection);
            //var count = (int)com.ExecuteScalar();

            //ViewData["TotalData"] = count; //Member資料表的資料數量

            //connection.Close();
            ViewData["login"] = "登入/註冊";
            return View();
        }


        public IActionResult Login(Member obj)

        {
            
            // 測試是否有抓到值
            ViewData["Exist"] = obj.Email;
            ViewData["login"] = "登入/註冊";
            return View();

        }
        public IActionResult Verify(Member obj)
        {
            /*少邦
            var SearchEmail = _db.Member.Where(x => x.Email.Equals(obj.Email.ToString()));
            var SearchPW = _db.Member.Where(x => x.Password.Equals(obj.Password.ToString()));
            
            if (SearchEmail != null) //email存在
            {
                if (SearchPW != null)
                {
                    @ViewData["login"] = "歡迎登入 " + obj.Email;
                    return View("Index");
                    
                }


            }
            return View("Login");
            */

            //冠廷
            try//檢測session 'acc'是否存在，若存在且不為空則表示已經登入
            {
                if(session.HttpContext.Session.GetString("acc") != null)//若已登入
                {
                    return View("Index");//跳到首頁
                }
            }
            catch
            {
                Debug.Print("Session不存在!");
            }
            int AccExist =(obj.Email==null && obj.Password == null) ? -1: _db.Member.Where(u => u.Email == (obj.Email.ToString()) && u.Password == obj.Password.ToString()).Count();
            if(AccExist == 1)
            {
                session.HttpContext.Session.SetString("acc", obj.Email.ToString());//登入成功時加入session
                return View("Index");
            }
            else if(AccExist == 0)
            {
                ModelState.AddModelError(nameof(Member.Email), "帳號或密碼錯誤，請重新輸入");//將錯誤訊息附加到欄位上           
            }
            return View("Login");
        }

        public IActionResult Realtime() 
        {
            ViewData["login"] = "登入/註冊";
            return View();
        }

        public ActionResult Logout() 

        {
            /*原本的內容
            ViewData["login"] = "登入/註冊";
            return Redirect("Register");
            */
            try
            {
                session.HttpContext.Session.Remove("acc");
            }
            catch
            {

            }
            return View("Index");
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
