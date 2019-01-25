using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp1.Model
{
    public class User
    {

        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static async Task<bool> Login(string email, string password)
        {
            bool result = false;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                result = false;
            }
            else
            {
                var user = (await AzureHelper.MobileService.GetTable<User>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();
                if (user.Password == password)
                {
                    result = true;
                }

                else
                    result = false;

            }


            return result;
        }

        public static async Task<bool> Register(string email, string password, string confirmpassword)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(password))
            {
                if (password == confirmpassword)
                {
                    var user = new User()
                    {
                        Email = email,
                        Password = password
                    };

                    //await AzureHelper.MobileService.GetTable<User>().InsertAsync(user);

                    //Using Generic function insert
                    await AzureHelper.Insert<User>(user); //<User> is not required as Type is taken from user

                    result = true;
                }

                }
            return result;
        }
    }

}
