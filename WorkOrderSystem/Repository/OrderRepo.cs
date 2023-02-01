using WorkOrderSystem.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WorkOrderSystem.Repository
{
    public class OrderRepo
    {
        private DBLocalKenContext dblocalkencontext;
        public OrderRepo(DBLocalKenContext _dblocalkencontext)
        {
            this.dblocalkencontext = _dblocalkencontext;
        }

        public async Task<bool> Insert(List<Order> item)
        {
            try
            {
                dblocalkencontext.Order.AddRange(item);
                await dblocalkencontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<List<Order>> GetPaging()
        {
            List<Order> result = new List<Order>();
            try
            {
                //result = await dblocalkencontext.Order.ToListAsync();
                
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public  async Task<bool> Insert(Order item)
        {
            try
            {
                dblocalkencontext.Order.Add(item);
                await dblocalkencontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> Update(Order item)
        {
            try
            {
                var existingdata = await dblocalkencontext.Order.Where(w => w.workorder_code == item.workorder_code).FirstOrDefaultAsync();
                existingdata.completionactual_date = item.completionactual_date;
                existingdata.completiontarget_date = item.completiontarget_date;
                existingdata.request_date = item.request_date;
                existingdata.pic_name = item.pic_name;
                existingdata.requestor_name = item.requestor_name;
                existingdata.work_title = item.work_title;

                await dblocalkencontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> Delete(Order item)
        {
            try
            {
                dblocalkencontext.Order.Remove(item);
                await dblocalkencontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<Order> GetData(Order item)
        {
            Order result = new Order();
            try
            {
                result = dblocalkencontext.Order.Where(w => w.workorder_code == item.workorder_code).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
