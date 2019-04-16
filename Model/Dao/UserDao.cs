using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class UserDao
    {
        OnlineShopDbContext db = null;
        public UserDao()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(User entity)
        {
            db.User.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public User GetById(string userName)
        {
            return db.User.SingleOrDefault(x => x.UserName == userName);
        }
        public int Login(string userName, string passWord)
        {
            /*
             * return 0 => "Tài khoản không tồn tại";
             * return -1 => "Tài khoản đang bị khóa";
             * return 1 => "Đăng nhập thành công";
             * return 2 => "Sai mật khẩu";
             */
            var result = db.User.SingleOrDefault(x => x.UserName == userName);

            if (result == null)
            {
                return 0; 
            }
            else
            {
                if (result.Status == false)
                {
                    return -1; 
                }
                else
                {
                    if (result.Password == passWord)
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
        }
    }
}
