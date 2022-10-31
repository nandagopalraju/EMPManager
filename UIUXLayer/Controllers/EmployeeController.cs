using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using UIUXLayer.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace UIUXLayer.Controllers
{
    
    public class EmployeeController : Controller
    {
        
        public async Task<IActionResult> viewEmployee()
        {
            if (HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Login");
            }
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7144");
            List<ModelClass>? employee = new List<ModelClass>();

            HttpResponseMessage res = await client.GetAsync("api/Employee");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<List<ModelClass>>(result);
            }
            return View(employee);

        }
        public async Task<IActionResult> Details(string username)
        {
            if (HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Login");
            }
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7144");
            ModelClass? employee = new ModelClass();

            HttpResponseMessage res = await client.GetAsync($"api/Employee/get/{username}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<ModelClass>(result);
            }
            return View(employee);

        }
        public async Task<ActionResult> create()
        {
            if (HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Login");
            }
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7144");
            List<DesignationClass>? designationTemp = new List<DesignationClass>();

            HttpResponseMessage res = await client.GetAsync("api/Designation");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                designationTemp = JsonConvert.DeserializeObject<List<DesignationClass>>(result);
                ViewData["designationtemp"] = designationTemp;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> create(ModelClassDTO emp)
        {
            if (HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Login");
            }

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7144");
            List<DesignationClass>? designationTemp = new List<DesignationClass>();

            HttpResponseMessage res = await client.GetAsync("api/Designation");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                designationTemp = JsonConvert.DeserializeObject<List<DesignationClass>>(result);
                ViewData["designationtemp"] = designationTemp;
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

            //create folder if not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //get file extension
            FileInfo fileInfo = new FileInfo(emp.imagePath.FileName);
            string fileName = emp.imagePath.FileName + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                emp.imagePath.CopyTo(stream);
            }

            var postTask = client.PostAsJsonAsync("/api/Employee/create", emp);
            postTask.Wait();
            var Result = postTask.Result;
            if (Result.IsSuccessStatusCode)
            {
                return RedirectToAction("ViewEmployee");
            }
            return View();
        }
        public async Task<IActionResult> Delete(string username)
        {
            if (HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Login");
            }
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7144");
            await client.DeleteAsync($"api/employee/delete/{username}");
            return RedirectToAction("ViewEmployee");

        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginClass user)
          {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7144");
            var postTask = client.PostAsJsonAsync<UserLoginClass>("api/auth/login", user);
            //var token = postTask.Result.Content;
            //var tokens = token.ToString();
           
            postTask.Wait();
            var Result = postTask.Result;
            if (Result.IsSuccessStatusCode)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8,"application/json");
                using (var response = await client.PostAsync("api/auth/login", content))
                {
                    string token = await response.Content.ReadAsStringAsync();
                    HttpContext.Session.SetString("JWToken", token);
                }



                    //HttpContext.Session.SetString("token", "hello");

                return RedirectToAction("DashBoard");
            }
            return View();
        }

        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegister user)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7144");
            var postTask = client.PostAsJsonAsync<UserRegister>("api/auth/Register", user);
            postTask.Wait();
            var Result = postTask.Result;
            if (Result.IsSuccessStatusCode)
            {
                return RedirectToAction("login");
            }
            return View();
        }
        public ActionResult DashBoard()
        {
            if(HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public async Task<IActionResult> Update(string username)
        {
            if (HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Login");
            }

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7144");
            List<DesignationClass>? designationTemp = new List<DesignationClass>();

            HttpResponseMessage res = await client.GetAsync("api/Designation");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                designationTemp = JsonConvert.DeserializeObject<List<DesignationClass>>(result);
                ViewData["designationtemp"] = designationTemp;
            }

            TempModelClass employee = new TempModelClass();
            HttpResponseMessage res1 = await client.GetAsync($"api/Employee/get/{username}");
            if (res.IsSuccessStatusCode)
            {
                var result = res1.Content.ReadAsStringAsync().Result;


                employee = JsonConvert.DeserializeObject<TempModelClass>(result);
            }
            


            return View(employee);
        }
        [HttpPost]
        public async Task<IActionResult> Update(TempModelClass temp)
        {
            if (HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Login");
            }
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7144");
            List<DesignationClass>? designationTemp = new List<DesignationClass>();

            HttpResponseMessage res = await client.GetAsync("api/Designation");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                designationTemp = JsonConvert.DeserializeObject<List<DesignationClass>>(result);
                ViewData["designationtemp"] = designationTemp;
            }


            var postTask = client.PostAsJsonAsync<TempModelClass>("api/Employee/Update", temp);
            postTask.Wait();
            var Result = postTask.Result;
            if (Result.IsSuccessStatusCode)
            {
                return RedirectToAction("viewEmployee");
            }
            return View();
        }
        public ActionResult designation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> designation(DesignationClass designationClass)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7144");
            var postTask = client.PostAsJsonAsync<DesignationClass>("api/Designation/designation", designationClass);

            /*  var postTask = client.PostAsJsonAsync<DesignationClass>("api/Designation/Designation", designationClass)*/
            postTask.Wait();
            var Result = postTask.Result;
            if (Result.IsSuccessStatusCode)
            {
                return RedirectToAction("DashBoard");
            }
            return View();
        }

    }
}