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
        public async Task<Pagination<spPagingOrderResult>> GetPaging(int PageIndex, int PageSize, int? recordCount)
        {
            var _PageIndex = new OutputParameter<int?> { _value = PageIndex };
            var _PageSize = new OutputParameter<double?> { _value = PageSize };
            var pagination = new Pagination<spPagingOrderResult>();
            var TotalPages = new OutputParameter<int?>();
            var RecordCount = new OutputParameter<double?> { _value = recordCount };
            try
            {
                List<spPagingOrderResult> result = await dblocalkencontext.GetProcedures().spPagingOrderAsync(_PageIndex, _PageSize, TotalPages, RecordCount);
                pagination.TotalPages = Convert.ToInt32(TotalPages.Value);
                pagination.RecordCount = Convert.ToInt32(RecordCount.Value);
                pagination.Model = result;
                pagination.PageIndex = PageIndex;
                pagination.PageSize = PageSize;

            }
            catch (Exception ex)
            {
                throw;
            }
            return pagination;
        }
        public  async Task<bool> Insert(Order item)
        {
            try
            {
                //generate unique workordercode
                string biggestcode = await (from Order in dblocalkencontext.Order
                                           orderby Order.workorder_code descending
                                           select Order.workorder_code).FirstOrDefaultAsync();
                int codenum =Convert.ToInt32(string.Join("",biggestcode.Where(Char.IsDigit)));
                item.workorder_code = "WO" + (codenum + 1);
                //save
                item.request_date = item.request_date;
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
        public async Task<Order> GetData(string workordercode)
        {
            Order result = new Order();
            try
            {
                result = await dblocalkencontext.Order.Where(w => w.workorder_code == workordercode).FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
