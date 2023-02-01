using WorkOrderSystem.Models;

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
    }
}
