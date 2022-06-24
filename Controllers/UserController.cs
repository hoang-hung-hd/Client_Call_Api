using apiclient.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace apiclient.Controllers
{
    public class UserController : Controller
    {

        private const string baseUrl = "https://localhost:7137";
        // GET: UserController
        public async Task<IActionResult> Index()
        {

            IEnumerable<User> users = await CallConnectToApi<List<User>>(HttpMethod.Get, "/Users");
            return View(users);
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            User user = await CallConnectToApi<User>(HttpMethod.Get, "/Users/" +id);
            return View(user);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRequest user)
        {

            HttpContent content = new StringContent(JsonSerializer.Serialize(user) , System.Text.Encoding.UTF8 , "application/json");
            HttpResponseMessage response = await CallConnectToApi(HttpMethod.Post, "/Users/", content);
            return RedirectToAction(nameof(Index));

        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            User user = await CallConnectToApi<User>(HttpMethod.Get, "/Users/" + id);
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UpdateRequest user)
        {
            try
            {
                HttpContent content = new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await CallConnectToApi(HttpMethod.Put, "/Users/" + id, content);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            User user = await CallConnectToApi<User>(HttpMethod.Get, "/Users/" + id);
            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                HttpResponseMessage response = await CallConnectToApi(HttpMethod.Delete, "/Users/" + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<T> CallConnectToApi<T>(HttpMethod method, string pathUrl, HttpContent content = null)
        {
            try
            {
                var rs = await CallConnectToApi(method, pathUrl, content);
                return await rs.Content.ReadFromJsonAsync<T>();
            }
            catch
            {
                return default(T);
            }
        }

        public async Task<HttpResponseMessage> CallConnectToApi(HttpMethod method, string pathUrl, HttpContent content = null)
        {

            HttpRequestMessage request = new HttpRequestMessage(method, pathUrl);
            if (content != null)
            {
                request.Content = content;
            }
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            return await client.SendAsync(request);
        }

    }
}
