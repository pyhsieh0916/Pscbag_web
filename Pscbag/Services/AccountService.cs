using Pscbag.Models;

namespace Pscbag.Services
{
    public interface AccountService
    {
        public bool update(string product_name, string price, string quantity, string booth_name, string booth_num, string phone, string demo,out string fullname);

        public bool orderBag(string booth_name, string booth_num, string total_price, out string fullname);

        public void getInfo(out List<List<object>> dataList);

        public void getInfo_all(out List<List<object>> dataList);

        public bool Login(string username, string password, out string fullname);
    }
}
