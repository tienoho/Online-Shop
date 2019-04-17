using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

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
        public User ViewDetail(long id)
        {
            return db.User.Find(id);
        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.User.Find(entity.ID);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                    user.Password = entity.Password;
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                user.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                //log
                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                var user = db.User.Find(id);
                db.User.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Tạo phân trang
        ///Phân trang bằng PagedList
        /// </summary>
        //
        public IEnumerable<User> ListAllPaging(string searchString, int page = 1, int pageSize = 10)
        {
            IQueryable<User> model = db.User;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));

            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);

        }
        //lấy ra tất cả danh sách
        public List<User> ListAll()
        {
            //sắp xếp theo ngày tạo và truyền thông số page và pageSize vào
            return db.User.ToList();
        }
    }
}
