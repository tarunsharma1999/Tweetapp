using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TweetConsoleApp.Models;

namespace TweetConsoleApp.DAO
{
    class UserService
    {
        string URL = "https://localhost:44322/api/v1/tweets/";

        public async Task<int> resetPassword(string userName, ResetPassword user, string oldPass)
        {
            int status = -1;

            if (user != null)
            {

                try
                {
                    using (HttpClient clien = new HttpClient())
                    {
                        URL += userName + "/reset/" + oldPass;
                        var reqObj = JsonConvert.SerializeObject(user);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(reqObj);
                        var bufferStream = new ByteArrayContent(buffer);
                        bufferStream.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                        var Response = await clien.PostAsync(URL,bufferStream);
                        var Result = await Response.Content.ReadAsStringAsync();

                        try
                        {
                            user = JsonConvert.DeserializeObject<ResetPassword>(Result);
                            status = 0;
                        }
                        catch
                        {
                            string msg = JsonConvert.DeserializeObject<string>(Result);

                            if (msg == "User not found" || msg == "User details do not matches") 
                            {
                                status = -1;
                                return status;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    status = 1;
                }
            }
            return status;
        }
        public async Task<List<Users>> GetAllUsers()
        {
            List<Users> users = new List<Users>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    URL += "users/all";
                    var Response = await client.GetAsync(URL);
                    var result = await Response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<Users>>(result);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return users;
        }


        public async Task<int> RegisterUser(Register register)
        {
            int RegisterDone = -1;
            if (register != null)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var reqObj = JsonConvert.SerializeObject(register);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(reqObj);
                        var bufferStream = new ByteArrayContent(buffer);
                        bufferStream.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                        var Response = await client.PostAsync(URL + "register/ ", bufferStream);

                        var res = await Response.Content.ReadAsStringAsync();

                        Register result = JsonConvert.DeserializeObject<Register>(res);

                        RegisterDone = result.userId;
                    }
                }
                catch (Exception e)
                {
                    RegisterDone = -1;
                }
            }
            return RegisterDone;
        }

        public async Task<Login> Login(Login login)
        {
            int isLoggedin = -1;
            if (login != null)
            {
                try 
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var obj = JsonConvert.SerializeObject(login);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(obj);
                        var req = new ByteArrayContent(buffer);
                        req.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                        var Respones = await client.PostAsync(URL + "login/", req);
                        string res = await Respones.Content.ReadAsStringAsync();

                        try
                        {
                            login = JsonConvert.DeserializeObject<Login>(res);
                            if (login.userName != "")
                            {
                                isLoggedin = 0;
                            }
                        }
                        catch
                        {
                            string result = JsonConvert.DeserializeObject<string>(res);
                            if (result == "Invalid User")
                            {
                                isLoggedin = 1;
                            }
                        }

                    }
                }
                catch(Exception e)
                {

                }
            }


            return login;

        }

        public async Task<int> ResetPassword(string f_name, ResetPassword reset)
        {
            int isPassword = -1;
            if (f_name != "" && reset != null)
            {
                try
                {
                    using(HttpClient clien = new HttpClient())
                    {
                        var obj = JsonConvert.SerializeObject(reset);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(obj);
                        var bufferStream = new ByteArrayContent(buffer);
                        bufferStream.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue( "application/json");

                        var Response = await clien.PostAsync(URL + f_name + "/forgot", bufferStream);
                        var Result = await Response.Content.ReadAsStringAsync();

                        try
                        {
                            var objRes = JsonConvert.DeserializeObject<ResetPassword>(Result);
                            isPassword = 0;
                        }
                        catch (Exception e)
                        {
                                var objRes = JsonConvert.DeserializeObject<string>(Result);
                                if (objRes == "User not found")
                                {
                                    isPassword = -1;
                                }
                            
                        }

                    }
                }
                catch (Exception e)
                { 
                
                }
            }
            return isPassword;
        }
    }
}
