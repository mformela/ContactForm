using ContactForm.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ContactForm.Repository //szablon ogólny dla wszystkich repozytoriów 
{


    public class AbstractRepository<T> where T : class /*// typ generyczny jest taki - staje si e taki jaki mu przekażemy */
    {
        public virtual void Create(T entity)
        {

            using (var context = new ApplicationDbContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }


        public virtual void Update(T entity)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        //GetWhere expression -  typ zdefiniowany - wyrażenie lambda wywołuje funkcje col.GetWhere(a=>a.ID==5) -> to oznacza, że przypisujemy do 5 na sztywno. 
        public virtual List<T> GetWhere(Expression<Func<T,bool>> expression)
        {
            using (var context = new ApplicationDbContext())
            {
                var query = context.Set<T>().Where(expression);
                return query.ToList();
            }
        }

        public virtual void Delete(T entity)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Set<T>().Remove(entity);
            }
        }
    }
        
    
}