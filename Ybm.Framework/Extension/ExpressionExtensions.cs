using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Extension
{
    class ExpressionExtensions
    {
    }
}



////using ConsoleApplication1.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApplication1
//{
//    public partial class book
//    {
//        public int Id { get; set; }
//        public string Nevisande { get; set; }
//        public string Name { get; set; }
//        public List<string> Names { get; set; }


//    }
//    public static class Extension
//    {

//        public static TRresult Method2<TSource, TRresult>(this TSource source, Expression<Func<TSource, TRresult>> updateExpression)
//        {

//            var memberInitExpression = updateExpression.Body as NewExpression;//   MemberInitExpression;


//            foreach (var item in memberInitExpression.Arguments)
//            {

//                var memberExpression = item as MemberExpression;

//                var member = memberExpression.Member;
//                var name = member.Name;
//                var type = memberExpression.Type;
//                var declaringType = member.DeclaringType;
//                var memberType = member.MemberType;




//            }


//            return default(TRresult);

//        }
//    }

//    class Program
//    {
//        static void Main(string[] args)
//        {

//            book b = new book();
//            b.Method2(q => new { q.Id, q.Name });


//            Method3(q => new { q.Id, q.Name });


//            Extension.Method2(b, q => new { q.Id, q.Name });

//            Method(u => new book
//            {
//                Name = "new Name",
//                Nevisande = "new Nevisande ",
//                Names = new List<string>() { }

//            });



//            Method(u => new book
//            {
//                Name = "new Name",
//                Nevisande = "new Nevisande ",
//                Names = new List<string>() { }

//            });




//        }
//        public static TRresult Method3<TRresult>(Expression<Func<book, TRresult>> updateExpression)
//        {

//            var memberInitExpression = updateExpression.Body as NewExpression;//   MemberInitExpression;


//            foreach (var item in memberInitExpression.Arguments)
//            {

//                var memberExpression = item as MemberExpression;

//                var member = memberExpression.Member;
//                var name = member.Name;
//                var type = memberExpression.Type;
//                var declaringType = member.DeclaringType;
//                var memberType = member.MemberType;


//            }


//            return default(TRresult);

//        }

//        public static void Method(Expression<Func<book, book>> updateExpression)
//        {


//            var memberInitExpression = updateExpression.Body as MemberInitExpression;
//            //var memberInitExpression = updateExpression.Body as System.Linq.Expressions.BinaryExpression;

//            //memberInitExpression.Left.NodeType

//            foreach (MemberBinding binding in memberInitExpression.Bindings)
//            {

//                string propertyName = binding.Member.Name;



//                var memberAssignment = binding as MemberAssignment;
//                if (memberAssignment == null)
//                    throw new ArgumentException("The update expression MemberBinding must only by type MemberAssignment.", "updateExpression");

//                Expression memberExpression = memberAssignment.Expression;

//                var value = Evaluate(memberExpression);

//                var val = memberAssignment.Expression as ConstantExpression;
//                //val.Value

//                ParameterExpression parameterExpression = null;
//                memberExpression.Visit((ParameterExpression p) =>
//                {

//                    //var test = p.Type;
//                    //if (p.Type == typeof(String))
//                    parameterExpression = p;
//                    return p;
//                    //if (test == p.Type)
//                    //return p;
//                    //else
//                    //    return p;
//                });


//            }

//        }


//        static object Evaluate(Expression expr)
//        {
//            switch (expr.NodeType)
//            {
//                case ExpressionType.Constant:
//                    return ((ConstantExpression)expr).Value;
//                case ExpressionType.MemberAccess:
//                    var me = (MemberExpression)expr;
//                    object target = Evaluate(me.Expression);
//                    switch (me.Member.MemberType)
//                    {
//                        case System.Reflection.MemberTypes.Field:
//                            return ((FieldInfo)me.Member).GetValue(target);
//                        case System.Reflection.MemberTypes.Property:
//                            return ((PropertyInfo)me.Member).GetValue(target, null);
//                        default:
//                            throw new NotSupportedException(me.Member.MemberType.ToString());
//                    }
//                default:
//                    throw new NotSupportedException(expr.NodeType.ToString());
//            }
//        }


//    }


//}

