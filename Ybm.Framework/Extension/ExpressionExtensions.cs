using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Extension
{
    public static class ExpressionExtensions
    {
        public static Expression Visit<TExpression>(this Expression expression, Func<TExpression, Expression> visitor)
            where TExpression : Expression
        {
            return ExpressionVisitor<TExpression>.Visit(expression, visitor);
        }

        public static TReturn Visit<TExpression, TReturn>(this TReturn expression, Func<TExpression, Expression> visitor)
            where TExpression : Expression
            where TReturn : Expression
        {
            return (TReturn)ExpressionVisitor<TExpression>.Visit(expression, visitor);
        }

        public static Expression<TDelegate> Visit<TExpression, TDelegate>(this Expression<TDelegate> expression, Func<TExpression, Expression> visitor)
            where TExpression : Expression
        {
            return ExpressionVisitor<TExpression>.Visit(expression, visitor);
        }
    }

    public class ExpressionVisitor<TExpression> : ExpressionVisitor where TExpression : Expression
    {
        private readonly Func<TExpression, Expression> _visitor;

        public ExpressionVisitor(Func<TExpression, Expression> visitor)
        {
            _visitor = visitor;
        }

        public override Expression Visit(Expression expression)
        {
            if (expression is TExpression && _visitor != null)
                expression = _visitor(expression as TExpression);

            return base.Visit(expression);
        }

        public static Expression Visit(Expression expression, Func<TExpression, Expression> visitor)
        {
            return new ExpressionVisitor<TExpression>(visitor).Visit(expression);
        }

        public static Expression<TDelegate> Visit<TDelegate>(Expression<TDelegate> expression, Func<TExpression, Expression> visitor)
        {
            return (Expression<TDelegate>)new ExpressionVisitor<TExpression>(visitor).Visit(expression);
        }

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

